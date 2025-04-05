using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public struct ModelData
{
    public Mesh model;
    public List<ItemType> neededItems;
}

public class ModelDataStorage: MonoBehaviour
{
    public ItemType modelIndicator;
    public List<ItemType> allowedItemTypes;
    public List<ModelData> storedModelData = new List<ModelData>();
}