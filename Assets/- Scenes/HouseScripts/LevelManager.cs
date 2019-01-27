using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Mime;
using UnityEngine;
using System.Timers;
using UnityEngine.UI;
using DG.Tweening;


public class LevelManager : MonoBehaviour
{
    public float enemy_spaw_rating = 10;
    public List<SpawnPoint> spawns;
    public Stopwatch timer;
    
    public int enemy_count = 0;

    public float spawn_time = 10;

    public int max_enemies = 15;

    public Text time_text;
    public Text enemy_count_text;
    
    System.Random r = new System.Random();
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("SpawnEnemys");
        timer = new Stopwatch();
        timer.Start();
        
    }

    // Update is called once per frame
    void Update()
    {
        float time = timer.ElapsedMilliseconds / 1000f;
        time_text.DOText(time.ToString(), 0.1f);
        
        enemy_count_text.DOText(enemy_count.ToString(), 0.2f);
        //enemy_count_text.DOColor()

        if (enemy_count > max_enemies)
        {
            timer.Stop();
        }
    }
    
    public void EnemyDead()
    {
        enemy_count--;
        if (enemy_count < 0)
            enemy_count = 0;
    }

    public IEnumerator SpawnEnemys()
    {
        while (true)
        {
            var spawn = rlist(spawns);
            spawn.Spawn();
            yield return new WaitForSeconds(spawn_time);
            spawn_time /= 1.05f;
            if (spawn_time <= 0.01f)
            {
                spawn_time = 0;
            }
            spawn_time += (float) r.NextDouble();
        }
    }

    public T rlist<T>(List<T> list)
    {
        System.Random rnd = new System.Random();
        return list[rnd.Next(list.Count)];
    }
    
}
