using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManagerScript : MonoBehaviour {

    public GameObject networkManager, startMenu, selectMenu, adminTbox, adminTbox2;
    private GameObject currentTbox;
    public string adminPassword;
    public bool connected = false;

    bool online;


    public void StartMenuFunction()
    {
        selectMenu.SetActive(true);
        startMenu.SetActive(false);
    }

    public void SelectMenuFunction()
    {
        startMenu.SetActive(true);
        selectMenu.SetActive(false);
    }

    public void ShowAdminText()
    {
        if (!online)
        {
            if (!adminTbox.activeInHierarchy)
                adminTbox.SetActive(true);
            else
                adminTbox.SetActive(false);
        }
        else
        {
            if (!adminTbox2.activeInHierarchy)
                adminTbox2.SetActive(true);
            else
                adminTbox2.SetActive(false);
        }
    }

    public void PlayButton()
    {
        networkManager.GetComponent<CustomLobbyManagerScript>().SetUpClient();
        online = true;
    }

    public void CheckPassword()
    {

        if (startMenu.activeSelf)
            currentTbox = adminTbox;
        else
            currentTbox = adminTbox2;

        if (currentTbox.GetComponentInChildren<InputField>().text == adminPassword)
        {
            if (!online)
            {
                networkManager.GetComponent<CustomLobbyManagerScript>().SetUpServer();
                StartMenuFunction();
                online = true;
            }
            else
                GetComponent<CameraMenu>().CameraMenuFunction();

            currentTbox.SetActive(false);
        }
        else
            print("Incorrect Password");

    }

  
}
