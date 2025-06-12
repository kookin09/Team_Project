using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/*
 * 클리커 게임 - 클릭 이벤트 + 파티클 효과 + 자동 공격 스크립트
 * 
 * 이 코드가 하는 일:
 * 1. 오브젝트를 마우스로 클릭하면 클릭 횟수가 증가합니다
 * 2. 화면에 현재 클릭 횟수를 표시합니다  
 * 3. 클릭할 때마다 오브젝트가 살짝 커졌다가 원래 크기로 돌아오는 효과를 줍니다
 * 4. 클릭할 때마다 파티클 시스템을 사용해서 부스러기 효과를 만듭니다
 * 5. 코루틴을 사용한 자동 공격 기능이 있습니다
 * 6. 콘솔창에 클릭 정보를 출력합니다 (디버깅용)
 */

public class ClickEvent : MonoBehaviour
{
    // 이 선언부들은 이런것들을 선언했습니다.
    // 클릭 횟수를 저장하는 변수
    // UI에 표시할 TextMeshPro 텍스트 (Inspector에서 연결)
    // 파티클 시스템 (Inspector에서 연결)
    // 자동 공격 관련 변수들
    // 자동 공격 활성화 여부
    // 자동 공격 간격 (초)
    // 코루틴 참조 저장용
    // 원래 크기 저장용 (클릭 효과 후 되돌리기 위해)
    // 치명타 확률 (0~100 사이)
    // 치명타 파티클 시스템 (Inspector에서 연결)
    public int clickCount = 0;
    public TextMeshProUGUI clickCountText;
    public ParticleSystem debrisParticle;
    public bool autoAttackEnabled = false;
    public float autoAttackInterval = 1.0f;
    private Coroutine autoAttackCoroutine;
    private Vector3 originalScale;
    public float criticalChance = 30f;
    public ParticleSystem criticalParticle;

    // 게임 시작 시 초기화 함수
    // 이 함수가 하는 일: 
    // 게임이 시작되자마자 화면에 "Combo: 0" 같은 초기 텍스트를 표시합니다
    // Inspector에서 autoAttackEnabled가 체크되어 있으면 게임 시작과 함께 자동 공격을 시작합니다
    // 원래 크기를 저장해둡니다 (클릭 효과 후 되돌리기 위해)
    void Start()
    {
        originalScale = transform.localScale;  // 원래 크기 저장
        UpdateClickText();

        if (autoAttackEnabled)
        {
            StartAutoAttack();
        }
    }

    // 마우스 클릭 감지 함수
    // 이 함수가 하는 일: 수동 클릭 공격을 실행합니다
    void OnMouseDown()
    {
        PerformAttack();
    }

    // 실제 공격 수행 함수 (수동/자동 공통)
    // 이 함수가 하는 일:
    // 클릭할 때마다 숫자를 1씩 증가시킵니다
    // 치명타 확률을 계산해서 치명타인지 판단합니다
    // 콘솔창에 현재 클릭 수를 출력해서 제대로 작동하는지 확인할 수 있습니다
    // 화면의 텍스트를 새로운 클릭 수로 업데이트합니다
    // 클릭했을 때 시각적 효과들(크기 변화 + 파티클)을 실행합니다
    void PerformAttack()
    {
        clickCount++;

        // 치명타 판정 (0~100 사이 랜덤 숫자가 설정한 확률보다 작으면 치명타)
        bool isCritical = Random.Range(0f, 100f) < criticalChance;

        if (isCritical)
        {
            Debug.Log("치명타!");
        }
        UpdateClickText();
        ClickEffect(isCritical); // 치명타 여부를 전달
    }

    // 화면 텍스트 업데이트 함수
    // 이 함수가 하는 일: TextMeshPro 텍스트가 연결되어 있는지 확인하고, 연결되어 있다면 "Combo: 숫자" 형태로 텍스트를 업데이트합니다
    void UpdateClickText()
    {
        if (clickCountText != null)
        {
            clickCountText.text = "Combo: " + clickCount;
        }
    }

