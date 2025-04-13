using System.Collections.Generic;
using System;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SwitchImageButton : MonoBehaviour, IPointerClickHandler
{
    public List<Sprite> sprites;
    public Image logo;

    private int index = 0;

    [Serializable]
    public class OnClickEventSIB : UnityEvent<int> {}

    [SerializeField]
    private OnClickEventSIB onClick = new OnClickEventSIB();

    void Start()
    {
        if (sprites == null || sprites.Count == 0)
        {
            return;
        }

        logo.sprite = sprites[0];
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (sprites == null || sprites.Count == 0) return;

        index = (index + 1) % sprites.Count;
        logo.sprite = sprites[index];

        onClick.Invoke(index);
    }

    public OnClickEventSIB OnClickSIB
    {
        get
        {
            return onClick;
        }
        set
        {
            onClick = value;
        }
    }

    public void setImage(int i)
    {
        index = i % sprites.Count;
        logo.sprite = sprites[index];
    }
}
