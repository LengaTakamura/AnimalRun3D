using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class DotoweenMove : MonoBehaviour
{
    [SerializeField]Vector3 vect;
    // Start is called before the first frame update
    Text text;
    CanvasGroup canG;
    private void Start()
    {
        DotweenUI();
    }
    public void DotweenUI()
    {
        RectTransform rect;

        rect = GetComponent<RectTransform>();

        rect.DOAnchorPos((vect), 3f).SetEase(Ease.OutBounce);

    }

    public void TextFadeOut()
    {
        text = GetComponent<Text>();
        text.DOFade(0.0f, 2f);
    }

    public void FadeOutButtun()
    {
       canG = GetComponent<CanvasGroup>();
        canG.DOFade(0f, 2f);
    }
}
