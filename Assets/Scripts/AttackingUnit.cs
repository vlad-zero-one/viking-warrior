using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackingUnit : MovingUnit
{
    public int AttackAngle = 90;
    public int SightAngle = 180;
    public int BaseAttack = 1;
    public int BaseAttackSpeed = 100;
    bool attackIsInCooldown = false;

    public void MeleeAttack(params Damagable[] attackReceivers)
    {
        //Debug.Log(attackIsInCooldown);
        if (!attackIsInCooldown)
        {
            attackIsInCooldown = true;
            StartCoroutine(AttackCooldown(BaseAttackSpeed));

            foreach (Damagable damagable in attackReceivers)
            {
                if (IsDamagableInTheSightAngle(damagable))
                {
                    damagable.TakeDamage(BaseAttack);
                }
            }
        }
    }

    // the function define possibility of attack BASED on AttackAngle of AttackingUnit
    public bool IsDamagableInTheSightAngle(Damagable damagable)
    {
        Vector2 vecToDamagable = damagable.gameObject.transform.position - gameObject.transform.position;

        float angleToCol = Vector2.Angle(LastMoveDirection, vecToDamagable);
        //Debug.Log(angleToCol);

        // if damagable is in the attack angle, damager may hit him
        if (angleToCol <= AttackAngle / 2)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    IEnumerator AttackCooldown(int attackSpeed)
    {
        yield return new WaitForSeconds(100 / attackSpeed);
        attackIsInCooldown = false;
    }
}
