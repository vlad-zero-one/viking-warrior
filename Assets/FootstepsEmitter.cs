using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepsEmitter : MonoBehaviour
{

    public GameObject footstepPrefab;
    GameObject parentInHierarchy, footstep;
    MovingUnit unit;
    Vector2 lastPosition;
    bool flipper = true;
    float rotateAngle;

    void Start()
    {
        parentInHierarchy = GameObject.Find("Footsteps");
        unit = gameObject.GetComponent<MovingUnit>();
        lastPosition = transform.position;

        footstep = Instantiate(footstepPrefab, transform.position, Quaternion.Euler(0, 0, -45), parentInHierarchy.transform);
        StartCoroutine(DeleteFootstep(footstep));
        footstep = Instantiate(footstepPrefab, transform.position, Quaternion.Euler(0, 0, -45), parentInHierarchy.transform);
        footstep.GetComponent<SpriteRenderer>().flipY = true;
        StartCoroutine(DeleteFootstep(footstep));
    }

    void Update()
    {
        if(Vector2.Distance(lastPosition, transform.position) > 1f)
        {
            rotateAngle = Vector2.SignedAngle(Vector2.right, unit.LastMoveDirection);

            footstep = Instantiate(footstepPrefab, transform.position, Quaternion.Euler(0, 0, rotateAngle), parentInHierarchy.transform);
            if (flipper)
            {
                footstep.GetComponent<SpriteRenderer>().flipY = true;
                flipper = false;
            }
            else
            {
                flipper = true;
            }
            lastPosition = transform.position;
            StartCoroutine(DeleteFootstep(footstep));
        }
    }

    IEnumerator DeleteFootstep(GameObject footstepInstance)
    {
        yield return new WaitForSeconds(120);
        Destroy(footstepInstance);
    }

}
