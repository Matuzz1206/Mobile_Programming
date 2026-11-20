using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class UIWindow : MonoBehaviour
{
    [SerializeField] RectTransform panel;    
    [SerializeField] float duration = 0.25f;
    [SerializeField] float hiddenScale = 0.7f;

    CanvasGroup cg;
    Coroutine routine;
    bool isOpen;

    void Start()
    {
        if (!isOpen) gameObject.SetActive(false);
    }

    public void Open()
    {
        if (cg == null) cg = GetComponent<CanvasGroup>();
        isOpen = true;

        if (!gameObject.activeSelf)
        {
            cg.alpha = 0f;
            panel.localScale = Vector3.one * hiddenScale;
            gameObject.SetActive(true);
        }

        if (routine != null) StopCoroutine(routine);
        routine = StartCoroutine(Animate(true));
    }

    public void Close()
    {
        if (!isOpen) return;
        isOpen = false;

        if (routine != null) StopCoroutine(routine);
        routine = StartCoroutine(Animate(false));
    }

    IEnumerator Animate(bool opening)
    {
        cg.interactable = false;
        cg.blocksRaycasts = opening;   

        float startAlpha = cg.alpha;
        float startScale = panel.localScale.x;
        float endAlpha = opening ? 1f : 0f;
        float endScale = opening ? 1f : hiddenScale;

        float t = 0f;
        while (t < duration)
        {
            t += Time.unscaledDeltaTime;
            float k = Mathf.Clamp01(t / duration);
            float e = opening ? EaseOutBack(k) : k;

            cg.alpha = Mathf.Lerp(startAlpha, endAlpha, k);
            panel.localScale = Vector3.one * Mathf.LerpUnclamped(startScale, endScale, e);
            yield return null;
        }

        cg.alpha = endAlpha;
        panel.localScale = Vector3.one * endScale;
        cg.interactable = opening;

        if (!opening) gameObject.SetActive(false);
        routine = null;
    }

    static float EaseOutBack(float x)
    {
        const float c1 = 1.70158f;
        const float c3 = c1 + 1f;
        return 1f + c3 * Mathf.Pow(x - 1f, 3) + c1 * Mathf.Pow(x - 1f, 2);
    }
}