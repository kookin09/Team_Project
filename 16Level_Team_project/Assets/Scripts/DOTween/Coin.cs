using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Pool;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Coin : MonoBehaviour
{

    IObjectPool<Coin> coinPool;



    public void SetManagedPool(IObjectPool<Coin> pool)
    {
        coinPool = pool;
    }

    public void DropCoin()
    {

        Invoke("DestroyCoin", 1f);
    }


    public void DestroyCoin()
    {
        coinPool.Release(this);
    }

}






