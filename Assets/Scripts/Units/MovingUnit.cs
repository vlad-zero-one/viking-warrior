using UnityEngine;

public class MovingUnit : Damagable
{
    public int Speed = 1;
    public Direction Direction = Direction.SE;
    public Vector2 LastMoveDirection = new Vector2(1, -1).normalized;

    public void Move(Vector2 moveDirection)
    {
        if (moveDirection != Vector2.zero)
        {
            moveDirection = moveDirection.normalized;
            gameObject.transform.Translate(moveDirection* Speed * Time.deltaTime);
            Direction = GetDirection(moveDirection);
            LastMoveDirection = moveDirection;
        }
    }

    // move closely to the point
    public void MoveNearTo(Vector3 finishPoint, float bufferArea = 0.5f)
    {
        if(Vector3.Magnitude(finishPoint - transform.position) > bufferArea)
        {
            Move(finishPoint - transform.position);
        }
    }

    public Direction GetDirection(Vector2 moveDirection)
    {
        Direction returnDirection = Direction.SE;
        // getting the angle between X axis and move direction, about 45 degrees for each direction
        float angle = Vector2.SignedAngle(moveDirection.normalized, Vector2.right);
        if (moveDirection.normalized != Vector2.zero)
        {
            if (angle > 0)
            {
                if (angle < 23) returnDirection = Direction.E;
                else if (angle < 68) returnDirection = Direction.SE;
                else if (angle < 113) returnDirection = Direction.S;
                else if (angle < 158) returnDirection = Direction.SW;
                else returnDirection = Direction.W;
            }
            else
            {
                if (angle > -23) returnDirection = Direction.E;
                else if (angle > -68) returnDirection = Direction.NE;
                else if (angle > -113) returnDirection = Direction.N;
                else if (angle > -158) returnDirection = Direction.NW;
                else returnDirection = Direction.W;
            }
        }
        return returnDirection;
    }
}
