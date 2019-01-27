using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

public class GameManager_Client : MonoBehaviour
{
	public IControlHandler m_controlHandler;
	private NetworkManager m_networkManager;

	// Use this for initialization
	private void Start ()
	{
		m_networkManager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
		m_controlHandler = GetComponent<IControlHandler>();
	}

	// Update is called once per frame
	private void Update()
	{
		ManageInput();
	}

	private void ManageInput()
	{
		if (!Mathf.Approximately(m_controlHandler.Movement, 0))
			SendMovement(m_controlHandler.Movement);
		else
			SendFinishedMovement();

		if (m_controlHandler.Special)
			SendSpecial();
	}

	private void SendMovement(float x)
	{
		m_networkManager.client.Send((short) GameMsgType.Movement,
			new MovementMessage {delta = x});
	}

	private void SendFinishedMovement()
	{
		m_networkManager.client.Send((short ) GameMsgType.FinishedMovement,
			new EmptyMessage());
	}

	private void SendSpecial()
	{
		m_networkManager.client.Send((short) GameMsgType.Special,
			new SpecialMessage());
	}
}
