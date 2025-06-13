using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalTabelPl : MonoBehaviour
{
    void Start()
    {

        //현재레벨을 레벨 범위테이블에서 탐색
        //찾은 범위에서 char형 ex ) a를 반환
        //해당구간당 코스트 값 증가량을 산출
        //해당구간범위의 레벨 증가값은?
        //maxLevelRange가 100이면 레벨당 코스트 500원씩 증가.
        //maxLeelRange가 200이면 레벨당 코스트 1000원씩 증가.
        //그러면 현재 코스트가 얼마인지? 를 저장하는 변수도 필요한가? 필요없나?
        //그냥 더해서 넣어준다가아니라 누적해서 더해준다가 맞지 않나?
        //아닌가? 내가 하던 게임들에는 어떻게했지?아 맞네
        //현재 코스트가 얼마인지를 정해주는 변수가 필요하고 그게 아마 플레이어의 GetUpgradeCost 고 거기에 계속 500원씩 더해주는거네 맞네 아 그러면 이렇게 하는게 맞네
        //보면 그럼  레벨당 변하는 코스트량도 있었나? 몰라 그냥 해
        
        
        
        string quotes = "~";

        int nowlevel = 0;
        int cost=0;

        char[] levelCharTable = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', };

        char nowCharTable;

        for (int n = 0; n < 10; n++)
        {

            //minLevelRange ~ MaxLevelRange
            //디버그의 플러스 문자 연산은 ()를 붙여야 계산을 해주네 근데 100+100이건 되는데 뭐지?
            Debug.Log((100 * n + 1) + quotes + (n * 100 + 100));

            
            int minLevelRange = (100 * n + 1);
            int maxLevelRange = (n * 100 + 100);
            

            if (maxLevelRange ==100)
            {

                cost += 500;
                nowlevel += 1;
                nowCharTable = levelCharTable[n];
                if (nowCharTable == 'A') { }

            }
            else if (maxLevelRange == 200)
            {
                cost += 1_000;
                nowlevel += 1;
                nowCharTable = levelCharTable[n];
            }
            else if (maxLevelRange == 300)
            {
                cost += 5_000;
                nowlevel += 1;
                nowCharTable = levelCharTable[n];

            }
            else if (maxLevelRange == 400)
            {
                cost += 10_000;
                nowlevel += 1;
                nowCharTable = levelCharTable[n];
            }
            else if (maxLevelRange == 500)
            {
                cost += 50_000;
                nowlevel += 1;
                nowCharTable = levelCharTable[n];
            }
            else if (maxLevelRange == 600)
            {
                cost += 60_000;
                nowlevel += 1;
                nowCharTable = levelCharTable[n];
            }
            else if (maxLevelRange == 700)
            {
                cost += 70_000;
                nowlevel += 1;
                nowCharTable = levelCharTable[n];
            }
            else if (maxLevelRange == 800)
            {
                cost += 80_000;
                nowlevel += 1;
                nowCharTable = levelCharTable[n];
            }
            else if (maxLevelRange == 900)
            {
                cost += 90_000;
                nowlevel += 1;
                nowCharTable = levelCharTable[n];
            }
            else if (maxLevelRange == 1000)
            {
                cost += 100_000;
                nowlevel += 1;
                nowCharTable = levelCharTable[n];
            }

            else
            {
                Debug.Log("cost테이블에서 잘못된 값을 반환");
            }

            switch (maxLevelRange)
            {

                case 100:
                    cost = 1_000;
                    break;

                case 200:
                    cost = 2_000;
                    break;
            }
        }

    }


}

