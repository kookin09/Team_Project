using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 이 코드가 하는 일:
 * 1. 화면에 별들을 생성해서 반짝이게 만듭니다
 * 2. 별들이 천천히 움직이면서 우주 느낌을 줍니다
 * 3. 클릭할 때마다 별똥별(유성) 효과를 만듭니다
 * 4. Inspector에서 별의 개수, 크기, 속도 등을 조절할 수 있습니다
 * 5. ClickEvent와 연동되어 클릭 시 자동으로 별똥별이 나타납니다
 */

public class StarrySpaceBackground : MonoBehaviour
{
    [Header("우주 배경 설정")]
    public Color spaceColor = new Color(0.05f, 0.05f, 0.15f, 1f);  // 우주 색깔 (짙은 파란색)
    public Canvas backgroundCanvas;                                   // 배경을 그릴 캔버스

    [Header("별 설정")]
    public int starCount = 100;                     // 별의 개수
    public float starSizeMin = 2f;                  // 별 최소 크기
    public float starSizeMax = 6f;                  // 별 최대 크기
    public float starMoveSpeed = 10f;               // 별 움직임 속도
    public float twinkleSpeed = 2f;                 // 반짝임 속도
    public Color starColor = Color.white;           // 별 색깔

    [Header("별똥별 설정")]
    public int shootingStarCount = 3;               // 클릭 시 생성할 별똥별 개수
    public float shootingStarSpeed = 200f;          // 별똥별 속도
    public float shootingStarLifetime = 2f;         // 별똥별 수명
    public Vector2 shootingStarSize = new Vector2(20f, 4f);  // 별똥별 크기 (길이, 두께)
    public Gradient shootingStarGradient;           // 별똥별 색깔 그라데이션

    [Header("특수 효과")]
    public bool enableStarTrails = true;            // 별 꼬리 효과
    public bool enableColorVariation = true;        // 별 색깔 다양화
    public bool enablePulseEffect = true;           // 맥박 효과

    // 내부 변수들
    private List<GameObject> stars = new List<GameObject>();
    private List<GameObject> shootingStars = new List<GameObject>();
    private Camera mainCamera;
    private RectTransform canvasRect;

    // 별 색깔 배열 (색깔 다양화용)
    private Color[] starColors = new Color[]
    {
        Color.white,
        new Color(1f, 0.9f, 0.7f),     // 따뜻한 흰색
        new Color(0.7f, 0.9f, 1f),     // 차가운 파란색
        new Color(1f, 0.8f, 0.6f),     // 노란색
        new Color(0.9f, 0.7f, 1f)      // 보라색
    };

    void Start()
    {
        // 카메라와 캔버스 설정
        SetupCameraAndCanvas();

        // 별똥별 그라데이션 기본값 설정
        SetupShootingStarGradient();

        // 별들 생성
        CreateStars();

        // ClickEvent와 연동
        SubscribeToClickEvents();

        Debug.Log("별이 반짝이는 우주 배경 생성 완료!");
    }

    void Update()
    {
        // 별들 애니메이션 업데이트
        UpdateStars();

        // 별똥별들 업데이트
        UpdateShootingStars();
    }

    // 카메라와 캔버스 초기 설정
    void SetupCameraAndCanvas()
    {
        // 메인 카메라 찾기
        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            mainCamera = FindObjectOfType<Camera>();
        }

        // 카메라 배경색을 우주색으로 설정
        if (mainCamera != null)
        {
            mainCamera.clearFlags = CameraClearFlags.SolidColor;
            mainCamera.backgroundColor = spaceColor;
        }

        // 캔버스 찾기 또는 생성
        //if (backgroundCanvas == null)
        //{
        //    backgroundCanvas = FindObjectOfType<Canvas>();
        //}

