using System.Collections;
using UnityEngine;

public class EnemyUnit : EquippedUnit
{
    public int Level = 1;
    public int SightRange = 5;
    public int HearingRange = 10;
    public string Atitude = "hostile";
    public int giveExpirienceWhenDie = 1;

    public bool ProvokedByPlayer
    {
        get { return _provokedByPlayer; }
    }
    private bool _provokedByPlayer = false;
    private bool _provokedByEnemy = false;
    private bool _tryFindPlayer = false;
    private bool _goindToTheDefaultPosition = false;

    private Vector2 findPlayerTarget;
    private Vector2 lastSeenPlayerPosition;
    private Vector2 lastSeenProvokedEnemyPosition;
    private Coroutine pursuitEnenmyCoroutine;
    private PlayerUnit playerUnit;
    private Vector2 defaultPosition;

    private Animator _animator;

    [SerializeField] private GameObject _deadPrefab;

    public override void Die()
    {
        Instantiate(_deadPrefab, transform.position, Quaternion.identity, GameObject.Find("DeadEnemies").transform);
        Destroy(gameObject);
        playerUnit.AddExperience(giveExpirienceWhenDie * Level);
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        playerUnit = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerUnit>();
        defaultPosition = transform.position;
        _animator = GetComponent<Animator>();

    }

    void Update()
    {
        if (_provokedByPlayer)
        {
            if (Vector2.Distance(transform.position, lastSeenPlayerPosition) > 0.7f)
            {
                MoveNearTo(lastSeenPlayerPosition, 0.7f);
            }
            if (Vector2.Distance(transform.position, playerUnit.transform.position) <= 0.7f)
            {
                AnimateAttack();
                MeleeAttack(playerUnit);
            }
            _tryFindPlayer = true;
        }
        else if (_provokedByEnemy)
        {
            MoveNearTo(lastSeenProvokedEnemyPosition, 0.7f);
        }
        else if (_tryFindPlayer)
        {
            if (findPlayerTarget == Vector2.zero)
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
                _tryFindPlayer = false;
            }
        }
        else
        {
            if (Vector3.Distance(transform.position, defaultPosition) > 0.5f)
            {
                MoveNearTo(defaultPosition, 0.5f);
                _goindToTheDefaultPosition = true;
            }
            else
            {
                _goindToTheDefaultPosition = false;
            }
        }
        AnimateMoving();
    }

    public void AnimateAttack()
    {
        if (!_animator.GetCurrentAnimatorStateInfo(0).IsTag("BasicEnemyAttacking"))
        {
            _animator.speed = 100 / BaseAttackSpeed;
            if (Direction == Direction.N
                || Direction == Direction.NE
                || Direction == Direction.E
                || Direction == Direction.SE)
            {
                _animator.Play("AttackingE" + Random.Range(1, 3));
            }
            else
            {
                _animator.Play("AttackingW" + Random.Range(1, 3));
            }
        }
    }

    public void AnimateMoving()
    {

        if (!_animator.GetCurrentAnimatorStateInfo(0).IsTag("BasicEnemyAttacking"))
        {
            // if some of the flags is true - play moving animation
            if (_provokedByPlayer ||  _provokedByEnemy || _tryFindPlayer || _goindToTheDefaultPosition)
            {
                _animator.speed = Speed * 0.5f;
                _animator.Play("Moving" + Direction.ToString());
            }
            else
            {
                _animator.Play("Idle" + Direction.ToString());
            }
        }
    }

    public void OnTriggerStay2D(Collider2D collider)
    {
        if (!collider.isTrigger)
        {
            if (collider.gameObject.tag == "Player")
            {
                _provokedByPlayer = true;
                _provokedByEnemy = false;
                lastSeenPlayerPosition = collider.transform.position;
            }
            else if (collider.gameObject.tag == "Enemy" && collider.gameObject.GetComponent<EnemyUnit>().ProvokedByPlayer)
            {
                _provokedByEnemy = true;
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
        _provokedByPlayer = false;
    }

    IEnumerator PursuitEnemy()
    {
        yield return new WaitForSeconds(3);
        _provokedByEnemy = false;
        pursuitEnenmyCoroutine = null;
    }

    public bool IsDamagableSeen(Damagable damagable)
    {
        if (IsDamagableInTheSightAngle(damagable))
        {
            if (Vector3.Magnitude(damagable.gameObject.transform.position - gameObject.transform.position) < SightRange)
            {
                return true;
            }
        }
        return false;
    }
}
