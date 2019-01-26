using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManger : MonoBehaviour
{
    public bool open = false;
    public int noknok_count = 10;
    public int max_noknok = 10;

    public Sprite Close;
    public Sprite Broken;
    
    private Sprite image;

    private BoxCollider2D col;
    
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Sprite>();
        col = GetComponent<BoxCollider2D>();
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


    public void Open()
    {
        
    }

    public void Break()
    {
        
    }
}
