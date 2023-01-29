using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GridLayoutGroup))]
public class OneRowGridLayoutSizeScaler : MonoBehaviour
{
    private GridLayoutGroup _layoutGroup;

    private void Awake()
    {
        _layoutGroup = GetComponent<GridLayoutGroup>();
    }

    private void OnEnable()
    {
        int rowCount = _layoutGroup.constraintCount;
        int spacingCount = rowCount - 1;
        float spacingsWidth = spacingCount * _layoutGroup.spacing.y;
        float cellWidth = (GetComponent<RectTransform>().rect.width - spacingsWidth) / rowCount;
        _layoutGroup.cellSize = new Vector2(cellWidth, _layoutGroup.cellSize.y);
    }
}
