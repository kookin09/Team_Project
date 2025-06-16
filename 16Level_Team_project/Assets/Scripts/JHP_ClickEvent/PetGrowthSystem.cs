using UnityEngine;
using UnityEngine.UI;

/* 이 코드가 하는 일:
 * 1. 클릭 수에 따라 펫이 성장합니다 (알 → 작은펫 → 성인펫)
 * 2. Inspector에서 성장에 필요한 클릭 수를 설정할 수 있습니다
 * 3. 성인펫이 되면 자동클릭 기능을 활성화합니다
 * 4. 펫이 살짝살짝 움직이는 애니메이션이 있습니다
 * 5. ClickEvent와 자동으로 연결되어 클릭 수를 받아옵니다
 */

public class PetGrowthSystem : MonoBehaviour
{
    // Inspector에서 설정할 수 있는 값들
    [Header("성장 필요 클릭 수")]
    public int eggToSmallPetClicks = 10;    // 알에서 작은펫이 되는데 필요한 클릭 수
    public int smallToAdultPetClicks = 50;  // 작은펫에서 성인펫이 되는데 필요한 클릭 수

    [Header("펫 이미지들 (Inspector에서 드래그해서 넣으세요)")]
    public Image petImage;              // 펫을 보여줄 Image 컴포넌트
    public Sprite eggSprite;           // 알 이미지
    public Sprite smallPetSprite;      // 작은펫 이미지  
    public Sprite adultPetSprite;      // 성인펫 이미지

    [Header("애니메이션 설정")]
    public float moveSpeed = 1f;       // 위아래 움직임 속도
    public float moveAmount = 5f;      // 위아래 움직임 크기
    public float breatheSpeed = 2f;    // 숨쉬기 속도 (크기 변화)
    public float breatheAmount = 0.05f; // 숨쉬기 크기 변화량 (5%)

    [Header("진화 효과")]
    public ParticleSystem evolutionParticle;  // 진화할 때 나올 파티클
    public float evolutionEffectDuration = 2f; // 진화 효과 지속 시간
    public AudioClip evolutionSound;          // 진화 사운드 (선택사항)
    public bool enableEvolutionPause = true;  // 진화할 때 잠깐 멈출지
    public Canvas backgroundCanvas;           // 배경 파티클을 표시할 캔버스
    public GameObject pixelParticlePrefab;    // 픽셀 파티클 프리팹 (선택사항)

    [Header("배경 픽셀 파티클 설정")]
    public float pixelDuration = 3f;          // 픽셀 효과 지속 시간
    public float pixelSpawnInterval = 0.1f;   // 픽셀 생성 간격 (초)
    public int maxPixelParticles = 15;        // 최대 동시 픽셀 수
    public Vector2 pixelSize = new Vector2(15, 15); // 픽셀 크기
    public float pixelMoveSpeedMin = 80f;     // 픽셀 최소 속도
    public float pixelMoveSpeedMax = 120f;    // 픽셀 최대 속도
    public float pixelWiggleSpeedMin = 2f;    // 좌우 흔들림 최소 속도
    public float pixelWiggleSpeedMax = 4f;    // 좌우 흔들림 최대 속도
    public float pixelWiggleAmountMin = 15f;  // 좌우 흔들림 최소 크기
    public float pixelWiggleAmountMax = 25f;  // 좌우 흔들림 최대 크기

    // 내부에서 사용하는 변수들
    private PetStage currentStage = PetStage.Egg;  // 현재 펫 단계
    private ClickEvent clickEvent;                  // ClickEvent 스크립트 참조
    private Vector2 originalPosition;              // 원래 위치 저장 (UI용 Vector2)
    private Vector3 originalScale;                 // 원래 크기 저장 (숨쉬기용)
    private float animationTimer = 0f;             // 애니메이션용 타이머
    private float breatheTimer = 0f;               // 숨쉬기 애니메이션용 타이머
    private RectTransform petRectTransform;        // 펫 이미지의 RectTransform
    private bool isEvolving = false;               // 진화 중인지 확인
    private AudioSource audioSource;              // 오디오 소스 (사운드용)

    // 펫의 성장 단계를 나타내는 enum
    public enum PetStage
    {
        Egg,    // 알
        Small,  // 작은펫
        Adult   // 성인펫
    }

