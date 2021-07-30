using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Units : MonoBehaviour
{

}

public class Damagable : Units
{
    public int Healthpoints { get; set; }

    public GameObject UnitGameObject { set; get; }

    public Damagable(GameObject unitGameObject, int healthpoints = 10)
    {
        Healthpoints = healthpoints;
        UnitGameObject = unitGameObject;
    }

    public void TakeDamage(int damage)
    {
        if (Healthpoints > 0)
        {
            Healthpoints -= damage;
        }
        else
        {
            Die();
        }
    }

    public void Die()
    {

    }
}

public class BaseUnit: Damagable
{
    public int Speed { get; set; }
    public string Direction { get; set; }
    public Vector2 LastMoveDirection { get; set; } = new Vector2(1, -1);

    public BaseUnit(GameObject unitGameObject, int healthpoints = 10, int speed = 1, string direction = "SE") : base(unitGameObject, healthpoints)
    {
        Speed = speed;
        Direction = direction;
    }

    public void Move(Vector2 moveDirection)
    {
        UnitGameObject.transform.Translate(moveDirection * Speed * Time.deltaTime);
        Direction = GetDirectionStr(moveDirection);
        LastMoveDirection = moveDirection;
    }

    public string GetDirectionStr(Vector2 moveDirection)
    {
        string returnDirection = "SE";

        // getting the angle between X axis and move direction, about 45 degrees for each direction
        float angle = Vector2.SignedAngle(moveDirection, Vector2.right);

        if (moveDirection != Vector2.zero)
        {
            if (angle > 0)
            {
                if (angle < 23) returnDirection = "E";
                else if (angle < 68) returnDirection = "SE";
                else if (angle < 113) returnDirection = "S";
                else if (angle < 158) returnDirection = "SW";
                else returnDirection = "W";
            }
            else
            {
                if (angle > -23) returnDirection = "E";
                else if (angle > -68) returnDirection = "NE";
                else if (angle > -113) returnDirection = "N";
                else if (angle > -158) returnDirection = "NW";
                else returnDirection = "W";
            }
        }

        return returnDirection;
    }
}

public class AttackingUnit: BaseUnit
{
    public int AttackAngle { get; set; }
    public int SightAngle { get; set; }
    public int BaseAttack { get; set; }
    public int BaseAttackSpeed { get; set; }
    bool attackIsInCooldown = false;

    public AttackingUnit(GameObject unitGameObject,
        int healthpoint = 10,
        int speed = 1,
        string direction = "SE",
        int attackAngle = 90,
        int sightAngle = 180,
        int baseAttack = 1,
        int baseAttackSpeed = 100) : base(unitGameObject, healthpoint, speed, direction)
    {
        AttackAngle = attackAngle;
        SightAngle = sightAngle;
        BaseAttackSpeed = baseAttackSpeed;
        BaseAttackSpeed = baseAttackSpeed;
    }

    public void MeleeAttack(Damagable attackReceiver)
    {
        if (!attackIsInCooldown)
        {
            Vector2 vecToUnit = attackReceiver.UnitGameObject.transform.position - UnitGameObject.transform.position;

            float angleToCol = Vector2.Angle(LastMoveDirection, vecToUnit);

            // if damagable is in the attack angle, damager may hit him
            if (angleToCol <= AttackAngle / 2)
            {
                attackReceiver.TakeDamage(BaseAttack);
                StartCoroutine(AttackCooldown(BaseAttackSpeed));
            }

            attackIsInCooldown = true;

        }
    }

    IEnumerator AttackCooldown(int attackSpeed)
    {
        yield return new WaitForSeconds(100 / attackSpeed);
        attackIsInCooldown = false;
    }
}


