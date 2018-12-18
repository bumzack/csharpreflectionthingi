using System;

namespace DaoThingi.DependencyInjection
{
    class BeanCreationException : Exception
    {
        public BeanCreationException(string msg) : base(msg)
        {

        }
    }
}
