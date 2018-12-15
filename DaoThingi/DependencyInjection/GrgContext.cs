using DaoThingi.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DaoThingi.DependencyInjection
{
    public class GrgContext
    {
        private List<string> classes = new List<string>();
        private List<string> injectables = new List<string>();

        private SortedDictionary<string, List<Type>> interfaces;

        private SortedDictionary<string, List<Type>> autowire;


        public GrgContext(IEnumerable<string> namespaces)
        {
            interfaces = new SortedDictionary<string, List<Type>>();
            autowire = new SortedDictionary<string, List<Type>>();

            IEnumerable<Type> allClasses = AppDomain.CurrentDomain.GetAssemblies()
                        .SelectMany(t => t.GetTypes())
                        .Where(t => t.IsClass);

            namespaces.ToList().ForEach(n =>
            {
                allClasses.ToList().ForEach(t =>
                {
                    if (t.Namespace != null)
                    {
                        if (t.Namespace.Equals(n))
                        {
                            Console.WriteLine("Class t Name: " + t.Name + ", namespace: " + t.Namespace);
                            classes.Add(t.Name);

                            foreach (var attribute in t.GetCustomAttributes())
                            {
                                if (attribute.GetType() == typeof(Injectable))
                                {
                                    injectables.Add(t.Name);
                                    Console.WriteLine("Injectable found.  Name: " + t.Name);

                                    foreach (var itype in t.GetInterfaces())
                                    {
                                        Console.WriteLine("\t Injectable " + t.Name + " implements interface: " + itype.Name);
                                        if (interfaces.ContainsKey(itype.FullName))
                                        {
                                            List<Type> types = interfaces[itype.FullName];
                                            types.Add(itype);
                                        }
                                        else
                                        {
                                            interfaces.Add(itype.FullName, new List<Type>());
                                            List<Type> types = interfaces[itype.FullName];
                                            types.Add(itype);
                                        }
                                    }
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

        public void ListInterfaces()
        {
            Console.WriteLine("Interfaces: ");

            foreach (KeyValuePair<string, List<Type>> entry in interfaces)
            {
                foreach (var t in entry.Value)
                {
                    Console.WriteLine("\t\tfully qualified name: " + entry.Key + "  interface name: " + t.Name);
                }
            }
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
            injectables.ToList().ForEach(i =>
            {
                Console.WriteLine("\t " + i);
            });
        }

        public void ListClasses()
        {
            Console.WriteLine("classes: ");
            classes.ToList().ForEach(i =>
            {
                Console.WriteLine("\t " + i);
            });
        }

        public object GetBean(string name)
        {
            Type t = Type.GetType(name);
            return Activator.CreateInstance(t);
        }
    }
}
