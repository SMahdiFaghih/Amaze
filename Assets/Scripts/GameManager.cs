using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;

public class GameManager : MonoBehaviour
{
    private Rigidbody Rigidbody;
    private PlayerController PlayerController;
    private ParticlesController[] ParticlesController;
    private ParticleSystem VictoryParticle;
    private ColorManager ColorManager;
    private AudioSource AudioSource;
    private GameObject[] FloorCubes;
    private int NoncoloredFloorCubesNum;
    private bool LevelCompleted = false;

    private readonly float CubeRotationDelay = 0.25f;
    private readonly float CubeRotatingExecutaionDelay = 2f;
    private readonly float CubeRotationFactor = 2f;

    public Text PressToNextLevel;

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
        VictoryParticle = FindObjectOfType<ParticleSystem>();
        ColorManager = GetComponent<ColorManager>();
        AudioSource = GetComponent<AudioSource>();
        FloorCubes = GameObject.FindGameObjectsWithTag("Floor");
        NoncoloredFloorCubesNum = FloorCubes.Length;
        SetVictoryParticlePosition();
    }

    void Update()
    {
        if (LevelCompleted)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                LoadNextLevel();
            }
        }
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

            if (NoncoloredFloorCubesNum == 0)
            {
                ExecuteAfterLevelCompleteInstructions();
            }
        }
        else if (collision.collider.tag == "Floor")
        {
            ColorManager.ChangeFloorColor(collision.collider.gameObject);
            collision.collider.tag = "ColoredBefore";
            NoncoloredFloorCubesNum --;
        }
        if (Rigidbody.velocity.magnitude != 0)
        {
            ParticlesController[1].SetMoveParticleRotation(Rigidbody.velocity);
            ParticlesController[1].PlayParticle();
        }
    }

    private void ExecuteAfterLevelCompleteInstructions()
    {
        PlayerController.enabled = false;
        Rigidbody.isKinematic = true;
        GetComponent<SphereCollider>().isTrigger = true;
        PressToNextLevel.gameObject.SetActive(true);
        VictoryParticle.Play();
        AudioSource.Play();
        SortFloorCubes();
        StartCoroutine(RotateFloorCubes());
        LevelCompleted = true;
    }

    private void SortFloorCubes()
    {
        FloorCubes = FloorCubes.OrderBy(FloorCubes => FloorCubes.transform.position.x).ToArray();
    }

    private IEnumerator RotateFloorCubes()
    {
        while (true)
        {
            for (int i=0;i < FloorCubes.Length;i++)
            {
                GameObject floor = FloorCubes[i];
                StartCoroutine(RotateOneFloorCube(floor));
                if (i == FloorCubes.Length - 1 || floor.transform.position.x != FloorCubes[i+1].transform.position.x)
                {
                    yield return new WaitForSeconds(CubeRotationDelay);
                }
            }
            yield return new WaitForSeconds(CubeRotatingExecutaionDelay);
        }
    }

    private IEnumerator RotateOneFloorCube(GameObject floor)
    {
        Vector3 currentRotation = floor.transform.eulerAngles;
        Vector3 newRotation = currentRotation + new Vector3(0, 0, 90);
        float lerpFactor = 0;
        while (lerpFactor < 1.2) //so that lerpFactor will definitley reach 1 before quit loop
        {
            floor.transform.eulerAngles = Vector3.Lerp(currentRotation, newRotation, lerpFactor);
            lerpFactor += Time.deltaTime * CubeRotationFactor;
            yield return null;
        }
    }

    private void SetVictoryParticlePosition()
    {
        float axisY = 2.5f;
        Vector3 newPosition = new Vector3(PressToNextLevel.transform.position.x, axisY, PressToNextLevel.transform.position.z);
        VictoryParticle.transform.position = newPosition;
    }

    private void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
