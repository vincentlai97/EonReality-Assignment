using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using Text = TMPro.TextMeshProUGUI;

public class TableCell : MonoBehaviour
{
    public string Text {
        get => GetComponentInChildren<Text>().text;
        set => GetComponentInChildren<Text>().text = value;
    }

    public Color TextColor {
        get => GetComponentInChildren<Text>().color;
        set { if (GetComponentInChildren<Text>()) GetComponentInChildren<Text>().color = value; }
    }

    public virtual TooltipHandler TooltipHandler {
        get => GetComponent<TooltipHandler>();
    }

    public virtual RectTransform TooltipRT {
        get => TooltipHandler.tooltip.transform as RectTransform;
    }

    public virtual Text TooltipText {
        get => TooltipRT.GetComponentInChildren<Text>();
    }

    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return null;
        if (GetComponentInChildren<Text>() && GetComponentInChildren<Text>().isTextTruncated)
        {
            TooltipHandler.enabled = true;
            TooltipText.text = Text;
            Vector2 sizeDelta = TooltipRT.sizeDelta;
            sizeDelta.x = TooltipText.preferredWidth + 36f;
            TooltipRT.sizeDelta = sizeDelta;
        }
    }
}
