using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ImageLoader : MonoBehaviour
{
    

    public void Get2DStructureImage(string CID)
    {

        /*string url = "https://pubchem.ncbi.nlm.nih.gov/image/imagefly.cgi?cid=" + CID + "&width=500&height=500";
        StartCoroutine(DownloadImage(url));
        return setImage(url);
    }

    public IEnumerator setImage(string url)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture("compounds_2D_struct");
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError)
            Debug.Log(request.error);
        else
            YourRawImage.texture = ((DownloadHandlerTexture)request.downloadHandler).texture;*/

    }
}
