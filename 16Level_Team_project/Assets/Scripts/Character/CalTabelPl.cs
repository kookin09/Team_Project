using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalTabelPl : MonoBehaviour
{
    void Start()
    {


        string quotes = "~";

        int nowLevel = 0;

        string[] levelCharTable = { "maxLevelRange100", "maxLevelRange200", "maxLevelRange300", "maxLevelRange400", "maxLevelRange500",
            "maxLevelRange600", "maxLevelRange700", "maxLevelRange800", "maxLevelRange900", "maxLevelRange1_000" };


        Dictionary<string, int> levelCostTable = new Dictionary<string, int>()
        {
            {"maxLevelRange100",500 },
            {"maxLevelRange200",1_000 },
            {"maxLevelRange300",3_000 },
            {"maxLevelRange400",5_000 },
            {"maxLevelRange500",8_000 },
            {"maxLevelRange600",10_000 },
            {"maxLevelRange700",30_000 },
            {"maxLevelRange800",50_000 },
            {"maxLevelRange900",80_000 },
            {"maxLevelRange1_000",100_000 },
        };

        for (int i = 0; i < 10; i++)
        {


            Debug.Log((100 * i + 1) + quotes + (i * 100 + 100));

            //minLevelRange ~ MaxLevelRange
            //1~100 maxLevelRange100 ,
            //101~200 maxLevelRange200,
            //201~300 maxLevelRange300
            int minLevelRange = (100 * i + 1);
            int maxLevelRange = (i * 100 + 100);

            //할필요가 있나? 아 해당 범위에 있을때에서 for문 break하면 되네
            //예를 들면 500이야now레벨이 그러면 if문은 첫번쨰는 100~101이니까 통과를 안하겠지? 그리고 5번쨰 for문이 될때 if문 로직이 돌아가겠지?
            if (nowLevel >= minLevelRange && nowLevel <= maxLevelRange)
            {
                //딕셔너리에서 해당키값을 바탕으로 밸류를 불러오기 위해 i번째 코스트 티어배열에서 꺼내오고 
                string nowCostTierStingKey = levelCharTable[i];

                //꺼내온걸 바탕으로 딕셔너리에서 골드값을 계산해
                int GoldCost = levelCostTable[$"{nowCostTierStingKey}"];
                nowLevel += 1;

            }


        }

    }


}


#region 더미코드


/*
 * 
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


 * int[] levelIntTable = { 500, 1_000, 3_000, 5_000, 8_000, 10_000, 30_000, 50_000, 80_000, 100_000 };
        //안그래도 이거 다 숫자를 변수에 담으면 헷갈릴거같았는데 스트링으로 판정하면 엄청 쉽네 //해시테이블 엄청 빠르대 빼먹지말고 주말에 공부해라
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






*/
#endregion












