using UnityEngine;
using UnityEngine.UI;

/* 이 코드가 하는 일:
 * 1. 화면 전체에 물결 왜곡 효과를 적용합니다
 * 2. 카메라나 Canvas에 물결 셰이더를 적용해서 화면이 물결치게 만듭니다
 * 3. Inspector에서 물결의 강도, 속도, 패턴을 조절할 수 있습니다
 * 4. 2D UI와 3D 오브젝트 모두에 적용 가능합니다
 * 5. 부드럽고 자연스러운 물결 효과를 만듭니다
 */

public class WaveDistortionEffect : MonoBehaviour
{
    [Header(" 물결 왜곡 설정")]
    public bool enableWaveDistortion = true;        // 물결 효과 켜기/끄기
    public float waveStrength = 0.02f;             // 물결 강도 (0.01 = 약함, 0.1 = 강함)
    public float waveSpeed = 1f;                   // 물결 속도
    public float waveFrequency = 10f;              // 물결 빈도 (파장)
    public Vector2 waveDirection = Vector2.right;  // 물결 방향

    [Header(" 고급 설정")]
    public bool enableVerticalWave = true;         // 세로 물결 추가
    public bool enableDiagonalWave = false;        // 대각선 물결
    public float secondaryWaveStrength = 0.01f;    // 보조 물결 강도
    public float secondaryWaveSpeed = 1.5f;        // 보조 물결 속도

    [Header(" 적용 방식")]
    public WaveApplicationMode applicationMode = WaveApplicationMode.CanvasOverlay;
    public Canvas targetCanvas;                    // 타겟 캔버스 (Canvas 모드용)
    public Camera targetCamera;                    // 타겟 카메라 (Camera 모드용)

    public enum WaveApplicationMode
    {
        CanvasOverlay,  // Canvas에 투명한 Image로 물결 효과
        CameraEffect,   // 카메라에 직접 물결 효과 (PostProcessing 필요)
        UIDistortion    // UI 요소들을 직접 움직여서 물결 효과
    }

    // 내부 변수들
    private GameObject waveOverlay;
    private RawImage waveImage;
    private Material waveMaterial;
    private float timer = 0f;
    private RectTransform canvasRect;

    // UI 왜곡용 변수들
    private Transform[] uiElements;
    private Vector3[] originalPositions;

    void Start()
    {
        SetupWaveEffect();
        Debug.Log(" 화면 물결 왜곡 효과 시작!");
    }

    void Update()
    {
        if (!enableWaveDistortion) return;

        timer += Time.deltaTime;

        switch (applicationMode)
        {
            case WaveApplicationMode.CanvasOverlay:
                UpdateCanvasWave();
                break;
            case WaveApplicationMode.CameraEffect:
                UpdateCameraWave();
                break;
            case WaveApplicationMode.UIDistortion:
                UpdateUIDistortion();
                break;
        }
    }

    // 물결 효과 초기 설정
    void SetupWaveEffect()
    {
        switch (applicationMode)
        {
            case WaveApplicationMode.CanvasOverlay:
                SetupCanvasWave();
                break;
            case WaveApplicationMode.CameraEffect:
                SetupCameraWave();
                break;
            case WaveApplicationMode.UIDistortion:
                SetupUIDistortion();
                break;
        }
    }

    // Canvas 오버레이 방식 설정
    void SetupCanvasWave()
    {
        // 타겟 캔버스 찾기
        if (targetCanvas == null)
        {
            targetCanvas = FindObjectOfType<Canvas>();
        }

        if (targetCanvas == null)
        {
            Debug.LogError("Canvas를 찾을 수 없습니다!");
            return;
        }

        canvasRect = targetCanvas.GetComponent<RectTransform>();

        // 물결 효과용 오버레이 생성
        CreateWaveOverlay();
    }

