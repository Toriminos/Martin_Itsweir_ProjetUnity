using UnityEngine;

public class VisibilityController : MonoBehaviour
{
    public GameObject wheelWidget;
    public GameObject boostWidget;
    public GameObject colorPicker;
    private bool wheelVisibility = false;
    private bool boostVisibility = false;
    private bool colorPickerVisibility = false;

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
}
