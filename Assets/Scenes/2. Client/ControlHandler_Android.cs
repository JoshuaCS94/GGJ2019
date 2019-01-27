using System;
using UnityEngine;

public class ControlHandler_Android : MonoBehaviour, IControlHandler
{
    public float Movement { get; set; }
    public bool Special { get; set; }

    public void HandleLeft(bool down)
    {
        Debug.Log("MEMEME" + down);
        Movement = down ? -1 : 0;
    }
    
    public void HandleUp(bool down)
    {
        
    }
    
    public void HandleRight(bool down)
    {
        Movement = down ? 1 : 0;
    }
    
    public void HandleDown(bool down)
    {
        
    }
    
    public void HandelSpecial(bool down)
    {
        Special = down;
    }
}
