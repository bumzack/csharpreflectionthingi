using System;

namespace DaoThingi.DependencyInjection
{
    public class BeanException : Exception
    {
        public BeanException(string msg) : base(msg) { }
    }
}
