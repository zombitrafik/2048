using UnityEngine;
using System.Collections;
using System;
using System.IO;

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

	private GameObject activeClicked;

	private int cameraMargin = 12;

    private int score = 0;
    private int bestScore = 0;
    private bool achieveChecked = false;

	private bool showRepostIco = false;
	public Texture facebook;
	public GUIStyle style;


	void Start()
	{
		

		instance = this;
		if (Ini.LoadControl() == "swipe") HideButtonControl();
	}

	private void HideButtonControl()
	{
		GameObject.Find("left").GetComponent<SpriteRenderer>().enabled = false;
		GameObject.Find("left").GetComponent<BoxCollider2D>().enabled = false;
		GameObject.Find("right").GetComponent<SpriteRenderer>().enabled = false;
		GameObject.Find("right").GetComponent<BoxCollider2D>().enabled = false;
		GameObject.Find("up").GetComponent<SpriteRenderer>().enabled = false;
		GameObject.Find("up").GetComponent<BoxCollider2D>().enabled = false;
		GameObject.Find("down").GetComponent<SpriteRenderer>().enabled = false;
		GameObject.Find("down").GetComponent<BoxCollider2D>().enabled = false;
	}

    void Repost(string text, string chooserTitle)
    {
        try
        {
            AndroidJavaClass ajc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject ajo = ajc.GetStatic<AndroidJavaObject>("currentActivity");

            AndroidJavaClass jc = new AndroidJavaClass("com.kimreik.moveandcrush.MainActivity");
                        
            Application.CaptureScreenshot("testasd.png");

            string picPath = jc.CallStatic<string>("getExternalStorageDirectory");

            File.Move(Application.persistentDataPath + "/testasd.png", jc.CallStatic<string>("getExternalStorageDirectory"));

            jc.CallStatic("shareText", ajo, picPath, text, chooserTitle);


        }
        catch (Exception e)
        {
        }
        
    }


    string MergeRepostText()
    {
        string res = "";
        res += Localization.GetWord("Repost text");
        res += score+" ";
        res += Localization.GetWord("Repost link")+" ";
        res += Localization.GetWord("Repost tags");
        return res;
    }


	void OnGUI()
	{
		if (showRepostIco)
		{
			if (GUI.Button(new Rect(Screen.width / 2 - 64, Screen.height / 2 + 40, 128, 128), facebook, style))
			{
				//Application.OpenURL("https://twitter.com/intent/tweet?text=&url=http%3A%2F%2Fkimreik.zz.mu%2FMoveandcrush%2Findex.html");
                CheckAchieve();
            }
			//twitter   Application.OpenURL("https://twitter.com/intent/tweet?text=&url=http%3A%2F%2Fkimreik.zz.mu%2FMoveandcrush%2Findex.html");
			//facebook  Application.OpenURL("https://www.facebook.com/sharer/sharer.php?m2w&u=http%3A%2F%2Fkimreik.zz.mu%2FMoveandcrush%2Findex.html");
			//vk.com    Application.OpenURL("http://vk.com/share.php?url=http%3A%2F%2Fkimreik.zz.mu%2FMoveandcrush%2Findex.html&title=&description=");

		}
	}

    private void CheckAchieve()
    {
        if (!achieveChecked)
        {
            GooglePlayServices.Instance.UpdateAchieveProgress(GooglePlayServices.ACHIEVE_SOCIALLY_ACTIVE, 100);
            achieveChecked = true;
        }    
    }

	public void ShowGameOverMenu()
	{

		score = Convert.ToInt32(GameObject.Find("Score").GetComponent<TextMesh>().text.Replace(Localization.GetWord("Score") + ": ", ""));
		bestScore = Convert.ToInt32(GameObject.Find("BestScore").GetComponent<TextMesh>().text.Replace(Localization.GetWord("You best score") + ": ", ""));

		retryCopy = (GameObject)Instantiate(retry, new Vector3(-2, -12, -5), Quaternion.identity);
		exitCopy = (GameObject)Instantiate(exit, new Vector3(2, -12, -5), Quaternion.identity);

		Destroy(GameObject.Find("GUI"));

		GameObject scoreCopy = Exp.ViewScore(score, Localization.GetWord("Score") + ": ");
		scoreCopy.transform.position = new Vector3(12, 9, -5);
		GameObject bestScoreCopy = Exp.ViewScore(bestScore, Localization.GetWord("You best score") + ": ");
		bestScoreCopy.transform.position = new Vector3(12, 7, -5);

		iTween.ValueTo(gameObject, iTween.Hash("from", 0, "to", 0.85f, "time", .5f, "delay", 0.5f, "onupdate", "changeColor"));

		iTween.MoveTo(retryCopy, iTween.Hash("position", new Vector3(0, -5, -5), "time", 0.5f, "delay", 0.8f, "easetype", iTween.EaseType.easeOutSine));
		iTween.MoveTo(exitCopy, iTween.Hash("position", new Vector3(0, -7, -5), "time", 0.5f, "delay", 1f, "easetype", iTween.EaseType.easeOutSine));

		iTween.MoveTo(scoreCopy, iTween.Hash("position", new Vector3(0, 9, -5), "time", 0.5f, "delay", 0.4f, "easetype", iTween.EaseType.easeOutSine));
		iTween.MoveTo(bestScoreCopy, iTween.Hash("position", new Vector3(0, 7, -5), "time", 0.5f, "delay", 0.6f, "easetype", iTween.EaseType.easeOutSine));
		
		if (score == bestScore) Ini.SaveRecord(bestScore);
		
		showRepostIco = true;
		GameObject.Find("Share").GetComponent<TextMesh>().text = Localization.GetWord("Share") + ":";
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

		backCopy = (GameObject)Instantiate(back, new Vector3(cameraMargin + 8, 2, -5), Quaternion.identity);
		retryCopy = (GameObject)Instantiate(retry, new Vector3(cameraMargin + 8, 0, -5), Quaternion.identity);
		exitCopy = (GameObject)Instantiate(exit, new Vector3(cameraMargin + 8, -2, -5), Quaternion.identity);


		iTween.MoveTo(backCopy, iTween.Hash("position", new Vector3(cameraMargin + -0.5f, 2, -5), "time", 0.5f, "delay", 0.22f, "easetype", iTween.EaseType.easeOutSine));
		iTween.MoveTo(retryCopy, iTween.Hash("position", new Vector3(cameraMargin + 0, 0, -5), "time", 0.5f, "delay", 0.24f, "easetype", iTween.EaseType.easeOutSine));
		iTween.MoveTo(exitCopy, iTween.Hash("position", new Vector3(cameraMargin + 0.5f, -2, -5), "time", 0.5f, "delay", 0.26f, "easetype", iTween.EaseType.easeOutSine));

		iTween.MoveTo(backCopy, iTween.Hash("position", new Vector3(cameraMargin + -0.1f, 2, -5), "time", 0.2f, "delay", 0.72f, "easetype", iTween.EaseType.easeOutSine));
		iTween.MoveTo(retryCopy, iTween.Hash("position", new Vector3(cameraMargin + 0.4f, 0, -5), "time", 0.2f, "delay", 0.74f, "easetype", iTween.EaseType.easeOutSine));
		iTween.MoveTo(exitCopy, iTween.Hash("position", new Vector3(cameraMargin + 0.9f, -2, -5), "time", 0.2f, "delay", 0.76f, "easetype", iTween.EaseType.easeOutSine));
	}

	public void HideAll()
	{
		iTween.MoveTo(gameObject, iTween.Hash("x", 0, "time", 0.4f, "easetype", iTween.EaseType.easeOutSine));

		pause = false;

		if (backCopy != null)
		{
			iTween.FadeTo(backCopy, iTween.Hash("alpha", 0, "time", 0.3f));
			Destroy(backCopy, 0.3f);
		}
		if (retryCopy != null)
		{
			iTween.FadeTo(retryCopy, iTween.Hash("alpha", 0, "time", 0.3f));
			Destroy(retryCopy, 0.3f);
		}
		if (exitCopy != null)
		{
			iTween.FadeTo(exitCopy, iTween.Hash("alpha", 0, "time", 0.3f));
			Destroy(exitCopy, 0.3f);
		}
		
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
				// arrows

				if (hit.transform.gameObject.name == "left")
				{
					activeClicked = hit.transform.gameObject;
					hit.transform.gameObject.GetComponent<SpriteRenderer>().color = new Color(0.1462f, 0.5374f, 0.9020f);
				}
				if (hit.transform.gameObject.name == "up")
				{
					activeClicked = hit.transform.gameObject;
					hit.transform.gameObject.GetComponent<SpriteRenderer>().color = new Color(0.1462f, 0.5374f, 0.9020f);
				}
				if (hit.transform.gameObject.name == "down")
				{
					activeClicked = hit.transform.gameObject;
					hit.transform.gameObject.GetComponent<SpriteRenderer>().color = new Color(0.1462f, 0.5374f, 0.9020f);
				}
				if (hit.transform.gameObject.name == "right")
				{
					activeClicked = hit.transform.gameObject;
					hit.transform.gameObject.GetComponent<SpriteRenderer>().color = new Color(0.1462f, 0.5374f, 0.9020f);
				}

				// /arrows

				if (hit.transform.parent == null) return;
				if (hit.transform.parent.name == "but_back(Clone)")
				{
					activeClicked = hit.transform.parent.gameObject;
					activeClicked.GetComponent<Button>().SetButtonDown();
				}
				if (hit.transform.parent.name == "but_mainmenu(Clone)")
				{
					activeClicked = hit.transform.parent.gameObject;
					activeClicked.GetComponent<Button>().SetButtonDown();
				}
				if (hit.transform.parent.name == "but_retry(Clone)")
				{
					activeClicked = hit.transform.parent.gameObject;
					activeClicked.GetComponent<Button>().SetButtonDown();
				}
				if (hit.transform.parent.name == "but_volume")
				{
					activeClicked = hit.transform.parent.gameObject;
					activeClicked.GetComponent<Volume>().SetButtonDown();
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
				if (hit.transform.gameObject.name == "left")
				{
					MainClass.Instance.MoveLeft();
				}
				if (hit.transform.gameObject.name == "up")
				{
					MainClass.Instance.MoveUp();
				}
				if (hit.transform.gameObject.name == "down")
				{
					MainClass.Instance.MoveDown();
				}
				if (hit.transform.gameObject.name == "right")
				{
					MainClass.Instance.MoveRight();
				}

				if (activeClicked.GetComponent<Button>() != null)
				{
					if (activeClicked.GetComponent<Button>().GetName() == "back" && hit.transform.parent.name == "but_back(Clone)")
					{
						pause = false;
						MainClass.Instance.isPauseMenu = false;
						HideAll();
					}
					if (activeClicked.GetComponent<Button>().GetName() == "mainmenu" && hit.transform.parent.name == "but_mainmenu(Clone)")
					{
						HideAll();
						Application.LoadLevel("main_menu");
					}
					if (activeClicked.GetComponent<Button>().GetName() == "retry" && hit.transform.parent.name == "but_retry(Clone)")
					{
						HideAll();
						Ini.DeleteSavedGame();
						Application.LoadLevel("main");
					}
				}
				else if (activeClicked.GetComponent<Volume>() != null)
				{
					if (activeClicked.GetComponent<Volume>().GetName() == "volume" && hit.transform.parent.name == "but_volume")
					{
						if (Ini.LoadVolume() == 0) Ini.SaveVolume(1); else Ini.SaveVolume(0);
					}
				}

				//arrows

				

			}
			if (activeClicked.GetComponent<Button>() != null)
				activeClicked.GetComponent<Button>().SetButtonUp();
			else if (activeClicked.GetComponent<Volume>() != null)
				activeClicked.GetComponent<Volume>().SetButtonUp();
			else activeClicked.GetComponent<SpriteRenderer>().color = Color.white;
			activeClicked = null;

			

		}
	}
}
