using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerObject : NetworkBehaviour {

    public GameObject PlayerPrefab;

    void Start ()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        CmdSpawnUnit();
	}


    [Command]
    void CmdSpawnUnit()
    {
        GameObject go = Instantiate(PlayerPrefab);
        NetworkServer.SpawnWithClientAuthority(go, connectionToClient);
    }

    
}
