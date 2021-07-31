using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : AttackingUnit
{
    public int Level;
    // the maximum of the enemies can be attacked at the same time
    public int MaximumDamagablesToAttack = 1;
    // list of the current enemies that can be attacked at the same time
    List<Damagable> damagablesInAttackRange = new List<Damagable>();

    // UI attack control variables
    public bool attackFromUI = false;
    public string bodyPartForAttackFromUI;

    void Start()
    {
    }

    void Update()
    {
        if (TouchPadMove.moveDirection != Vector2.zero)
        {
            Move(TouchPadMove.moveDirection);
        }
        if (damagablesInAttackRange.Count != 0)
        {
            Debug.Log(damagablesInAttackRange.Count);
            // mouse control
            if (Input.GetMouseButton(1))
            {
                MeleeAttack(damagablesInAttackRange.ToArray());
            }
            // touch control
            if (attackFromUI)
            {
                MeleeAttack(damagablesInAttackRange.ToArray());
                // drop the signal flag from UI
                attackFromUI = false;
            }
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {

        Damagable damagable = col.gameObject.GetComponent<EnemyController>();

        // if the AttackAngle allows
        if (DamagableIsInRange(damagable))
        {
            // and the maximum of the enemies can be attacked at the same time is higher than current Damagables in Range
            if (damagablesInAttackRange.Count < MaximumDamagablesToAttack)
            {
                // and the Damagable not in List yet
                if (!damagablesInAttackRange.Contains(damagable))
                {
                    // add it
                    damagablesInAttackRange.Add(damagable);
                }
            }
            // but if the maximum of the enemies that can be attacked at the same time is lower than maximum we should resize to exact range
            else if (damagablesInAttackRange.Count > MaximumDamagablesToAttack)
            {
                // from the index that is also maximum, remove the-difference-between-maximum-and-current number of objects
                damagablesInAttackRange.RemoveRange(MaximumDamagablesToAttack, damagablesInAttackRange.Count - MaximumDamagablesToAttack);
            }
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        Damagable damagable = col.gameObject.GetComponent<EnemyController>();

        if (damagablesInAttackRange.Contains(damagable))
        {
            damagablesInAttackRange.Remove(damagable);
        }
    }

    public override void Die()
    {

    }
}