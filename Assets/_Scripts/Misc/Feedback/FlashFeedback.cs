using System.Collections;
using UnityEngine;

public class FlashFeedback : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private float _invisibleTime = .3f, _visibleTime = .7f;

    public void PlayFeedback()
    {
        if (_sprite == null)
            return;

        StopFeedback();
        StartCoroutine(FlashRoutine());
    }

    public void StopFeedback()
    {
        StopAllCoroutines();

        Color spriteColor = _sprite.color;
        spriteColor.a = 1;
        _sprite.color = spriteColor;
    }

    private IEnumerator FlashRoutine()
    {
        Color spriteColor = _sprite.color;
        spriteColor.a = 0;
        _sprite.color = spriteColor;

        yield return new WaitForSeconds(_invisibleTime);

        spriteColor.a = 1;
        _sprite.color = spriteColor;

        yield return new WaitForSeconds(_visibleTime);

        StartCoroutine(FlashRoutine());
    }
}