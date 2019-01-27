using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DoorManger : MonoBehaviour
{
    public bool open = false;
    public int noknok_count = 10;
    public int max_noknok = 10;
    public bool reverse = false;

    public Sprite close;
    public Sprite broken;
    
    private SpriteRenderer image;

    private BoxCollider2D col;
    
    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<BoxCollider2D>();
        if (reverse)
        {
            transform.DORotate(new Vector3(0, 180, 0), 0.2f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NokNok()
    {
        noknok_count--;
        if (noknok_count == 0)
        {
            open = true;
            noknok_count = max_noknok;
        }
    }


    public void Fix()
    {
        col.enabled = true;
        if (reverse)
        {
            transform.DOMoveX(-3.375f, 0.2f);
        }
        else
        {
            transform.DOMoveX(3.375f, 0.2f);
        }
        GetComponent<SpriteRenderer>().sprite = close;
    }

    public void Break()
    {
        col.enabled = false;
        if (reverse)
        {
            transform.DOMoveX(3.125f, 0.2f);
        }
        else
        {
            transform.DOMoveX(-3.125f, 0.2f);
        }
        GetComponent<SpriteRenderer>().sprite = broken;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
    }
}
