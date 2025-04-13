using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragScrollSpinnerChoice : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Option to change")]
    public bool drag = true;
    public bool scroll = true;
    public bool cycle = true;
    public float lengthToChangeOnDrag = 1;

    [Header("Time between change")]
    public float timeDrag = 1f;
    public float timeScroll = 1f;


    [Header("Values and events")]
    public List<string> values;
    public DSSCEvent m_OnValueChanged = new DSSCEvent();
    private Text displayValue;
    private int actuIndex = 0;
    private bool isDraged = false;
    private bool enableScroll = false;
    private int valueToAdd = 0;
    private Vector2 positionBeginDrag;
    private bool isWaiting = false;

    private void Awake() {
        displayValue = GetComponentInChildren<Text>();
        positionBeginDrag = new Vector2(0,0);
        UpdateVisuals();
    }

    private void Update()
    {
        if (values.Count != 0)
        {
            if(isDraged && !isWaiting){
                Set(actuIndex + valueToAdd);
                StartCoroutine(LaunchWaitTime(timeDrag));
            }
            if(enableScroll && !isWaiting){
                if(Input.GetAxis("Mouse ScrollWheel") > 0){
                    valueToAdd = 1;
                    StartCoroutine(LaunchWaitTime(timeScroll));
                    Set(actuIndex + valueToAdd);
                }
                else if(Input.GetAxis("Mouse ScrollWheel") < 0){
                    valueToAdd = -1;
                    StartCoroutine(LaunchWaitTime(timeScroll));
                    Set(actuIndex + valueToAdd);
                }
                valueToAdd = 0;
            }
        }
    }

    public DSSCEvent OnValueChanged
    {
        get
        {
            return m_OnValueChanged;
        }
        set
        {
            m_OnValueChanged = value;
        }
    }

    public virtual void Set(int index, bool sendCallback = true)
    {
        if (index >= values.Count)
        {
            if(cycle)
                actuIndex = 0;
            else
                actuIndex = values.Count - 1;
        }
        else if (index < 0)
        {
            if(cycle)
                actuIndex = values.Count - 1;
            else
                actuIndex = 0;
        }
        else
            actuIndex = index;
        UpdateVisuals();
        if (sendCallback)
        {
            //UISystemProfilerApi.AddMarker("Slider.value", this);
            m_OnValueChanged.Invoke(actuIndex);
        }
    }

    private void UpdateVisuals()
    {
        if(values.Count != 0)
            displayValue.text = values[actuIndex];
        else
            displayValue.text = "No value";
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        positionBeginDrag = eventData.position;
        isDraged = true;
        valueToAdd = 0;
    }

    public void OnDrag(PointerEventData eventData)
    {
        valueToAdd = 0;
        Vector2 directionVecteur = eventData.position - positionBeginDrag;
        if (directionVecteur.y < -lengthToChangeOnDrag)
            valueToAdd = -1;
        else if(directionVecteur.y > lengthToChangeOnDrag)
            valueToAdd = 1;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        valueToAdd = 0;
        isDraged = false;
        positionBeginDrag = new Vector2(0, 0);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        enableScroll = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        enableScroll = false;
    }

    private IEnumerator LaunchWaitTime(float time)
    {
        isWaiting = true;
        yield return new WaitForSeconds(time);
        isWaiting = false;
    }
}

[Serializable]
public class DSSCEvent : UnityEvent<int>{}