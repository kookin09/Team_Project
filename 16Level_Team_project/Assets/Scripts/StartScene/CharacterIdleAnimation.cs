using UnityEngine;

public class CharacterIdleAnimation : MonoBehaviour
{
    [Header("Animation Settings")]
    public float scaleAmount = 0.05f;    // 크기 변화량 (5%)
    public float animationSpeed = 1f;    // 애니메이션 속도

    private Vector3 originalScale;
    private float timer = 0f;

    void Start()
    {
        // 원본 크기 저장
        originalScale = transform.localScale;
    }

    void Update()
    {
        // 시간 업데이트
        timer += Time.deltaTime * animationSpeed;

        // 사인파를 이용해 부드러운 크기 변화
        float scale = 1f + Mathf.Sin(timer) * scaleAmount;

        // 크기 적용
        transform.localScale = originalScale * scale;
    }
}
