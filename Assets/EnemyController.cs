using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public Enemy damagable;

    // Start is called before the first frame update
    void Start()
    {
        Enemy damagable = new Enemy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public class Enemy : AttackingUnit
{
    public int Level { get; set; } = 1;

    public Enemy(GameObject enemyGameObject,
        int healthpoints = 10,
        int speed = 1,
        string direction = "SE",
        int attackAngle = 90,
        int sightAngle = 180,
        int baseAttack = 1,
        int baseAttackSpeed = 100,
        int _level = 1) : base(enemyGameObject, healthpoints, speed, direction, attackAngle, sightAngle, baseAttack, baseAttackSpeed)
    {
        Level = _level;
    }

    new public void Die()
    {
        Destroy(UnitGameObject);
    }
}
