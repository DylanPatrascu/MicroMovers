using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//NEEDS WORK
public class ItemGrid : MonoBehaviour
{

    public const float tileSizeWidth = 32;
    public const float tileSizeHeight = 32;

    InventoryItem[,] inventoryItemSlot;

    [SerializeField] int gridSizeWidth = 7;
    [SerializeField] int gridSizeHeight = 7;

    [SerializeField] GameObject inventoryItemPrefab;

    RectTransform rectTransform;

    Vector2 positionOnGrid = new Vector2();
    Vector2Int tileGridPosition = new Vector2Int();



    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        Init(gridSizeWidth, gridSizeHeight);
    }

    internal InventoryItem PickUpItem(int x, int y)
    {
        InventoryItem toReturn = inventoryItemSlot[x, y];

        if (toReturn == null) { return null; }

        CleanGridReference(toReturn);

        return toReturn;
    }

    private void CleanGridReference(InventoryItem item)
    {
        for (int i = 0; i < item.itemData.width; i++)
        {
            for (int j = 0; j < item.itemData.height; j++)
            {
                if (item.itemData)
                inventoryItemSlot[item.onGridPositionX + i, item.onGridPositionY + j] = null;
            }
        }
    }

    public Vector2Int GetTileGridPosition(Vector2 mousePosition)
    {
        positionOnGrid.x = mousePosition.x - rectTransform.position.x;
        positionOnGrid.y = rectTransform.position.y - mousePosition.y;

        tileGridPosition.x = (int) (positionOnGrid.x / tileSizeWidth);
        tileGridPosition.y = (int) (positionOnGrid.y / tileSizeHeight);

        return tileGridPosition;
    }

    private void Init(int width, int height)
    {
        inventoryItemSlot = new InventoryItem[width, height];
        Vector2 size = new Vector2(width * tileSizeWidth, height * tileSizeHeight);
        rectTransform.sizeDelta = size;
    }

    public bool PlaceItem(InventoryItem inventoryItem, int posx, int posy, ref InventoryItem overlapItem)
    {

        /*
        if (!BoundaryCheck(posx, posy, inventoryItem.itemData.width, inventoryItem.itemData.height)) return false;

        if (!OverlapCheck(posx, posy, inventoryItem.itemData.width, inventoryItem.itemData.height, ref overlapItem))
        {
            overlapItem = null;
            return false;
        }
        */

        if (overlapItem != null) CleanGridReference(overlapItem);

        RectTransform rectTransform = inventoryItem.GetComponent<RectTransform>();
        rectTransform.SetParent(this.rectTransform);

        /*
        for (int i = 0; i < inventoryItem.itemData.width; i++)
        {
            for (int j = 0; j < inventoryItem.itemData.height; j++)
            {
                inventoryItemSlot[posx + i, posy + j] = inventoryItem;
            }
        }
        */

        inventoryItem.onGridPositionX = posx;
        inventoryItem.onGridPositionY = posy;
        Vector2 position = CalculatePositionOnGrid(posx, posy);

        rectTransform.localPosition = position;

        return true;

    }

    public Vector2 CalculatePositionOnGrid(int posx, int posy)
    {
        Vector2 position = new Vector2();
        position.x = posx * tileSizeWidth;
        position.y = -(posy * tileSizeHeight);
        return position;
    }

    private bool OverlapCheck(int posx, int posy, int width, int height, ref InventoryItem overlapItem)
    {

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (inventoryItemSlot[posx + i, posy + j])
                {
                    if (overlapItem == null) overlapItem = inventoryItemSlot[posx + i, posy + j];
                    else
                    {
                        if (overlapItem != inventoryItemSlot[posx + i, posy + j]) return false;
                    }
                }
            }
        }

        return true;
    }

    bool PositionCheck (int posx, int posy)
    {
        if (posx < 0 || posy < 0) return false;
        if (posx >= gridSizeWidth || posy >= gridSizeHeight) return false;
        return true;
    }

    public bool BoundaryCheck (int posx, int posy, int width, int height) 
    {
        if (!PositionCheck(posx, posy)) return false;
        posx += width - 1;
        posy += height - 1;
        if (!PositionCheck(posx, posy)) return false;
        return true;
    }

    internal InventoryItem GetItem(int x, int y)
    {
        return inventoryItemSlot[x, y];
    }
}
