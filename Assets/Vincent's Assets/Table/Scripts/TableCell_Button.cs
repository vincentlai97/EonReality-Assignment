using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Text = TMPro.TextMeshProUGUI;

public class TableCell_Button : TableCell
{
    public Button Button {
        get => GetComponentInChildren<Button>();
    }

    public Sprite Sprite {
        get => Button.GetComponentInChildren<Image>().sprite;
        set => Button.GetComponentInChildren<Image>().sprite = value;
    }

    public override TooltipHandler TooltipHandler => GetComponentInChildren<TooltipHandler>();

    public override RectTransform TooltipRT => TooltipHandler.tooltip.transform as RectTransform;

    public override Text TooltipText => TooltipRT.GetComponentInChildren<Text>();

    IEnumerator Start()
    {
        yield return null;
        TooltipHandler.enabled = true;
        Vector2 sizeDelta = TooltipRT.sizeDelta;
        sizeDelta.x = TooltipText.preferredWidth + 36f;
        TooltipRT.sizeDelta = sizeDelta;
    }

    public void AddListener(UnityEngine.Events.UnityAction call)
    {
        GetComponentInChildren<Button>().onClick.AddListener(call);
    }
}
