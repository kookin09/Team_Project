#define DOTWEEN_TMP

#if DOTWEEN_TMP
using DG.Tweening;
using TMPro;
using UnityEngine;

public static class DOTweenModuleTextMeshPro
{
    public static Tweener DOText(this TextMeshProUGUI target, string endValue, float duration, bool richTextEnabled = true, ScrambleMode scrambleMode = ScrambleMode.None, string scrambleChars = null)
    {
        return DOTween.To(() => target.text, x => target.text = x, endValue, duration)
            .SetOptions(richTextEnabled, scrambleMode, scrambleChars)
            .SetTarget(target);
    }

    public static Tweener DOFade(this TextMeshProUGUI target, float endValue, float duration)
    {
        return DOTween.ToAlpha(() => target.color, x => target.color = x, endValue, duration)
            .SetTarget(target);
    }

    public static Tweener DOColor(this TextMeshProUGUI target, Color endValue, float duration)
    {
        return DOTween.To(() => target.color, x => target.color = x, endValue, duration)
            .SetTarget(target);
    }
}
#endif