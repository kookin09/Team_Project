using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterClicker : MonoBehaviour
{
    [Header("Click Settings")]
    public int clickValue = 1;
    public GameObject clickEffectPrefab; // 나중에 만들 예정

    [Header("UI References")]
    public TextMeshProUGUI scoreText; // 점수 표시용 

    private int totalScore = 0;
    private Button characterButton;
    private RectTransform characterTransform;

    void Start()
    {
        characterButton = GetComponent<Button>();
        characterTransform = GetComponent<RectTransform>();

        // 버튼 클릭 이벤트 연결
        characterButton.onClick.AddListener(OnCharacterClicked);
    }

    public void OnCharacterClicked()
    {
        // 점수 증가
        totalScore += clickValue;

        if (scoreText != null) // 점수 텍스트가 연결되어 있다면
        {
            scoreText.text = "Score: " + totalScore;
        }

        // 콘솔에 출력 (테스트용)
        Debug.Log("클릭! 현재 점수: " + totalScore);

        // 캐릭터 크기 변화 효과
        StartCoroutine(ClickAnimation());


    }

    private System.Collections.IEnumerator ClickAnimation()
    {
        // 원래 크기 저장
        Vector3 originalScale = characterTransform.localScale;

        // 크게 만들기
        characterTransform.localScale = originalScale * 1.1f;

        // 0.1초 대기
        yield return new WaitForSeconds(0.1f);

        // 원래 크기로 복구
        characterTransform.localScale = originalScale;
    }
}