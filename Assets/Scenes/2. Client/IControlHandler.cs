using UnityEngine;

public interface IControlHandler
{
    Vector2 Movement { get; set; }
    KeyCode Special { get; set; }
}
