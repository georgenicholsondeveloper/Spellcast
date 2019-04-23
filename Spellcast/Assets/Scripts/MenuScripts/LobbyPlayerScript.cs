using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LobbyPlayerScript : NetworkLobbyPlayer {

    public static bool b1Clicked, b2Clicked, b3Clicked, b4Clicked; 
    GameObject lobbyManager, menuManager;
    bool character1, character2, character3, character4, hasSelected;
    Button wiz1Button, wiz2Button, cam1Button, cam2Button;

	void Start ()
    {
        lobbyManager = GameObject.FindGameObjectWithTag("LobbyManager");
        menuManager = GameObject.FindGameObjectWithTag("MenuManager");

        if (isLocalPlayer)
        {
            menuManager.GetComponent<MenuManagerScript>().StartMenuFunction();
            wiz1Button = menuManager.GetComponent<VariableStorageScript>().wiz1Button;
            wiz2Button = menuManager.GetComponent<VariableStorageScript>().wiz2Button;
            cam1Button = menuManager.GetComponent<VariableStorageScript>().cam1Button;
            cam2Button = menuManager.GetComponent<VariableStorageScript>().cam2Button;
            wiz1Button.onClick.AddListener(delegate { Call(); });
            wiz2Button.onClick.AddListener(delegate { Call2(); });
            cam1Button.onClick.AddListener(delegate { Call3(); });
            cam2Button.onClick.AddListener(delegate { Call4(); });
        }
        else
            return;     
    }
	

	void Update ()
    {
        if (isLocalPlayer)
        {
            if (character1 && !b1Clicked && !hasSelected)
            {
                CmdCharacterOne();
                SendReadyToBeginMessage();
                b1Clicked = true;
                hasSelected = true;
            }
            if (character2 && !b2Clicked && !hasSelected)
            {
                CmdCharacterTwo();
                SendReadyToBeginMessage();
                b2Clicked = true;
                hasSelected = true;
            }
            if (character3 && !b3Clicked && !hasSelected)
            {
                CmdCameraOne();
                SendReadyToBeginMessage();
                b3Clicked = true;
                hasSelected = true;
            }
            if (character4 && !b4Clicked && !hasSelected)
            {
                CmdCameraTwo();
                SendReadyToBeginMessage();
                b4Clicked = true;
                hasSelected = true;
            }
        }

	}

    public void Call()
    {
        if (isLocalPlayer)
        {
            character1 = true;            
        }
    }

    public void Call2()
    {
        if (isLocalPlayer)
        {
            character2 = true;           
        }
    }

    public void Call3()
    {
        if (isLocalPlayer)
        {
            character3 = true;
        }
    }

    public void Call4()
    {
        if (isLocalPlayer)
        {
            character4 = true;
        }
    }

    [Command]
    void CmdCharacterOne()
    {
        lobbyManager.GetComponent<CustomLobbyManagerScript>().SetPlayerType(connectionToClient, 0);
        RpcChar1();
    }

    [Command]
    void CmdCharacterTwo()
    {
        lobbyManager.GetComponent<CustomLobbyManagerScript>().SetPlayerType(connectionToClient, 1);
        RpcChar2();
    }

    [Command]
    void CmdCameraOne()
    {
        lobbyManager.GetComponent<CustomLobbyManagerScript>().SetPlayerType(connectionToClient, 2);
        RpcCam1();
    }

    [Command]
    void CmdCameraTwo()
    {
        lobbyManager.GetComponent<CustomLobbyManagerScript>().SetPlayerType(connectionToClient, 3);
        RpcCam2();
    }

    [ClientRpc]
    void RpcChar1()
    {
        b1Clicked = true;
    }

    [ClientRpc]
    void RpcChar2()
    {
        b2Clicked = true;
    }

    [ClientRpc]
    void RpcCam1()
    {
        b3Clicked = true;
    }


    [ClientRpc]
    void RpcCam2()
    {
        b4Clicked = true;
    }


}
