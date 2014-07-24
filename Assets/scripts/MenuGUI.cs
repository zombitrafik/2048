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

	private int cameraMargin = 12;

	private bool showRepostIco = false;
	public Texture facebook;
	public Texture twitter;
	public Texture vk;
	public GUIStyle style;
	void Start()
	{
		instance = this;
	}


	void OnGUI()
	{
		if (showRepostIco)
		{
			if (GUI.Button(new Rect(Screen.width / 2 - 32, Screen.height / 2 + 32, 64, 64), facebook, style))
			{
				Application.OpenURL("https://www.facebook.com/sharer/sharer.php?m2w&u=http%3A%2F%2Fkimreik.zz.mu%2FMoveandcrush%2Findex.html");
			}
			if (GUI.Button(new Rect(Screen.width / 2 - 128, Screen.height / 2 + 32, 64, 64), twitter, style))
			{
				Application.OpenURL("https://twitter.com/intent/tweet?text=&url=http%3A%2F%2Fkimreik.zz.mu%2FMoveandcrush%2Findex.html");
			}
			if (GUI.Button(new Rect(Screen.width / 2 + 64, Screen.height / 2 + 32, 64, 64), vk, style))
			{
				Application.OpenURL("http://vk.com/share.php?url=http%3A%2F%2Fkimreik.zz.mu%2FMoveandcrush%2Findex.html&title=&description=&");
			}
			//twitter   Application.OpenURL("https://twitter.com/intent/tweet?text=&url=http%3A%2F%2Fkimreik.zz.mu%2FMoveandcrush%2Findex.html");
			//facebook  Application.OpenURL("https://www.facebook.com/sharer/sharer.php?m2w&u=http%3A%2F%2Fkimreik.zz.mu%2FMoveandcrush%2Findex.html");
			//vk.com    Application.OpenURL("http://vk.com/share.php?url=http%3A%2F%2Fkimreik.zz.mu%2FMoveandcrush%2Findex.html&title=&description=");

		}
	}

	public void ShowGameOverMenu()
	{

		int score = Convert.ToInt32(GameObject.Find("Score").GetComponent<TextMesh>().text.Replace("Score: ", ""));
		int bestScore = Convert.ToInt32(GameObject.Find("BestScore").GetComponent<TextMesh>().text.Replace("Your best score: ", ""));

		retryCopy = (GameObject)Instantiate(retry, new Vector3(-2, -12, -5), Quaternion.identity);
		exitCopy = (GameObject)Instantiate(exit, new Vector3(2, -12, -5), Quaternion.identity);

		Destroy(GameObject.Find("GUI"));

		GameObject scoreCopy = Exp.ViewScore(score, "Score: ");
		scoreCopy.transform.position = new Vector3(12, 9, -5);
		GameObject bestScoreCopy = Exp.ViewScore(bestScore, "Best score: ");
		bestScoreCopy.transform.position = new Vector3(12, 7, -5);

		iTween.ValueTo(gameObject, iTween.Hash("from", 0, "to", 0.85f, "time", .5f, "delay", 0.5f, "onupdate", "changeColor"));

		iTween.MoveTo(retryCopy, iTween.Hash("position", new Vector3(-2, -7, -5), "time", 0.5f, "delay", 0.8f, "easetype", iTween.EaseType.easeOutSine));
		iTween.MoveTo(exitCopy, iTween.Hash("position", new Vector3(2, -7, -5), "time", 0.5f, "delay", 1f, "easetype", iTween.EaseType.easeOutSine));

		iTween.MoveTo(scoreCopy, iTween.Hash("position", new Vector3(0, 9, -5), "time", 0.5f, "delay", 0.4f, "easetype", iTween.EaseType.easeOutSine));
		iTween.MoveTo(bestScoreCopy, iTween.Hash("position", new Vector3(0, 7, -5), "time", 0.5f, "delay", 0.6f, "easetype", iTween.EaseType.easeOutSine));

		if (score == bestScore) Ini.SaveRecord(bestScore);

		showRepostIco = true;
		GameObject.Find("Share").GetComponent<TextMesh>().text = "Share with your friends: ";
	}

	void changeColor(float val)
	{
		GameObject bg = GameObject.Find("bg");
		bg.GetComponent<SpriteRenderer>().color = new Color(0,0,0,val);
	}

	public void ShowBackMenu()
	{
		iTween.MoveTo(gameObject, iTween.Hash("x", cameraMargin, "time", 0.2f, "easetype", iTween.EaseType.easeOutSine));

		pause = true;

		backCopy = (GameObject)Instantiate(back, new Vector3(cameraMargin + 7, 4, -5), Quaternion.identity);
		if (Ini.LoadVolume() == 1) sound_enCopy = (GameObject)Instantiate(sound_en, new Vector3(cameraMargin + 7, 2, -5), Quaternion.identity);
		if (Ini.LoadVolume() == 0) sound_disCopy = (GameObject)Instantiate(sound_dis, new Vector3(cameraMargin + 7, 2, -5), Quaternion.identity);
		retryCopy = (GameObject)Instantiate(retry, new Vector3(cameraMargin + 7, 0, -5), Quaternion.identity);
		exitCopy = (GameObject)Instantiate(exit, new Vector3(cameraMargin + 7, -2, -5), Quaternion.identity);


		iTween.MoveTo(backCopy, iTween.Hash("position", new Vector3(cameraMargin + -1, 4, -5), "time", 0.5f, "delay", 0.2f, "easetype", iTween.EaseType.easeOutSine));
		if (Ini.LoadVolume() == 1) iTween.MoveTo(sound_enCopy, iTween.Hash("position", new Vector3(cameraMargin + -0.5f, 2, -5), "time", 0.5f, "delay", 0.22f, "easetype", iTween.EaseType.easeOutSine));
		if (Ini.LoadVolume() == 0) iTween.MoveTo(sound_disCopy, iTween.Hash("position", new Vector3(cameraMargin + -0.5f, 2, -5), "time", 0.5f, "delay", 0.22f, "easetype", iTween.EaseType.easeOutSine));
		iTween.MoveTo(retryCopy, iTween.Hash("position", new Vector3(cameraMargin + 0, 0, -5), "time", 0.5f, "delay", 0.24f, "easetype", iTween.EaseType.easeOutSine));
		iTween.MoveTo(exitCopy, iTween.Hash("position", new Vector3(cameraMargin + 0.5f, -2, -5), "time", 0.5f, "delay", 0.26f, "easetype", iTween.EaseType.easeOutSine));

		iTween.MoveTo(backCopy, iTween.Hash("position", new Vector3(cameraMargin + -0.6f, 4, -5), "time", 0.2f, "delay", 0.7f, "easetype", iTween.EaseType.easeOutSine));
		if (Ini.LoadVolume() == 1) iTween.MoveTo(sound_enCopy, iTween.Hash("position", new Vector3(cameraMargin + -0.1f, 2, -5), "time", 0.2f, "delay", 0.72f, "easetype", iTween.EaseType.easeOutSine));
		if (Ini.LoadVolume() == 0) iTween.MoveTo(sound_disCopy, iTween.Hash("position", new Vector3(cameraMargin + -0.1f, 2, -5), "time", 0.2f, "delay", 0.72f, "easetype", iTween.EaseType.easeOutSine));
		iTween.MoveTo(retryCopy, iTween.Hash("position", new Vector3(cameraMargin + 0.4f, 0, -5), "time", 0.2f, "delay", 0.74f, "easetype", iTween.EaseType.easeOutSine));
		iTween.MoveTo(exitCopy, iTween.Hash("position", new Vector3(cameraMargin + 0.9f, -2, -5), "time", 0.2f, "delay", 0.76f, "easetype", iTween.EaseType.easeOutSine));
	}

	public void HideBackMenu()
	{
		iTween.MoveTo(gameObject, iTween.Hash("x", 0, "time", 0.4f, "easetype", iTween.EaseType.easeOutSine));

		pause = false;

		iTween.MoveTo(backCopy, iTween.Hash("position", new Vector3(cameraMargin + 7, 4, -5), "time", 0.5f, "easetype", iTween.EaseType.easeOutSine));
		if (Ini.LoadVolume() == 1) iTween.MoveTo(sound_enCopy, iTween.Hash("position", new Vector3(cameraMargin + 7, 2, -5), "time", 0.5f, "delay", 0.02f, "easetype", iTween.EaseType.easeOutSine));
		if (Ini.LoadVolume() == 0) iTween.MoveTo(sound_disCopy, iTween.Hash("position", new Vector3(cameraMargin + 7, 2, -5), "time", 0.5f, "delay", 0.02f, "easetype", iTween.EaseType.easeOutSine));
		iTween.MoveTo(retryCopy, iTween.Hash("position", new Vector3(cameraMargin + 7, 0, -5), "time", 0.5f, "delay", 0.04f, "easetype", iTween.EaseType.easeOutSine));
		iTween.MoveTo(exitCopy, iTween.Hash("position", new Vector3(cameraMargin + 7, -2, -5), "time", 0.5f, "delay", 0.06f, "easetype", iTween.EaseType.easeOutSine));
		

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
                    Ini.DeleteSavedGame();
					Application.LoadLevel("main");
				}
				if (activeClicked.GetComponent<Button>().GetName() == "sound_dis" && hit.transform.gameObject.name == "sound_down_dis(Clone)")
				{
					Ini.SaveVolume(1);
					ReloadMenu();
				}
				if (activeClicked.GetComponent<Button>().GetName() == "sound_en" && hit.transform.gameObject.name == "sound_down_en(Clone)")
				{
					Ini.SaveVolume(0);
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

		backCopy = (GameObject)Instantiate(back, new Vector3(cameraMargin + -0.6f, 4, -5), Quaternion.identity);
		if (Ini.LoadVolume() == 1) sound_enCopy = (GameObject)Instantiate(sound_en, new Vector3(cameraMargin + -0.1f, 2, -5), Quaternion.identity);
		if (Ini.LoadVolume() == 0) sound_disCopy = (GameObject)Instantiate(sound_dis, new Vector3(cameraMargin + -0.1f, 2, -5), Quaternion.identity);
		retryCopy = (GameObject)Instantiate(retry, new Vector3(cameraMargin + 0.4f, 0, -5), Quaternion.identity);
		exitCopy = (GameObject)Instantiate(exit, new Vector3(cameraMargin + 0.9f, -2, -5), Quaternion.identity);
	}
}
