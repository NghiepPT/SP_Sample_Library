using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPSwrapApi;
using System.Xml;
using System.Xml.Linq;
namespace SPSwrapApi.Common
{
    public class List:INode
    {
        #region Private properties
        private string sTitle;
        private string sDesription;
        private string sID;
        private string sUrl;
        private string sNodeType;
        private SPContext Context; 
        #endregion
        #region Constructors
        public List(SPContext context, string url)
        {
            Context = context;
            sTitle = string.Empty;
            sDesription = string.Empty;
            sID = string.Empty;
            sUrl = url;
            sNodeType = string.Empty;
            
        }
        #endregion
        #region Node members
        public string Title
        {
            get
            {
                return sTitle;
            }
           
        }
        public string Description
        {
            get
            {
                return sDesription;
            }
        }

        public string ID
        {
            get
            {
                return sID;
            }
        }
        public Uri Url
        {
            get
            {
                return new Uri(sUrl);
            }
        }

        public NodeType NodeType
        {
            get
            {
                if (sNodeType.Equals(BaseType.DocumentLibrary))
                    return NodeType.Library;
                else
                    return NodeType.List;
            }
        }
        #endregion
        #region Private Methods
        private XElement LoadListCollection()
        {
            XElement xEle;
            using (WebServices.SPListWS ws = new WebServices.SPListWS(Context, Url))
            {
                XmlNode xNode = ws.GetListCollection();
                xEle = xNode.GetXElement();
            }
            return xEle;
        }
         
        #endregion
        #region List  Members
        public IEnumerable<INode> Load()
        {
            XElement collection = LoadListCollection();
            List<List> lists = new List<List>();           
            if(collection != null)
            {
                
                XNamespace def = collection.GetDefaultNamespace();
                foreach(var e in collection.Elements(def + ListSchemaElements.List))
                {
                    List list = new List(Context, sUrl);
                    if (e.Attribute(WebSchemaAttributes.Hidden).Value == "False")
                    {
                        list.sTitle = e.Attribute(WebSchemaAttributes.Title).Value;
                        //this.sUrl = e.Attribute((WebSchemaAttributes.Url)).Value;
                        list.sNodeType = e.Attribute(WebSchemaAttributes.BaseType).Value;
                        lists.Add(list);
                    }
                }
            }
            return lists.Cast<INode>();
        }
        #endregion
    }
}
