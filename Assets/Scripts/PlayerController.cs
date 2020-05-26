using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody Rigidbody;
    private bool[] PossibleMoveDirections = { true, true, true, true };  //up, right, down, left
    private int MoveDirection;
    private Vector3 velocity;


    public float Speed = 10f;

    void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
        CheckNearbyWalls();
    }

    void Update()
    {
        if(Rigidbody.velocity.magnitude > 0)
        {
            AddVelocity();
            return;
        }

        float verticalInputValue = Input.GetAxis("Vertical");
        float horizontalInputValue = Input.GetAxis("Horizontal");
        if (verticalInputValue != 0)
        {
            velocity = transform.forward * Speed;
            MoveDirection = 0;
            if (verticalInputValue < 0)
            {
                velocity *= -1;
                MoveDirection = 2;
            }
            AddVelocity();
        }
        else if (horizontalInputValue != 0)
        {
            velocity = transform.right * Speed;
            MoveDirection = 1;
            if (horizontalInputValue < 0)
            {
                velocity *= -1;
                MoveDirection = 3;
            }
            AddVelocity();
        }
    }

    private void AddVelocity()
    {
        if (PossibleMoveDirections[MoveDirection])
        {
            Rigidbody.velocity = velocity;
        }
    }
    
    public void CheckNearbyWalls()
    {
        for (int i = 0; i < PossibleMoveDirections.Length; i++)
        {
            PossibleMoveDirections[i] = true;
        }

        Collider[] gameObjects = Physics.OverlapSphere(transform.position, 0.5f);
        foreach (Collider nearByGameObject in gameObjects)
        {
            if (nearByGameObject.tag == "Wall")
            {
                if (nearByGameObject.transform.position.z - transform.position.z == 1)
                {
                    PossibleMoveDirections[0] = false;
                }
                else if (nearByGameObject.transform.position.x - transform.position.x == 1)
                {
                    PossibleMoveDirections[1] = false;
                }
                else if (nearByGameObject.transform.position.z - transform.position.z == -1)
                {
                    PossibleMoveDirections[2] = false;
                }
                else if (nearByGameObject.transform.position.x - transform.position.x == -1)
                {
                    PossibleMoveDirections[3] = false;
                }
            }
        }
    }
}
