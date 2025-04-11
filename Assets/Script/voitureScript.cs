using System.Collections.Generic;
using UnityEngine;

public class voitureScript : MonoBehaviour
{
    public GameObject frontLeftWheel;
    public GameObject frontRightWheel;
    public ParticleSystem leftWheelParticule;
    public ParticleSystem rightWheelParticule;
    public KnobDragWidget wheelScript;
    public List<Material> toChangeColor;

    private float rightWheelStartY;
    private float leftWheelStartY;

    private void Start() {
        rightWheelStartY = rightWheelParticule.transform.rotation.eulerAngles.y;
        leftWheelStartY = leftWheelParticule.transform.rotation.eulerAngles.y; 
    }

    public void Drift(float driftAngle){
        this.gameObject.transform.rotation = Quaternion.Euler(0,driftAngle,0);
        driftAngle = driftAngle * 0.5f;
        leftWheelParticule.transform.rotation = Quaternion.Euler(leftWheelParticule.transform.eulerAngles.x, leftWheelStartY-driftAngle, leftWheelParticule.transform.eulerAngles.z);
        rightWheelParticule.transform.rotation = Quaternion.Euler(rightWheelParticule.transform.eulerAngles.x, rightWheelStartY-driftAngle, rightWheelParticule.transform.eulerAngles.z);
        frontLeftWheel.transform.rotation = Quaternion.Euler(frontLeftWheel.transform.eulerAngles.x, -driftAngle, frontLeftWheel.transform.eulerAngles.z);
        frontRightWheel.transform.rotation = Quaternion.Euler(frontRightWheel.transform.eulerAngles.x, -driftAngle, frontRightWheel.transform.eulerAngles.z);
    }

    public void ResetDrift(){
        wheelScript.Set(0);
    }

    public void ChangeColorOfCar(Color color){
        foreach (Material mat in toChangeColor)
        {
            mat.color = color;
        }
    }
}
