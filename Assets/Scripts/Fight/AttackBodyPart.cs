using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AttackBodyPart : MonoBehaviour, IPointerDownHandler
{
    public bool attacked = false;
    public string bodyPart = "head";

    GameObject player;
    PlayerController playerController;

    void Start()
    {
        player = GameObject.Find("Player");
        if (player != null)
        {
            playerController = player.GetComponent<PlayerController>();
        }
        else
        {
            throw new System.Exception("Can't find Player Game Object in scene!");
        }
    }

    void Update()
    {

    }

    public void OnPointerDown(PointerEventData data)
    {
        if (!attacked)
        {
            if (data.hovered.Count > 0)
            {
                attacked = true;
                StartCoroutine(Cooldown());
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
                playerController.attackFromUI = true;
                playerController.bodyPartForAttackFromUI = bodyPart;
            }
        }
    }

    IEnumerator Cooldown()
    {
        Debug.Log(bodyPart);
        yield return new WaitForSeconds(100 / playerController.BaseAttackSpeed);
        attacked = false;
    }
}
