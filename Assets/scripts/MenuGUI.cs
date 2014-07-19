using UnityEngine;
using System.Collections;
using System;

public class MenuGUI : MonoBehaviour {

	private static MenuGUI instance;
	public static MenuGUI Instance
	{
		get
		{
			return instance;
		}
	}
	private bool pause;

	public GameObject back;
	private GameObject backCopy;

	public GameObject exit;
	private GameObject exitCopy;

	public GameObject retry;
	private GameObject retryCopy;

	public GameObject sound_en;
	private GameObject sound_enCopy;

	public GameObject sound_dis;
	private GameObject sound_disCopy;

	private GameObject activeClicked;

	void Start()
	{
		instance = this;
	}

	public static void ShowGameOverMenu(GUIStyle style)
	{
		int score = Convert.ToInt32(GameObject.Find("Score").GetComponent<TextMesh>().text.Replace("Score: ", ""));
		int bestScore = Convert.ToInt32(GameObject.Find("BestScore").GetComponent<TextMesh>().text.Replace("Your best score: ", ""));
		GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 240, 100, 40), "Score: " + score.ToString(), style);
		GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 180, 200, 40), "Best score: " + bestScore.ToString(), style);

		if (GUI.Button(new Rect(Screen.width / 2 - 150, Screen.height / 2, 100, 50), "Retry"))
		{
			Application.LoadLevel("main");
		}
		if (GUI.Button(new Rect(Screen.width / 2 + 50, Screen.height / 2, 100, 50), "Exit"))
		{
			Application.LoadLevel("main_menu");
		}
		if (score == bestScore) Ini.SaveRecord(bestScore);
	}

	public void ShowBackMenu()
	{


		pause = true;

		backCopy = (GameObject)Instantiate(back, new Vector3(7, 4, -5), Quaternion.identity);
		if (Sound.volumeValue == 1) sound_enCopy = (GameObject)Instantiate(sound_en, new Vector3(7, 2, -5), Quaternion.identity);
		if (Sound.volumeValue == 0) sound_disCopy = (GameObject)Instantiate(sound_dis, new Vector3(7, 2, -5), Quaternion.identity);
		retryCopy = (GameObject)Instantiate(retry, new Vector3(7, 0, -5), Quaternion.identity);
		exitCopy = (GameObject)Instantiate(exit, new Vector3(7, -2, -5), Quaternion.identity);

	
		iTween.MoveTo(backCopy, iTween.Hash("position", new Vector3(-1, 4, -5), "time", 0.5f, "easetype", iTween.EaseType.easeOutSine));
		if (Sound.volumeValue == 1) iTween.MoveTo(sound_enCopy, iTween.Hash("position", new Vector3(-0.5f, 2, -5), "time", 0.5f, "delay", 0.02f, "easetype", iTween.EaseType.easeOutSine));
		if (Sound.volumeValue == 0) iTween.MoveTo(sound_disCopy, iTween.Hash("position", new Vector3(-0.5f, 2, -5), "time", 0.5f, "delay", 0.02f, "easetype", iTween.EaseType.easeOutSine));
		iTween.MoveTo(retryCopy, iTween.Hash("position", new Vector3(0, 0, -5), "time", 0.5f, "delay", 0.04f, "easetype", iTween.EaseType.easeOutSine));
		iTween.MoveTo(exitCopy, iTween.Hash("position", new Vector3(0.5f, -2, -5), "time", 0.5f, "delay", 0.06f, "easetype", iTween.EaseType.easeOutSine));
		
	}

	public void HideBackMenu()
	{
	

		pause = false;

		iTween.MoveTo(backCopy, iTween.Hash("position", new Vector3(7, 4, -5), "time", 0.5f, "easetype", iTween.EaseType.easeOutSine));
		if (Sound.volumeValue == 1) iTween.MoveTo(sound_enCopy, iTween.Hash("position", new Vector3(7, 2, -5), "time", 0.5f, "delay", 0.02f, "easetype", iTween.EaseType.easeOutSine));
		if (Sound.volumeValue == 0) iTween.MoveTo(sound_disCopy, iTween.Hash("position", new Vector3(7, 2, -5), "time", 0.5f, "delay", 0.02f, "easetype", iTween.EaseType.easeOutSine));
		iTween.MoveTo(retryCopy, iTween.Hash("position", new Vector3(7, 0, -5), "time", 0.5f, "delay", 0.04f, "easetype", iTween.EaseType.easeOutSine));
		iTween.MoveTo(exitCopy, iTween.Hash("position", new Vector3(7, -2, -5), "time", 0.5f, "delay", 0.06f, "easetype", iTween.EaseType.easeOutSine));
		

		Destroy(backCopy, 0.5f);
		Destroy(sound_enCopy, 0.52f);
		Destroy(sound_disCopy, 0.52f);
		Destroy(retryCopy, 0.54f);
		Destroy(exitCopy, 0.56f);
		
	}

	public bool IsShow()
	{
		return pause;
	}

	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

			if (hit.collider != null)
			{
				if (hit.transform.gameObject.name == "back_up(Clone)")
				{
					activeClicked = GameObject.Find("but_back(Clone)");
					activeClicked.GetComponent<Button>().SetButtonDown();
				}
				if (hit.transform.gameObject.name == "exit_up(Clone)")
				{
					activeClicked = GameObject.Find("but_exit(Clone)");
					activeClicked.GetComponent<Button>().SetButtonDown();
				}
				if (hit.transform.gameObject.name == "retry_up(Clone)")
				{
					activeClicked = GameObject.Find("but_retry(Clone)");
					activeClicked.GetComponent<Button>().SetButtonDown();
				}
				if (hit.transform.gameObject.name == "sound_up_dis(Clone)")
				{
					activeClicked = GameObject.Find("but_sound_dis(Clone)");
					activeClicked.GetComponent<Button>().SetButtonDown();
				}
				if (hit.transform.gameObject.name == "sound_up_en(Clone)")
				{
					activeClicked = GameObject.Find("but_sound_en(Clone)");
					activeClicked.GetComponent<Button>().SetButtonDown();
				}
			}
		}

		if (Input.GetMouseButtonUp(0))
		{
			if (activeClicked == null) return;

			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

			if(hit.collider != null) 
			{
				if (activeClicked.GetComponent<Button>().GetName() == "back" && hit.transform.gameObject.name == "back_down(Clone)")
				{
					pause = false;
					HideBackMenu();
				}
				if (activeClicked.GetComponent<Button>().GetName() == "exit" && hit.transform.gameObject.name == "exit_down(Clone)")
				{
					Application.LoadLevel("main_menu");
				}
				if (activeClicked.GetComponent<Button>().GetName() == "retry" && hit.transform.gameObject.name == "retry_down(Clone)")
				{
					Application.LoadLevel("main");
				}
				if (activeClicked.GetComponent<Button>().GetName() == "sound_dis" && hit.transform.gameObject.name == "sound_down_dis(Clone)")
				{
					Sound.volumeValue = 1;
					ReloadMenu();
				}
				if (activeClicked.GetComponent<Button>().GetName() == "sound_en" && hit.transform.gameObject.name == "sound_down_en(Clone)")
				{
					Sound.volumeValue = 0;
					ReloadMenu();
				}
			}
			activeClicked.GetComponent<Button>().SetButtonUp();
			activeClicked = null;
		}
	}

	private void ReloadMenu()
	{
		Destroy(backCopy);
		Destroy(exitCopy);
		Destroy(retryCopy);
		Destroy(sound_enCopy);
		Destroy(sound_disCopy);

		backCopy = (GameObject)Instantiate(back, new Vector3(-1, 4, -5), Quaternion.identity);
		if (Sound.volumeValue == 1) sound_enCopy = (GameObject)Instantiate(sound_en, new Vector3(-0.5f, 2, -5), Quaternion.identity);
		if (Sound.volumeValue == 0) sound_disCopy = (GameObject)Instantiate(sound_dis, new Vector3(-0.5f, 2, -5), Quaternion.identity);
		retryCopy = (GameObject)Instantiate(retry, new Vector3(0, 0, -5), Quaternion.identity);
		exitCopy = (GameObject)Instantiate(exit, new Vector3(0.5f, -2, -5), Quaternion.identity);
	}
}
