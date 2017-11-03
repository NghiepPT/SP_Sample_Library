using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPSwrapApi.Common
{
    public class Library: List
    {
        public Library(SPContext context, string url): base(context, url)
        {
        }
    }
}
