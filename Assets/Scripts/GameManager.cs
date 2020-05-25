using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Rigidbody Rigidbody;
    private PlayerController PlayerController;
    private ColorManager ColorManager;

    void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
        PlayerController = GetComponent<PlayerController>();
        ColorManager = GetComponent<ColorManager>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Wall")
        {
            Rigidbody.velocity = Vector3.zero;
            Vector3 playerPosition = new Vector3(Mathf.RoundToInt(transform.position.x), transform.position.y, Mathf.RoundToInt(transform.position.z));
            transform.position = playerPosition;

            PlayerController.CheckNearbyWalls();
        }
        else if (collision.collider.tag == "Floor")
        {
            print("Floor");
            ColorManager.ChangeFloorColor(collision.collider.gameObject);
            collision.collider.tag = "ColoredBefore";
        }
    }
}
