using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(HorizontalLayoutGroup))]
public class MenuManagementScript : MonoBehaviour
{
    public Text genericMenuStyle;
    public GameObject genericItemsContainer;
    public Text genericItem;
    public bool retractAllSubmenu = false;
    public List<SubMenuInterne> subMenus;
    private List<SubMenu> subMenusLink;

    private void Awake() {
        subMenusLink = new List<SubMenu>();
        foreach (SubMenuInterne subMenu in subMenus)
        {
            GameObject menu;
            if(subMenu.linkedTo == null)
                menu = Instantiate(genericMenuStyle.gameObject, gameObject.transform.position, gameObject.transform.rotation, transform);
            else
                menu = subMenu.linkedTo.gameObject;
            menu.GetComponent<Text>().text = subMenu.SubMenuName;
            SubMenu subMenuScript = menu.AddComponent<SubMenu>();
            subMenusLink.Add(subMenuScript);
            subMenuScript.setVariable(subMenu.SubMenuName, subMenu.containMenuItems, subMenu.items, subMenu.onMenuClicked, transform.parent, genericItemsContainer, genericItem);
        }
    }

    public void RetractAllSubmenu(){
        if(retractAllSubmenu){
            foreach (SubMenu subMenu in subMenusLink)
            {
                subMenu.RetractMenu();
            }
        }
    }

    [Serializable]
    public class SubMenuInterne
    {
        public Text linkedTo = null;
        public String SubMenuName;
        public bool containMenuItems;
        public List<MenuItemInterne> items;

        [SerializeField]
        public MenuEvent onMenuClicked = new MenuEvent();
    }
}
