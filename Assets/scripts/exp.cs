using UnityEngine;
using System.Collections;
using System;

public class Exp : MonoBehaviour {

    private static int oldCombo=1;
    private static int debuffCounter=5;

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
		instance.GetComponent<TextMesh>().characterSize = 0.3f + value / 100;
		MoveAndHide(instance, color);
	}

	public static void UpdateCombo(int value)
    {
        GooglePlayServices.Instance.UpdateRecord(GooglePlayServices.COMBO_LEADER_BOARD, value);
        if (value >= debuffCounter)
        {
            Debuff.Instance.Decrement();
            debuffCounter += 5;
        }
        if (value >= 5)
        {
            GooglePlayServices.Instance.UpdateAchieveProgress(GooglePlayServices.ACHIEVE_FIRST_COMBO, 100);
        }
        if (value >= 10)
        {
            GooglePlayServices.Instance.UpdateAchieveProgress(GooglePlayServices.ACHIEVE_X10_COMBO, 100);
        }
        if (value >= 15)
        {
            GooglePlayServices.Instance.UpdateAchieveProgress(GooglePlayServices.ACHIEVE_X15_COMBO, 100);
        }
        if (value >= 20)
        {
            GooglePlayServices.Instance.UpdateAchieveProgress(GooglePlayServices.ACHIEVE_X20_COMBO, 100);
        }

		GameObject.Find("Combo").GetComponent<TextMesh>().text = "x " + value;
        if (value > 1)
        {
            Exp.newCombo(value, new Vector3(0, 5.5f, 1f), Color.white);
        }
        else
        {
            if (oldCombo != 1)
            {
                Debuff.Instance.Increment();
                debuffCounter = 5;
            }
        }
        oldCombo=value;
	}

	public static void UpdateScore(int newVal)
	{
        GooglePlayServices.Instance.UpdateAchieveProgress(GooglePlayServices.ACHIEVE_FIRST_STEP, 100);

		int oldValue = Convert.ToInt32(GameObject.Find("Score").GetComponent<TextMesh>().text.Replace(Localization.GetWord("Score") + ": ", ""));
		GameObject.Find("Score").GetComponent<TextMesh>().text = Localization.GetWord("Score") + ": " + (oldValue + newVal);

		if ((newVal + oldValue) > Convert.ToInt32(GameObject.Find("BestScore").GetComponent<TextMesh>().text.Replace(Localization.GetWord("You best score") +": ", "")))
		{
			GameObject.Find("BestScore").GetComponent<TextMesh>().text = Localization.GetWord("You best score") + ": " + (oldValue + newVal);
		}
        if (newVal + oldValue >= 1000)
        {
            GooglePlayServices.Instance.UpdateAchieveProgress(GooglePlayServices.ACHIEVE_1000_POINTS, 100);
        }
        if (newVal + oldValue >= 10000)
        {
            GooglePlayServices.Instance.UpdateAchieveProgress(GooglePlayServices.ACHIEVE_10000_POINTS, 100);
        }
        if (newVal + oldValue >= 100000)
        {
            GooglePlayServices.Instance.UpdateAchieveProgress(GooglePlayServices.ACHIEVE_100000_POINTS, 100);
        }
        if (newVal + oldValue >= 1000000)
        {
            GooglePlayServices.Instance.UpdateAchieveProgress(GooglePlayServices.ACHIEVE_1000000_POINTS, 100);
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
