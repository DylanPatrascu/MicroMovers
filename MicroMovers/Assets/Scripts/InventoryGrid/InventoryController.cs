using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//NEEDS CHANGES

public class InventoryController : MonoBehaviour
{

    [HideInInspector]
    private ItemGrid selectedItemGrid;

    public ItemGrid SelectedItemGrid 
    { 
        get => selectedItemGrid; 
        set {
            selectedItemGrid = value;
            inventoryHighlight.SetParent(value);
        } 
    }
 

    InventoryItem selectedItem;
    InventoryItem overlapItem;
    RectTransform rectTransform;

    [SerializeField] List<ItemData> items;
    [SerializeField] GameObject itemPrefab;
    [SerializeField] Transform canvasTransform;

    InventoryHighlight inventoryHighlight;
    InventoryItem itemToHighlight;
    Vector2Int oldPosition;

    
    private void Awake()
    {
        inventoryHighlight = GetComponent<InventoryHighlight>();
    }

    private void Update()
    {

        ItemIconDrag();

        if (Input.GetKeyDown(KeyCode.Q))
        {
            CreateRandomItem();
        }

        if (selectedItemGrid == null) {  return; }

        

        Vector2Int mousePos = selectedItemGrid.GetTileGridPosition(Input.mousePosition);
        
        /*if (mousePos == new Vector2(0, 0) || 
            mousePos == new Vector2(0, 1) ||
            mousePos == new Vector2(1, 0) ||
            mousePos == new Vector2(0, 5) ||
            mousePos == new Vector2(0, 6) ||
            mousePos == new Vector2(1, 6) ||
            mousePos == new Vector2(5, 0) ||
            mousePos == new Vector2(6, 0) ||
            mousePos == new Vector2(6, 1) ||
            mousePos == new Vector2(5, 6) ||
            mousePos == new Vector2(6, 5) ||
            mousePos == new Vector2(6, 6)) { return; }*/

        if (selectedItemGrid == null)
        {
            inventoryHighlight.Show(false);
            return;
        }

        HandleHighlight();

        if (Input.GetMouseButtonDown(0))
        {
            LeftMouseButtonPress();
        }

    }

    private void HandleHighlight()
    {
        Vector2Int positionOnGrid = GetTileGridPosition();
        if (oldPosition == positionOnGrid) { return; }

        if (selectedItem == null)
        {

            itemToHighlight = selectedItemGrid.GetItem(positionOnGrid.x, positionOnGrid.y);

            if (itemToHighlight != null)
            {
                inventoryHighlight.Show(true);
                inventoryHighlight.SetSize(itemToHighlight);
                inventoryHighlight.SetPosition(selectedItemGrid, itemToHighlight);
            }
            else
            {
                inventoryHighlight.Show(false);
            }
            
        }
        else
        {
            //HERE
            //inventoryHighlight.Show(selectedItemGrid.BoundaryCheck(positionOnGrid.x, positionOnGrid.y, selectedItem.itemData.width, selectedItem.itemData.height));
            inventoryHighlight.SetSize(selectedItem);
            inventoryHighlight.SetPosition(selectedItemGrid, selectedItem, positionOnGrid.x, positionOnGrid.y);
        }
    }

    private void CreateRandomItem()
    {
        InventoryItem inventoryItem = Instantiate(itemPrefab).GetComponent<InventoryItem>();
        selectedItem = inventoryItem;
        rectTransform = inventoryItem.GetComponent<RectTransform>();
        rectTransform.SetParent(canvasTransform);

        int selectedItemID = UnityEngine.Random.Range(0, items.Count);
        inventoryItem.Set(items[selectedItemID]);
    }

    private void ItemIconDrag()
    {
        if (selectedItem != null)
        {
            rectTransform.position = Input.mousePosition;
        }
    }

    private void LeftMouseButtonPress()
    {
        Vector2Int tileGridPosition = GetTileGridPosition();

        if (selectedItem == null)
        {
            PickUpItem(tileGridPosition);
        }
        else
        {
            PlaceItem(tileGridPosition);
        }
    }

    //HERE
    private Vector2Int GetTileGridPosition()
    {
        Vector2 position = Input.mousePosition;

        if (selectedItem != null)
        {
            //position.x -= (selectedItem.itemData.width - 1) * ItemGrid.tileSizeWidth / 2;
            //position.x += (selectedItem.itemData.height - 1) * ItemGrid.tileSizeHeight / 2;
        }

        return selectedItemGrid.GetTileGridPosition(position);
    }

    private void PlaceItem(Vector2Int tileGridPosition)
    {
        bool complete = selectedItemGrid.PlaceItem(selectedItem, tileGridPosition.x, tileGridPosition.y, ref overlapItem);
        if (complete)
        {
            selectedItem = null;
            if (overlapItem != null)
            {
                selectedItem = overlapItem;
                overlapItem = null;
                rectTransform = selectedItem.GetComponent<RectTransform>();
            }
        }
    }

    private void PickUpItem(Vector2Int tileGridPosition)
    {
        selectedItem = selectedItemGrid.PickUpItem(tileGridPosition.x, tileGridPosition.y);
        if (selectedItem != null)
        {
            rectTransform = selectedItem.GetComponent<RectTransform>();
        }
    }
}
