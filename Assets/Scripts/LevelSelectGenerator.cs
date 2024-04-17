using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using YG;

public class LevelSelectGenerator : MonoBehaviour
{
    public GameObject levelSelectPrefab;
    public Sprite levelSelectOn, levelBlocked;
    public static int startLevelNum = 1;
    int width = Screen.width;
    int height = Screen.height;
    private void Start()
    {
        GenerateLevelSelects(5, 5);
    }
    public void GenerateLevelSelects(int sizeX, int sizeY)
    {
        int levelCount = FileManager.getLineCount();
        int width = Screen.width;
        int height = Screen.height;
        int levelSelectSize = 256;
        float trueHeight = 1400;
        float trueWidth = 2*width * 1000 / height;
        float scale = (Mathf.Min(trueWidth/2, trueHeight/2) * 2 / (sizeX + 2)) / levelSelectSize;
        float spaceX = (trueWidth-scale*levelSelectSize*sizeX)/sizeX;
        float spaceY = (trueHeight - scale * levelSelectSize * sizeY) / sizeY;
        float startX = (trueWidth-(sizeX-1)*scale*levelSelectSize)/2-(sizeX-1)*spaceX/2;
        float startY = (trueHeight - (sizeY + 1) * scale * levelSelectSize) / 2 - (sizeY + 1) * spaceY / 2;
        int levelSelectCount = startLevelNum;
        for (int i = sizeY; i > 0 ; i--)
        {
            for (int j = 0; j < sizeX; j++, levelSelectCount++)
            {
                if (levelSelectCount > levelCount) return;
                Vector2 spawnPos = new Vector2(j * levelSelectSize * scale+(j*spaceX) + startX-trueWidth/2, i * levelSelectSize * scale + (i * spaceY) + startY - trueHeight/2);
                GameObject newLevelSelect = Instantiate(levelSelectPrefab, spawnPos, Quaternion.identity);
                newLevelSelect.transform.localScale *= scale;
                newLevelSelect.name = "LevelSelect " + levelSelectCount;
                newLevelSelect.GetComponentInChildren<LevelSelectBehavior>().levelNumber = levelSelectCount;
                if (YandexGame.savesData.completedLevelsCount+5 < levelSelectCount)
                {
                    newLevelSelect.GetComponentInChildren<TMP_Text>().text = "";
                    newLevelSelect.GetComponentInChildren<Image>().sprite = levelBlocked;
                    newLevelSelect.GetComponentInChildren<Button>().enabled = false;
                }
                else
                {
                    newLevelSelect.GetComponentInChildren<TMP_Text>().text = levelSelectCount.ToString();
                    if (YandexGame.savesData.completedLevels[levelSelectCount-1])
                    {
                        newLevelSelect.GetComponentInChildren<Image>().sprite = levelSelectOn;
                    }
                }
            }
        }
    }
    public void GoNextPage()
    {
        if (FileManager.getLineCount() > 24+startLevelNum)
        {
            startLevelNum += 25;
            SceneChanger.GoToLevelSelect();
        }
    }
    public void GoPrevPage()
    {
        if (startLevelNum>1)
        {
            startLevelNum -= 25;
            SceneChanger.GoToLevelSelect();
        }
    }

    private void DestroyLevels(int sizeX, int sizeY)
    {
        int levelSelectCount = startLevelNum;
        for (int i = sizeY; i > 0; i--)
        {
            for (int j = 0; j < sizeX; j++, levelSelectCount++)
            {
                GameObject.Destroy(GameObject.Find("LevelSelect " + levelSelectCount));
            }
        }
    }
    private void RefreshLevelsSize(int sizeX, int sizeY)
    {
        DestroyLevels(sizeX, sizeY);
        GenerateLevelSelects(sizeX, sizeY);
    }
    private void FixedUpdate()
    {
        if (width != Screen.width || height != Screen.height)
        {
            RefreshLevelsSize(5, 5);
            width = Screen.width;
            height = Screen.height;
        }
    }
}
