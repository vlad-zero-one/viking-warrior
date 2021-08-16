using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonfire : MonoBehaviour
{
    public GameObject restButton;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(!collider.isTrigger && collider.CompareTag("Player"))
        {
            restButton.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (!collider.isTrigger && collider.CompareTag("Player"))
        {
            restButton.SetActive(false);
        }
    }
}
