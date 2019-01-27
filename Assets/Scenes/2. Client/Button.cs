using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Button : MonoBehaviour
{
    public UnityEvent OnClickDown;
    public UnityEvent OnClickUp;

    public void OnPointerDown(PointerEventData eventData)
    {
        OnClickDown.Invoke();
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
        OnClickUp.Invoke();
    }
}
