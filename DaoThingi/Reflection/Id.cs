﻿using System;

namespace DaoThingi.Reflection
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class Id : Attribute
    {
        private string name; 

        // private string type; 
        public Id()
        {
            Console.WriteLine("Constructor Attrbute Id");
        }

        //public string Name
        //{
        //    get
        //    {
        //        return name;
        //    }
        //}
    }
}
