﻿using UnityEngine;
using System.Collections;
using System;
using System.IO;

public class Ini : MonoBehaviour {

    public static string LoadLanguage()
    {
        StreamReader fileReader;
        string fileName = Application.persistentDataPath + "/language.txt";
        string res = "";
        if (File.Exists(fileName))
        {
            fileReader = File.OpenText(fileName);
            res += fileReader.ReadLine();
            fileReader.Close();
        }
        return res;
    }

    public static void SaveLanguage(string value)
    {
        StreamWriter fileWriter;
        string fileName = Application.persistentDataPath + "/language.txt";
        File.WriteAllText(fileName, String.Empty);
        fileWriter = File.CreateText(fileName);
        fileWriter.WriteLine(value);
        fileWriter.Close();
    }



	public static void LoadRecord()
	{
		StreamReader fileReader;
		string fileName = Application.persistentDataPath + "/record.txt";
		if (File.Exists(fileName))
		{
			fileReader = File.OpenText(fileName);
			GameObject.Find("BestScore").GetComponent<TextMesh>().text = Localization.GetWord("You best score") + ": " + fileReader.ReadLine();
			fileReader.Close();
		}
	}

	public static void SaveRecord(int value)
	{
		StreamWriter fileWriter;
		string fileName = Application.persistentDataPath + "/record.txt";
		File.WriteAllText(fileName, String.Empty);
		fileWriter = File.CreateText(fileName);
		fileWriter.WriteLine(value);
		fileWriter.Close();
	}

	public static void SaveVolume(int value)
	{
		StreamWriter fileWriter;
		string fileName = Application.persistentDataPath + "/volumeValue.txt";
		File.WriteAllText(fileName, String.Empty);
		fileWriter = File.CreateText(fileName);
		fileWriter.WriteLine(value);
		fileWriter.Close();
		Sound.volumeValue = value;
	}

	public static int LoadVolume()
	{
		StreamReader fileReader;
		string fileName = Application.persistentDataPath + "/volumeValue.txt";
		int res = 0;
		if (File.Exists(fileName))
		{
			fileReader = File.OpenText(fileName);
			res = Convert.ToInt32(fileReader.ReadLine());
			fileReader.Close();
		}
		return res;
	}

	public static void SaveMinBoom(int value)
	{
		StreamWriter fileWriter;
		string fileName = Application.persistentDataPath + "/minBoom.txt";
		File.WriteAllText(fileName, String.Empty);
		fileWriter = File.CreateText(fileName);
		fileWriter.WriteLine(value);
		fileWriter.Close();
	}

	public static int LoadMinBoom()
	{
		StreamReader fileReader;
		string fileName = Application.persistentDataPath + "/minBoom.txt";
		int res = 0;
		if (File.Exists(fileName))
		{
			fileReader = File.OpenText(fileName);
			res = Convert.ToInt32(fileReader.ReadLine());
			fileReader.Close();
		}
		return res;
	}

	public static void SaveGeneratingCount(int value)
	{
		StreamWriter fileWriter;
		string fileName = Application.persistentDataPath + "/generatingCount.txt";
		File.WriteAllText(fileName, String.Empty);
		fileWriter = File.CreateText(fileName);
		fileWriter.WriteLine(value);
		fileWriter.Close();
		Sound.volumeValue = value;
	}

	public static int LoadGeneratingCount()
	{
		StreamReader fileReader;
		string fileName = Application.persistentDataPath + "/generatingCount.txt";
		int res = 0;
		if (File.Exists(fileName))
		{
			fileReader = File.OpenText(fileName);
			res = Convert.ToInt32(fileReader.ReadLine());
			fileReader.Close();
		}
		return res;
	}

    public static bool HaveSavedGame()
    {
        return File.Exists(Application.persistentDataPath + "/savedGame.txt");
    }

