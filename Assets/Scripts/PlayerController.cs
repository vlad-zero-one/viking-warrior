using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int speed;
    public Vector3 targetMove;

    public static Player player;

    void Start()
    {
        player = new Player(100, speed, "SE", transform.gameObject, 1);
    }

    void Update()
    {
        //transform.position = new Vector3(Mathf.Clamp(transform.position.x, 0, Screen.width), transform.position.y, transform.position.z);
        //transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, 0, Screen.height), transform.position.z);

        //targetMove = AndroidIosInput.GetJoystickValue("Joystick");

        /*
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, 0, Screen.width),
            Mathf.Clamp(transform.position.y, 0, Screen.height),
            transform.position.z);
            */

        //transform.Translate(targetMove * speed * Time.deltaTime);
        if (TouchPadMove.moveDirection != Vector2.zero)
        {
            player.Move(TouchPadMove.moveDirection);
        }
        
    }

    void OnTriggerStay2D(Collider2D col)
    {
        /*
        Vector2 toCol = col.transform.position - transform.position;

        float angleToCol = Vector2.Angle(player.LastMoveDirection, toCol);

        // if enemy is in the attack angle, player may hit him
        if (angleToCol <= player.AttackAngle / 2)
        {
            if (Input.GetMouseButton(1))
            {
                col.gameObject.SetActive(false);
            }
        }
        */

        if (Input.GetMouseButton(1))
        {
            player.MeleeAttack(col.gameObject.GetComponent<EnemyController>().damagable);
        }
    }
}

/*
public class Player
{
    public int health;
    public int level;
    public int speed;
    public string direction = "SE";
    public Vector2 lastMoveDirection = new Vector2(1, -1);
    public int attackAngle = 90;
    public int sightAngle;
    public int baseAttackSpeed = 100;


    public GameObject playerGO;


    public Player(int _health, int _level, int _speed, GameObject _playerGO)
    {
        health = _health;
        level = _level;
        speed = _speed;
        playerGO = _playerGO;
    }

    public void TakeDamage(int damage)
    {
        if (health > 0)
        {
            health -= damage;
        }
        else
        {
            Die();
        }
    }

    public void Move(Vector2 moveDirection)
    {
        playerGO.transform.Translate(moveDirection * speed * Time.deltaTime);
        direction = GetDirectionStr(moveDirection);
        lastMoveDirection = moveDirection;
    }

    public void Die()
    {

    }

    public string GetDirectionStr(Vector2 moveDirection)
    {
        string returnDirection = "SE";

        // getting the angle between X axis and move direction, about 45 degrees for each direction
        float angle = Vector2.SignedAngle(moveDirection, Vector2.right);

        if (moveDirection != Vector2.zero)
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
*/

public class Player : AttackingUnit
{
    public int Level { get; set; } = 1;

    public Player(int healthpoints, int speed, string direction, GameObject playerGameObject, int _level) : base(playerGameObject)
    {
        Level = _level;
    }

    new public void Die()
    {

    }
}