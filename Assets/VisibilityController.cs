using UnityEngine;

public class VisibilityController : MonoBehaviour
{
    public GameObject wheelWidget;
    private bool wheelVisibility = false;

    public void ChangeWheelVisibility(){
        wheelVisibility = !wheelVisibility;
        wheelWidget.SetActive(wheelVisibility);
    }
}
