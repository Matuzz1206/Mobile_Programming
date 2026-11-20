using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GridLayoutGroup))]
public class GridCellResizer : MonoBehaviour
{
    [SerializeField] int columns = 2;
    [SerializeField] float heightRatio = 1.2f; 

    void Start()
    {
        Resize();
    }

    void OnRectTransformDimensionsChange()
    {
        Resize();
    }

    void Resize()
    {
        var grid = GetComponent<GridLayoutGroup>();
        var rt = (RectTransform)transform;
        float usable = rt.rect.width - grid.padding.left - grid.padding.right
                       - grid.spacing.x * (columns - 1);
        float w = usable / columns;
        grid.cellSize = new Vector2(w, w * heightRatio);
    }
}