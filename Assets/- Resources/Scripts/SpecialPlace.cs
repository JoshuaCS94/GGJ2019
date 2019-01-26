using System;
using DG.Tweening;
using UnityEngine;

public class SpecialPlace : MonoBehaviour
{
    public Transform camera_placement;
    public float in_duration;
    public float out_duration;
    private Tweener tracked_tweener;
    public bool active;
    public Transform tracked;
    public Tweener camera_motion;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("TriggerEnter");
        if (!active && other.CompareTag("Player"))
        {
            tracked = other.transform.GetChild(5);
            tracked.parent = null;
            camera_motion.Kill();
            camera_motion = tracked.DOMove(camera_placement.position, in_duration);
            active = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("TriggerExit");
        if (active && other.CompareTag("Player"))
        {
            camera_motion.Kill();
            this.tracked.parent = other.transform;
            camera_motion = tracked.DOLocalMove(new Vector3(0,1,0), out_duration);
            active = false;
        }
    }
}