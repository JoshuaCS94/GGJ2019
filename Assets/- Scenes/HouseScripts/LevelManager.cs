using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public float enemy_spaw_rating = 10;
    public List<SpawnPoint> spawns;

    public int enemy_count = 0;

    public float spawn_time = 10;
    
    System.Random r = new System.Random();
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
