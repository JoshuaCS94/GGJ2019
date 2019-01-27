using UnityEngine;

public class ControlHandler_Standalone : MonoBehaviour, IControlHandler
{
    public float Movement { get; set; }
    public bool Special { get; set; }

    private void Update()
    {
        HandleMovement();
        HandleSpecial();
    }

    private void HandleMovement()
    {
        Movement = Input.GetAxisRaw("Horizontal");
    }

    private void HandleSpecial()
    {
        Special = Input.GetKeyDown(KeyCode.Space);
    }
}
