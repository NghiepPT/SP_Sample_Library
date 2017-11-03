using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPSwrapApi
{
    public interface INode
    {
        string Title
        {
            get;
        }
        Uri Url
        {
            get;
        }

        string Description
        {
            get;
        }

        string ID
        {
            get;
        }
        NodeType NodeType
        {
            get;
        }
    }
}
