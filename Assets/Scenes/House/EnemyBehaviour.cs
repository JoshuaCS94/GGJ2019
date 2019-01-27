using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Runtime.InteropServices;
using Random = System.Random;

public class EnemyBehaviour : MonoBehaviour
{

    public float move_factor = 3;
    public int hit_points = 3;
    
    static System.Random rnd = new System.Random();
    LevelManager manager;

    
    [HideInInspector]
    public bool direction = false;

    private Tweener movement_tweener;
    public bool released = false;

    public void ReleaseEnemy(bool dir = true)
    {
        if (dir)
        {
            movement_tweener = transform.DOMoveX(transform.position.x + move_factor*3, 2);
        }
        else
        {
            transform.DORotate(new Vector3(0, 180, 0), .1f);
            movement_tweener = transform.DOMoveX(transform.position.x - move_factor*3, 2);
        }

        movement_tweener.OnComplete(() => { released = true; });
    }
    
    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<LevelManager>();
    }
    
    
    // Update is called once per frame
    void Update()
    {
        if (released)
        {
            var possibilities = new List<bool>() {direction, direction, direction, direction, direction, direction, direction, direction, direction, direction, direction, direction, direction, direction, direction, direction, direction, direction, direction, direction, direction, direction, direction, direction, direction, direction, direction, direction, direction, direction, !direction};
            bool step = possibilities[rnd.Next(possibilities.Count)];
            print(step);
            print(direction);
            direction = step;
            if (direction)
            {
                transform.DORotate(Vector3.zero, .1f);
                movement_tweener = transform.DOMoveX(transform.position.x + move_factor, 0.2f);
            }
            else
            {
                transform.DORotate(new Vector3(0, 180, 0), .1f);
                movement_tweener = transform.DOMoveX(transform.position.x - move_factor, 0.2f);
                

            }

            DOTween.Kill(movement_tweener.id);
        }
    }

    public virtual void Hit(bool father)
    {
        if (father)
        {
            hit_points = 0;
        }

        hit_points--;

        if (hit_points <= 0)
        {
            manager.EnemyDead();
            Destroy(gameObject,0.1f);
        }
    }
}    
