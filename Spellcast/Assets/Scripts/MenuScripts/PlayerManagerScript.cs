using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerManagerScript : NetworkBehaviour
{
    [SyncVar]
    public bool changeColor, character1, character2;
    [SyncVar]
    int selected = 0;

    public bool hasClicked;
    GameObject selectedGo, wiz1pic, wiz2pic, cam1pic, cam2pic, cam3pic,menuManager;
    Button wiz1Button, wiz2Button, cam1Button, cam2Button;

    private void Start()
    {
        menuManager = GameObject.Find("MenuManager");
        wiz1pic = menuManager.GetComponent<VariableStorageScript>().wiz1pic;
        wiz2pic = menuManager.GetComponent<VariableStorageScript>().wiz2pic;
        cam1pic = menuManager.GetComponent<VariableStorageScript>().cam1pic;
        cam2pic = menuManager.GetComponent<VariableStorageScript>().cam2pic;
        cam3pic = menuManager.GetComponent<VariableStorageScript>().cam3pic;


        if (isServer)
            cam1pic.GetComponent<Image>().color = Color.white;
        else
        {
            cam1pic.GetComponent<Image>().color = Color.grey;
        }


        if (isLocalPlayer)
        {
            wiz1Button = menuManager.GetComponent<VariableStorageScript>().wiz1Button;
            wiz2Button = menuManager.GetComponent<VariableStorageScript>().wiz2Button;
            cam1Button = menuManager.GetComponent<VariableStorageScript>().cam1Button;
            cam2Button = menuManager.GetComponent<VariableStorageScript>().cam2Button;
            wiz1Button.onClick.AddListener(delegate { Wiz1(); });
            wiz2Button.onClick.AddListener(delegate { Wiz2(); });
            cam1Button.onClick.AddListener(delegate { Cam1(); });
            cam2Button.onClick.AddListener(delegate { Cam2(); });
        }
    }

    private void Update()
    {
        MonitorSelected();
    }
    
    void MonitorSelected()
    {
        switch (selected)
        {
            case 1:
                selectedGo = wiz1pic;
                break;
            case 2:
                selectedGo = wiz2pic;
                break;
            case 3:
                selectedGo = cam2pic;
                break;
            case 4:
                selectedGo = cam3pic;
                break;
        }
        Change();
    }

    [ClientRpc]
    void RpcOnChange(bool value)
    {
        changeColor = true;
    }
 
    void Change()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(0))
        {
            if (selected != 0 && changeColor)
            {
                selectedGo.GetComponent<Image>().color = Color.grey;
            }
        }
    }

    [ClientRpc]
    void RpcSelectedChange(int value)
    {
        selected = value;
    }

    [Command]
    void CmdBuffer(bool value, int selected)
    {
        RpcOnChange(value);
        RpcSelectedChange(selected);
    }

    public void Wiz1()
    {
        if (!hasClicked)
        {
            selected = 1;
            changeColor = true;

            if (isServer)
            {
                RpcOnChange(changeColor);
                RpcSelectedChange(selected);
            }
            else
                CmdBuffer(changeColor, selected);

            hasClicked = true;
        }
    }


    public void Wiz2()
    {
        if (!hasClicked)
        {
            selected = 2;
            changeColor = true;

            if (isServer)
            {
                RpcOnChange(changeColor);
                RpcSelectedChange(selected);
            }
            else
                CmdBuffer(changeColor, selected);

            hasClicked = true;
        }
    }

    void Cam1()
    {
        if (!hasClicked)
        {
            selected = 3;
            changeColor = true;

            if (isServer)
            {
                RpcOnChange(changeColor);
                RpcSelectedChange(selected);
            }
            else
                CmdBuffer(changeColor, selected);

            hasClicked = true;
        }
    }

    public void Cam2()
    {
        if (!hasClicked)
        {
            selected = 4;
            changeColor = true;

            if (isServer)
            {
                RpcOnChange(changeColor);
                RpcSelectedChange(selected);
            }
            else
                CmdBuffer(changeColor, selected);

            hasClicked = true;
        }
    }

}
