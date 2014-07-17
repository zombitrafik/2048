using UnityEngine;
using System.Collections;

public class Exp : MonoBehaviour {


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



	
}
