using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeItem : MonoBehaviour
{
    public enum UpgradeType { ClickPower, AutoIncome }

    [Header("Settings")]
    [SerializeField] UpgradeType type;
    [SerializeField] string upgradeName = "Upgrade";
    [SerializeField] double baseCost = 10;
    [SerializeField] double costMultiplier = 1.15;  
    [SerializeField] double valuePerLevel = 1;      

    [Header("UI")]
    [SerializeField] TMP_Text nameText;
    [SerializeField] TMP_Text levelText;
    [SerializeField] TMP_Text costText;
    [SerializeField] TMP_Text effectText;
    [SerializeField] Button buyButton;

    int level;

    double Cost
    {
        get { return System.Math.Ceiling(baseCost * System.Math.Pow(costMultiplier, level)); }
    }

    void Start()
    {
        buyButton.onClick.AddListener(Buy);
        Refresh();
    }

    void Update()
    {
        buyButton.interactable = GameManager.Instance.Points >= Cost;
    }

    void Buy()
    {
        if (!GameManager.Instance.TrySpend(Cost)) return;
        level++;

        if (type == UpgradeType.ClickPower)
            GameManager.Instance.PointsPerClick += valuePerLevel;
        else
            GameManager.Instance.PointsPerSecond += valuePerLevel;

        GameManager.Instance.RefreshUI();
        Refresh();
    }

    void Refresh()
    {
        nameText.text = upgradeName;
        levelText.text = "Lv. " + level;
        costText.text = GameManager.Format(Cost);
        effectText.text = type == UpgradeType.ClickPower
            ? "+" + valuePerLevel + " per click"
            : "+" + valuePerLevel + " per second";
    }
}