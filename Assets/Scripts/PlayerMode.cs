using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMode : MonoBehaviour
{
    public static int currentLevel;
    private Generator generator;
    private TMP_Text levelText;
    public GameObject winPanel;
    void Start()
    {
        EditorMode.editMode = false;
        TutorialMode.tutorialMode = false;
        LampBehavior.isFinished = false;
        levelText = GameObject.Find("LevelText").GetComponent<TMP_Text>();
        levelText.text = "Уровень " + (currentLevel+1);
        levelText.GetComponent<RectTransform>().sizeDelta= new Vector2(levelText.GetComponent<RectTransform>().sizeDelta.x, Screen.width>Screen.height?250:1000 - (Screen.width * 1000 / Screen.height));
        levelText.GetComponent<RectTransform>().anchoredPosition= new Vector2(levelText.GetComponent<RectTransform>().anchoredPosition.x, -0.6f* (Screen.width > Screen.height ? 250 : 1000 - (Screen.width * 1000 / Screen.height)));
        generator = gameObject.GetComponent<Generator>();
        generator.levelPatterns = FileManager.ReadPatternsFromFile();
        if (generator.levelPatterns[currentLevel] != null)
            generator.ReadPattern(generator.levelPatterns[currentLevel]);
    }
}
