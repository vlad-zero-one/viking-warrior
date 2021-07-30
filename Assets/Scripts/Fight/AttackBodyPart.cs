using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AttackBodyPart : MonoBehaviour, IPointerDownHandler
{
    public bool attacked = false;
    public string bodyPart = "head";

    Player player;

    void Start()
    {
        
    }

    void Update()
    {

    }

    public void OnPointerDown(PointerEventData data)
    {
        if (player == null)
        {
            player = PlayerController.player;
        }
        if (!attacked)
        {
            attacked = true;
            if (data.hovered.Count > 0)
            {
                switch (data.hovered[0].name)
                {
                    case "AttackHead":
                        bodyPart = "head";
                        break;
                    case "AttackTorso":
                        bodyPart = "torso";
                        break;
                    case "AttackLegs":
                        bodyPart = "legs";
                        break;
                }
                StartCoroutine(Cooldown());
            }
        }
    }

    IEnumerator Cooldown()
    {
        Debug.Log(bodyPart);
        yield return new WaitForSeconds(100 / player.BaseAttackSpeed);
        attacked = false;
    }
}
