using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VariableStorageScript : MonoBehaviour
{
    public GameObject wiz1pic, wiz2pic, cam1pic, cam2pic, cam3pic, characterSelectMenu;
    public Button wiz1Button, wiz2Button, cam1Button, cam2Button;

	void Start ()
    {
        characterSelectMenu.SetActive(false);
    }

}
