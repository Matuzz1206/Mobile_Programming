using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopItemUI : MonoBehaviour
{
    [SerializeField] TMP_Text nameText;
    [SerializeField] TMP_Text pointsText;
    [SerializeField] TMP_Text priceText;
    [SerializeField] Image icon;
    [SerializeField] Button buyButton;

    public void Setup(ShopPack pack)
    {
        nameText.text = pack.name;
        pointsText.text = "+" + GameManager.Format(pack.points);
        priceText.text = "$" + pack.price.ToString("0.00");
        if (pack.icon != null) icon.sprite = pack.icon;

        buyButton.onClick.RemoveAllListeners();
        buyButton.onClick.AddListener(() =>
        {
            GameManager.Instance.AddPoints(pack.points);
        });
    }
}