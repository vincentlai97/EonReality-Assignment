using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Text = TMPro.TextMeshProUGUI;

[ExecuteAlways]
public class ButtonContentSizeFitter : MonoBehaviour
{
    public bool isTextButton;
    public int forceUpdateCount;

    Text tx_label;

    private IEnumerator Start()
    {
        tx_label = GetComponentInChildren<Text>();
        tx_label.rectTransform.hasChanged = false;
        yield return null;
        tx_label.rectTransform.hasChanged = true;
    }

    void LateUpdate()
    {
        if (tx_label.rectTransform.hasChanged || forceUpdateCount > 0)
        {
            Vector2 sizeDelta = (transform as RectTransform).sizeDelta;
            sizeDelta.x = tx_label.rectTransform.sizeDelta.x + (isTextButton ? 24f : 48f);
            sizeDelta.y = 40f;
            (transform as RectTransform).sizeDelta = sizeDelta;
            if (tx_label.rectTransform.sizeDelta.x != 0)
            {
                tx_label.rectTransform.hasChanged = false;
                forceUpdateCount--;
            }
        }

        if (isTextButton)
        {
            tx_label.color = new Color(0.2f, 0.2f, 0.2f, GetComponent<Button>().interactable ? 1f : 0.5f);
        }
    }
}
