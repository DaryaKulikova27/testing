using System;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;
using System.IO;
using System.Linq;

namespace lab2_links
{
    class Links
    {
        string linkValue;
        string statusCode;

        public Links(string url, string statusCode)
        {
            this.linkValue = url;
            this.statusCode = statusCode;
        }

        public string getLinkValue()
        {
            return linkValue;
        }

        public string getStatusCode()
        {
            return statusCode;
        }
    }

    class Program
    {
        //private const string URL = "http://links.qatl.ru/";
        //private const string URL = "file:///C:/Users/User/Documents/11_adaptive-layout-desktop/index.html";
        private const string URL = "http://deadlink.fcd.su/";
        private const string fileValidLinks = "validLinks.txt";
        private const string fileInvalidLinks = "invalidLinks.txt";
        List<Links> validLinks = new List<Links>();
        List<Links> invalidLinks = new List<Links>();
        List<string> notLinksElement = new List<string>(new string[] { "tel", "mailto", ".jpg", ".png", ".svg", ".gif", "tg", "popup"});

        static void Main(string[] args)
        {
            var program = new Program();
            string html = program.GetUrl(URL);
            program.GetHrefLists(ref html);
            program.writeLinkIntoFile(fileName: fileValidLinks, ref program.validLinks);
            program.writeLinkIntoFile(fileName: fileInvalidLinks, ref program.invalidLinks);
            Console.ReadKey();
        }

        public string GetUrl(string address)
        {
            WebClient client = new WebClient();
            return client.DownloadString(address);
        }

        void GetHrefLists(ref string html)
        {
            string[] d = html.Split('\n');
            string HRefPattern = "a href\\s*=\\s*(?:\"(?<1>[^\"]*)\"|(?<1>\\S+))";
            

            foreach (string str in d)
            {
                foreach (Match match in Regex.Matches(str, HRefPattern))
                {
                    bool containsNotLinksElements = false;

                    if  (match.Groups[1].Value == "#")
                    {
                        continue;
                    }

                    if (match.Groups[1].Value.Contains("http") && !match.Groups[1].Value.Contains(URL)) continue;

                    foreach (string element in notLinksElement)
                    {
                        if (match.Groups[1].Value.Contains(element))
                        {
                            containsNotLinksElements = true;
                            break;
                        }
                    }
          
                    if (!containsNotLinksElements && !match.Groups[1].Value.Contains(URL) && !match.Groups[1].Value.Contains("http"))
                    {
                        string text = "abcdefghjigklmnopkrstuvwxyz";
                        int i = 0;
                        foreach (char cr in match.Groups[1].Value)
                        {
                            if (text.Contains(cr)) break;
                            i++;
                        }

                        if (checkValidLink(URL + match.Groups[1].Value[i..]))
                        {
                            string newHtml = GetUrl(URL + match.Groups[1].Value[i..]);
                            GetHrefLists(ref newHtml);
                        }
                    }
                    else if (!containsNotLinksElements)
                    {
                        if (checkValidLink(match.Groups[1].Value))
                        {
                            string newHtml = GetUrl(match.Groups[1].Value);
                            GetHrefLists(ref newHtml);
                        }
                    }
                }
            }
        }

        void writeLinkIntoFile(string fileName, ref List<Links> listOfLinks)
        {
            try
            {
                StreamWriter sw = new StreamWriter(fileName);
                foreach (Links link in listOfLinks)
                {
                    sw.Write(link.getLinkValue() + "    " +  link.getStatusCode());
                    sw.WriteLine();
                }
                sw.WriteLine();
                sw.WriteLine("Number of links: " + listOfLinks.Count);
                sw.WriteLine("Last check date: " + DateTime.Now);
                sw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.WriteLine("OK");
            }
        }
        
        bool checkUniqueValue(string url)
        {
            bool isUnique = true;
            for (int p = 0; p < validLinks.Count; p++)
            {
                if (url == validLinks[p].getLinkValue())
                {
                    isUnique = false;
                    break;
                }
            }
            for (int p = 0; p < invalidLinks.Count; p++)
            {
                if (url == invalidLinks[p].getLinkValue())
                {
                    isUnique = false;
                    break;
                }
            }
            return isUnique;
        }

        bool checkValidLink(string url)
        {
            if (checkUniqueValue(url))
            {
                Console.WriteLine(url);
                try
                {
                    HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                    HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
                    if ((int)myHttpWebResponse.StatusCode < 400)
                    {
                        Links validLink = new(url, myHttpWebResponse.StatusCode.ToString());
                        validLinks.Add(validLink);
                    }
                    else
                    {
                        Links invalidLink1 = new(url, myHttpWebResponse.StatusCode.ToString());
                        invalidLinks.Add(invalidLink1);
                        return false;
                    }
                    myHttpWebResponse.Close();
                    return true;
                }
                catch (WebException e)
                {
                    string wRespStatusCode = ((HttpWebResponse)e.Response).StatusCode.ToString();
                    Links invalidLink2 = new(url, wRespStatusCode);
                    invalidLinks.Add(invalidLink2);
                }
                catch (Exception e)
                {
                    Links invalidLink3 = new(url, e.Message.ToString());
                    invalidLinks.Add(invalidLink3);
                }
                return false;
            }
            return false;
        }
    }
}
