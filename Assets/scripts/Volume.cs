using UnityEngine;
using System.Collections;

public class Volume : MonoBehaviour
{
	public GameObject but_up;
	public GameObject but_down;
	public Sprite ico_on;
	public Sprite ico_off;

	public string but_name;
	private GameObject ic;

	void Start()
	{
		but_up = (GameObject)Instantiate(but_up, gameObject.transform.position, Quaternion.identity);
		but_down = (GameObject)Instantiate(but_down, gameObject.transform.position, Quaternion.identity);
		but_up.transform.parent = gameObject.transform;
		but_down.transform.parent = gameObject.transform;
		ic = new GameObject();
		ic.AddComponent<SpriteRenderer>();
		if (Ini.LoadVolume() == 0)
		{
			ic.GetComponent<SpriteRenderer>().sprite = ico_off;
		}
		else
		{
			ic.GetComponent<SpriteRenderer>().sprite = ico_on;
		}
		
		ic.GetComponent<SpriteRenderer>().color = Color.gray;
		ic.transform.parent = but_up.transform;
		ic.transform.localPosition = Vector3.zero + new Vector3(0, 0, -1);

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

		if (Ini.LoadVolume() == 0)
		{
			ic.GetComponent<SpriteRenderer>().sprite = ico_off;
		}
		else
		{
			ic.GetComponent<SpriteRenderer>().sprite = ico_on;
		}

		ic.GetComponent<SpriteRenderer>().color = Color.white;
	}

	public void SetButtonUp()
	{
		but_up.GetComponent<SpriteRenderer>().enabled = true;
		but_down.GetComponent<SpriteRenderer>().enabled = false;

		but_up.transform.position += new Vector3(0, 0, -1);
		but_down.transform.position -= new Vector3(0, 0, -1);

		if (Ini.LoadVolume() == 0)
		{
			ic.GetComponent<SpriteRenderer>().sprite = ico_off;
		}
		else
		{
			ic.GetComponent<SpriteRenderer>().sprite = ico_on;
		}

		ic.GetComponent<SpriteRenderer>().color = Color.gray;
	}

	void OnDestroy()
	{
		Destroy(but_up);
		Destroy(but_down);
		Destroy(ic);
	}
}
