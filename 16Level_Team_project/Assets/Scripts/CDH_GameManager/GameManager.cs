using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public PlayerData playerData;
    public Player player { get; private set; }


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        player = new Player();
        DontDestroyOnLoad(gameObject);

        //LoadPlayerData();     //      나중에 구현할 부분
    }

    //public void SavePlayerData()      //      나중에 구현 
    //public void LoadPlayerData()      //      나중에 구현
}
