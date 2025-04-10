using UnityEngine;
using System.Collections.Generic;

public class VolumeControlGroup : MonoBehaviour
{
    public Transform parent;
    public List<AudioSource> sources;
    public GameObject volumeControlGeneralPrefab;
    public GameObject volumeControlPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (sources == null || sources.Count == 0)
        {
            return;
        }

        GameObject vcGeneralPrefab = Instantiate(volumeControlGeneralPrefab, parent);
        VolumeControlGeneral vcGeneral = vcGeneralPrefab.GetComponent<VolumeControlGeneral>();

        foreach (AudioSource source in sources)
        {
            GameObject vcPrefab = Instantiate(volumeControlPrefab, parent);
            VolumeControl vc = vcPrefab.GetComponent<VolumeControl>();
            vc.SetSource(source);
            vcGeneral.AddVC(vc);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
