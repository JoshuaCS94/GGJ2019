using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Button : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public UnityEvent OnClickDown;
    public UnityEvent OnClickUp;

    public void OnPointerDown(PointerEventData eventData)
    {
//        Debug.Log("AAAAAAAAAAA");
        OnClickDown.Invoke();
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
//        Debug.Log("KKKKKKKKK");
        OnClickUp.Invoke();
    }
}
