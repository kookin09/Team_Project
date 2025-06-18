using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Pool;
using static UnityEditor.PlayerSettings;

public class Coin : MonoBehaviour
{

    UIPlayer uiplayer;
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
        StartCoroutine(DropSequence());
    }

    IEnumerator DropSequence()
    {
        yield return StartCoroutine(ReadyToDestroy(3f, -3f, 1.5f, 4f, 3f));
        yield return new WaitForSeconds(2f);

        yield return StartCoroutine(ReadyToGetCoin(target, 3f, 3f));

        yield return new WaitForSeconds(1f);

        uiplayer.DOTweenCheatGold();
        DestroyCoin();
    }
    //public void DropCoin()
    //{
    //    //오브젝트 풀링에서 사용중인게 최기화전에 재호출되면 그 위치가 스폰 기준점이 되어버림 그래서 계속y위치가 올라감

    //    ReadyToDestroy(3f, -3f, 3f, 1f, 3f);
    //    ReadyToGetCoin(target, 3f, 3f);
    //    Invoke("DestroyCoin", 8f);


    //    uiplayer.DOTweenCheatGold();

    //}

    public Transform target;
    void Awake()
    {
        
        GameObject playerUIObj = GameObject.Find("Stat_UI");
        uiplayer = playerUIObj.GetComponent<UIPlayer>();

        GameObject holder = GameObject.Find("CoinHolder");
        if(holder != null)
        {
            target = holder.transform;
            Debug.Log("타켓연결함");
        }
        else { Debug.Log("타켓못찾겟음"); }
    }
    public IEnumerator ReadyToGetCoin(Transform target, float duration, float maxHeight)
    {
        Vector2 start = transform.position;
        Vector2 end = target.position;

        yield return StartCoroutine(MoveTOCoinUI(start, end, maxHeight, duration));
        //StartCoroutine(MoveTOCoinUI(start, end, maxHeight, duration));
    }

    IEnumerator MoveTOCoinUI(Vector2 start, Vector2 end, float maxHeight, float duration)
    {
        float accumulatedTime = 0f;

        while (accumulatedTime < duration)
        {
            float t = accumulatedTime / duration;

            // 선형 보간으로 위치 계산
            Vector2 lerpPos = Vector2.Lerp(start, end, t);

            // 포물선 높이 추가 (y축)
            lerpPos.y += 4 * maxHeight * t * (1 - t);

            transform.position = lerpPos;

            accumulatedTime += Time.deltaTime;
            yield return null;
        }

        // 마지막 위치 정확히 보정
        transform.position = end;
    }

    public IEnumerator ReadyToDestroy(float ranX, float minY,float maxY,float duration,float maxHeight)
    {


        //Vector2 randomVector = new Vector2(Random.Range(-ranX, ranX),  Random.Range(minY, maxY));


        Vector2 d1 = new Vector2(0,0);
        Vector2 randomVector = new Vector2(Random.Range(-ranX, ranX), Random.Range(minY, maxY));

        //Vector2 d2 = randomVector;
        Vector2 d2 =d1+ randomVector;


        float distanceX = d2.x - d1.x;
        float distanceY = d2.y - d1.y;

        float distance = Mathf.Sqrt(distanceX * distanceX + distanceY * distanceY);


        yield return StartCoroutine(MoveToEndPoint(d1,d2,maxHeight,duration));



        

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






