using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class GameReady : MonoBehaviour
{
    private static bool isReady = false;
    private void Update()
    {
        if (!isReady)
        {
            if (YandexGame.SDKEnabled)
            {
                YandexGame.GameReadyAPI();
                isReady = true;
            }
        }
    }
}
