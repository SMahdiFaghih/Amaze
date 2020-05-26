using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private Rigidbody Rigidbody;
    private PlayerController PlayerController;
    private ParticlesController[] ParticlesController;
    private ColorManager ColorManager;
    private int FloorCubesNum;

    void Awake()
    {
        Text Level = FindObjectOfType<Text>();
        Level.text = SceneManager.GetActiveScene().name;
        print("Current Level is: " + Level.text);
    }

    void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
        PlayerController = GetComponent<PlayerController>();
        ParticlesController = GetComponentsInChildren<ParticlesController>();
        ColorManager = GetComponent<ColorManager>();
        FloorCubesNum = GameObject.FindGameObjectsWithTag("Floor").Length;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Wall")
        {
            Rigidbody.velocity = Vector3.zero;
            Vector3 playerPosition = new Vector3(Mathf.RoundToInt(transform.position.x), transform.position.y, Mathf.RoundToInt(transform.position.z));
            transform.position = playerPosition;

            PlayerController.CheckNearbyWalls();

            ParticlesController[1].StopParticle();
            ParticlesController[0].PlayParticle();
        }
        else if (collision.collider.tag == "Floor")
        {
            ColorManager.ChangeFloorColor(collision.collider.gameObject);
            collision.collider.tag = "ColoredBefore";
            FloorCubesNum --;
            if (FloorCubesNum == 0)
            {
                LoadNextLevel();
            }
        }
        if (Rigidbody.velocity.magnitude != 0)
        {
            ParticlesController[1].SetMoveParticleRotation(Rigidbody.velocity);
            ParticlesController[1].PlayParticle();
        }
    }

    private void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