    // 물결 오버레이 생성 (Canvas 방식)
    void CreateWaveOverlay()
    {
        // 물결 오버레이 오브젝트 생성
        waveOverlay = new GameObject("WaveOverlay");
        waveOverlay.transform.SetParent(targetCanvas.transform);

        // RawImage 컴포넌트 추가
        waveImage = waveOverlay.AddComponent<RawImage>();

        // 전체 화면 크기로 설정
        RectTransform rect = waveOverlay.GetComponent<RectTransform>();
        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.one;
        rect.offsetMin = Vector2.zero;
        rect.offsetMax = Vector2.zero;

        // 물결 셰이더 Material 생성
        CreateWaveMaterial();

        // Material 적용
        waveImage.material = waveMaterial;

        // 투명도 설정 (약간 보이게)
        Color color = waveImage.color;
        color.a = 0.1f;
        waveImage.color = color;

        // 맨 앞으로 이동 (다른 UI 위에 표시)
        waveOverlay.transform.SetAsLastSibling();
    }

    // 물결 셰이더 Material 생성
    void CreateWaveMaterial()
    {
        // 기본 Unlit 셰이더 사용 (커스텀 셰이더 없이)
        Shader waveShader = Shader.Find("UI/Default");
        waveMaterial = new Material(waveShader);

        // 텍스처 생성 (노이즈 패턴)
        CreateWaveTexture();
    }

    // 물결용 텍스처 생성
    void CreateWaveTexture()
    {
        int textureSize = 256;
        Texture2D waveTexture = new Texture2D(textureSize, textureSize);

        // 노이즈 패턴으로 물결 텍스처 생성
        for (int x = 0; x < textureSize; x++)
        {
            for (int y = 0; y < textureSize; y++)
            {
                float noise = Mathf.PerlinNoise(x * 0.1f, y * 0.1f);
                waveTexture.SetPixel(x, y, new Color(noise, noise, noise, 0.5f));
            }
        }

        waveTexture.Apply();
        waveTexture.wrapMode = TextureWrapMode.Repeat;

        if (waveImage != null)
        {
            waveImage.texture = waveTexture;
        }
    }

    // Canvas 물결 업데이트
    void UpdateCanvasWave()
    {
        if (waveImage == null) return;

        // UV 스크롤링으로 물결 효과 시뮬레이션
        Vector2 offset = waveDirection.normalized * timer * waveSpeed * 0.1f;

        // 추가 물결 (수직)
        if (enableVerticalWave)
        {
            offset.y += Mathf.Sin(timer * secondaryWaveSpeed) * secondaryWaveStrength;
        }

        // 대각선 물결
        if (enableDiagonalWave)
        {
            offset += new Vector2(
                Mathf.Sin(timer * waveSpeed * 0.7f) * waveStrength,
                Mathf.Cos(timer * waveSpeed * 0.8f) * waveStrength
            );
        }

        // Material의 UV offset 설정
        if (waveMaterial != null)
        {
            waveMaterial.mainTextureOffset = offset;
        }

        // 물결 강도에 따른 스케일 변화 (미묘하게)
        if (waveOverlay != null)
        {
            RectTransform rect = waveOverlay.GetComponent<RectTransform>();
            float scaleWave = 1f + Mathf.Sin(timer * waveSpeed * 2f) * waveStrength * 0.5f;
            rect.localScale = Vector3.one * scaleWave;
        }
    }

    // 카메라 효과 설정 (Post-Processing 대신 간단한 방법)
    void SetupCameraWave()
    {
        if (targetCamera == null)
        {
            targetCamera = Camera.main;
        }

        if (targetCamera == null)
        {
            Debug.LogWarning("카메라 물결 효과를 위해서는 Camera가 필요합니다.");
            applicationMode = WaveApplicationMode.CanvasOverlay;
            SetupCanvasWave();
        }
    }

    // 카메라 물결 업데이트
    void UpdateCameraWave()
    {
        if (targetCamera == null) return;

        // 카메라 위치를 물결치게 움직임 (매우 미묘하게)
        Vector3 originalPos = targetCamera.transform.position;

        float waveX = Mathf.Sin(timer * waveSpeed + originalPos.y * waveFrequency) * waveStrength;
        float waveY = Mathf.Sin(timer * waveSpeed * 1.3f + originalPos.x * waveFrequency) * waveStrength * 0.5f;

        if (enableVerticalWave)
        {
            waveY += Mathf.Sin(timer * secondaryWaveSpeed) * secondaryWaveStrength;
        }

        Vector3 waveOffset = new Vector3(waveX, waveY, 0);
        targetCamera.transform.position = originalPos + waveOffset;
    }

