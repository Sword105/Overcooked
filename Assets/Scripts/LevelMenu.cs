using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[System.Serializable]
public struct MenuItems
{
    public List<ItemType> items;
    public Texture texture;
    public TextMeshProUGUI text;

    public Texture GetTexture()
    {
        return texture;
    }

    public TextMeshProUGUI GetText()
    {
        return text;
    }
}

public class LevelMenu : MonoBehaviour
{
    public List<MenuItems> menuItems;

    public MenuItems RandomMenuItem()
    {
        int randomIndex = Random.Range(0,menuItems.Count);
        return menuItems[randomIndex];
    }


}

