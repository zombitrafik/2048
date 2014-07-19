using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {

	public GameObject but_up;
	public GameObject but_down;

	public string but_name;

	void Start()
	{
		but_up = (GameObject)Instantiate(but_up, gameObject.transform.position, Quaternion.identity);
		but_down = (GameObject)Instantiate(but_down, gameObject.transform.position, Quaternion.identity);
		but_up.transform.parent = gameObject.transform;
		but_down.transform.parent = gameObject.transform;

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
	}

	public void SetButtonUp()
	{
		but_up.GetComponent<SpriteRenderer>().enabled = true;
		but_down.GetComponent<SpriteRenderer>().enabled = false;

		but_up.transform.position += new Vector3(0, 0, -1);
		but_down.transform.position -= new Vector3(0, 0, -1);
	}

	void OnDestroy()
	{
		Destroy(but_up);
		Destroy(but_down);
	}
}
