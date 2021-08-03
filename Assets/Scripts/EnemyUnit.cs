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

    public bool ProvokedByPlayer = false;
    public bool ProvokedByEnemy = false;
    GameObject ProvokatingEnemy;
    Coroutine trackingCoroutine;


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
        ProvokatingEnemy = gameObject;
    }

    void FixedUpdate()
    {
        if (ProvokedByPlayer)
        {
            MoveNearTo(playerGO.transform.position);
            distanceToPlayer = Vector2.Distance(playerGO.transform.position, transform.position);
            if (distanceToPlayer < 0.7f)
            {
                MeleeAttack(playerUnit);
            }
        }
        else if (ProvokedByEnemy)
        {
            MoveNearTo(ProvokatingEnemy.transform.position);
        }
    }


     // The EnemyUnit may be provoked by Player and by other Enemies. If it's ProvokedByPlayer it can't be ProvokedByEnemy. And will go strictly to the Player
    public void OnTriggerStay2D(Collider2D collider)
    {
        if (!collider.isTrigger)
        {
            if (ProvokedByPlayer)
            {

            }
            else
            {
                if (collider.gameObject.tag == "Player")
                {
                    ProvokedByPlayer = true;
                }
                else
                {
                    if (ProvokedByEnemy)
                    {
                        ProvokatingEnemy = collider.gameObject;
                    }
                    else
                    {
                        if (collider.tag == "Enemy")
                        {
                            if (collider.gameObject.GetComponent<EnemyUnit>().ProvokedByPlayer || collider.gameObject.GetComponent<EnemyUnit>().ProvokedByEnemy)
                            {
                                ProvokedByEnemy = true;
                            }
                        }
                    }
                }
            }
        }
    }

    public void OnTriggerExit2D(Collider2D collider)
    {
        if (!collider.isTrigger)
        {
            if (ProvokedByPlayer == true)
            {
                if (collider.gameObject.tag == "Player")
                {
                    StartCoroutine(PlayerTracking());
                }
            }
            else
            {
                if (collider.gameObject.tag == "Enemy")
                {
                    if (ProvokedByEnemy)
                    {
                        ProvokedByEnemy = false;
                    }
                }
            }
        }
    }

    IEnumerator PlayerTracking()
    {
        yield return new WaitForSeconds(2);
        ProvokedByPlayer = false;
    }

    public void IdleBehaviour()
    {

    }

    public void BehaviourWhenSeePlayer()
    {
        //Persuit(playerUnit);
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
