using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;

public static class SaveSystem
{
    static string levelPath = Application.persistentDataPath + "/LevelData/";

    private static void CheckLevelDirectory()
    // Checks if level directory exists and if not
    // creates level directory
    {
        if(!Directory.Exists(levelPath))
        {
            Directory.CreateDirectory(levelPath);
        }
    }

    public static void SaveLevelStats(string levelName, int starCount, bool locked)
    {
        CheckLevelDirectory();

        BinaryFormatter formatter = new BinaryFormatter();
        string path = levelPath + levelName;
        FileStream stream = new FileStream(path, FileMode.Create);

        LevelData data = new LevelData(starCount, locked);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static void SaveLevelStars(string levelName, int starCount)
    {
        CheckLevelDirectory();

        LevelData levelData = loadLevelStats(levelName);
        if(levelData != null)
        {
            SaveLevelStats(levelName, starCount, levelData.locked);
        }
        else
        {
            Debug.Log("Could not save stars of level " + levelName);
        }
    }

    public static int LoadLevelStars(string levelName)
    {
        CheckLevelDirectory();

        LevelData levelData = loadLevelStats(levelName);
        if(levelData != null)
        {
            return levelData.starCount;
        }
        else
        {
            Debug.Log("Could not load stars of level " + levelName);
            return 0;
        }
    }

    public static void UnlockLevel(string levelName)
    {
        CheckLevelDirectory();

        LevelData levelData = loadLevelStats(levelName);
        if(levelData != null)
        {
            SaveLevelStats(levelName, levelData.starCount, false);
        }
        else
        {
            Debug.Log("Could not unlock level " + levelName);
        }
    }

    public static LevelData loadLevelStats(string levelName)
    {
        CheckLevelDirectory();

        string path = levelPath + levelName;
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            LevelData data = formatter.Deserialize(stream) as LevelData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.Log("Save file not found in " + path);
            return null;
        }
    }

    public static void DeleteLevelStats(string levelName)
    {
        CheckLevelDirectory();

        string path = levelPath + levelName;

        if (File.Exists(path))
        {
            File.Delete(path);
        }
        else
        {
            Debug.Log("No file to delete at " + path);
        }
    }

    public static void DeleteAllLevelData()
    {
        CheckLevelDirectory();
        string[] fileNames = Directory.GetFiles(levelPath);
        for(int i = 0; i < fileNames.Length; i++)
        {
            File.Delete(fileNames[i]);
        }
    }
}
