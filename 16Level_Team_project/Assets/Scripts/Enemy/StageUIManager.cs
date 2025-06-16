using UnityEngine;
using TMPro;

public class StageUIManager : MonoBehaviour
{
    public TextMeshProUGUI stageText;   
    public TextMeshProUGUI killCountText;
    public TextMeshProUGUI goldText;

    public void UpdateStageUI(int stage)
    {
        stageText.text = "Stage: " + stage;
    }
    public void UpdateKillCountUI(int killed, int required)
    {
        killCountText.text = killed + " / " + required;
    }
    public void UpdateGoldUI(int gold)
    {
        goldText.text = "Gold: " + gold;
    }
}
