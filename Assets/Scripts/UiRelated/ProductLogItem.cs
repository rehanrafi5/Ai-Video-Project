using UnityEngine;
using UnityEngine.UI;

public class ProductLogItemUI : MonoBehaviour
{
    public Image previewImage;
    public Button actionButton;

    private ProductData data;

    public Sprite BadButton;

    public void Setup(Sprite sprite, ProductData productData)
    {
        previewImage.sprite = sprite;
        data = productData;

        if (data != null && data.isBad)
        {
            actionButton.gameObject.SetActive(true);
            actionButton.image.sprite = BadButton;
        }
        else
        {
            actionButton.interactable = false;
        }
    }
    
}