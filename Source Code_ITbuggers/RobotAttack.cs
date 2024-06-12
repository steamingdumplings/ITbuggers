using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotAttack : MonoBehaviour
{
    public float speed = 5.0f; // Speed at which the robot moves
    public Transform player; // Reference to the player's transform
    public float stopDistance = 1.0f; // Distance at which the robot stops before hitting the player

    private bool isAttacking = false;

    void Update()
    {
        if (isAttacking)
        {
            // Move towards the player
            float step = speed * Time.deltaTime;
            if (Vector3.Distance(transform.position, player.position) > stopDistance)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.position, step);
            }
            else
            {
                Debug.Log("Robot has reached the player and is attacking!");
            }
        }
    }

    public void StartAttack()
    {
        isAttacking = true;
    }
}
