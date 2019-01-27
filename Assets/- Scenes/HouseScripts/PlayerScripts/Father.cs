using System.Collections;
using System.Collections.Generic;
using HouseScripts.PlayerScripts;
using UnityEngine;

public class Father : Player
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Hit(EnemyBehaviour enemy, bool father = false)
    {
        base.Hit(enemy, true);
    }
}
