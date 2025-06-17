using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
public class DropTheCoin : MonoBehaviour
{

    [SerializeField]
    GameObject coinPreFab;

    IObjectPool<Coin> pool;

    //float minX = -2.7f;
    //float maxX = 2.7f;

    //float minY = 0.4f;
    //float minY = 1.5f;

    [SerializeField]
    Transform startPosition; //x-2.7~2.7 y-0.4~1.5

    [SerializeField]
    Transform endPosition;


    void Awake()
    {
        pool = new ObjectPool<Coin>(DropCoin, OnGetCoin, OnReleaseCoin, OnDestroyCoin, maxSize: 25);
    }

    void Update()
    {
        
    }

    void LateUpdate()
    {
        
    }

    void CalLerpPosition()
    {

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