    // 클릭 시각적 효과 함수
    // 이 함수가 하는 일:
    // 클릭한 오브젝트를 1.2배 크게 만듭니다 (치명타면 1.5배)
    // 0.2초 후에 ResetScale 함수를 실행해서 원래 크기로 돌아가게 합니다
    // 치명타 여부에 따라 다른 파티클 효과를 실행합니다
    void ClickEffect(bool isCritical = false)
    {
        // 치명타면 더 크게 확대
        float scaleMultiplier = isCritical ? 1.5f : 1.2f;
        transform.localScale = originalScale * scaleMultiplier;

        Invoke("ResetScale", 0.2f);
        PlayDebrisEffect(isCritical); // 치명타 여부를 전달
    }

    // 오브젝트 크기 리셋 함수
    // 이 함수가 하는 일: 오브젝트의 크기를 원래 크기로 되돌립니다
    void ResetScale()
    {
        transform.localScale = originalScale;  // 저장된 원래 크기로 복원
    }

    // 파티클 효과 재생 함수
    // 이 함수가 하는 일:
    // 치명타 여부에 따라 다른 파티클을 재생합니다
    // 치명타면 빨간색 파티클, 일반 공격이면 노란색 파티클
    // 파티클 시스템의 위치를 클릭한 오브젝트 위치로 이동시킵니다
    // 파티클 효과를 재생합니다
    // 파티클이 연결되지 않았을 때 콘솔에 경고 메시지를 출력합니다
    void PlayDebrisEffect(bool isCritical = false)
    {
        ParticleSystem targetParticle;

        // 치명타 여부에 따라 사용할 파티클 선택
        if (isCritical && criticalParticle != null)
        {
            targetParticle = criticalParticle;
        }
        else
        {
            targetParticle = debrisParticle;
        }

        if (targetParticle != null)
        {
            targetParticle.transform.position = transform.position;
            targetParticle.Play();
        }
        else
        {
            string particleType = isCritical ? "치명타" : "일반";
            Debug.LogWarning(particleType + " 파티클 시스템이 연결되지 않았습니다!");
        }
    }

    // 자동 공격 시작 함수
    // 이 함수가 하는 일:
    // 이미 자동 공격이 실행 중이라면 먼저 중지합니다
    // 새로운 자동 공격 코루틴을 시작하고 참조를 저장합니다
    public void StartAutoAttack()
    {
        if (autoAttackCoroutine != null)
        {
            StopCoroutine(autoAttackCoroutine);
        }

        autoAttackCoroutine = StartCoroutine(AutoAttackRoutine());
        autoAttackEnabled = true;

        Debug.Log("자동 공격 시작! 간격: " + autoAttackInterval + "초");
    }

    // 자동 공격 중지 함수
    // 이 함수가 하는 일: 실행 중인 자동 공격 코루틴을 중지합니다
    public void StopAutoAttack()
    {
        if (autoAttackCoroutine != null)
        {
            StopCoroutine(autoAttackCoroutine);
            autoAttackCoroutine = null;
        }

        autoAttackEnabled = false;
        Debug.Log("자동 공격 중지!");
    }

    // 자동 공격 코루틴 (핵심!)
    // 이 함수가 하는 일:
    // 무한 반복하면서 자동으로 공격을 수행합니다
    // 설정된 시간(autoAttackInterval)만큼 기다립니다
    // 자동 공격이 여전히 활성화되어 있는지 확인합니다
    // 실제 공격을 수행합니다 (클릭과 동일한 효과)
    private IEnumerator AutoAttackRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(autoAttackInterval);

            if (autoAttackEnabled)
            {
                PerformAttack();
            }
        }
    }

    // 자동 공격 토글 함수 (켜기/끄기)
    // 이 함수가 하는 일: 자동 공격이 켜져있으면 끄고, 꺼져있으면 켭니다
    public void ToggleAutoAttack()
    {
        if (autoAttackEnabled)
        {
            StopAutoAttack();
        }
        else
        {
            StartAutoAttack();
        }
    }
}