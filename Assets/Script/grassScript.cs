using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class grassScript : MonoBehaviour
{
    public float grassSpeed;
    public float minSpeed;
    public float maxSpeed;
    public List<GameObject> grass;

    private void Start() {
        for(int i = 0; i < transform.childCount; i++)
            grass.Add(transform.GetChild(i).gameObject);
    }

    void FixedUpdate()
    {
        foreach (GameObject obj in grass)
        {
            obj.transform.position += Vector3.back * grassSpeed;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (grass.Contains(other.gameObject))
        {
            other.transform.position += new Vector3(0, 0, gameObject.transform.localScale.z);
        }
    }

    public void SetClampSpeed(float mult){
        grassSpeed = Mathf.Lerp(minSpeed, maxSpeed, mult);
    }
}
