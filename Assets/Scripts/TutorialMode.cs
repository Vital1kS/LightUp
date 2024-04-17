using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMode : MonoBehaviour
{
    public static bool tutorialMode=false;
    void Start()
    {
        tutorialMode = true;
        EditorMode.editMode = false;
        LampBehavior.isFinished = false;
        GameObject.Find("Main Camera").GetComponent<Generator>().Generate(3, 3,0,180,400,0);
    }
}
