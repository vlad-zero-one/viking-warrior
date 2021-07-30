using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AttackHead : MonoBehaviour, IPointerDownHandler
{
    GameObject player;

    private float cooldown = 1.0f;
    private bool pressed = false;
    Color defaultColor;

    void Start()
    {
        player = GameObject.Find("Player");
        defaultColor = player.GetComponent<SpriteRenderer>().color;
    }

    void Update()
    {
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!pressed)
        {
            Debug.Log("Entered OnPointerDown");
            //player.gameObject.GetComponent<Image>().color = Color.red;
            player.GetComponent<SpriteRenderer>().color = Color.red;
            pressed = true;
            StartCoroutine(Cooldown());
        }
    }

    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(cooldown);
        //player.gameObject.GetComponent<Image>().color = Color.green;
        player.GetComponent<SpriteRenderer>().color = defaultColor;
        pressed = false;
    }
}
