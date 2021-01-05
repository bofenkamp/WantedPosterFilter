using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFeed : MonoBehaviour
{
    WebCamTexture camTex;
    int camID = 0;

    void Start()
    {
        WebCamDevice[] devices = WebCamTexture.devices;
#if (UNITY_EDITOR || UNITY_STANDALONE)
        camID = 0;
#elif (UNITY_IOS || UNITY_ANDROID)
        camID = devices.Length - 1;
#endif
        ResizeScreen();
        StartCamera();
    }

    void ResizeScreen()
    {
        float aspectRatio = (float)Screen.width / (float)Screen.height;
        float worldHeight = 10f;
        float worldWidth = worldHeight * aspectRatio;
        float windowSize = worldWidth * 0.9f;
#if UNITY_EDITOR
        transform.eulerAngles = Vector3.zero;
        transform.localScale = new Vector3(windowSize, windowSize / aspectRatio, 1);
#else
        transform.eulerAngles = Vector3.back * 90f;
        transform.localScale = new Vector3(windowSize / aspectRatio, windowSize, 1);
#endif
        //transform.localScale = new Vector3(windowSize, windowSize, 1);
        float y = (worldWidth + worldHeight) / 4f - 0.15f * worldHeight - 0.075f * worldHeight - windowSize / 2f;
        transform.position = new Vector3(0, y, 0);
    }

    void StartCamera()
    {
        WebCamDevice[] devices = WebCamTexture.devices;
        string webcamName = devices[camID].name;
        MeshRenderer renderer = GetComponent<MeshRenderer>();
        camTex = new WebCamTexture(webcamName);
        renderer.material.mainTexture = camTex;
        try
        {
            camTex.Play();
        }
        catch
        {
            FlipCamera();
        }
    }

    public void FlipCamera()
    {
        WebCamDevice[] devices = WebCamTexture.devices;
        camID++;
        if (camID >= devices.Length)
        {
            camID = 0;
        }

#if (UNITY_IOS || UNITY_ANDROID)

        float quadRot = (transform.eulerAngles.y + 180) % 360;
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, quadRot, transform.eulerAngles.z);

#endif

        StartCamera();
    }

    void OnDestroy()
    {
        camTex.Stop();
    }
}