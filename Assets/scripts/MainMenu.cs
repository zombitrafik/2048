using UnityEngine;
using System.Collections;
using System;

public class MainMenu : MonoBehaviour {

	private const string NORMAL_MODE_BOARD = "leaderboardMoveCrushLeaderboard";
	// Main
	public GameObject play;
	private GameObject playCopy;

	public GameObject settings;
	private GameObject settingsCopy;

	public GameObject tutorial;
	private GameObject tutorialCopy;

	public GameObject about;
	private GameObject aboutCopy;

	public GameObject exit;
	private GameObject exitCopy;

	// Settings
	public GameObject sound_en;
	private GameObject sound_enCopy;

	public GameObject sound_dis;
	private GameObject sound_disCopy;

	public GameObject back;
	private GameObject backCopy;

	public GameObject left;
	private GameObject leftCopyMin;
	private GameObject leftCopyGen;

	public GameObject right;
	private GameObject rightCopyMin;
	private GameObject rightCopyGen;

	private GameObject labelMain;
	private GameObject labelBoom;
	private GameObject valueMin;
	private GameObject labelGen;
	private GameObject valueGen;

	private GameObject activeClicked;

	public string url = "";
	public string nexturl = "";

	void Start()
	{
        if (GoogleAnalytics.instance)
        {
            GoogleAnalytics.instance.LogScreen("main menu");
        }
		if (Ini.LoadGeneratingCount() == 0)
		{
			Ini.SaveGeneratingCount(3);
		}
		if (Ini.LoadMinBoom() == 0)
		{
			Ini.SaveMinBoom(3);
		}
		ShowMenu();
	}

	
	IEnumerator VK()
	{
		WWW www = new WWW(url);
		yield return www;

		Debug.Log(www.text);
		WWW newxtwww = new WWW(nexturl);
		yield return newxtwww;
	}

