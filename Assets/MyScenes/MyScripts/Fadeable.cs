using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fadeable : MonoBehaviour
{
    private CanvasGroup _canvasGroup;
    private bool _visible= false;
    // Start is called before the first frame update
    void Start()
    {
        _canvasGroup = this.GetComponent<CanvasGroup>();
        _visible = (_canvasGroup.alpha != 0);
       // StartCoroutine(FadeToggle(1,2));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void Toggle(float Tr, float Keep)
    {
        StartCoroutine(FadeToggle(Tr, Keep));
    }
    IEnumerator FadeIn( float aTime)
    {
        float alpha = _canvasGroup.alpha;
        _visible = true;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            _canvasGroup.alpha = Mathf.Lerp(0, 1, t);
            yield return null;
        }

    }

    IEnumerator FadeOut(float aTime)
    {
        float alpha = _canvasGroup.alpha;
        _visible = false;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            _canvasGroup.alpha = Mathf.Lerp(1, 0, t);
            yield return null;
        }

    }

    IEnumerator FadeToggle(float aTime, float keepTime)
    {
        float alpha = _canvasGroup.alpha;
        float dur = aTime / 2;

       if(_visible)
        {
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / dur)
            {
                _canvasGroup.alpha = Mathf.Lerp(1, 0, t); //FadeOut
                yield return null;
            }
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / keepTime)
            {
                _canvasGroup.alpha = 0; //keep
                yield return null;
            }

            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / dur)
            {
                _canvasGroup.alpha = Mathf.Lerp(0, 1, t); //FadeIn
               yield return null;
            }

        }
        else
        {
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / dur)
            {
                _canvasGroup.alpha = Mathf.Lerp(0, 1, t); //FadeIn
                                                          yield return null;
            }

            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / keepTime)
            {
                _canvasGroup.alpha = 1; //keep
                yield return null;
            }

            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / dur)
            {
                _canvasGroup.alpha = Mathf.Lerp(1, 0, t); //FadeOut
                                                          yield return null;
            }

        }
        yield return null;
    }

}
