using UnityEngine;
using System.Collections;
using UnityEngine.Events;


public class Damagable : MonoBehaviour
{
    public float Healthpoints
    {
        set { _healthpoints = Healthpoints; }
        get { return _healthpoints; }
    }

    [SerializeField] private float _healthpoints = 10;
    [SerializeField] private TakeDamageEvent _takeDamageEvent;
    [SerializeField] private UnityEvent _dieEvent;


    public void TakeDamage(float damage)
    {
        if (_healthpoints > 0)
        {
            _healthpoints -= damage;
            _takeDamageEvent.Invoke(damage);

            StartCoroutine(ChangeColor());
        }
        if (_healthpoints <= 0)
        {
            _dieEvent.Invoke();
            Die();
        }
    }

    public void Heal(float healPoints)
    {
        _healthpoints += healPoints;
        _takeDamageEvent.Invoke(-healPoints);
    }

    public void SetHealthpoints(float healthpointsAmount)
    {
        _takeDamageEvent.Invoke(-(healthpointsAmount - _healthpoints));
        _healthpoints = healthpointsAmount;
    }

    IEnumerator ChangeColor()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.red;
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