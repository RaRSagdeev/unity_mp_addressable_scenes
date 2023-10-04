using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;

public class SpawnTest : NetworkBehaviour
{
    [SerializeField] private GameObject _cube;

    private GameObject spawnedObject;
    public void SpawnCube()
    {
        //Debug.LogError(gameObject.GetComponent<NetworkBehaviour>().NetworkObject.PrefabIdHash);
        SpawnServerRpc();
    }

    public override void OnNetworkSpawn()
    {
    }

    private void SpawnSomething()
    {
        spawnedObject = Instantiate(_cube);

        var netObject = spawnedObject.GetComponent<NetworkObject>();

        netObject.Spawn(true);

        SetMessageParent();
    }

    [ServerRpc(RequireOwnership = false)]
    private void SpawnServerRpc()
    {
        SpawnSomething();
    }

    private void SetMessageParent()
    {
        if (IsServer)
        {
            var networkObject = spawnedObject.GetComponent<NetworkObject>();
            var playerObject = NetworkManager.ConnectedClients[1].PlayerObject;
            networkObject.TrySetParent(playerObject, false);
        }
    }

    private void SceneManager_OnSceneEvent(SceneEvent sceneEvent)
    {
        switch (sceneEvent.SceneEventType)
        {
            case SceneEventType.SynchronizeComplete:
                if (sceneEvent.ClientId != NetworkManager.LocalClientId)
                {
                    SetMessageParent();
                }
                break;
            default:
                return;
        }
    }
}
