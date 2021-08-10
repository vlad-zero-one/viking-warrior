using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchPadMove : MonoBehaviour, IDragHandler, IEndDragHandler
{
    Transform joystickHolderTr, joystickTr;
    Vector2 defaultPosition;
    int range;
    public static Vector2 moveDirection;

    // Start is called before the first frame update
    void Start()
    {
        joystickHolderTr = transform.Find("JoystickHolder");
        joystickTr = joystickHolderTr.Find("Joystick");
        // getting maximum range of the joystick movement
        range = (int) (joystickHolderTr.gameObject.GetComponent<RectTransform>().rect.width / 2 - joystickTr.gameObject.GetComponent<RectTransform>().rect.width / 2);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            moveDirection = Vector2.up;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            moveDirection = Vector2.left;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            moveDirection = Vector2.down;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            moveDirection = Vector2.right;
        }
        else
        {
        }
    }

    public void OnDrag(PointerEventData data)
    {
        //defaultPosition = new Vector2(joystickHolderTr.position.x, joystickHolderTr.position.y);

        if (data.delta.magnitude > 0.1f)
        {
            joystickHolderTr.position = data.pressPosition;
            if ((data.position - data.pressPosition).magnitude < range)
            {
                joystickTr.position = data.position;
                moveDirection = new Vector2((data.position.x - data.pressPosition.x) / range, (data.position.y - data.pressPosition.y) / range);
            }
            else
            {
                float deltaX = data.position.x - data.pressPosition.x;
                float deltaY = data.position.y - data.pressPosition.y;
                //Ищем точку на тригонометрической окружности относительно смещения
                //И чтоб была на краю нашего Range
                Vector2 state = Vector2.ClampMagnitude(new Vector2(deltaX, deltaY), range);
                //перемещаем джойстик в эту точку
                joystickTr.position = state + data.pressPosition;
                moveDirection = new Vector2(state.x / range, state.y / range);
            }
            //Debug.Log(direction.ToString());
        }
    }

    public void OnEndDrag(PointerEventData data)
    {
        joystickHolderTr.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        joystickTr.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        moveDirection = Vector2.zero;
    }
}
