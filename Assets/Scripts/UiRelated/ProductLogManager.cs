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

        if (data == null)
        {
            ui.Setup(goodProductSprite, null);
            return;
        }

        Sprite spriteToUse;

        if (data.isBad)
        {
            spriteToUse = data.uiSprite != null ? data.uiSprite : goodProductSprite;
        }
        else
        {
            spriteToUse = data.uiSprite != null ? data.uiSprite : goodProductSprite;
        }

        ui.Setup(spriteToUse, data);
    }
}