        if (backgroundCanvas != null)
        {
            canvasRect = backgroundCanvas.GetComponent<RectTransform>();
        }
        else
        {
            Debug.LogError("Canvas를 찾을 수 없습니다! Inspector에서 연결해주세요.");
        }
    }

    // 별똥별 그라데이션 기본값 설정
    void SetupShootingStarGradient()
    {
        if (shootingStarGradient == null)
        {
            shootingStarGradient = new Gradient();

            // 그라데이션 키 설정 (하얀색 → 투명)
            GradientColorKey[] colorKeys = new GradientColorKey[2];
            colorKeys[0].color = Color.white;
            colorKeys[0].time = 0f;
            colorKeys[1].color = Color.cyan;
            colorKeys[1].time = 1f;

            GradientAlphaKey[] alphaKeys = new GradientAlphaKey[3];
            alphaKeys[0].alpha = 0f;
            alphaKeys[0].time = 0f;
            alphaKeys[1].alpha = 1f;
            alphaKeys[1].time = 0.3f;
            alphaKeys[2].alpha = 0f;
            alphaKeys[2].time = 1f;

            shootingStarGradient.SetKeys(colorKeys, alphaKeys);
        }
    }

    // 별들 생성
    void CreateStars()
    {
        if (canvasRect == null) return;

        for (int i = 0; i < starCount; i++)
        {
            CreateSingleStar();
        }
    }

    // 개별 별 생성
    void CreateSingleStar()
    {
        // 별 오브젝트 생성
        GameObject star = new GameObject("Star");
        star.transform.SetParent(backgroundCanvas.transform);

        // Image 컴포넌트 추가
        UnityEngine.UI.Image starImage = star.AddComponent<UnityEngine.UI.Image>();

        // Raycast Target 끄기
        starImage.raycastTarget = false;

        // 별 크기 설정
        RectTransform rect = star.GetComponent<RectTransform>();
        float size = Random.Range(starSizeMin, starSizeMax);
        rect.sizeDelta = Vector2.one * size;

        // 별 색깔 설정
        if (enableColorVariation)
        {
            starImage.color = starColors[Random.Range(0, starColors.Length)];
        }
        else
        {
            starImage.color = starColor;
        }

        // 랜덤 위치에 배치
        float randomX = Random.Range(-canvasRect.rect.width / 2, canvasRect.rect.width / 2);
        float randomY = Random.Range(-canvasRect.rect.height / 2, canvasRect.rect.height / 2);
        rect.anchoredPosition = new Vector2(randomX, randomY);

        // 별 컴포넌트 추가 (애니메이션용)
        StarBehavior starBehavior = star.AddComponent<StarBehavior>();
        starBehavior.Initialize(twinkleSpeed, enablePulseEffect);

        // 리스트에 추가
        stars.Add(star);
    }

    // 별들 애니메이션 업데이트
    void UpdateStars()
    {
        if (canvasRect == null) return;

        foreach (GameObject star in stars)
        {
            if (star == null) continue;

            RectTransform rect = star.GetComponent<RectTransform>();

            // 별을 천천히 위로 이동
            rect.anchoredPosition += Vector2.up * starMoveSpeed * Time.deltaTime;

            // 화면 위로 나가면 아래쪽에서 다시 시작
            if (rect.anchoredPosition.y > canvasRect.rect.height / 2 + 50)
            {
                float randomX = Random.Range(-canvasRect.rect.width / 2, canvasRect.rect.width / 2);
                rect.anchoredPosition = new Vector2(randomX, -canvasRect.rect.height / 2 - 50);
            }
        }
    }

    // ClickEvent와 연동
    void SubscribeToClickEvents()
    {
        // ClickEvent의 OnAttackPerformed 이벤트에 구독
        ClickEvent.OnAttackPerformed += OnPlayerClick;
    }

    void OnDestroy()
    {
        // 이벤트 구독 해제
        ClickEvent.OnAttackPerformed -= OnPlayerClick;
    }

    // 플레이어가 클릭했을 때 호출되는 함수
    void OnPlayerClick(bool isCritical)
    {
        // 치명타면 더 많은 별똥별 생성
        int count = isCritical ? shootingStarCount * 2 : shootingStarCount;

        for (int i = 0; i < count; i++)
        {
            CreateShootingStar(isCritical);
        }

        // 기존 별들을 더 밝게 반짝이게 (클릭 피드백)
        StartCoroutine(StarClickEffect());
    }

    // 별똥별 생성
    void CreateShootingStar(bool isCritical = false)
    {
        if (canvasRect == null) return;

        // 별똥별 오브젝트 생성
        GameObject shootingStar = new GameObject("ShootingStar");
        shootingStar.transform.SetParent(backgroundCanvas.transform);

        // Image 컴포넌트 추가
        UnityEngine.UI.Image starImage = shootingStar.AddComponent<UnityEngine.UI.Image>();

        // Raycast Target 끄기
        starImage.raycastTarget = false;

        // 별똥별 크기 설정
        RectTransform rect = shootingStar.GetComponent<RectTransform>();
        Vector2 size = shootingStarSize;
        if (isCritical)
        {
            size *= 1.5f; // 치명타면 더 크게
        }
        rect.sizeDelta = size;

        // 별똥별 색깔 설정
        Color shootingColor = isCritical ? Color.yellow : Color.white;
        starImage.color = shootingColor;

        // 화면 경계에서 시작 (랜덤 방향)
        Vector2 startPos = GetRandomEdgePosition();
        rect.anchoredPosition = startPos;

        // 별똥별 이동 방향 설정 (화면 중앙을 향해)
        Vector2 direction = (Vector2.zero - startPos).normalized;

        // 별똥별 컴포넌트 추가
        ShootingStarBehavior behavior = shootingStar.AddComponent<ShootingStarBehavior>();
        behavior.Initialize(direction, shootingStarSpeed, shootingStarLifetime, shootingStarGradient);

        // 리스트에 추가
        shootingStars.Add(shootingStar);
    }

    // 화면 경계의 랜덤 위치 반환
    Vector2 GetRandomEdgePosition()
    {
        if (canvasRect == null) return Vector2.zero;

        float width = canvasRect.rect.width / 2;
        float height = canvasRect.rect.height / 2;

        // 4개 모서리 중 랜덤 선택
        int edge = Random.Range(0, 4);

        switch (edge)
        {
            case 0: // 위쪽
                return new Vector2(Random.Range(-width, width), height + 50);
            case 1: // 아래쪽
                return new Vector2(Random.Range(-width, width), -height - 50);
            case 2: // 왼쪽
                return new Vector2(-width - 50, Random.Range(-height, height));
            case 3: // 오른쪽
                return new Vector2(width + 50, Random.Range(-height, height));
            default:
                return Vector2.zero;
        }
    }

    // 별똥별들 업데이트
    void UpdateShootingStars()
    {
        // 수명이 다한 별똥별들 제거
        for (int i = shootingStars.Count - 1; i >= 0; i--)
        {
            if (shootingStars[i] == null)
            {
                shootingStars.RemoveAt(i);
            }
        }
    }

    // 클릭 시 별들이 반짝이는 효과
    IEnumerator StarClickEffect()
    {
        // 모든 별들을 잠깐 밝게
        foreach (GameObject star in stars)
        {
            if (star != null)
            {
                UnityEngine.UI.Image image = star.GetComponent<UnityEngine.UI.Image>();
                if (image != null)
                {
                    Color original = image.color;
                    image.color = Color.white;

                    // 0.1초 후 원래 색으로
                    StartCoroutine(RestoreStarColor(image, original));
                }
            }
        }

        yield return null;
    }

    // 별 색깔 복원
    IEnumerator RestoreStarColor(UnityEngine.UI.Image image, Color originalColor)
    {
        yield return new WaitForSeconds(0.1f);

        if (image != null)
        {
            image.color = originalColor;
        }
    }

    // === 외부에서 호출할 수 있는 함수들 ===

    // 수동으로 별똥별 생성
    public void CreateManualShootingStar()
    {
        CreateShootingStar(false);
    }

    // 별 개수 변경
    public void ChangeStarCount(int newCount)
    {
        // 기존 별들 제거
        foreach (GameObject star in stars)
        {
            if (star != null)
            {
                Destroy(star);
            }
        }
        stars.Clear();

        // 새로운 개수로 별들 생성
        starCount = newCount;
        CreateStars();
    }

    // 우주 색깔 변경
    public void ChangeSpaceColor(Color newColor)
    {
        spaceColor = newColor;
        if (mainCamera != null)
        {
            mainCamera.backgroundColor = spaceColor;
        }
    }
}

