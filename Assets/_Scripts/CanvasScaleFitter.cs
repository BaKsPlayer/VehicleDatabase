using UnityEngine;
using UnityEngine.UI;

public class CanvasScaleFitter : MonoBehaviour
{
    private void Awake()
    {
        var canvasScaler = GetComponent<CanvasScaler>();

        float ipadRatio = 4f / 3;
        float iphoneRatio = 19.5f / 9;

        float currentRatio = (float)Screen.width / Screen.height;

        canvasScaler.matchWidthOrHeight = Mathf.InverseLerp(ipadRatio, iphoneRatio, currentRatio);
    }
}
