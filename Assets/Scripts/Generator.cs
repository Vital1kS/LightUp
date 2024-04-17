using System.IO;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Generator : MonoBehaviour
{
    public GameObject lampPrefab;
    public Sprite lampOff, lampOn;
    public static int currentXSize, currentYSize;
    public List<string> levelPatterns = new List<string>();
    public bool[] lampGlow;
    private void Start()
    {
    }
    public void SaveNewPattern()
    {
        string pattern = GeneratePattern();
        FileManager.WritePatternToFile(pattern);
    }
    public void GenerateNewField()
    {
        TMP_InputField xSizeTMP = GameObject.Find("xSize").GetComponent<TMP_InputField>();
        int xSize = int.Parse(xSizeTMP.text);
        TMP_InputField ySizeTMP = GameObject.Find("ySize").GetComponent<TMP_InputField>();
        int ySize = int.Parse(ySizeTMP.text);
        ClearField();
        Generate(xSize, ySize,0,0,400,0);
    }
    public void ClearField()
    {
        int lampNum = 1;
        GameObject lamp = GameObject.Find("Lamp " + lampNum);
        while (lamp != null)
        {
            Destroy(lamp);
            lampNum++;
            lamp = GameObject.Find("Lamp " + lampNum);
        }
    }
    private string GeneratePattern()
    {
        string pattern = "";
        TMP_InputField xSizeTMP = GameObject.Find("xSize").GetComponent<TMP_InputField>();
        int xSize = int.Parse(xSizeTMP.text);
        TMP_InputField ySizeTMP = GameObject.Find("ySize").GetComponent<TMP_InputField>();
        int ySize = int.Parse(ySizeTMP.text);
        pattern += xSize+"x";
        pattern += ySize+"x";
        int lampNum = 1;
        GameObject lamp = GameObject.Find("Lamp " + lampNum);
        while(lamp != null)
        {
            bool isOn = lamp.GetComponent<LampBehavior>().isOn;
            bool isUnused = lamp.GetComponent<LampBehavior>().isUnused;
            if (isUnused)
            {
                pattern += 2;
            }
            else
            {
                if (isOn)
                {
                    pattern += 1;
                }
                else
                {
                    pattern += 0;
                }
            }
            lampNum++;
            lamp = GameObject.Find("Lamp " + lampNum);
        }
        return pattern;
    }
    
    public void ReadPattern(string pattern)
    {
        string xSize = "";
        string ySize = "";
        int xSizei, ySizei;
        int phase = 0;
        int lampNum = 1;
        foreach (char c in pattern)
        {
            if (c != 'x')
            {
                if (phase == 0) { xSize += c; }
                else if (phase == 1) { ySize += c; }
                else
                {    
                    GameObject tempLamp = GameObject.Find("Lamp " + lampNum);
                    if (c == '1')
                    {
                        tempLamp.GetComponent<SpriteRenderer>().sprite = lampOn;
                        tempLamp.GetComponent<LampBehavior>().isOn = true;
                        lampGlow[lampNum - 1] = true;
                    }
                    else if (c == '2') {
                        lampGlow[lampNum - 1] = true;
                        if (EditorMode.editMode)
                        {
                            tempLamp.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
                        }
                        else
                        {
                            Destroy(tempLamp);
                        }
                    }
                    lampNum++;
                }
            }
            else { 
                phase += 1;
                if (phase == 2)
                {
                    xSizei = System.Convert.ToInt32(xSize);
                    ySizei = System.Convert.ToInt32(ySize);
                    lampGlow = new bool[xSizei * ySizei];
                    Generate(xSizei, ySizei);
                }
            }

        }
    }
    public void Generate(int sizeX,int sizeY,int xOffset=0, int yOffset=150,int heightDecrease=150, int widthDecrease = 0)
    {
        int width = Screen.width;
        int height = Screen.height;
        int trueWidth = (width * 1000 / height)-widthDecrease;
        int trueHeight = 1000 - heightDecrease;
        int lampSize = 256;
        float scale = (Mathf.Min(trueHeight,trueWidth)*2 / (sizeX+0.25f))/(float)lampSize;
        float startX = (lampSize * scale * (sizeX-1) / 2)+xOffset;
        float startY = (lampSize * scale * (sizeY-1) / 2)+yOffset;
        int lampCount = 1;
        for(int i = 0; i < sizeX; i++)
        {
            for (int j = 0; j < sizeY; j++, lampCount++)
            {
                Vector2 spawnPos = new Vector2(i * lampSize*scale-startX,j*lampSize*scale-startY);
                GameObject newLamp = Instantiate(lampPrefab, spawnPos, Quaternion.identity);
                newLamp.transform.localScale *= scale;
                newLamp.name = "Lamp " + lampCount;
                newLamp.GetComponent<LampBehavior>().lampNumber = lampCount;
                if (EditorMode.editMode)
                {
                    newLamp.GetComponent<LampBehavior>().LightOneLamp();
                }
            }
        }
        currentXSize = sizeX;
        currentYSize = sizeY;
    }
}
