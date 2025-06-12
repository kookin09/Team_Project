using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BackgroundParticles : MonoBehaviour
{
    [Header("Particle Settings")]
    public GameObject particlePrefab;        // 파티클로 사용할 프리팹
    public int particleCount = 10;           // 동시에 표시될 파티클 수
    public float spawnInterval = 2f;         // 생성 간격
    public float moveSpeed = 50f;            // 움직임 속도
    public Color[] particleColors;           // 파티클 색상들



    private RectTransform canvasRect;

    void Start()
    {
        canvasRect = GetComponent<RectTransform>();

        // 색상 배열이 비어있으면 기본 색상 설정
        if (particleColors.Length == 0)
        {
            particleColors = new Color[]
            {
                new Color(1f, 0.84f, 0f, 0.7f),      // 노란색
                new Color(0.3f, 0.9f, 0.8f, 0.7f),   // 청록색
                new Color(1f, 0.4f, 0.6f, 0.7f),     // 분홍색
                new Color(0.6f, 0.8f, 0.4f, 0.7f)    // 연두색
            };
        }

        StartCoroutine(SpawnParticles());
    }

    IEnumerator SpawnParticles()
    {
        while (true)
        {
            // 현재 파티클 수 확인
            if (transform.childCount < particleCount)
            {
                CreateParticle();
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void CreateParticle()
    {
        // 간단한 파티클 생성 (Image 사용)
        GameObject particle = new GameObject("Particle");
        particle.transform.SetParent(transform);

        // Image 컴포넌트 추가
        Image particleImage = particle.AddComponent<Image>();

        // 작은 사각형으로 설정
        RectTransform rect = particle.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(20, 20);

        // 랜덤 색상
        particleImage.color = particleColors[Random.Range(0, particleColors.Length)];

        // 화면 아래쪽에서 시작
        float randomX = Random.Range(-canvasRect.rect.width / 2, canvasRect.rect.width / 2);
        rect.anchoredPosition = new Vector2(randomX, -canvasRect.rect.height / 2 - 50);

        // 파티클 애니메이션 시작
        StartCoroutine(MoveParticle(particle));
    }

    IEnumerator MoveParticle(GameObject particle)
    {
        RectTransform rect = particle.GetComponent<RectTransform>();

        while (particle != null && rect.anchoredPosition.y < canvasRect.rect.height / 2 + 50)
        {
            // 위로 이동
            rect.anchoredPosition += Vector2.up * moveSpeed * Time.deltaTime;

            // 살짝 좌우로 흔들림
            float wiggle = Mathf.Sin(Time.time * 3f) * 10f;
            rect.anchoredPosition = new Vector2(rect.anchoredPosition.x + wiggle * Time.deltaTime, rect.anchoredPosition.y);

            yield return null;
        }

        // 화면 밖으로 나가면 삭제
        if (particle != null)
        {
            Destroy(particle);
        }
    }
}