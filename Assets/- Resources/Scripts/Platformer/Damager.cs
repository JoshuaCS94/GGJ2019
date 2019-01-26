using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{
    public float HitForce = 1500;
    public int Damage = 1;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            var life = other.GetComponent<CharacterLifeInteraction>();
            if (Damage > 0)
                life.GetHit(this);
            else
                life.GetLife(this);
        }
    }
}