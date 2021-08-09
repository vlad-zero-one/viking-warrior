using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleFootsteps : MonoBehaviour
{
    ParticleSystem.MainModule psMain;
    ParticleSystem ps;
    MovingUnit unit;
    Vector2 unitDirection;
    int counter = 0;
    float angle;

    Vector2 lastPosition;

    bool plusMinusAngle;

    void Start()
    {
        psMain = GetComponent<ParticleSystem>().main;
        ps = GetComponent<ParticleSystem>();
        unit = GetComponent<MovingUnit>();
        lastPosition = transform.position;
    }

    void Update()
    {
        if (Vector2.Distance(lastPosition, transform.position) > 0.9)
        {
            angle = Vector2.SignedAngle(Vector2.up, unit.LastMoveDirection);

            if (plusMinusAngle)
            {
                //angle += 10;
                plusMinusAngle = false;
            }
            else
            {
                //angle -= 10;
                plusMinusAngle = true;
            }

            ParticleSystemRenderer pr = ps.GetComponent<ParticleSystemRenderer>();

            //Debug.Log(angle);

            // startRotation get radians not degrees!
            psMain.startRotationZ = - Mathf.Deg2Rad * angle;
            //psMain.startRotationZMultiplier = 1;

            lastPosition = transform.position;
        }


        /*
        if(counter < ps.particleCount)
        {
            angle = Vector2.SignedAngle(Vector2.right, unit.LastMoveDirection);

            psMain.startRotationZ = angle;

            Debug.Log("New one!");
            counter = ps.particleCount;
        }
        else if (counter < ps.particleCount)
        {
            counter = ps.particleCount;
        }
        */
        /*
    if(psMain.startRotationZMultiplier != -80)
    {
        //ps.startRotation3D = new Vector3(0, 0, 45);
        //var main = ps.main;
        Debug.Log("DSADDS");
        psMain.startRotationZ = -80;
    }
    */
    }
}
