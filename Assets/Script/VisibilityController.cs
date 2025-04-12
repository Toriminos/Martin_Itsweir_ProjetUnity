using UnityEngine;

public class VisibilityController : MonoBehaviour
{
    public GameObject wheelWidget;
    public GameObject boostWidget;
    public GameObject colorPicker;
    public GameObject graph;
    private bool wheelVisibility = false;
    private bool boostVisibility = false;
    private bool colorPickerVisibility = false;
    private bool graphVisibility = false;

    public void ChangeWheelVisibility(){
        wheelVisibility = !wheelVisibility;
        wheelWidget.SetActive(wheelVisibility);
    }

    public void ChangeBoostlVisibility(){
        boostVisibility = !boostVisibility;
        boostWidget.SetActive(boostVisibility);
    }
    
    public void ChangeColorPickerlVisibility(){
        colorPickerVisibility = !colorPickerVisibility;
        colorPicker.SetActive(colorPickerVisibility);
    }

    public void ChangeGraphVisibility(){
        graphVisibility = !graphVisibility;
        graph.SetActive(graphVisibility);
    }
}
