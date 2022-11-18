using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.Extensions;
using Text = TMPro.TextMeshProUGUI;

public class HeaderCell : MonoBehaviour
{
    public string Text
    {
        get => GetComponentInChildren<Text>().text;
        set => GetComponentInChildren<Text>().text = value;
    }

    public TableLayoutGroup TableLayoutGroup
    {
        get => GetComponentInParent<TableLayoutGroup>();
    }
    public int ChildIndex
    {
        get => transform.GetSiblingIndex();
    }

    [ExecuteInEditMode]
    void OnAwake()
    {
        Update();
    }

    [ContextMenu("Update")]
    private void Update()
    {
        if ((transform as RectTransform).sizeDelta.x != TableLayoutGroup.ColumnWidths[ChildIndex])
        {
            Vector2 sizeDelta = (transform as RectTransform).sizeDelta;
            sizeDelta.x = TableLayoutGroup.ColumnWidths[ChildIndex];
            (transform as RectTransform).sizeDelta = sizeDelta;
        }
    }
}