    // 게임 시작할 때 실행되는 함수
    void Start()
    {
        // ClickEvent 스크립트 찾기
        clickEvent = FindObjectOfType<ClickEvent>();

        // ClickEvent를 못 찾으면 경고 메시지 출력
        if (clickEvent == null)
        {
            Debug.LogWarning("ClickEvent 스크립트를 찾을 수 없습니다!");
        }
        else
        {
            // ClickEvent를 찾았다면 자동클릭을 비활성화 상태로 설정
            clickEvent.autoAttackEnabled = false;
            Debug.Log("자동클릭이 비활성화되었습니다. (성인펫이 될 때까지 대기)");
        }

        // 펫 이미지가 연결되지 않았으면 경고 메시지 출력
        if (petImage == null)
        {
            Debug.LogWarning("Pet Image가 연결되지 않았습니다!");
        }
        else
        {
            // 펫 이미지의 RectTransform 가져오기
            petRectTransform = petImage.GetComponent<RectTransform>();

            // 원래 위치와 크기 저장 (애니메이션용)
            originalPosition = petRectTransform.anchoredPosition;
            originalScale = petRectTransform.localScale;
        }

        // 처음에는 알 상태로 시작
        UpdatePetDisplay();

        // 오디오 소스 가져오기 (없으면 추가)
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null && evolutionSound != null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        Debug.Log("펫 성장 시스템 시작! 현재 단계: 알");
    }

    // 매 프레임마다 실행되는 함수
    void Update()
    {
        // 클릭 수 확인해서 펫 성장시키기
        CheckForGrowth();

        // 펫 애니메이션 (위아래로 살짝 움직임)
        AnimatePet();
    }

    // 클릭 수를 확인해서 펫을 성장시키는 함수
    void CheckForGrowth()
    {
        // ClickEvent가 없으면 성장 확인 안함
        if (clickEvent == null) return;

        // 현재 클릭 수 가져오기
        int currentClicks = clickEvent.clickCount;

        // 알 단계에서 작은펫으로 성장 확인
        if (currentStage == PetStage.Egg && currentClicks >= eggToSmallPetClicks)
        {
            GrowToSmallPet();
        }
        // 작은펫에서 성인펫으로 성장 확인
        else if (currentStage == PetStage.Small && currentClicks >= smallToAdultPetClicks)
        {
            GrowToAdultPet();
        }
    }

    // 작은펫으로 성장하는 함수
    void GrowToSmallPet()
    {
        currentStage = PetStage.Small;
        StartCoroutine(EvolutionEffect(() =>
        {
            UpdatePetDisplay();
            Debug.Log("축하합니다! 펫이 작은펫으로 성장했습니다!");
        }));
    }

    // 성인펫으로 성장하는 함수
    void GrowToAdultPet()
    {
        currentStage = PetStage.Adult;
        StartCoroutine(EvolutionEffect(() =>
        {
            UpdatePetDisplay();

            // 성인펫이 되면 자동클릭 활성화!
            if (clickEvent != null && !clickEvent.autoAttackEnabled)
            {
                clickEvent.StartAutoAttack();
                Debug.Log("축하합니다! 펫이 성인펫으로 성장했습니다! 자동클릭이 활성화되었습니다!");
            }
        }));
    }

    // 펫 이미지를 현재 단계에 맞게 업데이트하는 함수
    void UpdatePetDisplay()
    {
        // petImage가 연결되지 않았으면 업데이트 안함
        if (petImage == null) return;

        // 현재 단계에 따라 이미지 변경
        switch (currentStage)
        {
            case PetStage.Egg:
                if (eggSprite != null)
                {
                    petImage.sprite = eggSprite;
                }
                break;

            case PetStage.Small:
                if (smallPetSprite != null)
                {
                    petImage.sprite = smallPetSprite;
                }
                break;

            case PetStage.Adult:
                if (adultPetSprite != null)
                {
                    petImage.sprite = adultPetSprite;
                }
                break;
        }
    }

