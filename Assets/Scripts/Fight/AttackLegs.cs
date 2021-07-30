using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AttackLegs : MonoBehaviour, IPointerDownHandler
{
    GameObject player;

    private float cooldown = 1.0f;
    private bool pressed = false;

    void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!pressed)
        {
            Debug.Log("Entered OnPointerDown");
            player.gameObject.GetComponent<Image>().color = Color.blue;
            pressed = true;
            StartCoroutine(Cooldown());
        }
    }

    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(cooldown);
        player.gameObject.GetComponent<Image>().color = Color.green;
        pressed = false;
    }
}
