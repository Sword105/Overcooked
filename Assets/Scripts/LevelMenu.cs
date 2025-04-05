using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct MenuItems
{
    public List<ItemType> items;
}

public class LevelMenu : MonoBehaviour
{
    public List<MenuItems> menuItems;
    public List<ItemType> RandomMenuItem()
    {
        int randomIndex = Random.Range(0,menuItems.Count);
        return menuItems[randomIndex].items;
    }
}

