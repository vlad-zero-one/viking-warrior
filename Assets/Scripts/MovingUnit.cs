using UnityEngine;

public class MovingUnit : Damagable
{
    public int Speed = 1;
    public string Direction = "SE";
    public Vector2 LastMoveDirection = new Vector2(1, -1).normalized;

    public void Move(Vector2 moveDirection)
    {
        gameObject.transform.Translate(moveDirection.normalized * Speed * Time.deltaTime);
        //Debug.Log(gameObject.name + " SPEED: " + Speed + " DELTA: " + Time.deltaTime);
        Direction = GetDirectionStr(moveDirection.normalized);
        LastMoveDirection = moveDirection.normalized;
    }

    // move closely to the point
    public void MoveNearTo(Vector3 finishPoint, float bufferArea = 0.5f)
    {
        if(Vector3.Magnitude(finishPoint - transform.position) > bufferArea)
        {
            Move(finishPoint - transform.position);
        }
    }

    public string GetDirectionStr(Vector2 moveDirection)
    {
        string returnDirection = "SE";

        // getting the angle between X axis and move direction, about 45 degrees for each direction
        float angle = Vector2.SignedAngle(moveDirection.normalized, Vector2.right);

        if (moveDirection.normalized != Vector2.zero)
        {
            if (angle > 0)
            {
                if (angle < 23) returnDirection = "E";
                else if (angle < 68) returnDirection = "SE";
                else if (angle < 113) returnDirection = "S";
                else if (angle < 158) returnDirection = "SW";
                else returnDirection = "W";
            }
            else
            {
                if (angle > -23) returnDirection = "E";
                else if (angle > -68) returnDirection = "NE";
                else if (angle > -113) returnDirection = "N";
                else if (angle > -158) returnDirection = "NW";
                else returnDirection = "W";
            }
        }

        return returnDirection;
    }

}
