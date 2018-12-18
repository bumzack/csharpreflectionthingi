using DaoThingi.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DaoThingi.DependencyInjection
{
    public class GrgContext
    {
        private SortedDictionary<string, string> beans = new SortedDictionary<string, string>();
        private SortedDictionary<string, List<string>> injectables = new SortedDictionary<string, List<string>>();

        //private SortedDictionary<string, List<Type>> interfaces;
        private SortedDictionary<string, List<Type>> autowire;

        private SortedDictionary<string, string> BeansWithId = new SortedDictionary<string, string>();
        private SortedDictionary<string, object> Singletons = new SortedDictionary<string, object>();

    
        public GrgContext()
        {
        }

        public GrgContext(IEnumerable<string> namespaces)
        {
            //interfaces = new SortedDictionary<string, List<Type>>();
            autowire = new SortedDictionary<string, List<Type>>();

            IEnumerable<Type> allClasses = AppDomain.CurrentDomain.GetAssemblies()
                        .SelectMany(t => t.GetTypes())
                        .Where(t => t.IsClass);

            namespaces.ToList().ForEach(n =>
            {
                allClasses.ToList().ForEach(c =>
                {
                    if (c.Namespace != null)
                    {
                        if (c.Namespace.Equals(n))
                        {
                            Console.WriteLine("Class t Name: " + c.Name + ", namespace: " + c.Namespace);
                            if (beans.ContainsKey(c.Name))
                            {
                                throw new DuplicateBeanException($"Bean with name {c.Name} ({beans[c.Name]}) already exists. can't add {c.FullName}");
                            }
                            beans.Add(c.Name, c.FullName);

                            foreach (var attribute in c.GetCustomAttributes())
                            {
                                if (attribute.GetType() == typeof(Injectable))
                                {
                                    // check which interface the injectable implements or throw exception
                                    foreach (var itype in c.GetInterfaces())
                                    {
                                        if (injectables.ContainsKey(itype.FullName))
                                        {
                                            List<string> types = injectables[itype.FullName];
                                            types.Add(c.FullName);
                                        }
                                        else
                                        {
                                            injectables.Add(itype.FullName, new List<string>());
                                            List<string> types = injectables[itype.FullName];
                                            types.Add(c.FullName);
                                        }
                                    } 
                                    Console.WriteLine("Injectable found.  Name: " + c.Name);
   }
                            }
                        }
                    }
                });
            }); 

            // find all classes which have a ´field with an Autowire aatribute 
            Console.WriteLine("\n\n Searching for Autowire attributes ");

            namespaces.ToList().ForEach(n =>
            {
                allClasses.ToList().ForEach(t =>
                {
                    if (t.Namespace != null)
                    {
                        if (t.Namespace.Equals(n))
                        {
                            foreach (var f in t.GetFields())
                            {
                                Console.WriteLine("  Class " + t.Name + " has field  " + f.Name + " with fieldType: " + f.FieldType.FullName);

                                if (f.FieldType.IsInterface)
                                {
                                    foreach (var attr in f.GetCustomAttributes())
                                    {
                                        if (attr.GetType() == typeof(Autowire))
                                        {
                                            Console.WriteLine(" AUTOWIRE FOUND  Class " + t.Name + ", field  " + f.Name + " is autowired with fieldtype " + f.FieldType.FullName);

                                            if (autowire.ContainsKey(t.FullName))
                                            {
                                                List<Type> types = autowire[t.FullName];
                                                types.Add(t);
                                            }
                                            else
                                            {
                                                autowire.Add(t.FullName, new List<Type>());
                                                List<Type> types = autowire[t.FullName];
                                                types.Add(t);
                                            }
                                        }
                                    }
                                }

                                //    if (a.GetType() == typeof(Autowire))
                                //{
                                //    Console.WriteLine("\t\t Injectable " + t.Name + " has attribute 'Autowire'");
                                //}
                            }
                        }
                    }
                });
            }); 
        }

       

        public void ListAutowire()
        {
            Console.WriteLine("Autowire: ");

            foreach (KeyValuePair<string, List<Type>> entry in autowire)
            {
                foreach (var t in entry.Value)
                {
                    Console.WriteLine("\t\tfully qualified name: " + entry.Key + "  autowire in class: " + t.Name);
                }
            }
        }

        public void ListInjectables()
        {
            Console.WriteLine("Injectables: ");

            foreach (KeyValuePair<string, List<string>> entry in injectables)
            {
                foreach (var t in entry.Value)
                {
                    Console.WriteLine($"\t\t Class {t} implements  Interface: " + entry.Key);
                }
            }

        }

        public void ListBeans()
        {
            Console.WriteLine("beans: ");
            beans.ToList().ForEach(i =>
            {
                Console.WriteLine("\t " + i);
            });
        }

        public object GetBean(string name)
        {
            Type t = Type.GetType(beans[name]);
            object o = null;
            try
            {
                o = Activator.CreateInstance(t);
            }
            catch (MissingMethodException e)
            {
                Console.WriteLine($"Error creating the bean {name} - empty constructor missing in class!");
                throw new BeanCreationException($"Error creating the bean {name} - empty constructor missing in class!   original exception: {e.Message}");
            }
            foreach (var f in t.GetFields())
            {
                foreach (var a in f.GetCustomAttributes())
                {
                    if (a.GetType() == typeof(Autowire))
                    {
                        //Console.WriteLine($"'GetBean() name: {name}, fullname {t.FullName},   found fields with Attr 'Autowire'");
                       // Console.WriteLine($"'GetBean() name: {name}, fullname {t.FullName},   fieldname: '{f.Name}',   fieldtype: {f.FieldType.ToString()}");
                        var interfaceType = f.FieldType.ToString();
                        List<string> interfaceImplementations = injectables[interfaceType];

                        if (interfaceImplementations == null)
                        {
                            throw new BeanNoImplementationFound($"could not fina an implemention of interface {interfaceType} while constructing bean {name} for field {f.Name}");
                        }

                        string classFullName = null;
                        if (interfaceImplementations.Count > 1)
                        {
                            Autowire aw = a as Autowire;
                            if (aw == null)
                            {
                                throw new BeanException($"could not typecast attribute to 'Autowire': ");
                            }
                            if (aw.Name == null)
                            {
                                string s = string.Join(",", interfaceImplementations);
                                throw new BeanMultipleImplementationsFoundException($"for field {f.Name}. multiple implementation for interface {interfaceType} found: '{s}', but no implementation specified with 'Name' selector  ");
                            }
                            if (!beans.ContainsKey(aw.Name))
                            {
                                throw new BeanNoImplementationFound($"for field {f.Name}. implementation of interface {interfaceType} specified in 'Name' not found. classname {aw.Name}");

                            }
                            classFullName = beans[aw.Name];
                        }
                        else
                        {
                            classFullName = interfaceImplementations[0];
                        }

                        Type ft = Type.GetType(classFullName);
                        object b = null;
                        try
                        {
                            b = Activator.CreateInstance(ft);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine($"error creating object classname  fullname: {classFullName}");
                            throw new BeanCreationException($"error creating object  , fullname: {classFullName}, {e.Message}");
                        }
                        f.SetValue(o, b);
                    }
                }
            }

            return o;
        }

        public void AddBean(string FullName, string BeanId, GrgScope scope)
        {
            //TODO check for existing beans
            BeansWithId.Add(BeanId, FullName);
            if (scope == GrgScope.Singleton)
            {
                try
                {
                    Singletons.Add(BeanId, GetBeanById(FullName));
                }
                catch (Exception e)
                {
                    Console.WriteLine($"error creating object    fullname: {  FullName}");
                    throw new BeanCreationException($"error creating object  , fullname: {  FullName}, {e.Message}");
                }
            }
        }

         
        public object GetBeanById(string BeanId)
        {
            if (!BeansWithId.ContainsKey(BeanId))
            {
                throw new BeanException($"bean with ID {BeanId} not found  - aborting");
            }
            if (Singletons.ContainsKey(BeanId))
            {
                return Singletons[BeanId];
            }
        
            Type t = Type.GetType(BeansWithId[BeanId]);
            object o = null;
            try
            {
                o = Activator.CreateInstance(t);
            }
            catch (MissingMethodException e)
            {
                Console.WriteLine($"Error creating the bean ID {BeanId} - empty constructor missing in class!");
                throw new BeanCreationException($"Error creating the bean ID  {BeanId} - empty constructor missing in class!   original exception: {e.Message}");
            }

            foreach (var f in t.GetFields())
            {
                foreach (var a in f.GetCustomAttributes())
                {
                    if (a.GetType() == typeof(Autowire))
                    {
                        //Console.WriteLine($"'GetBean() name: {name}, fullname {t.FullName},   found fields with Attr 'Autowire'");
                        // Console.WriteLine($"'GetBean() name: {name}, fullname {t.FullName},   fieldname: '{f.Name}',   fieldtype: {f.FieldType.ToString()}");
                        var interfaceType = f.FieldType.ToString();
                        List<string> interfaceImplementations = injectables[interfaceType];

                        if (interfaceImplementations == null)
                        {
                            throw new BeanNoImplementationFound($"could not fina an implemention of interface {interfaceType} while constructing bean ID {BeanId} for field {f.Name}");
                        }

                        string classFullName = null;
                        if (interfaceImplementations.Count > 1)
                        {
                            Autowire aw = a as Autowire;
                            if (aw == null)
                            {
                                throw new BeanException($"could not typecast attribute to 'Autowire': ");
                            }
                            if (aw.Name == null)
                            {
                                string s = string.Join(",", interfaceImplementations);
                                throw new BeanMultipleImplementationsFoundException($"for field {f.Name}. multiple implementation for interface {interfaceType} found: '{s}', but no implementation specified with 'Name' selector  ");
                            }
                            if (!beans.ContainsKey(aw.Name))
                            {
                                throw new BeanNoImplementationFound($"for field {f.Name}. implementation of interface {interfaceType} specified in 'Name' not found. classname {aw.Name}");

                            }
                            classFullName = beans[aw.Name];
                        }
                        else
                        {
                            classFullName = interfaceImplementations[0];
                        }

                        Type ft = Type.GetType(classFullName);
                        object b = null;
                        if (Singletons.ContainsKey(classFullName))
                        {
                            b = Singletons[classFullName];
                        }
                        else
                        {
                            try
                            {
                                b = Activator.CreateInstance(ft);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine($"error creating object classname  fullname: {classFullName}");
                                throw new BeanCreationException($"error creating object  , fullname: {classFullName}, {e.Message}");
                            }
                        }
                        
                        f.SetValue(o, b);
                    }
                }
            }

            return o;
        }
    }
}
