using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGrid : MonoBehaviour
{
    [SerializeField] private float tileSize = 100.0f;

    RectTransform rectTransform;

    Vector2 positionOnTheGrid = new Vector2();

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public Vector2Int GetGridIndexPosition(Vector2 mousePosition)
    {
        positionOnTheGrid.x = mousePosition.x - rectTransform.position.x;
        positionOnTheGrid.y = mousePosition.y - rectTransform.position.y;   
    }
}
