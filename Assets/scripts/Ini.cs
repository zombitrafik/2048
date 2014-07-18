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
}
