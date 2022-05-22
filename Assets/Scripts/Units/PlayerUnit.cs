using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerUnit : EquippedUnit
{
    // UI attack control variables
    [Header("UI ATTACK CONTROLL VARIABLES"), Space]
    public bool attackFromUI = false;
    public string bodyPartForAttackFromUI;

    // experience block
    [Header("EXPERIENCE"), Space]
    public int Level = 1;
    public int AvailableSkillPoints = 0;
    public float Experience;
    public float ExperienceToTheNextLevel = 10;

    public bool Silent = true;

    // the maximum of the enemies can be attacked at the same time
    public int MaximumDamagablesToAttack = 1;

    // list of the current enemies that can be attacked at the same time
    private List<Damagable> damagablesInAttackRange = new List<Damagable>();
    private bool isAttacking;
    private Coroutine currentlyAttacking;

    [SerializeField] private ExperienceChangeEvent _experienceChangeEvent;
    [SerializeField] private UnityEvent _levelUpEvent;

    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();

        BodyItem helmet = CreateBodyItem("Head", new Item("Helmet", 120));
        var oldItem = SwapItem(new BodyItem("Head", new Item("Mask", 10)));
    }

    void Update()
    {
        Debug.DrawLine(transform.position, (transform.position + (Vector3)LastMoveDirection * 0.7f), Color.red);

        Move(TouchPadMove.moveDirection);
        AnimateMoving();
        if (damagablesInAttackRange.Count != 0)
        {
            // mouse control
            if (Input.GetMouseButton(1))
            {
                MeleeAttack(damagablesInAttackRange.ToArray());
                AnimateAttack();
                if (currentlyAttacking == null)
                {
                    currentlyAttacking = StartCoroutine(AttackingAnimation());
                }
            }
            // touch control
            if (attackFromUI)
            {
                MeleeAttack(damagablesInAttackRange.ToArray());
                AnimateAttack();
                // drop the signal flag from UI
                attackFromUI = false;
                if (currentlyAttacking == null)
                {
                    currentlyAttacking = StartCoroutine(AttackingAnimation());
                }
            }
        }
    }

    public void AnimateAttack()
    {
        if (!_animator.GetCurrentAnimatorStateInfo(0).IsName("AttackingE"))
        {
            _animator.speed = 100 / BaseAttackSpeed;
            _animator.Play("AttackingE");
        }
    }

    public void AnimateMoving()
    {
        if (!isAttacking)
        {
            if (TouchPadMove.moveDirection != Vector2.zero)
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

    IEnumerator AttackingAnimation()
    {
        isAttacking = true;
        yield return new WaitForSeconds(0.5f);
        isAttacking = false;
        currentlyAttacking = null;
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (!collider.isTrigger && collider.CompareTag("Enemy"))
        {
            Damagable damagable = collider.gameObject.GetComponent<EnemyUnit>();

            // if the AttackAngle allows
            if (IsDamagableInTheSightAngle(damagable))
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
            else
            {
                // remove it from damagablesInAttackRange if it was in this list
                if (damagablesInAttackRange.Contains(damagable))
                {
                    damagablesInAttackRange.Remove(damagable);
                }
            }
        }      
    }
 
    void OnTriggerExit2D(Collider2D collider)
    {
        if (!collider.isTrigger && collider.CompareTag("Enemy"))
        {
            Damagable damagable = collider.gameObject.GetComponent<EnemyUnit>();

            if (damagablesInAttackRange.Contains(damagable))
            {
                damagablesInAttackRange.Remove(damagable);
            }
        }
    }

    public override void Die()
    {
        DropExperience();
    }

    public void AddExperience(float experience)
    {
        Experience += experience;
        _experienceChangeEvent.Invoke(experience);
        if (Experience >= ExperienceToTheNextLevel)
        {
            Experience -= ExperienceToTheNextLevel;
            _experienceChangeEvent.Invoke(-ExperienceToTheNextLevel + Experience);
            ExperienceToTheNextLevel = ExperienceToTheNextLevel * 1.1f;
            LevelUp();
        }
    }

    private void LevelUp()
    {
        _levelUpEvent.Invoke();
        Level++;
        AvailableSkillPoints++;
    }

    public void DropExperience()
    {
        // current exp should be given to the enemies
        Experience = 0;
    }

    [System.Serializable]
    private class ExperienceChangeEvent : UnityEvent<float> { }
}