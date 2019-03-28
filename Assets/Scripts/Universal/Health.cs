using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    public GameObject Explosion;

    public int health = 3;

    public bool isDead()
    {
        return health <= 0;
    }

    public void ModifyHealth(int HealthToAdd)
    {
        health += HealthToAdd;
    }

    public void SpawnExplosion()
    {
        Instantiate(Explosion, transform.position, Quaternion.identity);
    }

}
