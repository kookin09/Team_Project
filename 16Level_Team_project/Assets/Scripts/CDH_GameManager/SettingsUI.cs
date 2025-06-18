using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.EventSystems;

public class SettingsUI : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField] private GameObject settingPanel;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Toggle muteToggle; // 음소거 체크박스

    float lastValue;
    bool isMuted;

    private void Start()
    {
        settingPanel.SetActive(false);

        if (volumeSlider != null)
        {
            volumeSlider.onValueChanged.AddListener(SetVolume);
        }

        // 음소거 토글 연결
        if (muteToggle != null)
        {
            muteToggle.onValueChanged.AddListener(OnMuteToggleChanged);
        }

        // 볼륨 저장 불러오기 기능
        float savedVolume = PlayerPrefs.GetFloat("Volume", 1f);
        volumeSlider.value = savedVolume;
        SetVolume(savedVolume);

        // 시작할 때는 음소거 해제 상태 (체크박스 해제)
        isMuted = false;
        muteToggle.isOn = false; // 체크박스 해제 = 소리 나옴
    }

    public void ToggleUI()
    {
        settingPanel.SetActive(!settingPanel.activeSelf);
    }

    private void SetVolume(float value)
    {
        // 볼륨 슬라이더를 움직이면 음소거 해제
        if (isMuted && value > 0)
        {
            isMuted = false;

            // 코루틴으로 다음 프레임에 UI 업데이트
            StartCoroutine(UpdateMuteToggleNextFrame(false));

            Debug.Log("볼륨 슬라이더 조작으로 음소거 해제됨");
        }

        audioMixer.SetFloat("Volume", Mathf.Log10(value) * 20);
        lastValue = value;
    }

    // 다음 프레임에 체크박스 업데이트하는 코루틴
    private System.Collections.IEnumerator UpdateMuteToggleNextFrame(bool value)
    {
        yield return null; // 한 프레임 대기

        if (muteToggle != null)
        {
            muteToggle.onValueChanged.RemoveListener(OnMuteToggleChanged);
            muteToggle.isOn = value;
            muteToggle.onValueChanged.AddListener(OnMuteToggleChanged);
        }
    }

    // 체크박스가 변경될 때 호출되는 함수
    private void OnMuteToggleChanged(bool isChecked)
    {
        isMuted = isChecked;

        if (isChecked)
        {
            audioMixer.SetFloat("Volume", -80f);
            Debug.Log("음소거 ON");
        }
        else
        {
            audioMixer.SetFloat("Volume", Mathf.Log10(lastValue) * 20);
            Debug.Log("음소거 OFF");
        }
    }
}