// 개별 별의 행동을 담당하는 컴포넌트
public class StarBehavior : MonoBehaviour
{
    private float twinkleSpeed;
    private bool enablePulse;
    private UnityEngine.UI.Image starImage;
    private Color originalColor;
    private float timer = 0f;

    public void Initialize(float speed, bool pulse)
    {
        twinkleSpeed = speed;
        enablePulse = pulse;
        starImage = GetComponent<UnityEngine.UI.Image>();
        originalColor = starImage.color;

        // 랜덤 시작 시간으로 별들이 다르게 반짝이게
        timer = Random.Range(0f, Mathf.PI * 2f);
    }

    void Update()
    {
        if (starImage == null) return;

        timer += Time.deltaTime * twinkleSpeed;

        if (enablePulse)
        {
            // 반짝이는 효과 (사인파 이용)
            float alpha = 0.3f + 0.7f * (0.5f + 0.5f * Mathf.Sin(timer));
            Color newColor = originalColor;
            newColor.a = alpha;
            starImage.color = newColor;
        }
    }
}

// 별똥별의 행동을 담당하는 컴포넌트
public class ShootingStarBehavior : MonoBehaviour
{
    private Vector2 direction;
    private float speed;
    private float lifetime;
    private float timer = 0f;
    private UnityEngine.UI.Image starImage;
    private Gradient colorGradient;

    public void Initialize(Vector2 dir, float spd, float life, Gradient gradient)
    {
        direction = dir;
        speed = spd;
        lifetime = life;
        colorGradient = gradient;
        starImage = GetComponent<UnityEngine.UI.Image>();

        // 이동 방향에 맞게 회전
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void Update()
    {
        timer += Time.deltaTime;

        // 이동
        RectTransform rect = GetComponent<RectTransform>();
        rect.anchoredPosition += direction * speed * Time.deltaTime;

        // 색깔 변화 (그라데이션 적용)
        if (starImage != null && colorGradient != null)
        {
            float progress = timer / lifetime;
            starImage.color = colorGradient.Evaluate(progress);
        }

        // 수명이 다하면 삭제
        if (timer >= lifetime)
        {
            Destroy(gameObject);
        }
    }
}