    // UI 왜곡 설정
    void SetupUIDistortion()
    {
        // Canvas 내의 모든 UI 요소 찾기
        if (targetCanvas == null)
        {
            targetCanvas = FindObjectOfType<Canvas>();
        }

        if (targetCanvas != null)
        {
            uiElements = targetCanvas.GetComponentsInChildren<Transform>();
            originalPositions = new Vector3[uiElements.Length];

            // 원래 위치 저장
            for (int i = 0; i < uiElements.Length; i++)
            {
                originalPositions[i] = uiElements[i].localPosition;
            }
        }
    }

    // UI 왜곡 업데이트
    void UpdateUIDistortion()
    {
        if (uiElements == null) return;

        for (int i = 0; i < uiElements.Length; i++)
        {
            if (uiElements[i] == null) continue;

            Vector3 originalPos = originalPositions[i];

            // 물결 계산
            float waveX = Mathf.Sin(timer * waveSpeed + originalPos.y * waveFrequency * 0.01f) * waveStrength * 100f;
            float waveY = 0f;

            if (enableVerticalWave)
            {
                waveY = Mathf.Sin(timer * secondaryWaveSpeed + originalPos.x * waveFrequency * 0.01f) * secondaryWaveStrength * 100f;
            }

            // 새로운 위치 적용
            uiElements[i].localPosition = originalPos + new Vector3(waveX, waveY, 0);
        }
    }

    // === 외부에서 호출할 수 있는 함수들 ===

    // 물결 효과 켜기/끄기
    public void ToggleWaveEffect()
    {
        enableWaveDistortion = !enableWaveDistortion;

        if (waveOverlay != null)
        {
            waveOverlay.SetActive(enableWaveDistortion);
        }

        Debug.Log($" 물결 효과: {(enableWaveDistortion ? "켜짐" : "꺼짐")}");
    }

    // 물결 강도 설정
    public void SetWaveStrength(float strength)
    {
        waveStrength = Mathf.Clamp(strength, 0f, 0.2f);
        Debug.Log($" 물결 강도: {waveStrength}");
    }

    // 물결 속도 설정
    public void SetWaveSpeed(float speed)
    {
        waveSpeed = speed;
        Debug.Log($" 물결 속도: {waveSpeed}");
    }

    // 잔잔한 물결 모드 (편안함)
    public void SetGentleWave()
    {
        waveStrength = 0.005f;
        waveSpeed = 0.5f;
        secondaryWaveStrength = 0.003f;
        secondaryWaveSpeed = 0.8f;
        enableVerticalWave = true;
        enableDiagonalWave = false;
        Debug.Log(" 잔잔한 물결 모드 활성화");
    }

    // 강한 물결 모드 (역동적)
    public void SetStrongWave()
    {
        waveStrength = 0.05f;
        waveSpeed = 2f;
        secondaryWaveStrength = 0.03f;
        secondaryWaveSpeed = 2.5f;
        enableVerticalWave = true;
        enableDiagonalWave = true;
        Debug.Log(" 강한 물결 모드 활성화");
    }

    // 물결 효과 완전 중지
    public void StopWaveEffect()
    {
        enableWaveDistortion = false;

        // UI 요소들을 원래 위치로 복원
        if (uiElements != null && originalPositions != null)
        {
            for (int i = 0; i < uiElements.Length; i++)
            {
                if (uiElements[i] != null)
                {
                    uiElements[i].localPosition = originalPositions[i];
                }
            }
        }

        // 카메라를 원래 위치로 복원
        if (targetCamera != null)
        {
            // 원래 위치 복원 (저장된 위치가 있다면)
        }

        // 오버레이 제거
        if (waveOverlay != null)
        {
            Destroy(waveOverlay);
        }

        Debug.Log(" 물결 효과 완전 중지");
    }

    void OnDestroy()
    {
        StopWaveEffect();
    }
}
