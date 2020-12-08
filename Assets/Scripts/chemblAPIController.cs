using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;
using System.Net.Http;
using System.Net.Http.Headers;
using System;


/*

public class chemblAPIController : MonoBehaviour
{

    public const string URL = "https://www.ebi.ac.uk/chembl/api/data/molecule"; //This is what connects to the API


    private readonly string molURL = "https://www.ebi.ac.uk/chembl/api/data/molecule";   //Returns all molecules



    static void Main(string[] args)
    {
        HttpClient client = new HttpClient();
        client.BaseAddress = new Uri(URL);

        // Add an Accept header for JSON format.
        client.DefaultRequestHeaders.Accept.Add(
        new MediaTypeWithQualityHeaderValue("application/json"));

        // List data response.
        HttpResponseMessage response = client.GetAsync(urlParameters).Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.
        if (response.IsSuccessStatusCode)
        {
            // Parse the response body.
            var dataObjects = response.Content.ReadAsAsync<IEnumerable<DataObject>>().Result;  //Make sure to add a reference to System.Net.Http.Formatting.dll
            foreach (var d in dataObjects)
            {
                Console.WriteLine("{0}", d.Name);
            }
        }
        else
        {
            Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
        }

        //Make any other calls using HttpClient here.

        //Dispose once all HttpClient calls are complete. This is not necessary if the containing object will be disposed of; for example in this case the HttpClient instance will be disposed automatically when the application terminates so the following call is superfluous.
        client.Dispose();
    }










IEnumerator GetMoleculeAtIndex(int moleculeIndex)
    {
        //Get molecule info
        string moleculeURL = molURL + "molecule" + moleculeIndex.ToString();    //Trying to get stuff to print
        
        UnityWebRequest moleculeRequest = UnityWebRequest.Get(moleculeURL); //Sending the webrequest to the URL

        yield return moleculeRequest.SendWebRequest(); //Returns the info grabbed to the IEnumerator

        if(moleculeRequest.isNetworkError || moleculeRequest.isHttpError)   //Network error
        {
            Debug.LogError(moleculeRequest.error);
            yield break;
        }

        //If no errors above, then continue parsing through the JSON
        JSONNode molInfo = JSON.Parse(moleculeRequest.downloadHandler.text);

        string moleculeName = molInfo["name"];
        Debug.Log(moleculeName);

    }
    void Start()
    {
        Debug.Log(molURL + "Chicken nuggets");

    }

    // Update is called once per frame
    void Update()
    {

    }
}
*/