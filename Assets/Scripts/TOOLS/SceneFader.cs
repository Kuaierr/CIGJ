using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneFader : MonoBehaviour
{
    private CanvasGroup _canvasGroup;

    public float fadeInDuration;
    public float fadeOutDuration;
    
    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        
        DontDestroyOnLoad(gameObject);
    }

    /*private void OnEnable()
    {
        StartCoroutine(FadeOutIn());
    }*/

    public IEnumerator FadeOutIn()
    {
        yield return FadeOut(fadeOutDuration);
        yield return FadeIn(fadeInDuration);
    }


    public IEnumerator FadeOut(float time)
    {
        while (_canvasGroup.alpha<1)
        {
            _canvasGroup.alpha += Time.deltaTime / time;
            yield return null;
        }
    }
    
    public IEnumerator FadeIn(float time)
    {
        while (_canvasGroup.alpha > 0)
        {
            _canvasGroup.alpha -= Time.deltaTime / time;
            yield return null;
        }
        
        Destroy(this.gameObject);
    }
}
