using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIControls : MonoBehaviour
{
    [SerializeField] GameObject[] _controlImages;
    [SerializeField] float _fadeDuration;
    void Start() => StartCoroutine(ShowControls());

    IEnumerator ShowControls()
    {
        foreach (GameObject control in _controlImages)
        {
            control.SetActive(true);
            bool isOn = false;
            CanvasGroup canvasGroup = control.GetComponent<CanvasGroup>();
            while (canvasGroup.alpha < 1.0f && isOn == false)
            {
                yield return null;
                canvasGroup.alpha += Time.deltaTime / _fadeDuration;
            }
            isOn = true;
            while (canvasGroup.alpha > 0.0f && isOn == true)
            {
                yield return null;
                canvasGroup.alpha -= Time.deltaTime / _fadeDuration;
            }
            control.SetActive(false);
        }
        StopAllCoroutines();
    }
}
