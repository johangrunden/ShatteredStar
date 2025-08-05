using UnityEngine;
using Unity.Netcode;

public class SpawnManager : NetworkBehaviour
{
    public Transform[] spawnPoints;
    public GameObject playerPrefab;

    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            for (int i = 0; i < NetworkManager.Singleton.ConnectedClientsList.Count; i++)
            {
                var clientId = NetworkManager.Singleton.ConnectedClientsList[i].ClientId;
                Transform spawn = spawnPoints[i % spawnPoints.Length];
                GameObject player = Instantiate(playerPrefab, spawn.position, Quaternion.identity);
                player.GetComponent<NetworkObject>().SpawnAsPlayerObject(clientId);
            }
        }
    }
}
