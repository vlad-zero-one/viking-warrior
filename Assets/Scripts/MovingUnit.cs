using UnityEngine;
using System.Collections;

public enum DirEnum
{
    SE,
    S,
    SW,
    W,
    NW,
    N,
    NE,
    E
}

public class MovingUnit : Damagable
{
    public int Speed = 1;
    public string Direction = DirEnum.SE.ToString();
    protected Animator _animator;

    public Vector2 LastMoveDirection = new Vector2(1, -1).normalized;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void Move(Vector2 moveDirection)
    {
        gameObject.transform.Translate(moveDirection.normalized * Speed * Time.deltaTime);
        //Debug.Log(gameObject.name + " SPEED: " + Speed + " DELTA: " + Time.deltaTime);
        Direction = GetDirection(moveDirection.normalized).ToString();
        //_animator.SetInteger("intDirection", (int)GetDirection(moveDirection.normalized));
        //_animator.SetBool("isMoving", true);
        LastMoveDirection = moveDirection.normalized;
        _animator.Play("Moving" + Direction);
        _animator.speed = Speed * 0.5f;
    }

    // move closely to the point
    public void MoveNearTo(Vector3 finishPoint, float bufferArea = 0.5f)
    {
        if(Vector3.Magnitude(finishPoint - transform.position) > bufferArea)
        {
            Move(finishPoint - transform.position);
        }
    }

    public DirEnum GetDirection(Vector2 moveDirection)
    {
        DirEnum returnDirection = DirEnum.SE;

        // getting the angle between X axis and move direction, about 45 degrees for each direction
        float angle = Vector2.SignedAngle(moveDirection.normalized, Vector2.right);

        if (moveDirection.normalized != Vector2.zero)
        {
            if (angle > 0)
            {
                if (angle < 23) returnDirection = DirEnum.E;
                else if (angle < 68) returnDirection = DirEnum.SE;
                else if (angle < 113) returnDirection = DirEnum.S;
                else if (angle < 158) returnDirection = DirEnum.SW;
                else returnDirection = DirEnum.W;
            }
            else
            {
                if (angle > -23) returnDirection = DirEnum.E;
                else if (angle > -68) returnDirection = DirEnum.NE;
                else if (angle > -113) returnDirection = DirEnum.N;
                else if (angle > -158) returnDirection = DirEnum.NW;
                else returnDirection = DirEnum.W;
            }
        }

        return returnDirection;
    }

}
