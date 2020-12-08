using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using System.Net;
using System;
using System.Linq;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Threading;
using System.Web;

// Purpose:
//      Uses PubChem's public PUG REST API to gather the molecular formula, molecule weight, acidic pKa, melting point, boiling point
//          for a given compound / element / chemical
//
// Extra Info:
//      Utilizies PubChem's PUG REST API
//      For URL Construction Read -- https://pubchemdocs.ncbi.nlm.nih.gov/pug-rest
//      Max recommended API calls was ~5 per minute. Was not blacklisted from doing more, but added cacheing to help limit the amount made
//
// How it works:
//      First checks to see if the provided chemical name was found in the function ReadChemical in the class CacheChemical.cs
//          It looks through the CSV file and if it's found it will append the values to the chemical and return without API calls
//          Else, it will proceed below with the PubChem API calls
//          
//      Uses 4 different API calls to find the needed values that makes up the compound / element
//      First API call gets the CID (used to identify the element for other API Calls) and Formula / Weight
//          & parses the retrieved XML doc in ParseResponse
//      The following 3 API calls retrieve the melting / boiling points & pKa with the same methods, using the found CID as a primary key
//      Used within SpeecRecognizer.cs for lookup
//
// What else can be implemented.
//      Can add a 2D/3D schematic view of the compound structure using the found CID
//         Can use "https://pubchem.ncbi.nlm.nih.gov/compound/" + CID_value + "#section=2D-Structure" to retrieve a 2D view
//
// Extra Resources
//      https://chem.libretexts.org/Courses/St_Louis_College_of_Pharmacy/CHEM3351%3A_Cheminformatics/7%3A_Programmatic_Access_to_Public_Chemical_Databases
//

public class PubChem
{
    public static void LookupCompounds(string elementName, ref Chemical chemical)
    {
        CacheChemical cache = new CacheChemical();

        // Check to see if the element / chemical / compound was cached previously 
        //      if so append the values with those found in the CSV file
        //      Else attempt to find the values from pubChem's API

        if (!cache.ReadChemical(ref chemical, elementName))
        {
            // First Lookup CID (Used for other API, Molecular Formula & Molecular Weight
            string URL = "https://pubchem.ncbi.nlm.nih.gov/rest/pug/compound/name/" + elementName + "/property/MolecularFormula,MolecularWeight/xml";
            XmlDocument doc = PubGetRequest(URL);
            string CID = doc.GetElementsByTagName("CID")[0].InnerXml;
            string molecularFormula = ParseResponse(ref doc, "MolecularFormula", "Molecular Formula", true);
            string molecularWeight = ParseResponse(ref doc, "MolecularWeight", "Molecular Weight", true);

            // Get the melting point using the previously found CID
            URL = "https://pubchem.ncbi.nlm.nih.gov/rest/pug_view/data/compound/" + CID + "/xml/?heading=Melting+Point";
            doc = PubGetRequest(URL);
            string meltingPoint = ParseResponse(ref doc, "String", "Melting Point", true);

            // Boiling Point
            URL = "https://pubchem.ncbi.nlm.nih.gov/rest/pug_view/data/compound/" + CID + "/xml/?heading=Boiling+Point";
            doc = PubGetRequest(URL);
            string boilingPoint = ParseResponse(ref doc, "String", "Boiling Point", true);

            // Acidc pKa
            URL = "https://pubchem.ncbi.nlm.nih.gov/rest/pug_view/data/compound/" + CID + "/xml/?heading=pKa";
            doc = PubGetRequest(URL);
            string pka = ParseResponse(ref doc, "Number", "pKa", true);
            
            chemical.SetName(elementName);
            chemical.SetCid(CID);
            chemical.SetMolecularFormula(molecularFormula);
            chemical.SetMolecularWeight(molecularWeight);
            chemical.SetMeltingPoint(meltingPoint);
            chemical.SetBoilingPoint(boilingPoint);
            chemical.SetPka(pka);
            UnityEngine.Debug.Log("Writing to cache: " + chemical.GetAllProperties());
            cache.WriteChemical(ref chemical);
            
        }
        return; // Chemical is passed by reference so no need to return it
    }

    // Makes a GET Request to the URL, retrieves the response and returns the loaded XML Doc
    private static XmlDocument PubGetRequest(string URL)
    {
        try
        {
            WebRequest request = WebRequest.Create(URL);
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

    // Adds parses and adds the response to the list of responses .. Added here for clean up of redundent try / catch blocks
    // firstEntryOnly bool will only add the first found entry, since PubChem pulls from other sites, it may repeat information that is formatted differently
    private static string ParseResponse(ref XmlDocument doc, string tagName, string elementType, bool firstEntryOnly)
    {
        try
        {
            if (!firstEntryOnly)
            {
                // Adds all nodes to the reponse list
                XmlNodeList elements = doc.GetElementsByTagName(tagName);
                string multi = "";
                for (int i = 0; i < elements.Count; i++)
                    multi += elements[i].InnerXml + ", \t";
                return multi;
            }

            else
                return doc.GetElementsByTagName(tagName)[0].InnerXml;
        }

        catch
        {
            return "Unable to retrieve from server\t";
        }
    }

    //URL = "https://pubchem.ncbi.nlm.nih.gov/image/imagefly.cgi?cid=" + CID + "&width=500&height=500";
    //    return
}