using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStepFadeOut : MonoBehaviour
{
    SpriteRenderer sprite;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();

        StartCoroutine("DestroyFootStep");
    }

    IEnumerator FadeOutFootStep()
    {
        while(sprite.color.a > 0)
        {
            Color color = sprite.color;
            color.a -= Time.deltaTime;
            sprite.color = color;
            yield return null;
        }
    }

    IEnumerator DestroyFootStep()
    {
        yield return new WaitForSeconds(8f);

        StartCoroutine("FadeOutFootStep");

        Destroy(gameObject, 2f);
    }
}
