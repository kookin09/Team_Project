using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Hover Settings")]
    public float scaleAmount = 1.1f;        // 호버 시 크기 (110%)
    public float animationSpeed = 5f;       // 애니메이션 속도
    public Color hoverColor = Color.white;  // 호버 시 색상

    private Vector3 originalScale;
    private Color originalColor;
    private Image buttonImage;
    private Vector3 targetScale;
    private Color targetColor;

    void Start()
    {
        originalScale = transform.localScale;
        targetScale = originalScale;

        buttonImage = GetComponent<Image>();
        originalColor = buttonImage.color;
        targetColor = originalColor;
    }

    void Update()
    {
        // 부드러운 크기 변화
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, animationSpeed * Time.deltaTime);

        // 부드러운 색상 변화
        buttonImage.color = Color.Lerp(buttonImage.color, targetColor, animationSpeed * Time.deltaTime);
    }

    // 마우스가 버튼 위에 올라왔을 때
    public void OnPointerEnter(PointerEventData eventData)
    {
        targetScale = originalScale * scaleAmount;
        targetColor = hoverColor;
    }

    // 마우스가 버튼에서 벗어났을 때
    public void OnPointerExit(PointerEventData eventData)
    {
        targetScale = originalScale;
        targetColor = originalColor;
    }
}