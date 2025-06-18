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

    float lastValue;
    bool isMuted;

    private void Start()
    {
        settingPanel.SetActive(false);

        if (volumeSlider != null)
        {
            volumeSlider.onValueChanged.AddListener(SetVolume);
        }

        // 볼륨 저장 불러오기 기능
        float savedVolume = PlayerPrefs.GetFloat("Volume", 1f);
        volumeSlider.value = savedVolume;
        SetVolume(savedVolume);
    }

    // 이 함수 하나로 SetActive On/Off가 가능
    public void ToggleUI()
    {
        settingPanel.SetActive(!settingPanel.activeSelf);
                                          // activeSelf는 settingPanel이 true인지 false인지 말해줌. 앞에 !(not) 을 붙여 true면 false로 바꿔줌
    }

    private void SetVolume(float value)
    {
        // AudioMixer의 "MasterVolume" exposed parameter를 조절
        audioMixer.SetFloat("Volume", Mathf.Log10(value) * 20);
        lastValue = value;   //      PlayerPrefs로 볼륨값 저장
    }

    public void OnMuteToggle()
    {
        if (!isMuted)
        {   // 음소거를 위한 코드. 매우 작은 값으로 설정해준다
            audioMixer.SetFloat("Volume", -80f);
            isMuted = true;     //      음소거 true 로 변경
        }
        else
        {
            // 음소거 하기 전 볼륨값으로 되돌려준다
            audioMixer.SetFloat("Volume", Mathf.Log10(lastValue) * 20);
            isMuted= false;     //      음소거 false 로 변경
        }
    }

}
