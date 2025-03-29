using UnityEngine;
using UnityEngine.UI;

public class FlashFade : MonoBehaviour
{
    public Image panelImage;

    void Start()
    {
        panelImage.canvasRenderer.SetAlpha(0f);
    }

    public void Flash()
    {
        panelImage.CrossFadeAlpha(1f, 0.5f, false); // จอค่อยๆ ขาว
    }

    public void Hide()
    {
        panelImage.CrossFadeAlpha(0f, 0.5f, false); // จอค่อยๆ หาย
    }
}
