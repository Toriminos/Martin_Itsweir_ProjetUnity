using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class ColorPickerWheel : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    public Image colorWheelImage;
    public Renderer targetRenderer;
    public Slider brightness;
    public InputField RInput;
    public InputField GInput;
    public InputField BInput;

    private Texture2D colorWheelTexture;
    private Color selectedColor;

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
                targetRenderer.material.color = tempColor;
            }
        }
    }

    public void pickBrightness(Single value)
    {
        Color tempColor = selectedColor;

        tempColor.r *= value;
        tempColor.g *= value;
        tempColor.b *= value;

        targetRenderer.material.color = tempColor;
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
                    RVal /= 255f;
                    GVal /= 255f;
                    BVal /= 255f;

                    if (RVal >= 0 && RVal <= 255 && GVal >= 0 && GVal <= 255 && BVal >= 0 && BVal <= 255)
                    {
                        selectedColor = new Color(RVal, GVal, BVal);

                        Color tempColor = selectedColor;

                        tempColor.r *= brightness.value;
                        tempColor.g *= brightness.value;
                        tempColor.b *= brightness.value;

                        targetRenderer.material.color = tempColor;
                    }
                }
            }
        }
    }
}
