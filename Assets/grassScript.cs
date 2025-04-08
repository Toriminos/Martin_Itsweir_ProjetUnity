using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class grassScript : MonoBehaviour
{
    public float grassSpeed;
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
}
