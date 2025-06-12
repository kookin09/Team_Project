[System.Serializable]
public class PlayerData
{
    public int currentStage;        //      현재 스테이지
    public int gold;                //      보유 골드

    public int attackLevel;         //      공격력 레벨
    public int baseAttackLevel;     //      기본 공격력 레벨

    public int critLevel;           //      치명타 레벨
    public float critChance;        //      치명타 확률
    public float critMulti;         //      치명타 배율

    public int autoClickLevel;      //      자동 클릭 레벨
    public float autoClickSpeed;    //      자동 클릭 속도
    public float goldPerClick;      //      클릭당 획득 골드
    public bool isAutoClickUnlocked;//      자동 클릭 기능 해금 여부

    public float buffDuration;      //      버프 지속 시간
    public bool isDoubleGoldActive; //      2배 골드 버프 활성 여부

    public string playerName;       //      플레이어 이름
}
