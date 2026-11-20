using UnityEngine;

[System.Serializable]
public class ShopPack
{
    public string name;
    public float price;
    public double points;
    public Sprite icon;
}

public class CashShop : MonoBehaviour
{
    [SerializeField] ShopItemUI itemPrefab;
    [SerializeField] Transform content;
    [SerializeField] ShopPack[] packs;

    void Start()
    {
        foreach (var p in packs)
        {
            ShopItemUI item = Instantiate(itemPrefab, content);
            item.Setup(p);
        }
    }
}