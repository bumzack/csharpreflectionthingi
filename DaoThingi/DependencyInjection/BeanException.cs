using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaoThingi.DependencyInjection
{
    public class BeanException : Exception
    {
        public BeanException(string msg) : base(msg) { }
    }
}
