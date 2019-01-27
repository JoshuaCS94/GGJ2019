using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets._2D;


internal enum GameMsgType
{
	Movement = 1000, FinishedMovement, Special
}

internal class MovementMessage : MessageBase
{
	public float delta;
}

internal class SpecialMessage : MessageBase
{
	
}


public class GameManager_Server : MonoBehaviour
{
	public GameObject[] players;
	public Transform[] playersPositions;
	
	// Use this for initialization
	private void Start()
	{
		NetworkServer.RegisterHandler((short)GameMsgType.Movement, MovementHandler);
		NetworkServer.RegisterHandler((short)GameMsgType.FinishedMovement, FinishedMovementHandler);
		NetworkServer.RegisterHandler((short)GameMsgType.Special, BurstHandler);
	}

	private void FinishedMovementHandler(NetworkMessage netMsg)
	{
		var player = netMsg.conn.playerControllers[0].gameObject.GetComponentInChildren<Platformer2DUserControl>();
//
//		player.x = 0;
//		player.y = 0;
		player.Move(0);
	}

	private void MovementHandler(NetworkMessage netMsg)
	{
		var movMsg = netMsg.ReadMessage<MovementMessage>();

		var player = netMsg.conn.playerControllers[0].gameObject.GetComponentInChildren<Platformer2DUserControl>();

		player.Move(movMsg.delta);
	}

	private void BurstHandler(NetworkMessage netMsg)
	{
//		var burstMsg = netMsg.ReadMessage<SpecialMessage>();
//
//		var player = netMsg.conn.playerControllers[0].gameObject.GetComponentInChildren<PlayerBurst>();
//
//		player.Burst(burstMsg.keyCode);
	}

	public GameObject AddPlayer(int index)
	{
		return Instantiate(players[index], playersPositions[index]);
	}
}
