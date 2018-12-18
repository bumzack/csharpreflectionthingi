using System;

namespace DaoThingi.DependencyInjection
{
    class BeanNoImplementationFound : Exception
    {
        public BeanNoImplementationFound(string v) : base(v) { }
    }
}
