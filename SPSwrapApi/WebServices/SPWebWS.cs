using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
namespace SPSwrapApi.WebServices
{
    internal class SPWebWS:Webs.Webs
    {
        #region Properties
        internal Uri Url
        {
            get
            {
                return new Uri(base.Url);

            }
            set
            {
                base.Url = value != null ? value.ToString() : String.Empty;
            }
        }
        #endregion

        #region Constructor
        internal SPWebWS(NetworkCredential credentials, CookieContainer cookies, Uri url)
        {
            if (credentials == null)
                throw new ArgumentNullException("credentials");
            if (url == null)
                throw new ArgumentNullException("url");
            string sUrlService = url.ToString().TrimEnd('/') + "/" + SPWebServiceUrlEnding.WebsWSUrlEnding;
            this.Url = new Uri(sUrlService);
            this.Credentials = credentials;
            if(cookies != null)
            {
                this.CookieContainer = cookies;
            }
        }
        #endregion
    }
}
