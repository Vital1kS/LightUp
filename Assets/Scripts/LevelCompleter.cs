using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelCompleter : MonoBehaviour
{
    void Start()
    {
        GameObject.Find("LevelText").GetComponent<TMP_Text>().text = "������� " + (PlayerMode.currentLevel + 1) + " �������";
    }

}
