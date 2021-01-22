using System.Collections.Generic;
using System.Xml;
using System.Net;
using System.Web;
using UnityEngine;
using System.IO;


public class XMLParser
{
    public static string ParseLocalXML(string fileName, string tagName)
    {
        try
        {
            XmlDocument xmlDoc = new XmlDocument(); // Create an XML document object
            xmlDoc.Load(fileName); // Load the XML document from the specified file
            return xmlDoc.GetElementsByTagName(tagName)[0].InnerText;
        }
        catch
        {
            return "Unable to retrieve from local XML file ";
        }
    }

    private static XmlDocument LoadDoc(string url)
    {
        try
        {
            WebRequest request = WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            Stream responseStream = (response.GetResponseStream());
            XmlDocument doc = new XmlDocument();
            XmlTextReader XMLReader = null;
            XMLReader = new XmlTextReader(response.GetResponseStream());
            doc.Load(XMLReader);
            response.Close();
            return doc;
        }
        catch
        {
            return null;
        }
    }

    public static void ParseListHTTPXML(string URL, ref List<string> tagNames) // parses for a list of tags
    {
        XmlDocument xmlDoc = LoadDoc(URL);
        for (int i = 0; i < tagNames.Count; i++)
        {
            try
            {
                tagNames[i] = xmlDoc.GetElementsByTagName(tagNames[i])[0].InnerText;
            }
            catch
            {
                tagNames[i] = "Unable to retrieve from server ";
            }
        }
    }

    public static string ParseSingleHTTPXML(string URL, string tagName) // Parses for a single tag
    {
        try
        {
            XmlDocument xmlDoc = LoadDoc(URL);
            return xmlDoc.GetElementsByTagName(tagName)[0].InnerText;
        }
        catch
        {
            return "Unable to retrieve from server ";
        }
    }
}
