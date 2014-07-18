using UnityEngine;
using System.Collections;

public class Neon : MonoBehaviour {

	private static Neon instance;
	public static Neon Instance
	{
		get
		{
			return instance;
		}
	}

	void Start()
	{
		instance = this;
	}

	public void NeonLight(string side)
	{
		switch (side)
		{
			case "left":
				iTween.ValueTo(gameObject, iTween.Hash("from", 0, "to", 1, "time", .2f, "onupdate", "NeonChangeLeft"));
				iTween.ValueTo(gameObject, iTween.Hash("from", 1, "to", 0, "time", .2f, "delay", .2f, "onupdate", "NeonChangeLeft"));
				break;
			case "right":
				iTween.ValueTo(gameObject, iTween.Hash("from", 0, "to", 1, "time", .2f, "onupdate", "NeonChangeRight"));
				iTween.ValueTo(gameObject, iTween.Hash("from", 1, "to", 0, "time", .2f, "delay", .2f, "onupdate", "NeonChangeRight"));
				break;
			case "up":
				iTween.ValueTo(gameObject, iTween.Hash("from", 0, "to", 1, "time", .2f, "onupdate", "NeonChangeUp"));
				iTween.ValueTo(gameObject, iTween.Hash("from", 1, "to", 0, "time", .2f, "delay", .2f, "onupdate", "NeonChangeUp"));
				break;
			case "down":
				iTween.ValueTo(gameObject, iTween.Hash("from", 0, "to", 1, "time", .2f, "onupdate", "NeonChangeDown"));
				iTween.ValueTo(gameObject, iTween.Hash("from", 1, "to", 0, "time", .2f, "delay", .2f, "onupdate", "NeonChangeDown"));
				break;
			default: break;
		}
	}

	public void NeonChangeLeft(float newValue)
	{
		GameObject.Find("mirrLeft").GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, newValue);
	}

	public void NeonChangeRight(float newValue)
	{
		GameObject.Find("mirrRight").GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, newValue);
	}

	public void NeonChangeUp(float newValue)
	{
		GameObject.Find("mirrUp").GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, newValue);
	}

	public void NeonChangeDown(float newValue)
	{
		GameObject.Find("mirrDown").GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, newValue);
	}
}
