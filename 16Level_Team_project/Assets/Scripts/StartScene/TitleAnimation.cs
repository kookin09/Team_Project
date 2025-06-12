using UnityEngine;
using TMPro;

public class TitleAnimation : MonoBehaviour
{
    [Header("Animation Settings")]
    public float glowSpeed = 2f;        // 빛나는 속도
    public float moveAmount = 5f;       // 위아래 움직임 정도
    public float moveSpeed = 1f;        // 움직임 속도

    private TextMeshProUGUI titleText;
    private RectTransform rectTransform;
    private Vector3 originalPosition;
    private Color originalColor;
    private float timer = 0f;

    void Start()
    {
        titleText = GetComponent<TextMeshProUGUI>();
        rectTransform = GetComponent<RectTransform>();
        originalPosition = rectTransform.anchoredPosition;
        originalColor = titleText.color;
    }

    void Update()
    {
        timer += Time.deltaTime;

        // 위아래 부드러운 움직임
        float yOffset = Mathf.Sin(timer * moveSpeed) * moveAmount;
        rectTransform.anchoredPosition = originalPosition + Vector3.up * yOffset;

        // 글로우 효과 (알파값 변화)
        float alpha = 0.8f + Mathf.Sin(timer * glowSpeed) * 0.2f; // 0.6 ~ 1.0
        titleText.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
    }
}