    // 펫이 살짝살짝 움직이는 애니메이션 함수 (위아래 + 숨쉬기)
    void AnimatePet()
    {
        // 펫 이미지가 없거나 진화 중이면 애니메이션 안함
        if (petRectTransform == null || isEvolving) return;

        // 위아래 움직임 타이머 업데이트
        animationTimer += Time.deltaTime * moveSpeed;

        // 숨쉬기 타이머 업데이트  
        breatheTimer += Time.deltaTime * breatheSpeed;

        // 사인파를 이용해서 위아래로 부드럽게 움직임
        float yOffset = Mathf.Sin(animationTimer) * moveAmount;

        // UI 이미지의 위치 변경 (anchoredPosition 사용)
        petRectTransform.anchoredPosition = originalPosition + Vector2.up * yOffset;

        // 숨쉬는 듯한 크기 변화 (사인파 이용)
        float scaleChange = 1f + Mathf.Sin(breatheTimer) * breatheAmount;

        // UI 이미지의 크기 변경
        petRectTransform.localScale = originalScale * scaleChange;
    }

    // 진화 효과 코루틴 (더 간단하게!)
    System.Collections.IEnumerator EvolutionEffect(System.Action onComplete)
    {
        isEvolving = true;

        // 진화 사운드 재생
        if (audioSource != null && evolutionSound != null)
        {
            audioSource.PlayOneShot(evolutionSound);
        }

        // 1단계: 펫이 커지기 (0.8초 동안)
        yield return StartCoroutine(ScalePet(originalScale * 1.5f, evolutionEffectDuration * 0.4f));

        // 2단계: 진화! (펫 변경 + 파티클 + 배경 효과)
        onComplete?.Invoke(); // 펫 이미지 바뀜

        // 파티클 터뜨리기
        if (evolutionParticle != null && petRectTransform != null)
        {
            Vector3 petPosition = petRectTransform.TransformPoint(Vector3.zero);
            evolutionParticle.transform.position = petPosition;
            evolutionParticle.Play();
        }

        // 배경 픽셀 효과 시작
        StartCoroutine(ShowBackgroundPixels(pixelDuration));

        // 잠깐 대기 (0.4초)
        if (enableEvolutionPause)
        {
            yield return new WaitForSeconds(evolutionEffectDuration * 0.2f);
        }

        // 3단계: 펫이 원래 크기로 돌아가기 (0.8초 동안)
        yield return StartCoroutine(ScalePet(originalScale, evolutionEffectDuration * 0.4f));

        isEvolving = false;
        Debug.Log("진화 완료!");
    }

    // 펫 크기 변경하는 간단한 함수
    System.Collections.IEnumerator ScalePet(Vector3 targetScale, float duration)
    {
        if (petRectTransform == null) yield break;

        Vector3 startScale = petRectTransform.localScale;
        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float progress = timer / duration; // 0에서 1까지

            // 부드럽게 크기 변경
            petRectTransform.localScale = Vector3.Lerp(startScale, targetScale, progress);
            yield return null;
        }

