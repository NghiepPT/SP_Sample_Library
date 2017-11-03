using System.Xml;
using System.Xml.Linq;
namespace SPSwrapApi.Common
{
   internal  static class XmlExtensions
    {
        internal static XElement GetXElement(this XmlNode node)
        {
            XDocument xDoc = new XDocument();
            using (XmlWriter xmlWriter = xDoc.CreateWriter())
                node.WriteTo(xmlWriter);
            return xDoc.Root;
        }
        internal static XElement GetContentNode(this XmlNode node)
        {
            XDocument xDoc = new XDocument();
            using (XmlWriter xmlWriter = xDoc.CreateWriter())
                node.WriteContentTo(xmlWriter);
            return xDoc.Root;
        }
    }
}
