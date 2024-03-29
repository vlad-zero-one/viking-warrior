﻿using System.Collections;
using UnityEngine;

public class AttackingUnit : MovingUnit
{
    public int AttackAngle = 90;
    public int SightAngle = 180;
    public int BaseAttack = 1;
    public int BaseAttackSpeed = 100;

    private bool _attackIsInCooldown = false;

    public void MeleeAttack(params Damagable[] attackReceivers)
    {
        if (!_attackIsInCooldown)
        {
            foreach (Damagable damagable in attackReceivers)
            {
                if (IsDamagableInTheSightAngle(damagable))
                {
                    damagable.TakeDamage(BaseAttack);
                    _attackIsInCooldown = true;
                    StartCoroutine(AttackCooldown(BaseAttackSpeed));
                }
            }
        }
    }

    // the function define possibility of attack BASED on AttackAngle of AttackingUnit
    public bool IsDamagableInTheSightAngle(Damagable damagable)
    {
        Vector2 vecToDamagable = damagable.gameObject.transform.position - gameObject.transform.position;
        float angleToCol = Vector2.Angle(LastMoveDirection, vecToDamagable);
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
        _attackIsInCooldown = false;
    }
}
