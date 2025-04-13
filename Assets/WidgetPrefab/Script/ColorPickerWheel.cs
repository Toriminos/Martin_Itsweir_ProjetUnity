using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using UnityEngine.Events;

public class ColorPickerWheel : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    public Image colorWheelImage;
    public Slider brightness;
    public InputField RInput;
    public InputField GInput;
    public InputField BInput;
    public ColorSelectedEvent m_OnColorSelected = new ColorSelectedEvent();

    private Texture2D colorWheelTexture;
    private Color selectedColor;

    public ColorSelectedEvent OnColorSelected
    {
        get
        {
            return m_OnColorSelected;
        }
        set
        {
            m_OnColorSelected = value;
        }
    }

    private void Start()
    {
        colorWheelTexture = colorWheelImage.sprite.texture;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        PickColor(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        PickColor(eventData);
    }

    private void PickColor(PointerEventData eventData)
    {
        RectTransform rectTransform = colorWheelImage.rectTransform;
        Vector2 cursorCoord;

        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData.position, eventData.pressEventCamera, out cursorCoord))
            return;

        float x = cursorCoord.x + rectTransform.rect.width * 0.5f;
        float y = cursorCoord.y + rectTransform.rect.height * 0.5f;

        float u = x / rectTransform.rect.width;
        float v = y / rectTransform.rect.height;

        if (u >= 0f && u <= 1f && v >= 0f && v <= 1f)
        {
            selectedColor = colorWheelTexture.GetPixelBilinear(u, v);

            Color tempColor = selectedColor;

            tempColor.r *= brightness.value;
            tempColor.g *= brightness.value;
            tempColor.b *= brightness.value;

            RInput.text = ((int) (tempColor.r * 255)).ToString();
            GInput.text = ((int)(tempColor.g * 255)).ToString();
            BInput.text = ((int)(tempColor.b * 255)).ToString();

            if (selectedColor.a > 0.1f)
            {
                m_OnColorSelected.Invoke(tempColor);
            }
        }
    }

    public void pickBrightness(Single value)
    {
        Color tempColor = selectedColor;

        tempColor.r *= value;
        tempColor.g *= value;
        tempColor.b *= value;

        m_OnColorSelected.Invoke(tempColor);
    }

    public void writeColor(String value)
    {
        float RVal;
        float GVal;
        float BVal;

        if (float.TryParse(RInput.text, out RVal))
        {
            if (float.TryParse(GInput.text, out GVal))
            {
                if (float.TryParse(BInput.text, out BVal))
                {
                    float normalizedRValue = Mathf.InverseLerp(0f, 255f, RVal);
                    float normalizedGValue = Mathf.InverseLerp(0f, 255f, GVal);
                    float normalizedBValue = Mathf.InverseLerp(0f, 255f, BVal);

                    RInput.text = (normalizedRValue * 255f).ToString();
                    GInput.text = (normalizedGValue * 255f).ToString();
                    BInput.text = (normalizedBValue * 255f).ToString();

                    selectedColor = new Color(normalizedRValue, normalizedGValue, normalizedBValue);

                    Color tempColor = selectedColor;

                    tempColor.r *= brightness.value;
                    tempColor.g *= brightness.value;
                    tempColor.b *= brightness.value;

                    m_OnColorSelected.Invoke(tempColor);
                    
                }
            }
        }
    }
}

[Serializable]
public class ColorSelectedEvent : UnityEvent<Color>{}
