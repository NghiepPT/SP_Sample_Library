using System;
using System.Net;
using SPSwrapApi.Common;

namespace SPSwrapApi.WebServices
{
    internal class SPListWS : Lists.Lists
    {
        public SPListWS(SPContext ct, Uri url)
        {
            NetworkCredential nc = ct.Credentials;

            if (nc == null)
                throw new ArgumentNullException("credentials");
            if (url == null)
                throw new ArgumentNullException("url");
            string sUrlService = url.ToString().TrimEnd('/') + "/" + SPWebServiceUrlEnding.ListsWSUrlEnding;
            this.Url = sUrlService;
            this.PreAuthenticate = true;
            this.Credentials = nc;
            if (ct.Cookies != null)
            {
                this.CookieContainer = ct.Cookies;
            }

        }
    }
}
