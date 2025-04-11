using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using System.Collections.Generic;
using UnityEngine.PlayerLoop;


[RequireComponent(typeof(RectTransform))]
public class KnobDragWidget : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Serializable]
    public class KnobDragEvent : UnityEvent<float>{}

    [SerializeField]
    public KnobDragEvent m_OnValueChanged = new KnobDragEvent();

    [Header("Value")]
    public float minValue;
    public float maxValue = 1f;
    public float m_Value;
    public bool WholeNumbers;

    [Header("Multiplier")]
    public float magnitudeMultiplier = 0.1f;
    public float visualMultiplier = 0.1f;

    [Header("Spring")]
    public bool springBack = false;
    public float springBackSpeed = 5f;
    public float springSnap =  0.01f;

    private Vector2 positionBeginDrag;
    private Vector3 rotationStart;
    private RectTransform rectTransform;
    private bool enableAction = false;
    private float valueToAdd = 0f;
    private float initialValue;
    private bool isSpringingBack = false;

    public KnobDragEvent OnValueChanged
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

    private void Start(){
        if(maxValue < minValue){
            maxValue = 1f;
            minValue = 0f;
        }
        rectTransform = GetComponent<RectTransform>();
        rotationStart = rectTransform.eulerAngles;
        positionBeginDrag = new Vector2(0, 0);
        m_Value = ClampValue(m_Value);
        initialValue = m_Value;
    }

    public void FixedUpdate(){
        if(enableAction){
            Set(m_Value + valueToAdd);
        }
        else if (springBack && isSpringingBack)
        {
            float toMinus = Mathf.Lerp(m_Value, initialValue, Time.fixedDeltaTime * springBackSpeed);
            valueToAdd = toMinus - m_Value;
            Set(toMinus);
            if (Mathf.Abs(m_Value + initialValue) < 0.01f)
            {
                ResetVisuals();
                m_Value = initialValue;
                isSpringingBack = false;
            }
        }
    }
    
    public virtual void Set(float input, bool sendCallback = true)
    {
        float num = ClampValue(input);
        if (m_Value != num)
        {
            m_Value = num;
            UpdateVisuals();
            if (sendCallback)
            {
                m_OnValueChanged.Invoke(num);
            }
        }
    }

    private void UpdateVisuals()
    {
        rectTransform.transform.rotation = Quaternion.Euler(0, 0, rectTransform.eulerAngles.z - (valueToAdd * visualMultiplier));
    }

    public void ResetVisuals()
    {
        rectTransform.transform.rotation = Quaternion.Euler(rotationStart);
    }

    private float ClampValue(float input)
    {
        float num = Mathf.Clamp(input, minValue, maxValue);
        if (WholeNumbers)
        {
            num = Mathf.Round(num);
        }
        return num;
    }

    public float getValue(){
        return m_Value;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        positionBeginDrag = eventData.position;
        enableAction = true;
        isSpringingBack = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 directionVecteur = eventData.position - positionBeginDrag;
        if (directionVecteur.x < 0)
            valueToAdd = -(directionVecteur.magnitude * magnitudeMultiplier);
        else
            valueToAdd = directionVecteur.magnitude * magnitudeMultiplier;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        valueToAdd = 0f;
        enableAction = false;
        positionBeginDrag = new Vector2(0, 0);
        
        if (springBack)
        {
            isSpringingBack = true;
        }
    }
}