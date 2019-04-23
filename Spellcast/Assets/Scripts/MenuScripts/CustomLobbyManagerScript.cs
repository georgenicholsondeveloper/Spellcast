using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CustomLobbyManagerScript : NetworkLobbyManager
{
    public Dictionary<int, int> currentPlayers = new Dictionary<int, int>();
    public string ipAddress;

    public void SetUpServer()
    {
        SetPort();
        NetworkLobbyManager.singleton.StartHost();
    }

    public void SetUpClient()
    {
        SetPort();
        SetAddress();
        NetworkLobbyManager.singleton.StartClient();
    }

    void SetPort()
    {
        NetworkLobbyManager.singleton.networkPort = 7777;
    }

    void SetAddress()
    {
        NetworkLobbyManager.singleton.networkAddress = ipAddress;
    }


    public override GameObject OnLobbyServerCreateLobbyPlayer(NetworkConnection conn, short playerControllerId)
    {
        if (!currentPlayers.ContainsKey(conn.connectionId))
            currentPlayers.Add(conn.connectionId, 0);
     
        return base.OnLobbyServerCreateLobbyPlayer(conn, playerControllerId);
    }

    public void SetPlayerType(NetworkConnection conn, int character)
    {
        if (currentPlayers.ContainsKey(conn.connectionId))
        {
            currentPlayers[conn.connectionId] = character;
        }
   
    }

    public override GameObject OnLobbyServerCreateGamePlayer(NetworkConnection conn, short playerControllerId)
    {
        int index = currentPlayers[conn.connectionId];

        GameObject spawn = Instantiate(spawnPrefabs[index]);

        return spawn;
        
    }
}
