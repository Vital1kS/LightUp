using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger:MonoBehaviour
{
    public static string nextScene;
    public static AudioSource clickSound;
    public static void GoToLevelSelect()
    {
        AudioHandler.PLayClickSound();
        nextScene = "LevelSelectScene";
        GameObject.Find("Transition").GetComponent<TransitionScript>().isOpening = false;
    }
    public static void GoToMainMenu()
    {
        AudioHandler.PLayClickSound();
        nextScene = "MainMenuScene";
        GameObject.Find("Transition").GetComponent<TransitionScript>().isOpening = false;
    }
    public static void GoToEdit()
    {
        AudioHandler.PLayClickSound();
        nextScene = "EditScene";
        GameObject.Find("Transition").GetComponent<TransitionScript>().isOpening = false;
    }
    public static void GoToLevel()
    {
        AudioHandler.PLayClickSound();
        nextScene = "LevelScene";
        GameObject.Find("Transition").GetComponent<TransitionScript>().isOpening = false;
    }
    public static void GoToTutorial()
    {
        AudioHandler.PLayClickSound();
        nextScene = "TutorialScene";
        GameObject.Find("Transition").GetComponent<TransitionScript>().isOpening = false;
    }
    public static void GoToLevelComplete()
    {
        AudioHandler.PLayClickSound();
        nextScene = "LevelCompleteScene";
        GameObject.Find("Transition").GetComponent<TransitionScript>().isOpening = false;
    }

}
