using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ModelData
{
    public Mesh model;
    public List<ItemType> neededItems;
}

public class ModelDataStorage: MonoBehaviour
{
    public List<ModelData> storedModelData = new List<ModelData>();
}