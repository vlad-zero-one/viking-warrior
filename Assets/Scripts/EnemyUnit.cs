using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnit : AttackingUnit
{
    public int Level = 1;
    public int SightRange = 5;
    public int HearingRange = 10;
    public string Atitude = "hostile";
    GameObject playerGO;
    PlayerUnit playerUnit;
    float distanceToPlayer;
    // variable for moving
    Vector3 lastSeenDamagablePosition;


    public override void Die()
    {
        Destroy(gameObject);
    }

    void Start()
    {
        //Healthpoints = 3;
        playerGO = GameObject.FindGameObjectWithTag("Player");
        playerUnit = playerGO.GetComponent<PlayerUnit>();
        lastSeenDamagablePosition = transform.position;
    }

    void Update()
    {
        distanceToPlayer = Vector3.Magnitude(transform.position - playerGO.transform.position);
        if (playerUnit.ProvokedEnemies.Count != 0)
        {
            foreach(EnemyUnit provoked in playerUnit.ProvokedEnemies)
            {
                if (Vector3.Magnitude(transform.position - provoked.transform.position) < HearingRange)
                {
                    // ПРОВОКАЦИЯ !!!!!!!!!!
                    break;
                }
            }
        }
        if (IsDamagableSeen(playerUnit))
        {
            // ПРОВОКАЦИЯ !!!!!!!!!!!
        }
        else
        {
            // CHILL
        }

        //BehaviourWhenSeePlayer();
    }

    public void Persuit(Damagable damagable)
    {
        // if EnemuUnit sees whom he need to persuit
        if (IsDamagableSeen(damagable))
        {
            lastSeenDamagablePosition = damagable.transform.position;
            MoveNearTo(lastSeenDamagablePosition);
        }
        else
        {
            MoveNearTo(lastSeenDamagablePosition);
        }
    }

    public void IdleBehaviour()
    {

    }

    public void BehaviourWhenSeePlayer()
    {
        Persuit(playerUnit);
    }

    public void Patroling( )
    {

    }

    public void StandStill()
    {

    }

    public bool IsDamagableSeen(Damagable damagable)
    {
        if (IsDamagableInTheSightAngle(damagable))
        {
            if (Vector3.Magnitude(damagable.gameObject.transform.position - gameObject.transform.position) < SightRange)
            {
                //Debug.Log(Vector3.Magnitude(damagable.gameObject.transform.position - gameObject.transform.position));
                return true;
            }
        }
        return false;
    }

}
