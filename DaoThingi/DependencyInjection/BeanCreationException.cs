using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaoThingi.DependencyInjection
{
    class BeanCreationException : Exception
    {
        public BeanCreationException(string msg) : base(msg)
        {

        }
    }
}
