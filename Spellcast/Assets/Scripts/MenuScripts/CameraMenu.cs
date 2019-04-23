using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMenu : MonoBehaviour {

    public GameObject wiz1Pic, wiz2Pic, camSelectPic, cam1pic, cam2pic;
	
	public void CameraMenuFunction()
    {
        wiz1Pic.SetActive(false);
        wiz2Pic.SetActive(false);
        camSelectPic.SetActive(false);
        cam1pic.SetActive(true);
        cam2pic.SetActive(true);
    }
}
