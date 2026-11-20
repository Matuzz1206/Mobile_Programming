using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("UI")]
    [SerializeField] TMP_Text pointsText;
    [SerializeField] TMP_Text perSecondText;
    [SerializeField] RectTransform clickButtonRect;

    public double Points { get; private set; }
    public double PointsPerClick { get; set; } = 1;
    public double PointsPerSecond { get; set; } = 0;

    float timer;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        RefreshUI();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 1f)
        {
            timer -= 1f;
            if (PointsPerSecond > 0) AddPoints(PointsPerSecond);
        }
    }
    public void OnClickButton()
    {
        AddPoints(PointsPerClick);
        StopAllCoroutines();
        StartCoroutine(Punch());
    }

    public void AddPoints(double amount)
    {
        Points += amount;
        RefreshUI();
    }

    public bool TrySpend(double cost)
    {
        if (Points < cost) return false;
        Points -= cost;
        RefreshUI();
        return true;
    }

    public void RefreshUI()
    {
        pointsText.text = Format(Points);
        perSecondText.text = Format(PointsPerSecond) + " / sec";
    }
    System.Collections.IEnumerator Punch()
    {
        float t = 0f, dur = 0.12f;
        while (t < dur)
        {
            t += Time.unscaledDeltaTime;
            float s = 1f + 0.15f * Mathf.Sin(t / dur * Mathf.PI);
            clickButtonRect.localScale = Vector3.one * s;
            yield return null;
        }
        clickButtonRect.localScale = Vector3.one;
    }
    public static string Format(double v)
    {
        if (v < 1000) return v.ToString("0");
        string[] suffix = { "", "K", "M", "B", "T", "Qa", "Qi" };
        int i = 0;
        while (v >= 1000 && i < suffix.Length - 1)
        {
            v /= 1000;
            i++;
        }
        return v.ToString("0.##") + suffix[i];
    }
}