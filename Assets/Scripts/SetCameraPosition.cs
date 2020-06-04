using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCameraPosition : MonoBehaviour
{
    private Camera Camera;
    private GameObject[] Floors;
    private Vector3 CameraPosition;
    private float CameraYAxis = 15;
    private Quaternion CameraRotation = new Quaternion(85, 0, 0, 0);

    private float MinSize = 20;
    private float xMax = 0;
    private float xMin = 0;
    private float zMax = 0;
    private float zMin = 0;

    void Awake()
    {
        Floors = GameObject.FindGameObjectsWithTag("Floor");
        Camera = GetComponent<Camera>();
        SetPosition();
        SetSize();
    }

    private void SetPosition()
    {
        foreach (GameObject floor in Floors)
        {
            xMax = Mathf.Max(xMax, floor.transform.position.x);
            xMin = Mathf.Min(xMin, floor.transform.position.x);
            zMax = Mathf.Max(zMax, floor.transform.position.z);
            zMin = Mathf.Min(zMin, floor.transform.position.z);
        }
        CameraPosition.x = (xMax + xMin) / 2;
        CameraPosition.y = CameraYAxis;
        CameraPosition.z = zMin;
        transform.position = CameraPosition;
        transform.LookAt(new Vector3(CameraPosition.x, 0, (zMin + zMax * 2) / 3));
    }

    private void SetSize()
    {
        float properSize = MinSize + Mathf.Max(zMax - zMin + 1, xMax - xMin + 1) * 2;
        Camera.fieldOfView = properSize;
    }
}
