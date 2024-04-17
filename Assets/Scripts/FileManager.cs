using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEditor;

public class FileManager
{
    static string fileName = "Assets/Resources/patternSaves.txt";
    static TextAsset patternsFile = (TextAsset)Resources.Load("patternSaves");
    public static List<string> ReadPatternsFromFile()
    {
        return patternsFile.text.Split("\n").OfType<string>().ToList();
    }
    public static void WritePatternToFile(string pattern)
    {
        using (StreamWriter writer = new StreamWriter(fileName,true))
        {
            writer.WriteLine(pattern);
        }
        //AssetDatabase.Refresh();
    }
    public static int getLineCount()
    {
        return patternsFile.text.Split("\n").Length-1;
    }
}
