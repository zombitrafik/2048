using UnityEngine;
using System.Collections;
using System;

public class Exp : MonoBehaviour {
    private const string ACHIEVE_1000_POINTS = "achievement1000Point";

	public static void newExp(int value, Vector3 position, Color color)
	{
		GameObject instance = (GameObject)Instantiate(Resources.Load("text", typeof(GameObject)), position, Quaternion.identity);
		instance.GetComponent<TextMesh>().text = "+"+value;
		MoveAndHide(instance, color);
	}

	private static void MoveAndHide(GameObject obj, Color color)
	{
		Vector3 pos = obj.transform.position + new Vector3(0, 1.5f, -1);
		iTween.MoveTo(obj, iTween.Hash("position", pos, "time", 1f, "easetype", iTween.EaseType.easeOutSine));
		iTween.ColorTo(obj, iTween.Hash("r", color.r, "g", color.g, "b", color.b, "time", 0.3f, "easetype", iTween.EaseType.easeOutQuint));
		iTween.ColorTo(obj, iTween.Hash("a", 0f, "delay", 0.5f));
		GameObject.Destroy(obj, 2);
	}

	public static void newCombo(int value, Vector3 position, Color color)
	{
		GameObject instance = (GameObject)Instantiate(Resources.Load("text", typeof(GameObject)), position, Quaternion.identity);
		instance.GetComponent<TextMesh>().text = "X " + value;
		instance.GetComponent<TextMesh>().characterSize = 0.6f + value / 50;
		MoveAndHide(instance, color);
	}

	public static void UpdateCombo(int value)
	{
		GameObject.Find("Combo").GetComponent<TextMesh>().text = "x " + value;
		if (value <= 1) return;
		Exp.newCombo(value, new Vector3(0, 5.5f, 1f), Color.white);
	}

	public static void UpdateScore(int newVal)
	{
		int oldValue = Convert.ToInt32(GameObject.Find("Score").GetComponent<TextMesh>().text.Replace("Score: ", ""));
		GameObject.Find("Score").GetComponent<TextMesh>().text = "Score: " + (oldValue + newVal);

		if ((newVal + oldValue) > Convert.ToInt32(GameObject.Find("BestScore").GetComponent<TextMesh>().text.Replace("Your best score: ", "")))
		{
			GameObject.Find("BestScore").GetComponent<TextMesh>().text = "Your best score: " + (oldValue + newVal);
		}
        if (newVal + oldValue >= 1000)
        {
            GooglePlayServices.Instance.UpdateAchieveProgress(ACHIEVE_1000_POINTS, 100);
        }
	}

	public static GameObject ViewScore(int score, string text)
	{
		GameObject instance = (GameObject)Instantiate(Resources.Load("text", typeof(GameObject)), Vector3.zero, Quaternion.identity);
		instance.GetComponent<TextMesh>().text = text + score;
		instance.GetComponent<TextMesh>().color = new Color(192, 192, 192);
		return instance;
	}

	public static GameObject Label(string text, float charSize)
	{
		GameObject instance = (GameObject)Instantiate(Resources.Load("text", typeof(GameObject)), Vector3.zero, Quaternion.identity);
		instance.GetComponent<TextMesh>().text = text;
		instance.GetComponent<TextMesh>().characterSize = charSize;
		instance.GetComponent<TextMesh>().color = new Color(192, 192, 192);
		return instance;
	}
}
