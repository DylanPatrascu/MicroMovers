using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGrid : MonoBehaviour
{

    const float tileSizeWidth = 32;
    const float tileSizeHeight = 32;

    RectTransform rectTransform;

    Vector2 positionOnGrid = new Vector2();
    Vector2Int tileGridPosition = new Vector2Int();

    public Vector2Int GetTileGridPosition(Vector2 mousePosition)
    {
        positionOnGrid.x = mousePosition.x - rectTransform.position.x;
        positionOnGrid.y = rectTransform.position.y - mousePosition.y;

        tileGridPosition.x = (int) (positionOnGrid.x / tileSizeWidth);
        tileGridPosition.y = (int) (positionOnGrid.y / tileSizeHeight);

        return tileGridPosition;
    }

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

}
