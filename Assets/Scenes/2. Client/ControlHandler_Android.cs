using UnityEngine;

public class ControlHandler_Android : MonoBehaviour, IControlHandler
{
    public Vector2 Movement { get; set; }
    public KeyCode Special { get; set; }
}
