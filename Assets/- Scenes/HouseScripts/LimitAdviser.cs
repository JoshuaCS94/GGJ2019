using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitAdviser : MonoBehaviour
{
    public bool In;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyBehaviour enemy = other.GetComponent<EnemyBehaviour>();
        if (enemy != null)
        {
            if (In)
            {
                enemy.YouAreIn();
            }
            else
            {
                enemy.YouAreOut();
            }
        }
    }
}
