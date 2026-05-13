using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CanvasEnabler : MonoBehaviour
{
    public GameObject panel;
    public ScrollRect scrollRect;

    private void OnEnable()
    {
        panel.SetActive(true);
        StartCoroutine(MoveToBottom());
    }

    IEnumerator MoveToBottom()
    {
        yield return null; // wait 1 frame for UI rebuild

        if (scrollRect != null)
        {
            scrollRect.verticalNormalizedPosition = 0f;
        }
    }

    private void OnDisable()
    {
        if (panel != null && panel.activeSelf)
            panel.SetActive(false);
    }
}