using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Land : MonoBehaviour
{
    public enum LandStatus
    {
        Soil, Watered
    }

    public LandStatus landStatus;
    public Material soilMat, wateredMat;
    new Renderer renderer;

    void Start()
    {
        //Get the renderer component
        renderer = GetComponent<Renderer>();

        //Get the land soil by default
        SwitchLandStatus(LandStatus.Soil);
    }
    public void SwitchLandStatus(LandStatus statusToSwitch)
    {
        landStatus = statusToSwitch;
        Material materialToSwitch = soilMat;

        switch (statusToSwitch)
        {
            case LandStatus.Soil:
                //Switch to soil material
                materialToSwitch = soilMat;
                break;

            case LandStatus.Watered:
                //Switch to watered material
                materialToSwitch = wateredMat;
                break;
        }

        //Get the renderer to apply the changes
        renderer.material = materialToSwitch;
    }
}
