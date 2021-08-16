using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnit : EquippedUnit
{
    public int Level = 1;
    public int SightRange = 5;
    public int HearingRange = 10;
    public string Atitude = "hostile";

    public bool ProvokedByPlayer = false;
    public bool ProvokedByEnemy = false;
    public bool TryFindPlayer = false;
    Vector2 findPlayerTarget;
    Vector2 lastSeenPlayerPosition;
    Vector2 lastSeenProvokedEnemyPosition;
    Coroutine pursuitEnenmyCoroutine;
    PlayerUnit playerUnit;
    Vector2 defaultPosition;
    int giveExpirienceWhenDie = 1;

    public override void Die()
    {
        Destroy(gameObject);
        playerUnit.AddExperience(giveExpirienceWhenDie * Level);
    }

    void Start()
    {
        playerUnit = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerUnit>();
        defaultPosition = transform.position;
        _animator = GetComponent<Animator>();

    }

    void FixedUpdate()
    {
        if (ProvokedByPlayer)
        {
            if (Vector2.Distance(transform.position, lastSeenPlayerPosition) > 0.7f)
            {
                MoveNearTo(lastSeenPlayerPosition, 0.7f);
            }
            if (Vector2.Distance(transform.position, playerUnit.transform.position) < 0.7f)
            {
                MeleeAttack(playerUnit);
            }
            TryFindPlayer = true;
        }
        else if (ProvokedByEnemy)
        {
            MoveNearTo(lastSeenProvokedEnemyPosition, 0.7f);
        }
        else if (TryFindPlayer)
        {
            if (findPlayerTarget == null)
            {
                findPlayerTarget = new Vector2(transform.position.x + Random.Range(-SightRange, SightRange), transform.position.y + Random.Range(-SightRange, SightRange));
            }
            if (Vector2.Distance(findPlayerTarget, transform.position) > 1f)
            {
                MoveNearTo(findPlayerTarget);
            }
            else
            {
                findPlayerTarget = new Vector2();
                TryFindPlayer = false;
            }
        }
        else
        {
            MoveNearTo(defaultPosition);
        }
    }

    public void OnTriggerStay2D(Collider2D collider)
    {
        if (!collider.isTrigger)
        {
            if (collider.gameObject.tag == "Player")
            {
                ProvokedByPlayer = true;
                ProvokedByEnemy = false;
                lastSeenPlayerPosition = collider.transform.position;
            }
            else if (collider.gameObject.tag == "Enemy" && collider.gameObject.GetComponent<EnemyUnit>().ProvokedByPlayer)
            {
                ProvokedByEnemy = true;
                lastSeenProvokedEnemyPosition = collider.transform.position;
                if (pursuitEnenmyCoroutine == null)
                {
                    pursuitEnenmyCoroutine = StartCoroutine(PursuitEnemy());
                }
            }
        }
    }

    public void OnTriggerExit2D(Collider2D collider)
    {
        if (!collider.isTrigger)
        {
            if (collider.gameObject.tag == "Player")
            {
                StartCoroutine(PursuitPlayer());
            }
            if (collider.gameObject.tag == "Enemy")
            {
                StartCoroutine(PursuitEnemy());
            }
        }
    }

    IEnumerator PursuitPlayer()
    {
        yield return new WaitForSeconds(3);
        ProvokedByPlayer = false;
    }

    IEnumerator PursuitEnemy()
    {
        yield return new WaitForSeconds(3);
        ProvokedByEnemy = false;
        pursuitEnenmyCoroutine = null;
    }


    public void IdleBehaviour()
    {

    }

    public void BehaviourWhenSeePlayer()
    {
        //Pursuit(playerUnit);
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
