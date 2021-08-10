using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepsEmitter : MonoBehaviour
{

    public GameObject footstepPrefab;
    GameObject parentInHierarchy, footstep;
    MovingUnit unit;
    float rotateAngle;
    List<GameObject> footstepsBuffer = new List<GameObject>();

    AudioClip[] footstepsAudio;
    AudioSource audioSource;

    Vector3 lastPosition;

    void Start()
    {
        parentInHierarchy = GameObject.Find("Footsteps");
        unit = gameObject.GetComponent<MovingUnit>();
        footstepsAudio = Resources.LoadAll<AudioClip>("Footstep(Snow and Grass)");
        audioSource = GetComponent<AudioSource>();

    }

    public void EmitIdleFootsteps()
    {
        if (transform.position != lastPosition)
        {
            EmitFootstepLeft();
            EmitFootstepRight();
        }
    }

    GameObject EmitFootstep()
    {
        rotateAngle = Vector2.SignedAngle(Vector2.right, unit.LastMoveDirection) + Random.Range(-5f, 5f);
        if (footstepsBuffer.Count < 10)
        {
            footstep = Instantiate(footstepPrefab, transform.position, Quaternion.Euler(45, 0, rotateAngle), parentInHierarchy.transform);
        }
        else
        {
            footstep = footstepsBuffer[0];
            footstepsBuffer.RemoveAt(0);
            footstep.transform.rotation = Quaternion.Euler(45, 0, rotateAngle);
            footstep.transform.position = transform.position;
        }
        footstepsBuffer.Add(footstep);

        audioSource.clip = footstepsAudio[Random.Range(0, 29)];
        audioSource.Play();

        lastPosition = footstep.transform.position;

        return footstep;
    }

    public void EmitFootstepRight()
    {
        EmitFootstep().GetComponent<SpriteRenderer>().flipY = false;
    }

    public void EmitFootstepLeft()
    {
        EmitFootstep().GetComponent<SpriteRenderer>().flipY = true;
    }
}
