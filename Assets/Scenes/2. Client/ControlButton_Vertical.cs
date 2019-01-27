using UnityEngine;
using UnityEngine.EventSystems;


public class ControlButton_Vertical : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public float value;
    public KeyCode keyCode;

    private IControlHandler m_controlHandler;
    private float m_doubleTapStartTime;

    private const float DRAG_THRESHOLD = .5f;

    private void Start()
    {
        m_controlHandler = GameObject.Find("Game Manager").GetComponent<IControlHandler>();
    }

    private void LateUpdate()
    {
        m_controlHandler.Special = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (Time.time - m_doubleTapStartTime < DRAG_THRESHOLD)
        {
            m_controlHandler.Special = true;
        }

        m_controlHandler.Movement = m_controlHandler.Movement;

        m_doubleTapStartTime = Time.time;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        m_controlHandler.Movement = m_controlHandler.Movement;
    }

}
