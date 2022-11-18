using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using DG.Tweening.Core;

public class TooltipHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public CanvasGroup tooltip;
    private int tweenId;
    TweenerCore<float, float, DG.Tweening.Plugins.Options.FloatOptions> tweener;

    private void Start()
    {
        tweener = DOTween.To(() => tooltip.alpha, x => tooltip.alpha = x, 1f, 0.5f).SetEase(Ease.InOutQuint).SetAutoKill(false);
        OnPointerExit(null);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!tooltip) return;
        tooltip.gameObject.SetActive(true);
        tweener?.PlayForward();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!tooltip) return;
        tweener?.PlayBackwards();
    }
}
