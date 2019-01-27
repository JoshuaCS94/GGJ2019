using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public List<GameObject> enemys;

    public bool direction;
    
    public void Spawn()
    {
        System.Random rnd = new System.Random();
        var enemy = enemys[rnd.Next(enemys.Count)];
        var enemy_obj = Instantiate(enemy, transform).GetComponent<EnemyBehaviour>();
        enemy_obj.ReleaseEnemy(direction);
    }
}
