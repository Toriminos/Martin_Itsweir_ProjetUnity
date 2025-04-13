using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Text), typeof(RectTransform))]
public class SubMenu : MonoBehaviour, IPointerClickHandler
{
    public String SubMenuName;
    public bool containMenuItems;
    public List<MenuItemInterne> items;
    
    [SerializeField]
    private MenuEvent onMenuClicked = new MenuEvent();
    private GameObject menuItemsContainer;
    private Text itemsText;
    private Transform grandParent;
    private GameObject itemMenu;
    private bool hasBeenClicked = false;
    private MenuManagementScript management;
    private RectTransform rectTransform;
    private RectTransform fatherRectTransform;

    public void Start()
    {
        if(containMenuItems){
            itemMenu = Instantiate(menuItemsContainer, grandParent);
            itemMenu.SetActive(false);
            foreach (MenuItemInterne item in items)
            {
                Text itemObject = Instantiate(itemsText, itemMenu.transform);
                MenuItem itemScript = itemObject.gameObject.AddComponent<MenuItem>();
                itemScript.setValue(item.itemName, item.val, item.onItemClicked);
            }
        }
        management = transform.parent.GetComponent<MenuManagementScript>();
        rectTransform = GetComponent<RectTransform>();
        fatherRectTransform = transform.parent.GetComponent<RectTransform>();
    }

    public void setVariable(String SubMenuName, bool containMenuItems, List<MenuItemInterne> items, MenuEvent onMenuClicked, Transform grandParent, GameObject itemsContainer, Text itemsStyle){
        this.menuItemsContainer = itemsContainer;
        this.itemsText = itemsStyle;
        this.grandParent = grandParent;
        this.SubMenuName = SubMenuName;
        this.containMenuItems = containMenuItems;
        this.items = items;
        this.onMenuClicked = onMenuClicked;
        GetComponent<Text>().text = SubMenuName;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(containMenuItems){
            if(itemMenu.activeSelf)
                itemMenu.SetActive(false);
            else{
                if(management != null)
                    management.RetractAllSubmenu();
                itemMenu.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(rectTransform.anchoredPosition3D.x, rectTransform.anchoredPosition3D.y - fatherRectTransform.sizeDelta.y/2, 0);
                itemMenu.SetActive(true);
            }
        }
        hasBeenClicked = !hasBeenClicked;
        onMenuClicked.Invoke(hasBeenClicked);
    }

    public void RetractMenu(){
        if(containMenuItems)
            itemMenu.SetActive(false);
    }

}

[Serializable]
public class MenuItemInterne
{
    public String itemName;
    public int val;
    public ItemEvent onItemClicked = new ItemEvent();
}

[Serializable]
public class MenuEvent : UnityEvent<bool>{}
