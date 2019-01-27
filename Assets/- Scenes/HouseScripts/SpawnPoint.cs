using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public List<GameObject> enemys;

    public bool direction;
    
    public virtual void Spawn()
    {
        System.Random rnd = new System.Random();
        var enemy = enemys[rnd.Next(enemys.Count)];
        var enemy_obj = Instantiate(enemy, transform, true).GetComponent<EnemyBehaviour>();
        enemy.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        enemy_obj.ReleaseEnemy(direction);
    }
}
