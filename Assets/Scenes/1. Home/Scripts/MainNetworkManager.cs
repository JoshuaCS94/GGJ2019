using System;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using Debug = UnityEngine.Debug;


public class PlayerCreationMsg : MessageBase
{
    public int prefabId;
}


public class MainNetworkManager : NetworkManager
{

    private int m_currentPrefabId = 0;

    public GameManager_Server gameManager;

    // Methods

    public override void OnServerSceneChanged(string sceneName)
    {
        Debug.Log("Tamos");
        SceneManager.LoadScene("Main", LoadSceneMode.Single);
//        SceneManager.LoadScene("Battlefield 1", LoadSceneMode.Additive);
    }

    public override void OnClientSceneChanged(NetworkConnection conn)
    {
        SceneManager.LoadScene("Client", LoadSceneMode.Additive);

//        // TODO: This must not be loaded on a Host
//        SceneManager.LoadScene("Battlefield 1", LoadSceneMode.Additive);
    }

    public void OnSelectPrefabId(int prefabId)
    {
        m_currentPrefabId = prefabId;
    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        ClientScene.Ready(conn);

        if (!autoCreatePlayer)
            return;

        ClientScene.AddPlayer(conn, 0, new PlayerCreationMsg
        {
            prefabId = m_currentPrefabId
        });
        Debug.Log("Tamos aqui!!");
    }

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId,
        NetworkReader extraMessageReader)
    {
//        if (!playerPrefab)
//        {
//            if (!LogFilter.logError)
//                return;
//            Debug.LogError("The PlayerPrefab is empty on the NetworkManager. Please setup a PlayerPrefab object.");
//        }
//        else if (!playerPrefab.GetComponent<NetworkIdentity>())
//        {
//            if (!LogFilter.logError)
//                return;
//            Debug.LogError("The PlayerPrefab does not have a NetworkIdentity. Please add a NetworkIdentity to the player prefab.");
//        }
//        else if (playerControllerId < conn.playerControllers.Count &&
//                 conn.playerControllers[playerControllerId].IsValid &&
//                 conn.playerControllers[playerControllerId].gameObject != null)
//        {
//            if (!LogFilter.logError)
//                return;
//            Debug.LogError("There is already a player at that playerControllerId for this connections.");
//        }
//        else
//        {
//            var startPosition = GetStartPosition();
        var playerMsg = extraMessageReader.ReadMessage<PlayerCreationMsg>();

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager_Server>();
        
        NetworkServer.AddPlayerForConnection(conn, gameManager.AddPlayer(playerMsg.prefabId), playerControllerId);
//            var player = startPosition
//                ? Instantiate(playerPrefab, startPosition.position, startPosition.rotation)
//                : Instantiate(playerPrefab, playerPrefab.transform.position, Quaternion.identity);
//
//            player.name = playerMsg.name;

//            var playerData = player.GetComponent<PlayerData>();

//            playerData.Name = playerMsg.name;
//            playerData.Color = playerMsg.color;
//            playerData.BodyPath = playerMsg.bodyBase;
//            playerData.CorePath = playerMsg.bodyCore;

//            NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);

//            // Add players
//            var gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
//
//            var team1 = gameManager.team1;
//            var team2 = gameManager.team2;
//
//            (m_currentTeam % 2 == 0 ? team1 : team2).AddPlayer(player);
//
//            m_currentTeam++;
    }

    public new void StartClient()
    {
        base.StartClient();
    }

    public new void StartServer()
    {
        base.StartServer();
    }

    public void ChangeIP(string IP)
    {
        networkAddress = IP;
    }

    public void ChangePort(string port)
    {
        networkPort = int.Parse(port);
    }
}
