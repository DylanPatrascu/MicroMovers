using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//NEEDS WORK

public class InventoryHighlight : MonoBehaviour
{

    [SerializeField] RectTransform highlighter;

    public void Show(bool b)
    {
        highlighter.gameObject.SetActive(b);
    }

    public void SetSize (InventoryItem targetItem)
    {
        Vector2 size = new Vector2();
        size.x = targetItem.itemData.width * ItemGrid.tileSizeWidth - 1;
        size.y = targetItem.itemData.height * ItemGrid.tileSizeHeight - 1;
        highlighter.sizeDelta = size;
    }

    public void SetPosition(ItemGrid targetGrid, InventoryItem targetItem)
    {
        SetParent(targetGrid);
        Vector2 pos = targetGrid.CalculatePositionOnGrid(targetItem.onGridPositionX, targetItem.onGridPositionY);

        highlighter.localPosition = pos;
    }

    public void SetParent(ItemGrid targetGrid)
    {
        if (targetGrid == null) { return; }
        highlighter.SetParent(targetGrid.GetComponent<RectTransform>());
    }

    public void SetPosition(ItemGrid targetGrid, InventoryItem targetItem, int posx, int posy)
    {
        Vector2 pos = targetGrid.CalculatePositionOnGrid(posx, posy);
        highlighter.localPosition = pos;
    }

}
