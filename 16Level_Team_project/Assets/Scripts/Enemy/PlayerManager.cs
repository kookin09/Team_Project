using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }

    public Player player;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            player = new Player();  // 실제 Player 객체 생성
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
