using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Distortion : MonoBehaviour {
    public Camera dispCamera;
    private Texture2D gridTexture;
    private Texture2D finalTexture;

    public GameObject Screen;

    public float k1 = 0f;
    public float k2 = 0f;
    public float k3 = 0f;
    public float k4 = 1f;
    public float k5 = 1f;
    public float k6 = 1f;

    public int cameraResolutionX = 1440;
    public int cameraResolutionY = 1440;

    public int displayResolutionX = 1440;
    public int displayResolutionY = 1440;


    public bool rendering = false;

    



	// Use this for initialization
	void Start () {
        dispCamera.targetTexture.width = cameraResolutionX;
        dispCamera.targetTexture.height = cameraResolutionY;
        var tex = dispCamera.targetTexture;
        gridTexture = new Texture2D(tex.width, tex.height, TextureFormat.ARGB32, false);
        distortion(tex);
		
	}
	
	// Update is called once per frame
	void Update () {
        if (k1 != k4 || k2 != k5 || k3 != k6 || rendering != true)
        {
            var tex = dispCamera.targetTexture;
            distortion(tex);
            k4 = k1;
            k5 = k2;
            k6 = k3;

            rendering = false;
        }
    }


    private void distortion(RenderTexture tex)
    {
        RenderTexture.active = dispCamera.targetTexture;
        gridTexture.ReadPixels(new Rect(0, 0, tex.width, tex.height), 0, 0);
        gridTexture.Apply();

        finalTexture = new Texture2D(displayResolutionX, displayResolutionY, TextureFormat.ARGB32, false);
        for(int y = 0; y < finalTexture.height; y++)
        {
            for(int x = 0; x < finalTexture.width; x++)
            {
                float x0 = x / (float)finalTexture.width - 0.5f;
                float y0 = y / (float)finalTexture.height - 0.5f;
                float r2 = x0 * x0 + y0 * y0;
                float r4 = r2 * r2;
                float r6 = r4 * r2;
                finalTexture.SetPixel(x, y, gridTexture.GetPixel(
                    (int)((x0 * (1f + k1 * r2 + k2 * r4 + k3 * r6) + 0.5f) * (float)finalTexture.width),
                    (int)((y0 * (1f + k1 * r2 + k2 * r4 + k3 * r6) + 0.5f) * (float)finalTexture.height)
                    ));
            }
        }
        finalTexture.Apply();
        Screen.GetComponent<Renderer>().material.mainTexture = finalTexture;


    }







}
