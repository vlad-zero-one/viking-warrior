using System;
using UnityEngine;
using System.Collections;
using UnityEngine.Events;


public class Damagable : MonoBehaviour
{
    public float Healthpoints = 10;

    [SerializeField] private TakeDamageEvent _takeDamageEvent;
    [SerializeField] private UnityEvent _dieEvent;


    public void TakeDamage(float damage)
    {
        if (Healthpoints > 0)
        {
            Healthpoints -= damage;
            _takeDamageEvent.Invoke(damage);
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            StartCoroutine(ChangeColor());
        }
        if (Healthpoints == 0)
        {
            _dieEvent.Invoke();
            Die();
        }
    }

    public void Heal(float healPoints)
    {
        Healthpoints += healPoints;
        _takeDamageEvent.Invoke(-healPoints);
    }

    public void SetHealthpoints(float healthpoints)
    {
        _takeDamageEvent.Invoke(-(healthpoints - Healthpoints));
        Healthpoints = healthpoints;
    }

    IEnumerator ChangeColor()
    {
        yield return new WaitForSeconds(1);
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }

    public virtual void Die()
    {
        gameObject.SetActive(false);
    }

    [System.Serializable]
    private class TakeDamageEvent : UnityEvent<float> { }
}