using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Button : MonoBehaviour {

	public GameObject but_up;
	public GameObject but_down;
	public GameObject but_caption;
	public string caption;
	public Sprite ico;

	public string but_name;

	private GameObject ic;

	private List<string> lang = new List<string>();

	void Start()
	{
		lang.Add("ru");
		lang.Add("en");
		lang.Add("fr");
		lang.Add("de");

		but_up = (GameObject)Instantiate(but_up, gameObject.transform.position, Quaternion.identity);
		but_down = (GameObject)Instantiate(but_down, gameObject.transform.position, Quaternion.identity);
		but_up.transform.parent = gameObject.transform;
		but_down.transform.parent = gameObject.transform;
		but_caption = (GameObject)Instantiate(but_caption, gameObject.transform.position, Quaternion.identity);
		but_caption.transform.parent = but_up.transform;
		but_caption.transform.localPosition = Vector3.zero + new Vector3(0,0, -1);
		but_caption.GetComponent<TextMesh>().text = Localization.GetWord(caption);
		but_caption.GetComponent<TextMesh>().color = Color.gray;
		but_caption.GetComponent<TextMesh>().characterSize = 0.2f;
		ic = new GameObject();
		ic.AddComponent<SpriteRenderer>();
		ic.GetComponent<SpriteRenderer>().sprite = ico;
		ic.GetComponent<SpriteRenderer>().color = getColor();
		ic.transform.parent = but_up.transform;
		ic.transform.localPosition = Vector3.zero + new Vector3(-2.1f, 0, -1);

		but_up.AddComponent<BoxCollider2D>();
		but_down.AddComponent<BoxCollider2D>();

		SetButtonUp();
	}

	public string GetName()
	{
		return but_name;
	}

	public void SetButtonDown()
	{
		but_up.GetComponent<SpriteRenderer>().enabled = false;
		but_down.GetComponent<SpriteRenderer>().enabled = true;

		but_up.transform.position -= new Vector3(0, 0, -1);
		but_down.transform.position += new Vector3(0, 0, -1);

		but_caption.GetComponent<TextMesh>().color = Color.white;
		ic.GetComponent<SpriteRenderer>().color = Color.white;
	}

	public void SetButtonUp()
	{
		but_up.GetComponent<SpriteRenderer>().enabled = true;
		but_down.GetComponent<SpriteRenderer>().enabled = false;

		but_up.transform.position += new Vector3(0, 0, -1);
		but_down.transform.position -= new Vector3(0, 0, -1);

		but_caption.GetComponent<TextMesh>().color = Color.gray;
		ic.GetComponent<SpriteRenderer>().color = getColor();
	}

	void OnDestroy()
	{
		Destroy(but_up);
		Destroy(but_down);
		Destroy(but_caption);
		Destroy(ic);
	}

	public Color getColor()
	{
		if (lang.Contains(but_name)) return Color.white;
		return new Color(0.1462f, 0.5374f, 0.9020f);
	}
}
