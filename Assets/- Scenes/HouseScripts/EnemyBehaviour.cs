using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Runtime.InteropServices;
using Random = System.Random;

public class EnemyBehaviour : MonoBehaviour
{

    public float move_factor = 3;
    public float speed = 0.1f;
    public int hit_points = 3;
    public bool all_random = false;
    public float hike_duration = 3;
    public bool random_mov = false;
    
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

    public void ChangeDir()
    {
        direction = !direction;
    }

    public void ChangeDir(bool dir)
    {
        direction = dir;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<LevelManager>();
        ReleaseEnemy();
        if (!all_random)
        {
            StartCoroutine("SemiRandomMovement");
        }
    }
    
    
    // Update is called once per frame
    void Update()
    {
        if (direction)
        {
            transform.DORotate(Vector3.zero, .1f);
        }
        else
        {
            transform.DORotate(new Vector3(0, 180, 0), .1f);
        }
        
        if (released)
        {
            DOTween.Kill(movement_tweener.id);
            if (random_mov)
            {
                var possibilities = new List<bool>() {direction, direction, direction, direction, direction, direction, direction, direction, direction, direction, direction, direction, direction, direction, direction, direction, direction, direction, direction, direction, direction, direction, direction, direction, direction, direction, direction, direction, direction, direction, !direction};
                bool step = possibilities[rnd.Next(possibilities.Count)];
                direction = step;
            }
            print(direction);
            if (direction)
            {
                movement_tweener = transform.DOMoveX(transform.position.x + move_factor, speed);
            }
            else
            {
                movement_tweener = transform.DOMoveX(transform.position.x - move_factor, speed);
            }
            
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

    public IEnumerator SemiRandomMovement()
    {
        while (true)
        {
            ChangeDir();
            yield return new WaitForSeconds(hike_duration);
        }
    }
}    
