using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Pool;
using static UnityEditor.PlayerSettings;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Coin : MonoBehaviour
{

    IObjectPool<Coin> coinPool;



    public void SetManagedPool(IObjectPool<Coin> pool)
    {
        coinPool = pool;
    }


    //float minX = -2.7f;
    //float maxX = 2.7f;

    //float minY = 0.4f;
    //float minY = 1.5f;
    public void DropCoin()
    {
        //오브젝트 풀링에서 사용중인게 최기화전에 재호출되면 그 위치가 스폰 기준점이 되어버림 그래서 계속y위치가 올라감

        ReadyToDestroy(3f, -3f, 3f, 1f, 3f);
        Invoke("DestroyCoin", 3f);
    }


    public void ReadyToDestroy(float ranX, float minY,float maxY,float duration,float maxHeight)
    {


        //Vector2 randomVector = new Vector2(Random.Range(-ranX, ranX),  Random.Range(minY, maxY));


        Vector2 d1 = new Vector2(0,0);
        Vector2 randomVector = new Vector2(Random.Range(-ranX, ranX), Random.Range(minY, maxY));

        //Vector2 d2 = randomVector;
        Vector2 d2 =d1+ randomVector;


        float distanceX = d2.x - d1.x;
        float distanceY = d2.y - d1.y;

        float distance = Mathf.Sqrt(distanceX * distanceX + distanceY * distanceY);


        StartCoroutine(MoveToEndPoint(d1,d2,maxHeight,duration));



        

    }
    IEnumerator MoveToEndPoint(Vector2 d1, Vector2 d2, float maxHeight, float duration)
    {
        
        //accumulatedTime는 현재까지 진행된 시간값을계속 판정할 변수

        float accumulatedTime = 0f;

        while (accumulatedTime < duration)
        {
            float t = accumulatedTime / duration;
            Vector2 lerpMid = Vector2.Lerp(d1, d2, t);

            lerpMid.y += 4 * maxHeight * t * (1 - t);

            transform.position = lerpMid;

            accumulatedTime += Time.deltaTime;

            yield return null;



        }
        transform.position = d2;

        //yield return new WaitForSeconds(3f);
        

       
    }

    public void DestroyCoin()
    {
        coinPool.Release(this);
    }

}






