using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickController : MonoBehaviour
{
    public GameObject touchMarker;

    Vector3 targetVector;

    public PlayerController playerController;

    void Start()
    {
        touchMarker.transform.position = transform.position;
    }

    void Update()
    {
        // if (Input.touchCount > 0)
        if (Input.GetMouseButton(0))
        {
            // Vector3 touchPos = Input.GetTouch(0).position;
            Vector3 touchPos = Input.mousePosition;
            targetVector = touchPos - transform.position;

            if(targetVector.magnitude < 100)
            {
                touchMarker.transform.position = touchPos;
                playerController.targetMove = targetVector;
            }
            else
            {
                touchMarker.transform.position = transform.position + (touchPos - transform.position).normalized * 100.0f;
                playerController.targetMove = touchMarker.transform.position - transform.position;
            }
        }
        else
        {
            touchMarker.transform.position = transform.position;
            playerController.targetMove = new Vector3(0, 0, 0);
        }
    }
}
