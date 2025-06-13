using UnityEngine;
using UnityEngine.UI; // 혹은 TMPro 사용 시 using TMPro;

public class StageUIManager : MonoBehaviour
{
    public Text stageText;   // TMP_Text stageText;

    public void UpdateStageUI(int stage)
    {
        stageText.text = "Stage: " + stage;
    }
}
