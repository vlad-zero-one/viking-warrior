using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerEquipmentManager : MonoBehaviour, IPointerClickHandler//, IPointerDownHandler, IPointerUpHandler
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
        //Transform slot;

        foreach (var equiped in player.GetComponent<PlayerUnit>().EquippedItems)
        {
            //slot = playerEquipment.Find(equiped.Bodypart + "Item");
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

    // Update is called once per frame
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
    /*
    public void OnPointerDown(PointerEventData data)
    {
        clickStartedTime = Time.fixedTime;

        if (data.hovered.Count > 0)
        {
            pressedSlot = data.hovered[0];
            if (occupiedSlots.ContainsKey(pressedSlot.name))
            {
                if (writeItemInfo != null)
                {
                    StopCoroutine(writeItemInfo);
                }
                apearingText = occupiedSlots[pressedSlot.name];
                writeItemInfo = StartCoroutine(PrintItemInfo());
            }
        }
    }

    IEnumerator PrintItemInfo()
    {
        yield return new WaitForSeconds(1);
        var textComp = pressedSlot.transform.GetChild(0).gameObject;
        textComp.SetActive(true);
        textComp.GetComponent<Text>().text = apearingText;

    }

    public void OnPointerUp(PointerEventData data)
    {
        if (Time.fixedTime - clickStartedTime > 2f)
        {
            if (data.hovered.Count > 0)
            {
                pressedSlot = data.hovered[0];
                if (occupiedSlots.ContainsKey(pressedSlot.name))
                {
                    if (writeItemInfo != null)
                    {
                        StopCoroutine(writeItemInfo);
                    }
                    foreach (var item in player.GetComponent<PlayerUnit>().EquippedItems)
                    {
                        if (item.Bodypart + "Item" == pressedSlot.name)
                        {
                            player.GetComponent<PlayerUnit>().EquippedItems.Remove(item);
                            break;
                        }
                    }
                }
            }
        }
    }
    */
}
