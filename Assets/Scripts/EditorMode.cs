using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditorMode : MonoBehaviour
{
    public static bool plusMode = false, editMode = false;
    
    void Start()
    {
        TutorialMode.tutorialMode = false;
        editMode = true;
    }
    public static void ChangePlusMode(bool mode)
    {
        plusMode = GameObject.Find("PlusToggle").GetComponent<Toggle>().isOn;
    }
}
