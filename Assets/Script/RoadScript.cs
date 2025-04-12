using UnityEngine;

public class RoadScript : MonoBehaviour
{
    public Material roadMaterial;
    public Vector2 roadSpeed;
    public Vector2 roadSpeedMin;
    public Vector2 roadSpeedMax;

    private void Start() {
        roadMaterial.SetVector("_roadDirection", roadSpeed);
    }

    public void SetClampSpeed(float mult){
        float roadSpeedX = Mathf.Lerp(roadSpeedMin.x, roadSpeedMax.x, mult);
        float roadSpeedY = Mathf.Lerp(roadSpeedMin.y, roadSpeedMax.y, mult);
        Vector2 newV = new Vector2(roadSpeedX, roadSpeedY);
        roadMaterial.SetVector("_roadDirection", newV);
    }
}
