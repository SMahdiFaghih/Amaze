using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    private Material[] SphereMaterials;
    private Material[] CubeMaterials;
    private int SelectedMaterialNumber;

    void Awake()
    {
        SphereMaterials = Resources.LoadAll<Material>("Sphere Materials/");
        CubeMaterials = Resources.LoadAll<Material>("Cube Colors/MAterials/");
        SelectedMaterialNumber = Random.Range(0, SphereMaterials.Length);
        GetComponent<MeshRenderer>().material = SphereMaterials[SelectedMaterialNumber];
    }

    public void ChangeFloorColor(GameObject floor)
    {
        floor.GetComponent<MeshRenderer>().material = CubeMaterials[SelectedMaterialNumber];
    }

}
