using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu]
public class ItemData : ScriptableObject
{
    public string itemName;
    public int width;
    public int height;
    public List<List<int>> blockPlacement;
    public Sprite texture;
    public bool shrinkable;
    public ItemData shrunkItem;
}
