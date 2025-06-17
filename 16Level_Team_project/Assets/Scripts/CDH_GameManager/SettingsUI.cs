using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsUI : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField] private GameObject settingPanel;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private AudioMixer audioMixer;

    private void Start()
    {
        settingPanel.SetActive(false);

        if (volumeSlider != null)
        {
            volumeSlider.onValueChanged.AddListener(SetVolume);
        }
    }

    public void OpenSettings()
    {
        settingPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        settingPanel.SetActive(false);
    }

    private void SetVolume(float value)
    {
        // AudioMixer의 "MasterVolume" exposed parameter를 조절
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(value) * 20);
    }
}
