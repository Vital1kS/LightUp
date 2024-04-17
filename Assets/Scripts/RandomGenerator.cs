using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RandomGenerator : MonoBehaviour
{
    public IEnumerator GenerateRandomField()
    {
        Generator generator = GameObject.Find("Main Camera").GetComponent<Generator>();
        TMP_InputField xSizeTMP = GameObject.Find("xSize").GetComponent<TMP_InputField>();
        int xSize = int.Parse(xSizeTMP.text);
        TMP_InputField ySizeTMP = GameObject.Find("ySize").GetComponent<TMP_InputField>();
        int ySize = int.Parse(ySizeTMP.text);
        generator.ClearField();
        yield return new WaitForEndOfFrame();
        generator.GenerateNewField();
        int rnd = Random.Range(0, xSize * ySize/2);
        for(int i = 0; i< rnd; i++)
        {
            LampBehavior lamp = GameObject.Find("Lamp " + Random.Range(1, xSize * ySize +1)).GetComponent<LampBehavior>();
            lamp.ChangeUsage();
        }
        rnd = Random.Range( xSize * ySize, xSize * ySize*2);
        for (int i = 0; i < rnd; i++)
        {
            LampBehavior lamp = GameObject.Find("Lamp " + Random.Range(1, xSize * ySize +1)).GetComponent<LampBehavior>();
            if(!lamp.isUnused) lamp.LightLamps();
        }
    }
    public void StartRandGenerator() => StartCoroutine(GenerateRandomField());
    public void GenerateRandomBatch() => StartCoroutine(GenerateRandomBatchEnum());
    public IEnumerator GenerateRandomBatchEnum()
    {
        Generator generator = GameObject.Find("Main Camera").GetComponent<Generator>();
        TMP_InputField batchSizeTMP = GameObject.Find("batchSize").GetComponent<TMP_InputField>();
        int batchSize = int.Parse(batchSizeTMP.text)+1;
        Toggle isSaveToggle = GameObject.Find("isRandomSave").GetComponentInChildren<Toggle>();
        for(int i = 0; i< batchSize; i++)
        {
            if (i != 0)
            {
                StartRandGenerator();
                if (isSaveToggle.isOn)
                    generator.SaveNewPattern();
            }
            yield return new WaitForEndOfFrame();
        }
    }
}
