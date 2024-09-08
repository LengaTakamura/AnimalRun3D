using DG.Tweening;
using UnityEngine;

public class DotoweenMove : MonoBehaviour
{
    [SerializeField]Vector3 vect;
    // Start is called before the first frame update

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
}
