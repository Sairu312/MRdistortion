using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSphere : MonoBehaviour {
    public int widthNum;
    public int heightNum;

    public float dotsD;
    public float camD;


    public GameObject mainCam;
    public GameObject dotsPrefab;


	// Use this for initialization
	void Start () {
        for(int x = 0; x < widthNum; x++)
        {
            for(int y = 0; y < heightNum; y++)
            {
                GameObject dots = Instantiate(dotsPrefab) as GameObject;
                dots.transform.position = new Vector3(
                    x * dotsD + mainCam.transform.position.x - (dotsD * (widthNum-1) / 2f),
                    y * dotsD + mainCam.transform.position.y - (dotsD * (heightNum-1) /2f),
                    camD + mainCam.transform.position.z);
            }
        }
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
