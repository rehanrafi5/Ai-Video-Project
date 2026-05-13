using UnityEngine;
using UnityEngine.UI;

public class ProductLogItemUI : MonoBehaviour
{
    public Image previewImage;

    public void SetImage(Sprite sprite)
    {
        previewImage.sprite = sprite;
    }
}