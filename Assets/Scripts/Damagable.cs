using System;
using UnityEngine;
using System.Collections;


public class Damagable : MonoBehaviour
{
    public float Healthpoints = 10;

    public void TakeDamage(int damage)
    {
        if (Healthpoints > 0)
        {
            Healthpoints -= damage;
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            StartCoroutine(ChangeColor());
        }
        if (Healthpoints == 0)
        {
            Die();
        }
    }

    IEnumerator ChangeColor()
    {
        yield return new WaitForSeconds(1);
        gameObject.GetComponent<SpriteRenderer>().color = Color.black;
    }

    public virtual void Die()
    {
        gameObject.SetActive(false);
    }
}