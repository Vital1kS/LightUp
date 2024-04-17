using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class LampBehavior : MonoBehaviour
{
    public Sprite lampOff, lampOn;
    public bool isOn = false, isUnused=false;
    public static bool isFinished;
    private Color fullColor = Color.white, semiTransparent= new Color(1,1,1,0.5f);
    public int lampNumber;
    private void OnMouseDown()
    {
        if (EditorMode.editMode)
        {
            if (EditorMode.plusMode)
            {
                LightLamps();
            }
            else
            {
                if (Input.GetKey(KeyCode.R))
                {
                    ChangeUsage();
                }
                else
                {
                    isOn = !isOn;
                    if (isOn) gameObject.GetComponent<SpriteRenderer>().sprite = lampOn;
                    else gameObject.GetComponent<SpriteRenderer>().sprite = lampOff;
                }
            }
        }
        else
        {
            if (!isFinished)
            {
                AudioHandler.PLayClickSound();
                LightLamps();
                if (!TutorialMode.tutorialMode)
                {
                    isFinished = true;
                    foreach (bool isGlow in GameObject.Find("Main Camera").GetComponent<Generator>().lampGlow)
                    {
                        isFinished = isFinished && isGlow;
                    }
                    if (isFinished)
                    {
                        if (!YandexGame.savesData.completedLevels[PlayerMode.currentLevel]){
                            YandexGame.savesData.completedLevels[PlayerMode.currentLevel] = true;
                            YandexGame.savesData.completedLevelsCount++;
                            YandexGame.SaveProgress();
                            YandexGame.NewLeaderboardScores("completedLevelsCountLB", YandexGame.savesData.completedLevelsCount);
                        }
                        SceneChanger.GoToLevelComplete();
                    }
                }
            }
        }
    }
    public void LightLamps()
    {
        List<GameObject> plusLamps = new List<GameObject>();
        plusLamps.Add(gameObject);
        if (lampNumber % Generator.currentYSize != 0)
        {
            GameObject tempLamp = GameObject.Find("Lamp " + (lampNumber + 1));
            if(tempLamp!=null)
                plusLamps.Add(tempLamp);
        }
        if ((lampNumber - 1) % Generator.currentYSize != 0)
        {
            GameObject tempLamp = GameObject.Find("Lamp " + (lampNumber - 1));
            if (tempLamp != null)
                plusLamps.Add(tempLamp);
        }
        if (lampNumber <= Generator.currentXSize * Generator.currentYSize - Generator.currentYSize)
        {
            GameObject tempLamp = GameObject.Find("Lamp " + (lampNumber + Generator.currentYSize));
            if (tempLamp != null)
                plusLamps.Add(tempLamp);
        }
        if (lampNumber > Generator.currentYSize)
        {
            GameObject tempLamp = GameObject.Find("Lamp " + (lampNumber - Generator.currentYSize));
            if (tempLamp != null)
                plusLamps.Add(tempLamp);
        }
        foreach (GameObject lamp in plusLamps)
        {
            lamp.GetComponent<LampBehavior>().isOn = !lamp.GetComponent<LampBehavior>().isOn;
            if (lamp.GetComponent<LampBehavior>().isOn) lamp.GetComponent<SpriteRenderer>().sprite = lampOn;
            else lamp.GetComponent<SpriteRenderer>().sprite = lampOff;
            if (!EditorMode.editMode)
            {
                if (!TutorialMode.tutorialMode)
                {
                    GameObject.Find("Main Camera").GetComponent<Generator>().lampGlow[lamp.GetComponent<LampBehavior>().lampNumber - 1] = lamp.GetComponent<LampBehavior>().isOn;
                }
            }
        }
    }
    public void LightOneLamp()
    {
        gameObject.GetComponent<LampBehavior>().isOn = !gameObject.GetComponent<LampBehavior>().isOn;
        if (gameObject.GetComponent<LampBehavior>().isOn) gameObject.GetComponent<SpriteRenderer>().sprite = lampOn;
        else gameObject.GetComponent<SpriteRenderer>().sprite = lampOff;
    }

    public void ChangeUsage()
    {
        if (!isUnused)
        {
            isUnused = true;
            gameObject.GetComponent<SpriteRenderer>().color = semiTransparent;
        }
        else
        {
            isUnused = false;
            gameObject.GetComponent<SpriteRenderer>().color = fullColor;
        }
    }
}
