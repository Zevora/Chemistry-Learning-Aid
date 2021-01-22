using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Xml;
using System.IO;
using System.Net;
using System;

public class chemblAPI : MonoBehaviour
{
    // Start is called before the first frame update

    //What the API needs to do: molecular formula, molecule weight, acidic pKa, melting point, boiling point

    //test purposes
    //https://www.ebi.ac.uk/chembl/api/data/molecule/search?q=water 

    private const string URL = "https://www.ebi.ac.uk/chembl/api/data/molecule";
    /*

   void Start()
   {
       //correct website page
       //StartCoroutine(GetRequest("https://www.example.com"));
       //StartCoroutine(GetRequest(URL));

   }

   IEnumerator GetRequest(string uri)  //GetRequest of the string URL which is the website
   {
       using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
       {
           //Request and wait for the desired page.
           yield return webRequest.SendWebRequest();

           string[] pages = uri.Split('/');
           int page = pages.Length - 1;

           if(webRequest.isNetworkError)
           {
               Debug.Log(pages[page] + ": Error: " + webRequest.error);
           }
           else
           {
               Debug.Log(pages[page] + ":\nRecieved: " + webRequest.downloadHandler.text);
           }

           XmlNodeList nodes = root.SelectNodes("//games/game");
           foreach (XmlNode node in nodes)
           {
               listBox1.Items.Add(node["name"].InnerText);
           }

       }
   }*/





    /*
    void Start()
    {
        // Create a request for the URL.
        WebRequest request = WebRequest.Create("https://www.ebi.ac.uk/chembl/api/data/molecule/search?q=hydrochloric%20acid "); //show in console

        // Get the response.
        WebResponse response = request.GetResponse();

        Stream responseStream = (response.GetResponseStream());
        XmlDocument doc = new XmlDocument();
        XmlTextReader myXMLReader = null;
        myXMLReader = new XmlTextReader(response.GetResponseStream());

        XmlNodeList idList = doc.GetElementsByTagName("molecule_chembl_id"); //getting the idList
        XmlNodeList formulaList = doc.GetElementsByTagName("full_molformula"); //getting the moleculeFormula
        XmlNodeList weightList = doc.GetElementsByTagName("full_mwt"); //getting the molecularWeight
        XmlNodeList pkaList = doc.GetElementsByTagName("cx_most_apka");//cx_most_apka
        XmlNodeList molSpeciesList = doc.GetElementsByTagName("molecular_species"); //molecular_species

        doc.Load(myXMLReader);
        for (int i = 0; i < formulaList.Count; i++)
        {
            Console.WriteLine(formulaList[i].InnerXml);
            Debug.Log(idList[i].InnerXml + ", " + formulaList[i].InnerXml + ", " + weightList[i].InnerXml + ", " + pkaList[i].InnerXml + ", " + molSpeciesList[i].InnerXml); //Going through the element list
        }

        response.Close();
    }*/
}
