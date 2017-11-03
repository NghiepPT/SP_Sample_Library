using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;


namespace SPSwrapApi.WebServices
{
    class SPSiteWS : Sites.Sites
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

        #region Contructors
        internal SPSiteWS(NetworkCredential credentials, Uri url, CookieContainer cookies)
        {
            if (credentials == null)
                throw new ArgumentNullException("credentials");
            if (url == null)
                throw new ArgumentNullException("url");

            string sUrl = url.ToString().TrimEnd('/') + "/" + SPWebServiceUrlEnding.SitesWSUrlEnding;
            Uri serviceUrl = new Uri(sUrl);

            this.Credentials = credentials;

            this.Url = serviceUrl;

            if (cookies != null)
            {

                this.CookieContainer = cookies;
            }


        }
        #endregion
    }
}
