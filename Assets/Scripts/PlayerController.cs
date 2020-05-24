using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody Rigidbody;

    public float Speed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();    
    }

    // Update is called once per frame
    void Update()
    {
        float verticalInputValue = Input.GetAxis("Vertical");
        float horizontalInputValue = Input.GetAxis("Horizontal");
        if (verticalInputValue != 0)
        {
            Vector3 velocity = transform.forward * Speed;
            if (verticalInputValue < 0)
            {
                velocity *= -1;
            }
            Rigidbody.velocity = velocity;
        }
        else if ( horizontalInputValue != 0)
        {
            Vector3 velocity = transform.right * Speed;
            if (horizontalInputValue < 0)
            {
                velocity *= -1;
            }
            Rigidbody.velocity = velocity;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Wall")
        {
            //
        }
    }
}
