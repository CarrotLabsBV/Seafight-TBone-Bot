using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace Seafight_TBone.HTTP
{
    public class HttpOptions
    {
        public static string GetPage(string uri)
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(uri);

            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.Method = "GET";
            webRequest.UserAgent = "Mozilla/5.0 (Windows NT 5.0; rv:5.0) Gecko/20100101 Firefox/5.0";
            // in diesem Fall geben wir uns als Firefoxbrowser aus

            try
            {
                // Anfrage wird abgeschickt
                WebResponse webResponse = webRequest.GetResponse();
                if (webResponse == null)
                {
                    return null;
                }
                // Die Antwort(z.B. eine Seite in HTML) des Servers wird über einen Stream geladen.
                StreamReader sr = new StreamReader(webResponse.GetResponseStream());
                return sr.ReadToEnd().Trim(); // Stream wird bis zum Ende gelesen und zurückgegeben.
            }

            catch (WebException ex) // mögliche WebExceptions werden abgefangen
            {
                // response.StatusCode <- enthält den Typ des Fehlers

                // Der Inhalt/Content wird trotzdem gelesen.
                HttpWebResponse response = (HttpWebResponse)ex.Response;
                StreamReader sr = new StreamReader(response.GetResponseStream());
                return sr.ReadToEnd().Trim(); // Der Inhalt/Content gegeben.
            }
            catch (Exception)// mögliche Exceptions werden abgefangen
            {
                throw;
            }
        }
    }
}
