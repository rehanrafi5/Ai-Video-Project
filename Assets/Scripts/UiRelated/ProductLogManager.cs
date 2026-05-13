using UnityEngine;

public class ProductLogManager : MonoBehaviour
{
    public static ProductLogManager instance;

    [Header("UI")]
    public Transform contentParent;
    public GameObject logItemPrefab;

    [Header("Default")]
    public Sprite goodProductSprite;

    void Awake()
    {
        instance = this;
    }

    public void AddProduct(ProductData data)
    {
        GameObject item = Instantiate(logItemPrefab, contentParent);
        ProductLogItemUI ui = item.GetComponent<ProductLogItemUI>();

        if (data != null && !data.isBad && data.uiSprite != null)
        {
            ui.SetImage(data.uiSprite);
        }
        else if (data != null && data.isBad)
        {
            ui.SetImage(data.uiSprite);
        }
        else
        {
            ui.SetImage(goodProductSprite);
        }
    }
}