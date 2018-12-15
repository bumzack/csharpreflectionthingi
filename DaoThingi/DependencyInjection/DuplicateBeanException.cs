using System;

namespace DaoThingi.DependencyInjection
{
    public class DuplicateBeanException : Exception
    {
        public DuplicateBeanException(string msg) : base(msg)
        {

        }
    }
}
