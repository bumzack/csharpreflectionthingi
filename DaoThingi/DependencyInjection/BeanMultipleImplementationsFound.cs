using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaoThingi.DependencyInjection
{
    class BeanMultipleImplementationsFoundException : Exception
    {
        public BeanMultipleImplementationsFoundException(string m) : base(m) { }
    }
}
