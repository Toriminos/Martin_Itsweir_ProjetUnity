using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class MenuItem : MonoBehaviour, IPointerClickHandler
{
    public String itemName;
    public int val;
    private ItemEvent onItemClicked = new ItemEvent();
    public ItemEvent OnSelected
    {
        get
        {
            return onItemClicked;
        }
        set
        {
            onItemClicked = value;
        }
    }

    public void setValue(String itemName, int val, ItemEvent onItemClicked){
        this.itemName = itemName;
        this.val = val;
        this.onItemClicked = onItemClicked;
        GetComponent<Text>().text = itemName;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        onItemClicked.Invoke(val);
    }
}

[Serializable]
public class ItemEvent : UnityEvent<int>{}
