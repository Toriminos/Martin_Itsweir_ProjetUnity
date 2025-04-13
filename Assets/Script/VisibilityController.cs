using UnityEngine;

public class VisibilityController : MonoBehaviour
{
    public GameObject wheelWidget;
    public GameObject boostWidget;
    public GameObject colorPicker;
    public GameObject graph;
    public GameObject info;
    private bool wheelVisibility = false;
    private bool boostVisibility = false;
    private bool colorPickerVisibility = false;
    private bool graphVisibility = false;
    private bool infoVisibility = false;

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

    public void ChangeInfoVisibility(){
        infoVisibility = !infoVisibility;
        info.SetActive(infoVisibility);
    }
}