    public static void SaveGameState(int[,] arr)
    {
        StreamWriter fileWriter;
        string fileName = Application.persistentDataPath + "/savedGame.txt";
        File.WriteAllText(fileName, String.Empty);
        fileWriter = File.CreateText(fileName);
        
        for (int i = 0; i < 10; i++)
        {
            string line = "";
            for (int j = 0; j < 10; j++)
            {
                line += arr[i, j] + " ";
            }
            fileWriter.WriteLine(line);
        }
        fileWriter.Close();
    }

    public static int[,] LoadGameState()
    {
        StreamReader fileReader;
        string fileName = Application.persistentDataPath + "/savedGame.txt";
        int[,] res = new int[10,10];
        if (File.Exists(fileName))
        {
            fileReader = File.OpenText(fileName);
            for (int i = 0; i < 10; i++)
            {
                string[] stringArr = fileReader.ReadLine().Split(' ');
                for (int j = 0; j < 10; j++)
                {
                    res[i, j] = Convert.ToInt32(stringArr[j]);
                }

            }
            fileReader.Close();
        }
        return res;
    }

    public static void DeleteSavedGame()
    {
        try
        {
            File.Delete(Application.persistentDataPath + "/savedGame.txt");
            File.Delete(Application.persistentDataPath + "/savedScore.txt");
            File.Delete(Application.persistentDataPath + "/savedCombo.txt");
            File.Delete(Application.persistentDataPath + "/savedDebuff.txt");
            Ini.SaveGeneratingCount(3);
        }
        catch (Exception) { }
        
    }

    public static int LoadDebuff()
    {
        StreamReader fileReader;
        string fileName = Application.persistentDataPath + "/savedDebuff.txt";
        int res = 0;
        if (File.Exists(fileName))
        {
            fileReader = File.OpenText(fileName);
            res = Convert.ToInt32(fileReader.ReadLine());
            fileReader.Close();
        }
        return res;
    }

    public static void SaveDebuff(int value)
    {
        StreamWriter fileWriter;
        string fileName = Application.persistentDataPath + "/savedDebuff.txt";
        File.WriteAllText(fileName, String.Empty);
        fileWriter = File.CreateText(fileName);
        fileWriter.WriteLine(value);
        fileWriter.Close();
    }


    public static int LoadScore()
    {
        StreamReader fileReader;
        string fileName = Application.persistentDataPath + "/savedScore.txt";
        int res = 0;
        if (File.Exists(fileName))
        {
            fileReader = File.OpenText(fileName);
            res = Convert.ToInt32(fileReader.ReadLine());
            fileReader.Close();
        }
        return res;
    }

    public static void SaveScore(int value)
    {
        StreamWriter fileWriter;
        string fileName = Application.persistentDataPath + "/savedScore.txt";
        File.WriteAllText(fileName, String.Empty);
        fileWriter = File.CreateText(fileName);
        fileWriter.WriteLine(value);
        fileWriter.Close();
    }

	public static void SaveControl(string value)
	{
		StreamWriter fileWriter;
		string fileName = Application.persistentDataPath + "/control.txt";
		File.WriteAllText(fileName, String.Empty);
		fileWriter = File.CreateText(fileName);
		fileWriter.WriteLine(value);
		fileWriter.Close();
	}

	public static string LoadControl()
	{
		StreamReader fileReader;
		string fileName = Application.persistentDataPath + "/control.txt";
		string res = "";
		if (File.Exists(fileName))
		{
			fileReader = File.OpenText(fileName);
			res = fileReader.ReadLine();
			fileReader.Close();
		}
		return res;
	}

    public static int LoadCombo()
    {
        StreamReader fileReader;
        string fileName = Application.persistentDataPath + "/savedCombo.txt";
        int res = 1;
        if (File.Exists(fileName))
        {
            fileReader = File.OpenText(fileName);
            res = Convert.ToInt32(fileReader.ReadLine());
            fileReader.Close();
        }
        return res;
    }

    public static void SaveCombo(int value)
    {
        StreamWriter fileWriter;
        string fileName = Application.persistentDataPath + "/savedCombo.txt";
        File.WriteAllText(fileName, String.Empty);
        fileWriter = File.CreateText(fileName);
        fileWriter.WriteLine(value);
        fileWriter.Close();
    }



}
