using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChargeButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool WholeNumbers;
    public bool feedbackOnDropping = true;
    public float fillDuration = 2f; 
    public float min = 0;
    public float max = 100;
    private float val = 0;
    public Image chargeImage;
    public Image fillImage;
    public ChargeEvent m_OnValueChanged = new ChargeEvent();
    public EndChargeEvent m_OnValueReached = new EndChargeEvent();

    private RectTransform chargeImageRect;
    private float targetHeightWidth;
    private float currentTime;
    private bool isCharging = false;
    private bool maxReached = false;

    public ChargeEvent OnValueChanged
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

    public EndChargeEvent OnValueReached
    {
        get
        {
            return m_OnValueReached;
        }
        set
        {
            m_OnValueReached = value;
        }
    }

    private void Start() {
        if(max < min){
            max = 1f;
            min = 0f;
        }
        val = min;
        chargeImageRect = chargeImage.GetComponent<RectTransform>();
        RectTransform fillRect = fillImage.GetComponent<RectTransform>();
        targetHeightWidth = fillRect.rect.height;
        chargeImageRect.sizeDelta = new Vector2(chargeImageRect.sizeDelta.x, 0);
    }

    private void Update() {
        float newVal = 0;
        if (isCharging)
        {
            currentTime += Time.deltaTime;
            newVal = Mathf.Lerp(min, max, currentTime / fillDuration);
            Set(newVal);
            if(val == max)
                UpdateVisuals(fillDuration);
        }
        else if(val >= min)
        {
            currentTime -= Time.deltaTime;
            newVal = Mathf.Lerp(min, max, currentTime / fillDuration);
            Set(newVal, feedbackOnDropping);
            if(val == min)
                UpdateVisuals(0);
        }
    }

    protected virtual void Set(float input, bool sendCallback = true)
    {
        float num = ClampValue(input);
        if (val != num)
        {
            val = num;
            UpdateVisuals(currentTime);
            if (sendCallback)
            {
                //UISystemProfilerApi.AddMarker("Slider.value", this);
                if(!maxReached){
                    m_OnValueChanged.Invoke(num);
                    if (val >= max && !maxReached)
                    {
                        maxReached = true;
                        m_OnValueReached.Invoke(true);
                    }
                }
            }
        }
    }

    private float ClampValue(float input)
    {
        float num = Mathf.Clamp(input, min, max);
        if (WholeNumbers)
        {
            num = Mathf.Round(num);
        }

        return num;
    }

    private void UpdateVisuals(float current)
    {
        float div = current / fillDuration;
        chargeImageRect.sizeDelta = new Vector2(chargeImageRect.sizeDelta.x, Mathf.Lerp(0, targetHeightWidth, div));  
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (currentTime < 0)
            currentTime = 0;
        isCharging = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (currentTime > fillDuration)
            currentTime = fillDuration;
        isCharging = false;
        maxReached = false;
    }
}

[Serializable]
public class ChargeEvent : UnityEvent<float>{}

[Serializable]
public class EndChargeEvent : UnityEvent<bool>{}
