using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerEquipmentManager : MonoBehaviour, IPointerClickHandler
{
    GameObject player;
    float clickStartedTime;
    Coroutine writeItemInfo;
    GameObject pressedSlot;
    Transform itemInfo;

    Dictionary<string, string> occupiedSlots = new Dictionary<string, string>();

    void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Transform playerEquipment = transform.Find("PlayerEquipment");
        itemInfo = playerEquipment.Find("ItemInfo");

        foreach (var equiped in player.GetComponent<PlayerUnit>().EquippedItems)
        {
            if (playerEquipment.Find(equiped.Bodypart + "Item"))
            {
                playerEquipment.Find(equiped.Bodypart + "Item").GetComponent<Image>().color = new Color(0.1f, 1f, 0.3f, 0.5f);
                if(!occupiedSlots.ContainsKey(equiped.Bodypart + "Item"))
                {
                    occupiedSlots.Add(equiped.Bodypart + "Item", equiped.Represent());
                }
            }
        }

    }

    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData data)
    {
        if (data.hovered.Count > 0)
        {
            if (occupiedSlots.ContainsKey(data.hovered[0].name))
            {
                if (data.clickCount == 1)
                {
                    itemInfo.GetComponent<Text>().text = occupiedSlots[data.hovered[0].name];
                }
                else if (data.clickCount == 2)
                {
                    Debug.Log("2" + (data.hovered[0].name));
                }
            }
        }
    }
}
