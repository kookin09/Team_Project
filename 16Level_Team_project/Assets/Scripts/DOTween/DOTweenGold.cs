using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;

public class DOTweenGold : MonoBehaviour
{

    public TextMeshProUGUI TestDo;
    public TextMeshProUGUI David;

    void Start()
    {
        TestDo.text = null;
        David.text = null;

        transform.DOMove(new Vector3(15, 0, 0), 3f);

        Color startColor = new Color32(150, 127, 127, 255);
        Color pulseColor = new Color32(255, 47, 47, 255);
        TestDo.color = startColor;

        TestDo.DOText("얍얍얍 ! ! ! ! !", 1.5f, true)
        .OnStart(() => {
            TestDo.DOColor(pulseColor, 1.5f)
                  .SetLoops(-1, LoopType.Yoyo)
                  .SetEase(Ease.InOutSine);
        })
        .OnComplete(() =>
        {
            David.color = new Color(0f, 0.5f, 1f, 0f); 
            David.DOText("엣헴", 1.5f, true);
            David.DOFade(1f, 1.5f); 
        });
    }


}
