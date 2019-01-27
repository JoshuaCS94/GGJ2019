using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Repell_enemy : SpawnPoint
{
    public bool is_Touching => col.IsTouchingLayers(LayerMask.NameToLayer("Player"));

    private Collider2D col;
    
    private void Awake()
    {
        col = GetComponent<Collider2D>();
    }

    public override void Spawn()
    {
        if(!is_Touching)
            base.Spawn();
    }

}
