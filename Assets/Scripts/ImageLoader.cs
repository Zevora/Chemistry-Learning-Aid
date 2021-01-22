/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;

public class ImageLoader : MonoBehaviour
{
    public Renderer renderer;

    public IEnumerator Load2DStructure(string CID_value)
    {
        string url = "https://pubchem.ncbi.nlm.nih.gov/compound/" + CID_value + "#section=2D-Structure";

        renderer.material.color = Color.white;
        UnityEngine.Debug.Log("Rendering Structure");
        WWW www = new WWW(url);
        yield return www;


        UnityEngine.Debug.Log("Finished Rendering Structure");
        renderer.material.mainTexture = www.texture;
    }
}*/

