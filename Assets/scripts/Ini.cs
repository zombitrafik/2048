using UnityEngine;
using System.Collections;
using System;
using System.IO;

public class Ini : MonoBehaviour {

	public static void LoadScore()
	{
		StreamReader fileReader;
		string fileName = Application.persistentDataPath + "/bestScore.txt";
		if (File.Exists(fileName))
		{
			fileReader = File.OpenText(fileName);
			GameObject.Find("BestScore").GetComponent<TextMesh>().text = "Your best score: " + fileReader.ReadLine();
			fileReader.Close();
		}
	}

	public static void SaveRecord(int value)
	{
		StreamWriter fileWriter;
		string fileName = Application.persistentDataPath + "/bestScore.txt";
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
		Sound.volumeValue = value;
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
}
