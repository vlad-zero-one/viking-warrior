using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerUnit : EquippedUnit
{
    public int Level = 1;
    public int AvailableSkillPoints = 0;
    public float Experience;
    public float ExperienceToTheNextLevel = 10;

    public float MaximumHealthpoints = 20;

    // the maximum of the enemies can be attacked at the same time
    public int MaximumDamagablesToAttack = 1;
    // list of the current enemies that can be attacked at the same time
    List<Damagable> damagablesInAttackRange = new List<Damagable>();
    public bool Silent = true;
    bool isAttacking;
    Coroutine currentlyAttacking;

    // UI attack control variables
    public bool attackFromUI = false;
    public string bodyPartForAttackFromUI;

    [SerializeField] private ExperienceChangeEvent _experienceChangeEvent;
    //[SerializeField] private LevelUpEvent _levelUpEvent;
    [SerializeField] private UnityEvent _levelUpEvent;

    void Start()
    {
        _animator = GetComponent<Animator>();

        BodyItem helmet = CreateBodyItem("Head", new Item("Helmet", 120));

        var oldItem = SwapItem(new BodyItem("Head", new Item("Mask", 10)));
    }

    void Update()
    {

        Debug.DrawLine(transform.position, (transform.position + (Vector3)LastMoveDirection * 0.7f), Color.red);

        if (!isAttacking)
        {
            if (TouchPadMove.moveDirection != Vector2.zero)
            {
                Move(TouchPadMove.moveDirection);
            }
            else
            {
                _animator.Play("Idle" + Direction);
            }
        }
        if (damagablesInAttackRange.Count != 0)
        {
            // mouse control
            if (Input.GetMouseButton(1))
            {
                MeleeAttack(damagablesInAttackRange.ToArray());
                if (currentlyAttacking == null)
                    currentlyAttacking = StartCoroutine(AttackingAnimation());
            }
            // touch control
            if (attackFromUI)
            {
                MeleeAttack(damagablesInAttackRange.ToArray());
                // drop the signal flag from UI
                attackFromUI = false;
                if (currentlyAttacking == null)
                    currentlyAttacking = StartCoroutine(AttackingAnimation());
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
        //_levelUpEvent.Invoke();
    }

    public void DropExperience()
    {
        // current exp should be given to the enemies
        Experience = 0;
    }

    [Serializable]
    private class ExperienceChangeEvent : UnityEvent<float> { }

    //[Serializable]
    //private class LevelUpEvent : UnityEvent<float> { }
}