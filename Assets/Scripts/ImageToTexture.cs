using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;

public class ImageToTexture : MonoBehaviour
{
    private const string webImage = "https://upload.wikimedia.org/wikipedia/commons/thumb/1/15/Cat_August_2010-4.jpg/2560px-Cat_August_2010-4.jpg";

    // Cache for the downloaded texture
    private static Texture2D cachedTexture;


    private void Start()
    {
        Renderer billboardRenderer = GetComponent<Renderer>();
        StartCoroutine(GetWebImage(texture => {billboardRenderer.material.mainTexture = texture;}));
    }
    public IEnumerator DownloadImage(Action<Texture2D> callback)
    {

        UnityWebRequest request = UnityWebRequestTexture.GetTexture(webImage);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogError($"network problem: {request.error}");
        }
        else if (request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError($"response error: {request.responseCode}");
        }
        else
        {
            cachedTexture = DownloadHandlerTexture.GetContent(request);
            callback?.Invoke(cachedTexture);
        }
    }
    public IEnumerator GetWebImage(Action<Texture2D> callback)
    {
        if (cachedTexture != null)
        {
            callback?.Invoke(cachedTexture);
            yield break;
        }


        yield return DownloadImage(callback);
    }
}
