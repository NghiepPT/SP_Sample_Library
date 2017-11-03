using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPSwrapApi.WebServices;
using System.Xml;
using System.Xml.Linq;
namespace SPSwrapApi.Common
{
    public class Web
    {
        private Uri Url;
        private SPContext Context;
        
        public Web(SPContext context, Uri url)
        {
            Context = context;
            Url = url;

        }
        public List<Site> GetSites()
        {

            List<Site> sites = new List<Common.Site>();
            using (SPWebWS web = new SPWebWS(Context.Credentials, Context.Cookies, this.Url))
            {
                try
                {
                    XElement xE = null;
                    XmlNode xNode = web.GetWebCollection();
                    if (xNode != null)
                    {
                        xE = xNode.GetXElement();
                        if (xE != null)
                        {
                            XNamespace defaultNS = xE.GetDefaultNamespace();
                            foreach (var element in xE.Elements(defaultNS + "Web"))
                            {
                                Site site = new Site();
                                site.Url = element.Attribute(WebSchemaAttributes.Url).Value;
                                site.Title = element.Attribute(WebSchemaAttributes.Title).Value;
                                //site.Desription = element.Attribute(WebSchemaAttributes.Description).Value;
                                sites.Add(site);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return sites;
            }

        }

    }
}
