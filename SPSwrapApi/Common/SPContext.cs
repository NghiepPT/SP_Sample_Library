using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Xml.Linq;
using SPSwrapApi.WebServices;
using SPSwrapApi.Authentication;

namespace SPSwrapApi.Common
{

    public class SPContext
    {

        #region Properties
        private CookieContainer cookies;
        private Uri url;
        public CookieContainer Cookies
        {
            get
            {
                return cookies;
            }
            set
            {
                cookies = value;
            }
        }

        public Uri Url
        {
            get
            {
                return url;
            }
        }
        protected internal NetworkCredential Credentials { get; private set; }
        protected internal bool _fbaenabled = false;
        #endregion

        #region Contructor
        public SPContext(NetworkCredential credentials, CookieContainer cookies, Uri Url)
        {
            if (credentials == null)
                throw new ArgumentNullException("credentials");
            else
                this.Credentials = credentials;
            if (Url == null)
                throw new ArgumentNullException("Url");
            url = Url;
            if (cookies != null)
                this.Cookies = cookies;
            else
            {
                try
                {
                    Login(Url);
                }
                catch(Exception)
                {
                    if(_fbaenabled)
                    {
                        throw;
                    }
                }
            }
        }
        #endregion
        #region  Methods
        private void Login(Uri url)
        {
            using (SPAuthenticationWS auth = new SPAuthenticationWS(Credentials, url))
            {
                try
                {   
                    LoginResult loginresult = auth.Login
                        (!String.IsNullOrEmpty(Credentials.Domain) ? Credentials.Domain + '\\' + Credentials.UserName : Credentials.UserName,
                        Credentials.Password);
                    if (loginresult.ErrorCode == LoginErrorCode.NoError)
                    {
                        if (auth.CookieContainer != null && auth.CookieContainer.Count > 0)
                        {
                            _fbaenabled = true;
                            this.cookies = new CookieContainer();
                            this.cookies.Add(auth.CookieContainer.GetCookies(url));
                        }
                    }
                    else
                    {
                        throw new Exception("Authentication failed");
                    }
                }
                catch (WebException we)
                {
                    if (we.Response.ContentLength == 0 && !String.IsNullOrEmpty(we.Response.Headers["Location"]))
                    {   // we can get redirect request when FBA is configured
                        _fbaenabled = true;
                        Login(auth.Url.AbsoluteUri, we.Response.Headers["Location"]);
                    }
                    else
                    {
                        throw;
                    }
                }
            }
        }
        private void Login(String serviceurl, String url)
        {
            Uri loginUrl = new Uri(url);
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.Credentials = Credentials;
            request.PreAuthenticate = true;
            String[] lines;
            String content;
            using (HttpWebResponse resp = request.GetResponse() as HttpWebResponse)
            {
                using (Stream s = resp.GetResponseStream())
                {
                    using (TextReader r = new StreamReader(s))
                    {
                        content = r.ReadToEnd();
                        lines = content.Split(new String[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                    }
                }
            }
            // evaluate the response and call the real fba url to get cookies
            String loginurl = String.Empty;
            bool done = false;
            List<String> param = new List<String>();
            for (int i = 0; !done && i < lines.Count(); i++)
            {
                XElement x;
                if (lines[i].Contains("<form action="))
                {
                    loginurl = lines[i++].Split(new char[] { '"' })[1];
                    while (i < lines.Count())
                    {
                        try
                        {
                            x = XElement.Parse(lines[i++]);
                        }
                        catch (Exception)
                        {   //eliminate parsing errors;
                            done = true;
                            break;
                        }
                        if (x.Name == "input")
                        {
                            String name = x.Attributes("name").First().Value;
                            String value = x.Attributes("value").First().Value;
                            param.Add(name + '=' + value);
                        }
                        else
                        {
                            done = true;
                            break;
                        }
                    }
                }
            }
            DoLogin(String.Join("&", param.ToArray()), new Uri(loginUrl, loginurl));
        }

        private bool DoLogin(String param, Uri uri)
        {
            System.Diagnostics.Trace.Write("DoFBAlogin");
            bool b = false;
            HttpWebRequest request = WebRequest.Create(uri) as HttpWebRequest;
            request.Method = WebRequestMethods.Http.Post;
            request.UserAgent = "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1)";
            request.ContentType = "application/x-www-form-urlencoded;charset=utf-8";
            request.Credentials = Credentials;

            string content = String.Format(@"{0}&username={1}&password={2}",
                param, Credentials.Domain + @"\" + Credentials.UserName, Credentials.Password);
            request.ContentLength = Encoding.UTF8.GetByteCount(content);
            request.CookieContainer = new CookieContainer();
            request.AllowAutoRedirect = false;
            using (Stream reqStream = request.GetRequestStream())
            {
                using (StreamWriter sw = new StreamWriter(reqStream))
                {
                    sw.Write(content);
                    sw.Flush();
                }
            }

            using (HttpWebResponse resp = request.GetResponse() as HttpWebResponse)
            {
                if (resp.Cookies.Count > 0)
                {
                    this.cookies = new CookieContainer();
                    cookies.Add(resp.Cookies);
                }
                else
                {
                    throw new Exception("Error: Unexpected response received from the server");
                }
            }
            return b;
        }
        #endregion
    }
}
