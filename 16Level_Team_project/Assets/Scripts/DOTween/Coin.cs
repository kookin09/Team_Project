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
        ReadyToDestroy(2.7f, 0.4f, 1.5f, 2f, 1.5f);
        Invoke("DestroyCoin", 1f);
    }


    public void ReadyToDestroy(float ranX, float minZ,float maxZ,float duration,float maxHeight)
    {


        Vector3 randomVector = new Vector3(Random.Range(-ranX, ranX), 0, Random.Range(minZ, maxZ));


        Vector3 d1 = transform.position;
        Vector3 d2 = randomVector;

        float distanceX = d2.x - d1.x;
        float distanceZ = d2.z - d1.z;

        float distance = Mathf.Sqrt(distanceX * distanceX + distanceZ * distanceZ);


        StartCoroutine(MoveToEndPoint(d1,d2,maxHeight,duration));



        

    }
    IEnumerator MoveToEndPoint(Vector3 d1, Vector3 d2, float maxHeight, float duration)
    {

        //accumulatedTime는 현재까지 진행된 시간값을계속 판정할 변수

        float accumulatedTime = 0f;

        while (accumulatedTime < duration)
        {
            float t = accumulatedTime / duration;
            Vector3 lerpMid = Vector3.Lerp(d1, d2, t);

            lerpMid.y += 4 * maxHeight * t * (1 - t);

            transform.position = lerpMid;

            accumulatedTime += Time.deltaTime;

            yield return null;

        }

        transform.position = d2;
    }

    public void DestroyCoin()
    {
        coinPool.Release(this);
    }

}






