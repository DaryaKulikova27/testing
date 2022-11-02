using System;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;

namespace lab2_links
{
    class Program
    {
        private const string url = "http://links.qatl.ru/";
        static void Main(string[] args)
        {
            var program = new Program();
            string result = program.GetUrl(url);
            //Console.Write(result);
            List<string> list = program.GetHref(result);
            foreach (string s in list) Console.WriteLine(s);
            Console.ReadKey();
        }

        public string GetUrl(string address)
        {
            WebClient client = new WebClient();
            client.Credentials = CredentialCache.DefaultNetworkCredentials;
            return client.DownloadString(address);
        }

        public List<string> GetHref(string html)
        {
            string[] d = html.Split('\n');
            Console.WriteLine(d.Length);
            List<string> HREFS = new List<string>();
            //string pattern = "(?<=<a href=\")\\S+(?=\">)";
            //Regex pattern2 = new Regex(@"(\b\w+:\/\/\w+((\.\w)*\w+)*\.\w{2,4})");
            string HRefPattern = "href\\s*=\\s*(?:\"(?<1>[^\"]*)\"|(?<1>\\S+))";
            bool uniqueLink = true;

            foreach (string str in d)
            {
                foreach (Match match in Regex.Matches(str, HRefPattern))
                {
                    uniqueLink = true;
                    for (int p = 0; p < HREFS.Count; p++) // цикл проверки уникальности ссылки
                    {
                        if (match.Groups[1].Value == HREFS[p])
                        {
                            uniqueLink = false;
                            break;
                        }
                    }
                    if (uniqueLink == true)
                    {
                        HREFS.Add(match.Value);
                    }
                }
            }
            return HREFS;
        }

        bool IsPageExists(string url)
        {
            try
            {
                WebClient client = new WebClient();
                client.DownloadString(url);
            }
            catch (WebException ex)
            {
                HttpWebResponse response = ex.Response != null ? ex.Response as HttpWebResponse : null;
                if (response != null && response.StatusCode == HttpStatusCode.NotFound)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
