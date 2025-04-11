using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class VolumeControlGeneral : MonoBehaviour
{
    public Text text;
    public Slider slider;
    public SwitchImageButton button;

    public List<VolumeControl> VCs = new List<VolumeControl>();

    private float savedVolume;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text.text = slider.value.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddVC(VolumeControl vc)
    {
        VCs.Add(vc);
    }

    public void DisplayVolume(Single value)
    {
        text.text = value.ToString();
        foreach (VolumeControl VC in VCs) {
            float minValue = Mathf.Min(value, VC.slider.value);
            VC.maxValueGeneral = value;
            VC.slider.value = minValue;
            VC.source.volume = minValue / 100f;
        }
    }

    public void Mute(int state)
    {
        if (state == 1)
        {
            savedVolume = slider.value;

            Debug.Log("VC count = " + VCs.Count);

            foreach (VolumeControl VC in VCs)
            {
                VC.Mute();
                VC.slider.interactable = false;
                VC.SIbutton.enabled = false;
            }

            slider.value = 0;
            slider.interactable = false;
        }
        else
        {
            slider.value = savedVolume;
            slider.interactable = true;

            foreach (VolumeControl VC in VCs)
            {
                VC.Unmute();
                VC.slider.interactable = true;
                VC.SIbutton.enabled = true;
            }
        }
    }
}
