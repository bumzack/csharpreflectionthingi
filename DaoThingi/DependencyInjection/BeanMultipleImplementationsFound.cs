using System;

namespace DaoThingi.DependencyInjection
{
    class BeanMultipleImplementationsFoundException : Exception
    {
        public BeanMultipleImplementationsFoundException(string m) : base(m) { }
    }
}
