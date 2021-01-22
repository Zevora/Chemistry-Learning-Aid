using System.Collections.Generic;

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
            List<string> tagNames = new List<string>();
            tagNames.Add("CID");
            tagNames.Add("MolecularFormula");
            tagNames.Add("MolecularWeight");
            XMLParser.ParseListHTTPXML("https://pubchem.ncbi.nlm.nih.gov/rest/pug/compound/name/" + elementName + "/property/MolecularFormula,MolecularWeight/xml", ref tagNames);
            chemical.SetName(elementName);
            chemical.SetCid(tagNames[0]);
            chemical.SetMolecularFormula(tagNames[1]);
            chemical.SetMolecularWeight(tagNames[2]);
            chemical.SetMeltingPoint(XMLParser.ParseSingleHTTPXML("https://pubchem.ncbi.nlm.nih.gov/rest/pug_view/data/compound/" + chemical.GetCid() + "/xml/?heading=Melting+Point", "String"));
            chemical.SetBoilingPoint(XMLParser.ParseSingleHTTPXML("https://pubchem.ncbi.nlm.nih.gov/rest/pug_view/data/compound/" + chemical.GetCid() + "/xml/?heading=Boiling+Point", "String"));
            chemical.SetPka(XMLParser.ParseSingleHTTPXML("https://pubchem.ncbi.nlm.nih.gov/rest/pug_view/data/compound/" + chemical.GetCid() + "/xml/?heading=pKa", "Number"));
            cache.WriteChemical(ref chemical);
        }
        return; // Chemical is passed by reference so no need to return it
    }
}