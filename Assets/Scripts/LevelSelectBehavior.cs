using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectBehavior : MonoBehaviour
{
    public int levelNumber;

    public void OpenLevel()
    {
        AudioHandler.PLayClickSound();
        PlayerMode.currentLevel = levelNumber-1;
        SceneChanger.nextScene = "LevelScene";
        GameObject.Find("Transition").GetComponent<TransitionScript>().isOpening = false;
    }
    public void RestartLevel()
    {
        AudioHandler.PLayClickSound();
        SceneChanger.nextScene = "LevelScene";
        GameObject.Find("Transition").GetComponent<TransitionScript>().isOpening = false;
    }
    public void OpenNextLevel()
    {
        AudioHandler.PLayClickSound();
        if (FileManager.getLineCount() > PlayerMode.currentLevel+1)
        {
            PlayerMode.currentLevel++;
            RestartLevel();
        }
        else
        {
            SceneChanger.GoToLevelSelect();
        }
    }
}
