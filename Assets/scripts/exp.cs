using UnityEngine;
using System.Collections;

public class Exp : MonoBehaviour {

	public static void newExp(int value, Vector2 position)
	{
		GameObject objText = new GameObject("Text3D");
		TextMesh textMesh = objText.AddComponent<TextMesh>();
		objText.AddComponent<MeshRenderer>();
		textMesh.renderer.material = new Material(Shader.Find("GUI/Text Shader"));
		textMesh.text = "Hello World!";
	}

	private static void Ini_Tpl(GameObject experience)
	{
		
		GameObject text = GameObject.Find("text");
		experience.GetComponent<TextMesh>().characterSize = text.GetComponent<TextMesh>().characterSize;
		experience.GetComponent<TextMesh>().font = text.GetComponent<TextMesh>().font;
		experience.GetComponent<TextMesh>().fontStyle = text.GetComponent<TextMesh>().fontStyle;
		experience.GetComponent<TextMesh>().anchor = text.GetComponent<TextMesh>().anchor;
		experience.GetComponent<TextMesh>().alignment = text.GetComponent<TextMesh>().alignment;
		experience.GetComponent<TextMesh>().color = text.GetComponent<TextMesh>().color;

	}

}
