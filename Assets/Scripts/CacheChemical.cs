using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// Purpose:
//      Allows the user to lookup previously found chemicals / compounds / elements from a CSV file to help with reducing the latency & 
//          continues to allow some functionality of the lookup features if the API's servers are unable to respond. Another case is
//          to prevent the user from getting blacklisted from sending too many requests in a short period of time
//
// Extra Info:
//      Currently the stored info is saved in Assets/Scripts/Data/cachedChemicals.csv, this can be updated later to a SQLite db if desired
//
// How it works:
//      Works within PubChem.cs, when the lookupcompound function is called, it will first look to see if the chemical was previously
//          stored within the csv file. 
//          If so it will append all the values of the provided ref. chemical and return true
//          Else the function will return false and allow the program to attempt at looking up the values in the API
//          The values found within the API are then used to append the CSV file to add the new chemical, all 
//              values are passed through a filter to remove any ',' that may be present.
//
// What else can be implemented
//      A way to update for info that wasn't able to gathered previously from the server
//          maybe add timestamps that expire and force a new API call to replace old data?
//
//      Limit size of the file. Not sure if this would necessarily be a problem, most users may not lookup very many compounds
//          but it might eventually be slower than always using the APIs in extreme cases.
//

public class CacheChemical
{
    private const string PATH = "Assets/Scripts/Data/";
    private const string FILE = PATH + "cachedChemicals.csv"; // A list of all elements that have been previously queried before

    public CacheChemical()
    {
        // Check if cache file exists, if not create it
        if (!File.Exists(FILE))
        {
            UnityEngine.Debug.Log("The chemical cache csv file was not found.. Creating a new one");
            File.WriteAllText(FILE, "name,cid,formula,weight,pka,meltingpoint,boilingpoint,\n");
        }
    }

    // Attempts to read the chemical from the CSV file. appends values based on chemical ref. and returns true if found
    public bool ReadChemical(ref Chemical chemical, string chemicalName)
    {
        using (var reader = new StreamReader(@FILE))
        {
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(',');

                // Adds values to chemical based on position it was pulled in the row of the csv. Element 0 is name, 1 is cid, 2 formula, 3 weight, 4 acidic pka, 5 melting point, 6 boiling point
                if (values[0] == chemicalName)
                {
                    chemical.SetName(values[0]);
                    chemical.SetCid(values[1]);
                    chemical.SetMolecularFormula(values[2]);
                    chemical.SetMolecularWeight(values[3]);
                    chemical.SetPka(values[4]);
                    chemical.SetMeltingPoint(values[5]);
                    chemical.SetBoilingPoint(values[6]);
                    UnityEngine.Debug.Log("The chemical was found in cache");
                    return true;
                }
            }
            UnityEngine.Debug.Log("The chemical was not found in cache");
            return false;
        }
    }

    public void WriteChemical(ref Chemical chemical)
    {
        // Replaces the ',' due to being the delimiter in a CSV file
        string invalidString = ",";
        string replacementString = " ";

        string chemicalRow = chemical.GetName() + ",";
        chemicalRow += FilterDelimiter(chemical.GetCid(), invalidString, replacementString) + ",";
        chemicalRow += FilterDelimiter(chemical.GetMolecularFormula(), invalidString, replacementString) + ",";
        chemicalRow += FilterDelimiter(chemical.GetMolecularWeight(), invalidString, replacementString) + ",";
        chemicalRow += FilterDelimiter(chemical.GetPka(), invalidString, replacementString) + ",";
        chemicalRow += FilterDelimiter(chemical.GetMeltingPoint(), invalidString, replacementString) + ",";
        chemicalRow += FilterDelimiter(chemical.GetBoilingPoint(), invalidString, replacementString) + ",";

        using (FileStream fileStream = new FileStream(FILE, FileMode.Append, FileAccess.Write))
        {
            using (StreamWriter streamWriter = new StreamWriter(fileStream))
            {
                streamWriter.WriteLine(chemicalRow);
            }
        }
        return;
    }

    private string FilterDelimiter(string filteringString, string invalidString, string replacementString)
    {
        return filteringString.Replace(invalidString, replacementString);
    }
}
