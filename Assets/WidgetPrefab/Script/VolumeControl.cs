using System;
using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    public Text text;
    public Slider slider;
    public SwitchImageButton SIbutton;

    public AudioSource source = null;

    private float savedVolume;
    public float maxValueGeneral;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text.text = slider.value.ToString();
        slider.value = maxValueGeneral;
    }

    public void SetSource(AudioSource AS)
    {
        source = AS;
        source.volume = slider.value / 100.0f;
    }

    public void DisplayVolume(Single value)
    {
        if (value <= maxValueGeneral)
        {
            text.text = value.ToString();
            if (source != null)
            {
                source.volume = value / 100.0f;
            }
        } else
        {
            slider.value = maxValueGeneral;
            DisplayVolume(maxValueGeneral);
        }
    }

    public void MuteButtonAction(int state)
    {
        if (state == 1)
        {
            savedVolume = slider.value;
            slider.value = 0;
            slider.interactable = false;

            if (source != null)
            {
                source.volume = 0;
            }
        } else
        {
            slider.interactable = true;
            slider.value = savedVolume;

            if (source != null)
            {
                source.volume = savedVolume / 100f;
            }
        }
    }

    public void Mute()
    {
        SIbutton.setImage(1);
        savedVolume = slider.value;
        slider.value = 0;
        slider.interactable = false;

        if (source != null)
        {
            source.volume = 0;
        }
    }

    public void Unmute()
    {
        SIbutton.setImage(0);
        slider.interactable = true;
        slider.value = savedVolume;

        if (source != null)
        {
            source.volume = savedVolume / 100f;
        }
    }
}
