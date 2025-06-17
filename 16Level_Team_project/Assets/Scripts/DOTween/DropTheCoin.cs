using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using static UnityEngine.RuleTile.TilingRuleOutput;
public class DropTheCoin : MonoBehaviour
{

    [SerializeField]
    GameObject coinPreFab;

    IObjectPool<Coin> pool;


    void Awake()
    {
        pool = new ObjectPool<Coin>(DropCoin, OnGetCoin, OnReleaseCoin, OnDestroyCoin, maxSize: 25);
    }

    void Update()
    {
        //if (Input.GetMouseButton(0))
        //{

        //}
    }

    public void ButtonPool()
    {
        var coin = pool.Get();

        coin.DropCoin();
    }

    //Create
    Coin DropCoin()
    {
        Coin coinTouch = Instantiate(coinPreFab).GetComponent<Coin>();
        coinTouch.SetManagedPool(pool);
        return coinTouch;
    }

    void OnGetCoin(Coin coinTouch)
    {
        coinTouch.gameObject.SetActive(true);
    }

    void OnReleaseCoin(Coin coinTouch)
    {
        coinTouch.gameObject.SetActive(false);
    }

    void OnDestroyCoin(Coin coinTouch)
    {
        Destroy(coinTouch.gameObject);
    }

}
