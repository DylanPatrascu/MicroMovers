using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeScriptableObjects : MonoBehaviour
{
    private List<ItemData> items;
    void Start()
    {
        for (int i = 0; i < items.Count; i++)
        {
            ItemData item = items[i];
            if (item.itemName == "chair")
                item.blockPlacement = new List<List<int>>
                {
                    new List<int>{1, 1, 1},
                    new List<int>{1, 1, 1},
                    new List<int>{1, 0, 1}
                };
            else if (item.itemName == "table")
                item.blockPlacement = new List<List<int>>
                {
                    new List<int>{1, 1, 1},
                    new List<int>{1, 1, 1},
                    new List<int>{1, 0, 1}
                };
        }
    }
}
