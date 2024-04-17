using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdManager : MonoBehaviour
{
    private void Start()
    {
        RunAdDelay();
    }
    public void RunAdDelay()
    {
        StartCoroutine(showFullScreenAd());
    }
    static IEnumerator showFullScreenAd()
    {
        yield return new WaitForSeconds(0.5f);
        YG.YandexGame.FullscreenShow();
    }
}
