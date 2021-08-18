using System.Collections.Generic;
using UnityEngine;

public class FootstepsEmitter : MonoBehaviour
{

    public GameObject footstepPrefab;
    GameObject parentInHierarchy, footstep;
    MovingUnit unit;
    float rotateAngle;
    Queue<GameObject> footstepsBuffer = new Queue<GameObject>();

    AudioSource audioSource;

    void Start()
    {
        parentInHierarchy = GameObject.Find("Footsteps");
        unit = gameObject.GetComponent<MovingUnit>();
        audioSource = GetComponent<AudioSource>();
    }

    public void EmitIdleFootsteps()
    { 
        EmitFootstepLeft();
        EmitFootstepRight();    
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
            footstep = footstepsBuffer.Dequeue();
            footstep.transform.rotation = Quaternion.Euler(45, 0, rotateAngle);
            footstep.transform.position = transform.position;
        }
        footstepsBuffer.Enqueue(footstep);
        return footstep;
    }

    public void EmitFootstepRight()
    {
        if (Options.FootstepsOn)
        {
            EmitFootstep().GetComponent<SpriteRenderer>().flipY = false;
        }
        if (Options.FootstepsSoundOn)
        {
            audioSource.clip = FootstepsSound.GetRandom();
            audioSource.Play();
        }
    }

    public void EmitFootstepLeft()
    {
        if (Options.FootstepsOn)
        {
            EmitFootstep().GetComponent<SpriteRenderer>().flipY = true;
        }
        if (Options.FootstepsSoundOn)
        {
            audioSource.clip = FootstepsSound.GetRandom();
            audioSource.Play();
        }
    }
}