	public void ShowMenu()
	{
		playCopy = (GameObject)Instantiate(play, new Vector3(8, 4, -5), Quaternion.identity);
		settingsCopy = (GameObject)Instantiate(settings, new Vector3(8, 2, -5), Quaternion.identity);
		tutorialCopy = (GameObject)Instantiate(tutorial, new Vector3(8, 0, -5), Quaternion.identity);
		aboutCopy = (GameObject)Instantiate(about, new Vector3(8, -2, -5), Quaternion.identity);
		exitCopy = (GameObject)Instantiate(exit, new Vector3(8, -4, -5), Quaternion.identity);

		iTween.MoveTo(playCopy, iTween.Hash("position", new Vector3(-1.5f, 4, -5), "time", 0.5f, "delay", 0.5f, "easetype", iTween.EaseType.easeOutSine));
		iTween.MoveTo(settingsCopy, iTween.Hash("position", new Vector3(-1f, 2, -5), "time", 0.5f, "delay", 0.54f, "easetype", iTween.EaseType.easeOutSine));
		iTween.MoveTo(tutorialCopy, iTween.Hash("position", new Vector3(-0.5f, 0, -5), "time", 0.5f, "delay", 0.56f, "easetype", iTween.EaseType.easeOutSine));
		iTween.MoveTo(aboutCopy, iTween.Hash("position", new Vector3(0, -2, -5), "time", 0.5f, "delay", 0.58f, "easetype", iTween.EaseType.easeOutSine));
		iTween.MoveTo(exitCopy, iTween.Hash("position", new Vector3(0.5f, -4, -5), "time", 0.5f, "delay", 0.6f, "easetype", iTween.EaseType.easeOutSine));

		iTween.MoveTo(playCopy, iTween.Hash("position", new Vector3(-1.1f, 4, -5), "time", 0.2f, "delay", 1f, "easetype", iTween.EaseType.easeOutSine));
		iTween.MoveTo(settingsCopy, iTween.Hash("position", new Vector3(-0.6f, 2, -5), "time", 0.2f, "delay", 1.04f, "easetype", iTween.EaseType.easeOutSine));
		iTween.MoveTo(tutorialCopy, iTween.Hash("position", new Vector3(-0.1f, 0, -5), "time", 0.2f, "delay", 1.06f, "easetype", iTween.EaseType.easeOutSine));
		iTween.MoveTo(aboutCopy, iTween.Hash("position", new Vector3(0.4f, -2, -5), "time", 0.2f, "delay", 1.08f, "easetype", iTween.EaseType.easeOutSine));
		iTween.MoveTo(exitCopy, iTween.Hash("position", new Vector3(0.9f, -4, -5), "time", 0.2f, "delay", 1.1f, "easetype", iTween.EaseType.easeOutSine));
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			HideAll();
			ShowMenu();
		}

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
				// Main
				if (hit.transform.gameObject.name == "play_up(Clone)")
				{
					activeClicked = GameObject.Find("but_play(Clone)");
					activeClicked.GetComponent<Button>().SetButtonDown();
				}
				if (hit.transform.gameObject.name == "mode_up(Clone)")
				{
					activeClicked = GameObject.Find("but_mode(Clone)");
					activeClicked.GetComponent<Button>().SetButtonDown();
				}
				if (hit.transform.gameObject.name == "settings_up(Clone)")
				{
					activeClicked = GameObject.Find("but_settings(Clone)");
					activeClicked.GetComponent<Button>().SetButtonDown();
				}
				if (hit.transform.gameObject.name == "tutorial_up(Clone)")
				{
					activeClicked = GameObject.Find("but_tutorial(Clone)");
					activeClicked.GetComponent<Button>().SetButtonDown();
				}
				if (hit.transform.gameObject.name == "about_up(Clone)")
				{
					activeClicked = GameObject.Find("but_about(Clone)");
					activeClicked.GetComponent<Button>().SetButtonDown();
				}
				if (hit.transform.gameObject.name == "exit_up(Clone)")
				{
					activeClicked = GameObject.Find("but_exit(Clone)");
					activeClicked.GetComponent<Button>().SetButtonDown();
				}
				// Settings
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
				if (hit.transform.gameObject.name == "left_up(Clone)")
				{
					activeClicked = hit.transform.parent.gameObject;
					activeClicked.GetComponent<Button>().SetButtonDown();
				}
				if (hit.transform.gameObject.name == "right_up(Clone)")
				{
					activeClicked = hit.transform.parent.gameObject;
					activeClicked.GetComponent<Button>().SetButtonDown();
				}
			}
		}

		if (Input.GetMouseButtonUp(0))
		{
			if (activeClicked == null) return;

			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

			if (hit.collider != null)
			{
				if (activeClicked.GetComponent<Button>().GetName() == "back" && hit.transform.gameObject.name == "back_down(Clone)")
				{
					HideAll();
					ShowMenu();
				}
				// Main
				if (activeClicked.GetComponent<Button>().GetName() == "play" && hit.transform.gameObject.name == "play_down(Clone)")
				{
					HideAll();
					ShowSelectModeMenu();
                    Application.LoadLevel("main");
				}
				if (activeClicked.GetComponent<Button>().GetName() == "settings" && hit.transform.gameObject.name == "settings_down(Clone)")
				{
					HideAll();
					ShowSettingsMenu();
				}
				if (activeClicked.GetComponent<Button>().GetName() == "tutorial" && hit.transform.gameObject.name == "tutorial_down(Clone)")
				{
					GooglePlayServices.Instance.ShowAchievements();
				}
				if (activeClicked.GetComponent<Button>().GetName() == "about" && hit.transform.gameObject.name == "about_down(Clone)")
				{
					GooglePlayServices.Instance.ShowLeaderBoard(NORMAL_MODE_BOARD);
				}
				if (activeClicked.GetComponent<Button>().GetName() == "exit" && hit.transform.gameObject.name == "exit_down(Clone)")
				{
					Application.Quit();
				}
				// Settings
				if (activeClicked.GetComponent<Button>().GetName() == "sound_dis" && hit.transform.gameObject.name == "sound_down_dis(Clone)")
				{
					Ini.SaveVolume(1);
					ReloadSound();
				}
				if (activeClicked.GetComponent<Button>().GetName() == "sound_en" && hit.transform.gameObject.name == "sound_down_en(Clone)")
				{
					Ini.SaveVolume(0);
					ReloadSound();
				}
				if (activeClicked.GetComponent<Button>().GetName() == "left" && hit.transform.parent.gameObject.name == "left_min")
				{
					GameObject value_min = GameObject.Find("value_min");
					int oldValue = Convert.ToInt32(value_min.GetComponent<TextMesh>().text);
					if (oldValue != 3)
					{
						value_min.GetComponent<TextMesh>().text = (oldValue - 1).ToString();
						Ini.SaveMinBoom(oldValue - 1);
					}
				}
				if (activeClicked.GetComponent<Button>().GetName() == "right" && hit.transform.parent.gameObject.name == "right_min")
				{
					GameObject value_min = GameObject.Find("value_min");
					int oldValue = Convert.ToInt32(value_min.GetComponent<TextMesh>().text);
					if (oldValue != 100)
					{
						value_min.GetComponent<TextMesh>().text = (oldValue + 1).ToString();
						Ini.SaveMinBoom(oldValue + 1);
					}
				}
				if (activeClicked.GetComponent<Button>().GetName() == "left" && hit.transform.parent.gameObject.name == "left_gen")
				{
					GameObject value_gen = GameObject.Find("value_gen");
					int oldValue = Convert.ToInt32(value_gen.GetComponent<TextMesh>().text);
					if (oldValue != 3)
					{
						value_gen.GetComponent<TextMesh>().text = (oldValue - 1).ToString();
						Ini.SaveGeneratingCount(oldValue - 1);
					}
				}
				if (activeClicked.GetComponent<Button>().GetName() == "right" && hit.transform.parent.gameObject.name == "right_gen")
				{
					GameObject value_gen = GameObject.Find("value_gen");
					int oldValue = Convert.ToInt32(value_gen.GetComponent<TextMesh>().text);
					if (oldValue != 100)
					{
						value_gen.GetComponent<TextMesh>().text = (oldValue + 1).ToString();
						Ini.SaveGeneratingCount(oldValue + 1);
					}
				}
			}
			activeClicked.GetComponent<Button>().SetButtonUp();
			activeClicked = null;
		}
	}

	private void HideAll()
	{
		if (playCopy != null)
		{
			iTween.FadeTo(playCopy, iTween.Hash("alpha", 0, "time", 0.3f));
			Destroy(playCopy, 0.3f);
		}
		if (settingsCopy != null)
		{
			iTween.FadeTo(settingsCopy, iTween.Hash("alpha", 0, "time", 0.3f));
			Destroy(settingsCopy, 0.3f);
		}
		if (tutorialCopy != null)
		{
			iTween.FadeTo(tutorialCopy, iTween.Hash("alpha", 0, "time", 0.3f));
			Destroy(tutorialCopy, 0.3f);
		}
		if (aboutCopy != null)
		{
			iTween.FadeTo(aboutCopy, iTween.Hash("alpha", 0, "time", 0.3f));
			Destroy(aboutCopy, 0.3f);
		}
		if (exitCopy != null)
		{
			iTween.FadeTo(exitCopy, iTween.Hash("alpha", 0, "time", 0.3f));
			Destroy(exitCopy, 0.3f);
		}
		if (sound_enCopy != null)
		{
			iTween.FadeTo(sound_enCopy, iTween.Hash("alpha", 0, "time", 0.3f));
			Destroy(sound_enCopy, 0.3f);
		}
		if (sound_disCopy != null)
		{
			iTween.FadeTo(sound_disCopy, iTween.Hash("alpha", 0, "time", 0.3f));
			Destroy(sound_disCopy, 0.3f);
		}
		if (backCopy != null)
		{
			iTween.FadeTo(backCopy, iTween.Hash("alpha", 0, "time", 0.3f));
			Destroy(backCopy, 0.3f);	
		}
		if (leftCopyGen != null)
		{
			iTween.FadeTo(leftCopyGen, iTween.Hash("alpha", 0, "time", 0.3f));
			Destroy(leftCopyGen, 0.3f);
		}
		if (leftCopyMin != null)
		{
			iTween.FadeTo(leftCopyMin, iTween.Hash("alpha", 0, "time", 0.3f));
			Destroy(leftCopyMin, 0.3f);
		}
		if (rightCopyGen != null)
		{
			iTween.FadeTo(rightCopyGen, iTween.Hash("alpha", 0, "time", 0.3f));
			Destroy(rightCopyGen, 0.3f);
		}
		if (rightCopyMin != null)
		{
			iTween.FadeTo(rightCopyMin, iTween.Hash("alpha", 0, "time", 0.3f));
			Destroy(rightCopyMin, 0.3f);
		}
		if (labelMain != null)
		{
			iTween.FadeTo(labelMain, iTween.Hash("alpha", 0, "time", 0.3f));
			Destroy(labelMain, 0.3f);
		}
		if (labelGen != null)
		{
			iTween.FadeTo(labelGen, iTween.Hash("alpha", 0, "time", 0.3f));
			Destroy(labelGen, 0.3f);
		}
		if (labelBoom != null)
		{
			iTween.FadeTo(labelBoom, iTween.Hash("alpha", 0, "time", 0.3f));
			Destroy(labelBoom, 0.3f);
		}
		if (valueGen != null)
		{
			iTween.FadeTo(valueGen, iTween.Hash("alpha", 0, "time", 0.3f));
			Destroy(valueGen, 0.3f);
		}
		if (valueMin != null)
		{
			iTween.FadeTo(valueMin, iTween.Hash("alpha", 0, "time", 0.3f));
			Destroy(valueMin, 0.3f);
		}

	}

	private void ShowSettingsMenu()
	{
		if (Ini.LoadVolume() == 1) sound_enCopy = (GameObject)Instantiate(sound_en, new Vector3(7, 4, -5), Quaternion.identity);
		if (Ini.LoadVolume() == 0) sound_disCopy = (GameObject)Instantiate(sound_dis, new Vector3(7, 4, -5), Quaternion.identity);

		if (Ini.LoadVolume() == 1) iTween.MoveTo(sound_enCopy, iTween.Hash("position", new Vector3(-2, 4, -5), "time", 0.5f, "delay", 0.22f, "easetype", iTween.EaseType.easeOutSine));
		if (Ini.LoadVolume() == 0) iTween.MoveTo(sound_disCopy, iTween.Hash("position", new Vector3(-2, 4, -5), "time", 0.5f, "delay", 0.22f, "easetype", iTween.EaseType.easeOutSine));

		if (Ini.LoadVolume() == 1) iTween.MoveTo(sound_enCopy, iTween.Hash("position", new Vector3(-1.6f, 4, -5), "time", 0.2f, "delay", 0.72f, "easetype", iTween.EaseType.easeOutSine));
		if (Ini.LoadVolume() == 0) iTween.MoveTo(sound_disCopy, iTween.Hash("position", new Vector3(-1.6f, 4, -5), "time", 0.2f, "delay", 0.72f, "easetype", iTween.EaseType.easeOutSine));

		labelMain = Exp.Label("Settings for Training mode", 0.4f);
		labelMain.transform.position = new Vector3(7.5f, 2, -5);
		iTween.MoveTo(labelMain, iTween.Hash("position", new Vector3(-0.4f, 2, -5), "time", 0.5f, "delay", 0.22f, "easetype", iTween.EaseType.easeOutSine));
		iTween.MoveTo(labelMain, iTween.Hash("position", new Vector3(0, 2, -5), "time", 0.2f, "delay", 0.72f, "easetype", iTween.EaseType.easeOutSine));

		labelBoom = Exp.Label("Min block for crush: ", 0.3f);
		labelBoom.transform.position = new Vector3(7.5f, 1.2f, -5);
		iTween.MoveTo(labelBoom, iTween.Hash("position", new Vector3(-0.4f, 1.2f, -5), "time", 0.5f, "delay", 0.22f, "easetype", iTween.EaseType.easeOutSine));
		iTween.MoveTo(labelBoom, iTween.Hash("position", new Vector3(0, 1.2f, -5), "time", 0.2f, "delay", 0.72f, "easetype", iTween.EaseType.easeOutSine));

		// But left Min
		leftCopyMin = (GameObject)Instantiate(left, new Vector3(8, 0.2f, -5), Quaternion.identity);
		iTween.MoveTo(leftCopyMin, iTween.Hash("position", new Vector3(-2, 0.2f, -5), "time", 0.5f, "delay", 0.5f, "easetype", iTween.EaseType.easeOutSine));
		iTween.MoveTo(leftCopyMin, iTween.Hash("position", new Vector3(-1.6f, 0.2f, -5), "time", 0.2f, "delay", 1f, "easetype", iTween.EaseType.easeOutSine));
		leftCopyMin.name = "left_min";

		// Value min
		valueMin = Exp.Label(Ini.LoadMinBoom().ToString(), 0.6f);
		valueMin.transform.position = new Vector3(7.5f, 0.2f, -5);
		iTween.MoveTo(valueMin, iTween.Hash("position", new Vector3(-0.4f, 0.2f, -5), "time", 0.5f, "delay", 0.5f, "easetype", iTween.EaseType.easeOutSine));
		iTween.MoveTo(valueMin, iTween.Hash("position", new Vector3(0, 0.2f, -5), "time", 0.2f, "delay", 1f, "easetype", iTween.EaseType.easeOutSine));
		valueMin.name = "value_min";

		// But Right Min
		rightCopyMin = (GameObject)Instantiate(right, new Vector3(11.2f, 0.2f, -5), Quaternion.identity);
		iTween.MoveTo(rightCopyMin, iTween.Hash("position", new Vector3(1.2f, 0.2f, -5), "time", 0.5f, "delay", 0.5f, "easetype", iTween.EaseType.easeOutSine));
		iTween.MoveTo(rightCopyMin, iTween.Hash("position", new Vector3(1.6f, 0.2f, -5), "time", 0.2f, "delay", 1f, "easetype", iTween.EaseType.easeOutSine));
		rightCopyMin.name = "right_min";

		labelGen = Exp.Label("Generated blocks count: ", 0.3f);
		labelGen.transform.position = new Vector3(7.5f, -0.8f, -5);
		iTween.MoveTo(labelGen, iTween.Hash("position", new Vector3(-0.4f, -0.8f, -5), "time", 0.5f, "delay", 0.22f, "easetype", iTween.EaseType.easeOutSine));
		iTween.MoveTo(labelGen, iTween.Hash("position", new Vector3(0, -0.8f, -5), "time", 0.2f, "delay", 0.72f, "easetype", iTween.EaseType.easeOutSine));

		// But left Gen
		leftCopyGen = (GameObject)Instantiate(left, new Vector3(8, -1.8f, -5), Quaternion.identity);
		iTween.MoveTo(leftCopyGen, iTween.Hash("position", new Vector3(-2, -1.8f, -5), "time", 0.5f, "delay", 0.5f, "easetype", iTween.EaseType.easeOutSine));
		iTween.MoveTo(leftCopyGen, iTween.Hash("position", new Vector3(-1.6f, -1.8f, -5), "time", 0.2f, "delay", 1f, "easetype", iTween.EaseType.easeOutSine));
		leftCopyGen.name = "left_gen";

		// Value Gen
		valueGen = Exp.Label(Ini.LoadGeneratingCount().ToString(), 0.6f);
		valueGen.transform.position = new Vector3(7.5f, -1.8f, -5);
		iTween.MoveTo(valueGen, iTween.Hash("position", new Vector3(-0.4f, -1.8f, -5), "time", 0.5f, "delay", 0.5f, "easetype", iTween.EaseType.easeOutSine));
		iTween.MoveTo(valueGen, iTween.Hash("position", new Vector3(0, -1.8f, -5), "time", 0.2f, "delay", 1f, "easetype", iTween.EaseType.easeOutSine));
		valueGen.name = "value_gen";

		// But Right Gen
		rightCopyGen = (GameObject)Instantiate(right, new Vector3(11.2f, -1.8f, -5), Quaternion.identity);
		iTween.MoveTo(rightCopyGen, iTween.Hash("position", new Vector3(1.2f, -1.8f, -5), "time", 0.5f, "delay", 0.5f, "easetype", iTween.EaseType.easeOutSine));
		iTween.MoveTo(rightCopyGen, iTween.Hash("position", new Vector3(1.6f, -1.8f, -5), "time", 0.2f, "delay", 1f, "easetype", iTween.EaseType.easeOutSine));
		rightCopyGen.name = "right_gen";

		backCopy = (GameObject)Instantiate(back, new Vector3(9.5f, -6, -5), Quaternion.identity);
		iTween.MoveTo(backCopy, iTween.Hash("position", new Vector3(0.5f, -6, -5), "time", 0.5f, "delay", 0.22f, "easetype", iTween.EaseType.easeOutSine));
		iTween.MoveTo(backCopy, iTween.Hash("position", new Vector3(0.9f, -6, -5), "time", 0.2f, "delay", 0.72f, "easetype", iTween.EaseType.easeOutSine));
		
	}

	private void ReloadSound()
	{
		Destroy(sound_disCopy);
		Destroy(sound_enCopy);

		if (Ini.LoadVolume() == 1) sound_enCopy = (GameObject)Instantiate(sound_en, new Vector3(-1.6f, 4, -5), Quaternion.identity);
		if (Ini.LoadVolume() == 0) sound_disCopy = (GameObject)Instantiate(sound_dis, new Vector3(-1.6f, 4, -5), Quaternion.identity);
	}

	private void ShowSelectModeMenu()
	{

	}
}