        petRectTransform.localScale = targetScale; // 정확한 크기로 설정
    }

    // 배경 픽셀 파티클 효과 (StartScreen처럼 아래에서 올라오는 효과)
    System.Collections.IEnumerator ShowBackgroundPixels(float duration)
    {
        if (backgroundCanvas == null)
        {
            Debug.LogWarning("Background Canvas가 연결되지 않았습니다!");
            yield break;
        }

        // 픽셀 파티클들을 저장할 리스트
        System.Collections.Generic.List<GameObject> pixelParticles = new System.Collections.Generic.List<GameObject>();

        // 캔버스 정보 가져오기
        RectTransform canvasRect = backgroundCanvas.GetComponent<RectTransform>();

        float timer = 0f;
        float spawnInterval = 0.1f; // 0.1초마다 생성
        int maxParticles = 15; // 최대 동시 파티클 수

        // 진화 효과용 색상들
        Color[] evolutionColors = new Color[]
        {
            new Color(1f, 0.84f, 0f, 0.8f),      // 황금색
            new Color(1f, 1f, 1f, 0.8f),         // 하얀색  
            new Color(1f, 0.6f, 0.8f, 0.8f),     // 분홍색
            new Color(0.8f, 0.9f, 1f, 0.8f)      // 연한 파란색
        };

        while (timer < duration)
        {
            timer += Time.deltaTime;

            // 파티클 생성 간격 체크
            if (timer % spawnInterval < Time.deltaTime && pixelParticles.Count < maxParticles)
            {
                CreateEvolutionPixel(canvasRect, evolutionColors, pixelParticles);
            }

            // 화면 밖으로 나간 파티클 제거
            for (int i = pixelParticles.Count - 1; i >= 0; i--)
            {
                if (pixelParticles[i] == null)
                {
                    pixelParticles.RemoveAt(i);
                }
            }

            yield return null;
        }

        // 남은 파티클들이 자연스럽게 사라질 때까지 대기
        yield return new WaitForSeconds(2f);

        // 남은 파티클들 정리
        foreach (GameObject particle in pixelParticles)
        {
            if (particle != null)
            {
                Destroy(particle);
            }
        }
    }

    // 개별 픽셀 파티클 생성
    void CreateEvolutionPixel(RectTransform canvasRect, Color[] colors, System.Collections.Generic.List<GameObject> particleList)
    {
        // 픽셀 파티클 생성
        GameObject pixel = new GameObject("EvolutionPixel");
        pixel.transform.SetParent(backgroundCanvas.transform);

        // Image 컴포넌트 추가
        UnityEngine.UI.Image pixelImage = pixel.AddComponent<UnityEngine.UI.Image>();

        // 크기 설정 (Inspector에서 설정한 크기 사용)
        RectTransform rect = pixel.GetComponent<RectTransform>();
        rect.sizeDelta = pixelSize;

        // 랜덤 색상
        pixelImage.color = colors[Random.Range(0, colors.Length)];

        // 화면 아래쪽에서 시작 (랜덤 X 위치)
        float randomX = Random.Range(-canvasRect.rect.width / 2, canvasRect.rect.width / 2);
        rect.anchoredPosition = new Vector2(randomX, -canvasRect.rect.height / 2 - 30);

        // 파티클 리스트에 추가
        particleList.Add(pixel);

        // 픽셀 애니메이션 시작
        StartCoroutine(MoveEvolutionPixel(pixel, canvasRect));
    }

    // 픽셀 파티클 움직임
    System.Collections.IEnumerator MoveEvolutionPixel(GameObject pixel, RectTransform canvasRect)
    {
        RectTransform rect = pixel.GetComponent<RectTransform>();
        UnityEngine.UI.Image image = pixel.GetComponent<UnityEngine.UI.Image>();

        // Inspector에서 설정한 값들 사용
        float moveSpeed = Random.Range(pixelMoveSpeedMin, pixelMoveSpeedMax);
        float wiggleSpeed = Random.Range(pixelWiggleSpeedMin, pixelWiggleSpeedMax);
        float wiggleAmount = Random.Range(pixelWiggleAmountMin, pixelWiggleAmountMax);

        while (pixel != null && rect.anchoredPosition.y < canvasRect.rect.height / 2 + 50)
        {
            // 위로 이동
            rect.anchoredPosition += Vector2.up * moveSpeed * Time.deltaTime;

            // 좌우 흔들림
            float wiggle = Mathf.Sin(Time.time * wiggleSpeed) * wiggleAmount;
            Vector2 currentPos = rect.anchoredPosition;
            rect.anchoredPosition = new Vector2(currentPos.x + wiggle * Time.deltaTime, currentPos.y);

            // 점점 투명해지기 (위로 올라갈수록)
            float alpha = Mathf.Lerp(0.8f, 0f, (rect.anchoredPosition.y + canvasRect.rect.height / 2) / canvasRect.rect.height);
            Color currentColor = image.color;
            image.color = new Color(currentColor.r, currentColor.g, currentColor.b, alpha);

            yield return null;
        }

        // 화면 밖으로 나가면 삭제
        if (pixel != null)
        {
            Destroy(pixel);
        }
    }

    // === 외부에서 호출할 수 있는 함수들 ===

    // 현재 펫 단계 확인하는 함수 (다른 스크립트에서 호출 가능)
    public PetStage GetCurrentStage()
    {
        return currentStage;
    }

    // 다음 성장까지 필요한 클릭 수 확인하는 함수
    public int GetClicksNeededForNextStage()
    {
        if (clickEvent == null) return 0;

        int currentClicks = clickEvent.clickCount;

        switch (currentStage)
        {
            case PetStage.Egg:
                return Mathf.Max(0, eggToSmallPetClicks - currentClicks);

            case PetStage.Small:
                return Mathf.Max(0, smallToAdultPetClicks - currentClicks);

            case PetStage.Adult:
                return 0; // 이미 다 자란 상태
        }

        return 0;
    }

    // 펫을 강제로 특정 단계로 설정하는 함수 (테스트용)
    public void SetPetStage(PetStage newStage)
    {
        currentStage = newStage;
        UpdatePetDisplay();
        Debug.Log($"펫 단계가 {newStage}로 설정되었습니다!");
    }
}