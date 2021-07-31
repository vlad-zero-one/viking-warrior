using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : AttackingUnit
{
    public int Level = 1;

    new public void Die()
    {
        Destroy(gameObject);
    }

    void Start()
    {
        //Healthpoints = 3;
    }

    void Update()
    {
        
    }
}
