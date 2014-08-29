using UnityEngine;
using System.Collections;
using System;
using System.IO;

public class MainMenu : MonoBehaviour
{
	// Achive
	public GameObject achievement;
	private GameObject achievementCopy;

	public GameObject ladderScore;
	private GameObject ladderScoreCopy;

	public GameObject ladderCombo;
	private GameObject ladderComboCopy;

	public GameObject ladderFigSize;
	private GameObject ladderFigSizeCopy;

	// Lang
	public GameObject ru;
	private GameObject ruCopy;

	public GameObject en;
	private GameObject enCopy;

	public GameObject de;
	private GameObject deCopy;

	public GameObject fr;
	private GameObject frCopy;

	// Main
	public GameObject play;
	private GameObject playCopy;

	public GameObject settings;
	private GameObject settingsCopy;

	public GameObject tutorial;
	private GameObject tutorialCopy;

	public GameObject achievements;
	private GameObject achievementsCopy;

	public GameObject evaluation;
	private GameObject evaluationCopy;

	public GameObject control;
	private GameObject controlCopy;

	public GameObject language;
	private GameObject languageCopy;

	public GameObject keyboard;
	private GameObject keyboardCopy;

	public GameObject swipe;
	private GameObject swipeCopy;

	// Settings
	public GameObject back;
	private GameObject backCopy;

	private GameObject labelMain;

	private GameObject activeClicked;

	void Start()
	{
		if (GoogleAnalytics.instance)
		{
			GoogleAnalytics.instance.LogScreen("main menu");
		}
		if (Ini.LoadControl() == "")
		{
			Ini.SaveControl("swipe");
		}

        if (!Ini.HaveSavedGame())
        {
            Ini.SaveGeneratingCount(3);
        }

		Ini.SaveMinBoom(4);
		ShowMenu();
	}

	/* Button Template 
	 
		playCopy = (GameObject)Instantiate(play, new Vector3(8, 4, -5), Quaternion.identity);
	    iTween.MoveTo(playCopy, iTween.Hash("position", new Vector3(-1.5f, 4, -5), "time", 0.5f, "delay", 0.5f, "easetype", iTween.EaseType.easeOutSine));
		iTween.MoveTo(playCopy, iTween.Hash("position", new Vector3(-1.1f, 4, -5), "time", 0.2f, "delay", 1f, "easetype", iTween.EaseType.easeOutSine));
	 
	*/
	public void ShowMenu()
	{
		playCopy = (GameObject)Instantiate(play, new Vector3(9, 4, -5), Quaternion.identity);
		settingsCopy = (GameObject)Instantiate(settings, new Vector3(9, 2, -5), Quaternion.identity);
		tutorialCopy = (GameObject)Instantiate(tutorial, new Vector3(9, 0, -5), Quaternion.identity);
		achievementsCopy = (GameObject)Instantiate(achievements, new Vector3(9, -2, -5), Quaternion.identity);
		evaluationCopy = (GameObject)Instantiate(evaluation, new Vector3(9, -4, -5), Quaternion.identity);

		iTween.MoveTo(playCopy, iTween.Hash("position", new Vector3(-1.0f, 4, -5), "time", 0.5f, "delay", 0.54f, "easetype", iTween.EaseType.easeOutSine));
		iTween.MoveTo(settingsCopy, iTween.Hash("position", new Vector3(-0.5f, 2, -5), "time", 0.5f, "delay", 0.56f, "easetype", iTween.EaseType.easeOutSine));
		iTween.MoveTo(tutorialCopy, iTween.Hash("position", new Vector3(0f, 0, -5), "time", 0.5f, "delay", 0.58f, "easetype", iTween.EaseType.easeOutSine));
		iTween.MoveTo(achievementsCopy, iTween.Hash("position", new Vector3(0.5f, -2, -5), "time", 0.5f, "delay", 0.60f, "easetype", iTween.EaseType.easeOutSine));
		iTween.MoveTo(evaluationCopy, iTween.Hash("position", new Vector3(1f, -4, -5), "time", 0.5f, "delay", 0.62f, "easetype", iTween.EaseType.easeOutSine));

		iTween.MoveTo(playCopy, iTween.Hash("position", new Vector3(-0.6f, 4, -5), "time", 0.2f, "delay", 1.04f, "easetype", iTween.EaseType.easeOutSine));
		iTween.MoveTo(settingsCopy, iTween.Hash("position", new Vector3(-0.1f, 2, -5), "time", 0.2f, "delay", 1.06f, "easetype", iTween.EaseType.easeOutSine));
		iTween.MoveTo(tutorialCopy, iTween.Hash("position", new Vector3(0.4f, 0, -5), "time", 0.2f, "delay", 1.08f, "easetype", iTween.EaseType.easeOutSine));
		iTween.MoveTo(achievementsCopy, iTween.Hash("position", new Vector3(0.9f, -2, -5), "time", 0.2f, "delay", 1.10f, "easetype", iTween.EaseType.easeOutSine));
		iTween.MoveTo(evaluationCopy, iTween.Hash("position", new Vector3(1.4f, -4, -5), "time", 0.2f, "delay", 1.12f, "easetype", iTween.EaseType.easeOutSine));
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

				if (hit.transform.parent.name == "but_back(Clone)")
				{
					activeClicked = hit.transform.parent.gameObject;
					activeClicked.GetComponent<Button>().SetButtonDown();
				}
				if (hit.transform.parent.name == "but_volume")
				{
					activeClicked = hit.transform.parent.gameObject;
					activeClicked.GetComponent<Volume>().SetButtonDown();
				}
				// Main
				if (hit.transform.parent.name == "but_play(Clone)")
				{
					activeClicked = hit.transform.parent.gameObject;
					activeClicked.GetComponent<Button>().SetButtonDown();
				}
				if (hit.transform.parent.name == "mode_up(Clone)")
				{
					activeClicked = GameObject.Find("but_mode(Clone)");
					activeClicked.GetComponent<Button>().SetButtonDown();
				}
				if (hit.transform.parent.name == "but_settings(Clone)")
				{
					activeClicked = hit.transform.parent.gameObject;
					activeClicked.GetComponent<Button>().SetButtonDown();
				}
				if (hit.transform.parent.name == "but_tutorial(Clone)")
				{
                    activeClicked = hit.transform.parent.gameObject;
					activeClicked.GetComponent<Button>().SetButtonDown();
				}
				if (hit.transform.parent.name == "but_achievements(Clone)")
				{
					activeClicked = hit.transform.parent.gameObject;
					activeClicked.GetComponent<Button>().SetButtonDown();
				}
				if (hit.transform.parent.name == "but_evaluation(Clone)")
				{
					activeClicked = hit.transform.parent.gameObject;
					activeClicked.GetComponent<Button>().SetButtonDown();
				}
				// Settings
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
				if (hit.transform.parent.name == "but_control(Clone)")
				{
					activeClicked = hit.transform.parent.gameObject;
					activeClicked.GetComponent<Button>().SetButtonDown();
				}
				if (hit.transform.parent.name == "but_language(Clone)")
				{
					activeClicked = hit.transform.parent.gameObject;
					activeClicked.GetComponent<Button>().SetButtonDown();
				}
				// Langs
				if (hit.transform.parent.name == "but_ru(Clone)")
				{
					activeClicked = hit.transform.parent.gameObject;
					activeClicked.GetComponent<Button>().SetButtonDown();
				}
				if (hit.transform.parent.name == "but_en(Clone)")
				{
					activeClicked = hit.transform.parent.gameObject;
					activeClicked.GetComponent<Button>().SetButtonDown();
				}
				if (hit.transform.parent.name == "but_fr(Clone)")
				{
					activeClicked = hit.transform.parent.gameObject;
					activeClicked.GetComponent<Button>().SetButtonDown();
				}
				if (hit.transform.parent.name == "but_de(Clone)")
				{
					activeClicked = hit.transform.parent.gameObject;
					activeClicked.GetComponent<Button>().SetButtonDown();
				}
				// Controls
				if (hit.transform.parent.name == "but_keyboard(Clone)")
				{
					activeClicked = hit.transform.parent.gameObject;
					activeClicked.GetComponent<Button>().SetButtonDown();
				}
				if (hit.transform.parent.name == "but_swipe(Clone)")
				{
					activeClicked = hit.transform.parent.gameObject;
					activeClicked.GetComponent<Button>().SetButtonDown();
				}
				// Selecting
				if (hit.transform.parent.name == "but_left(Clone)")
				{
					activeClicked = hit.transform.parent.gameObject;
					activeClicked.GetComponent<Selecting>().SetButtonDown();
				}
				if (hit.transform.parent.name == "but_right(Clone)")
				{
					activeClicked = hit.transform.parent.gameObject;
					activeClicked.GetComponent<Selecting>().SetButtonDown();
				}
				// Achive
				if (hit.transform.parent.name == "but_achive(Clone)")
				{
					activeClicked = hit.transform.parent.gameObject;
					activeClicked.GetComponent<Button>().SetButtonDown();
				}
				if (hit.transform.parent.name == "but_ladderCombo(Clone)")
				{
					activeClicked = hit.transform.parent.gameObject;
					activeClicked.GetComponent<Button>().SetButtonDown();
				}
				if (hit.transform.parent.name == "but_ladderFigSize(Clone)")
				{
					activeClicked = hit.transform.parent.gameObject;
					activeClicked.GetComponent<Button>().SetButtonDown();
				}
				if (hit.transform.parent.name == "but_ladderScore(Clone)")
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
				// <Button>()
				if (activeClicked.GetComponent<Button>() != null)
				{
					if (activeClicked.GetComponent<Button>().GetName() == "back" && hit.transform.parent.name == "but_back(Clone)")
					{
						HideAll();
						ShowMenu();
					}
					if (activeClicked.GetComponent<Button>().GetName() == "play" && hit.transform.parent.name == "but_play(Clone)")
					{
						HideAll();
						Application.LoadLevel("main");
					}
					if (activeClicked.GetComponent<Button>().GetName() == "settings" && hit.transform.parent.name == "but_settings(Clone)")
					{
						HideAll();
						ShowSettingsMenu();
					}
					if (activeClicked.GetComponent<Button>().GetName() == "tutorial" && hit.transform.parent.name == "but_tutorial(Clone)")
					{
                        
					}
					if (activeClicked.GetComponent<Button>().GetName() == "achievements" && hit.transform.parent.name == "but_achievements(Clone)")
					{
                        HideAll();
						if(GooglePlayServices.Instance.Authenticate()!=false){
							ShowAchiveMenu();
						} else{
							ShowAchiveErrorMenu();
						}

					}
					if (activeClicked.GetComponent<Button>().GetName() == "evaluation" && hit.transform.parent.name == "but_evaluation(Clone)")
					{
                        Application.OpenURL("market://details?id=com.kimreik.moveandcrush");
					}
					if (activeClicked.GetComponent<Button>().GetName() == "control" && hit.transform.parent.name == "but_control(Clone)")
					{
						HideAll();
						ShowSelectControlMenu();
					}
					if (activeClicked.GetComponent<Button>().GetName() == "language" && hit.transform.parent.name == "but_language(Clone)")
					{
						HideAll();
						ShowSelectLanguageMenu();
					}
					if (activeClicked.GetComponent<Button>().GetName() == "ru" && hit.transform.parent.name == "but_ru(Clone)")
					{
						Localization.languageManager.ChangeLanguage("ru");
					}
					if (activeClicked.GetComponent<Button>().GetName() == "en" && hit.transform.parent.name == "but_en(Clone)")
					{
						Localization.languageManager.ChangeLanguage("en");
					}
					if (activeClicked.GetComponent<Button>().GetName() == "de" && hit.transform.parent.name == "but_de(Clone)")
					{
						Localization.languageManager.ChangeLanguage("ge");
					}
					if (activeClicked.GetComponent<Button>().GetName() == "fr" && hit.transform.parent.name == "but_fr(Clone)")
					{
						Localization.languageManager.ChangeLanguage("fr");
					}

					// <achive>
					if (activeClicked.GetComponent<Button>().GetName() == "achive" && hit.transform.parent.name == "but_achive(Clone)")
					{
						GooglePlayServices.Instance.ShowAchievements();
					}
					if (activeClicked.GetComponent<Button>().GetName() == "ladderCombo" && hit.transform.parent.name == "but_ladderCombo(Clone)")
					{
						GooglePlayServices.Instance.ShowLeaderBoard(GooglePlayServices.COMBO_LEADER_BOARD);
					}
					if (activeClicked.GetComponent<Button>().GetName() == "ladderFigSize" && hit.transform.parent.name == "but_ladderFigSize(Clone)")
					{
						GooglePlayServices.Instance.ShowLeaderBoard(GooglePlayServices.FIGURE_SIZE_LEADER_BOARD);
					}
					if (activeClicked.GetComponent<Button>().GetName() == "ladderScore" && hit.transform.parent.name == "but_ladderScore(Clone)")
					{
						GooglePlayServices.Instance.ShowLeaderBoard(GooglePlayServices.SCORE_LEADER_BOARD);
					}
					// </achive>

					if (activeClicked.GetComponent<Button>().GetName() == "keyboard" && hit.transform.parent.name == "but_keyboard(Clone)")
					{
						Ini.SaveControl("keyboard");
						GameObject.Find("swipe_ico").GetComponent<SpriteRenderer>().enabled = false;
						GameObject.Find("keyboard").GetComponent<SpriteRenderer>().enabled = true;

					}
					if (activeClicked.GetComponent<Button>().GetName() == "swipe" && hit.transform.parent.name == "but_swipe(Clone)")
					{
						Ini.SaveControl("swipe");
						GameObject.Find("swipe_ico").GetComponent<SpriteRenderer>().enabled = true;
						GameObject.Find("keyboard").GetComponent<SpriteRenderer>().enabled = false;
					}
				}
				// <Volume>()
				else if (activeClicked.GetComponent<Volume>() != null)
				{
					if (activeClicked.GetComponent<Volume>().GetName() == "volume" && hit.transform.parent.name == "but_volume")
					{
						if (Ini.LoadVolume() == 0) Ini.SaveVolume(1); else Ini.SaveVolume(0);
					}
				}
				// <Selecting>()
				else
				{
	
				}
			}
			if (activeClicked.GetComponent<Button>() != null)
				activeClicked.GetComponent<Button>().SetButtonUp();
			else if (activeClicked.GetComponent<Volume>() != null) 
				activeClicked.GetComponent<Volume>().SetButtonUp();
			else activeClicked.GetComponent<Selecting>().SetButtonUp();
			activeClicked = null;
		}
	}


	// Зарефакторить
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
		if (achievementsCopy != null)
		{
			iTween.FadeTo(achievementsCopy, iTween.Hash("alpha", 0, "time", 0.3f));
			Destroy(achievementsCopy, 0.3f);
		}
		if (evaluationCopy != null)
		{
			iTween.FadeTo(evaluationCopy, iTween.Hash("alpha", 0, "time", 0.3f));
			Destroy(evaluationCopy, 0.3f);
		}
		if (backCopy != null)
		{
			iTween.FadeTo(backCopy, iTween.Hash("alpha", 0, "time", 0.3f));
			Destroy(backCopy, 0.3f);
		}	
		if (labelMain != null)
		{
			iTween.FadeTo(labelMain, iTween.Hash("alpha", 0, "time", 0.3f));
			Destroy(labelMain, 0.3f);
		}
		if (evaluationCopy != null)
		{
			iTween.FadeTo(evaluationCopy, iTween.Hash("alpha", 0, "time", 0.3f));
			Destroy(evaluationCopy, 0.3f);
		}
		if (controlCopy != null)
		{
			iTween.FadeTo(controlCopy, iTween.Hash("alpha", 0, "time", 0.3f));
			Destroy(controlCopy, 0.3f);
		}
		if (languageCopy != null)
		{
			iTween.FadeTo(languageCopy, iTween.Hash("alpha", 0, "time", 0.3f));
			Destroy(languageCopy, 0.3f);
		}
		if (ruCopy != null)
		{
			iTween.FadeTo(ruCopy, iTween.Hash("alpha", 0, "time", 0.3f));
			Destroy(ruCopy, 0.3f);
		}
		if (enCopy != null)
		{
			iTween.FadeTo(enCopy, iTween.Hash("alpha", 0, "time", 0.3f));
			Destroy(enCopy, 0.3f);
		}
		if (deCopy != null)
		{
			iTween.FadeTo(deCopy, iTween.Hash("alpha", 0, "time", 0.3f));
			Destroy(deCopy, 0.3f);
		}
		if (frCopy != null)
		{
			iTween.FadeTo(frCopy, iTween.Hash("alpha", 0, "time", 0.3f));
			Destroy(frCopy, 0.3f);
		}
		if (keyboardCopy != null)
		{
			iTween.FadeTo(keyboardCopy, iTween.Hash("alpha", 0, "time", 0.3f));
			Destroy(keyboardCopy, 0.3f);
		}
		if (swipeCopy != null)
		{
			iTween.FadeTo(swipeCopy, iTween.Hash("alpha", 0, "time", 0.3f));
			Destroy(swipeCopy, 0.3f);
		}
		if (ladderScoreCopy != null)
		{
			iTween.FadeTo(ladderScoreCopy, iTween.Hash("alpha", 0, "time", 0.3f));
			Destroy(ladderScoreCopy, 0.3f);
		}
		if (ladderComboCopy != null)
		{
			iTween.FadeTo(ladderComboCopy, iTween.Hash("alpha", 0, "time", 0.3f));
			Destroy(ladderComboCopy, 0.3f);
		}
		if (ladderFigSizeCopy != null)
		{
			iTween.FadeTo(ladderFigSizeCopy, iTween.Hash("alpha", 0, "time", 0.3f));
			Destroy(ladderFigSizeCopy, 0.3f);
		}
		if (achievementCopy != null)
		{
			iTween.FadeTo(achievementCopy, iTween.Hash("alpha", 0, "time", 0.3f));
			Destroy(achievementCopy, 0.3f);
		}
		

		GameObject.Find("swipe_ico").GetComponent<SpriteRenderer>().enabled = false;
		GameObject.Find("keyboard").GetComponent<SpriteRenderer>().enabled = false;
	}

	private void ShowSettingsMenu()
	{
		// language
		languageCopy = (GameObject)Instantiate(language, new Vector3(9, 2, -5), Quaternion.identity);
		iTween.MoveTo(languageCopy, iTween.Hash("position", new Vector3(-0.9f, 2, -5), "time", 0.5f, "delay", 0.20f, "easetype", iTween.EaseType.easeOutSine));
		iTween.MoveTo(languageCopy, iTween.Hash("position", new Vector3(-0.5f, 2, -5), "time", 0.2f, "delay", 0.70f, "easetype", iTween.EaseType.easeOutSine));
		// controls
		controlCopy = (GameObject)Instantiate(control, new Vector3(9, 0, -5), Quaternion.identity);
		iTween.MoveTo(controlCopy, iTween.Hash("position", new Vector3(-0.4f, 0, -5), "time", 0.5f, "delay", 0.22f, "easetype", iTween.EaseType.easeOutSine));
		iTween.MoveTo(controlCopy, iTween.Hash("position", new Vector3(0f, 0, -5), "time", 0.2f, "delay", 0.72f, "easetype", iTween.EaseType.easeOutSine));

		backCopy = (GameObject)Instantiate(back, new Vector3(9, -6, -5), Quaternion.identity);
		iTween.MoveTo(backCopy, iTween.Hash("position", new Vector3(0.1f, -6, -5), "time", 0.5f, "delay", 0.24f, "easetype", iTween.EaseType.easeOutSine));
		iTween.MoveTo(backCopy, iTween.Hash("position", new Vector3(0.5f, -6, -5), "time", 0.2f, "delay", 0.74f, "easetype", iTween.EaseType.easeOutSine));
	}

	private void ShowAchiveMenu()
	{
		
		achievementCopy = (GameObject)Instantiate(achievement, new Vector3(9, 2, -5), Quaternion.identity);
		iTween.MoveTo(achievementCopy, iTween.Hash("position", new Vector3(-0.9f, 2, -5), "time", 0.5f, "delay", 0.20f, "easetype", iTween.EaseType.easeOutSine));
		iTween.MoveTo(achievementCopy, iTween.Hash("position", new Vector3(-0.5f, 2, -5), "time", 0.2f, "delay", 0.70f, "easetype", iTween.EaseType.easeOutSine));

		ladderScoreCopy = (GameObject)Instantiate(ladderScore, new Vector3(9, 0, -5), Quaternion.identity);
		iTween.MoveTo(ladderScoreCopy, iTween.Hash("position", new Vector3(-0.4f, 0, -5), "time", 0.5f, "delay", 0.22f, "easetype", iTween.EaseType.easeOutSine));
		iTween.MoveTo(ladderScoreCopy, iTween.Hash("position", new Vector3(0f, 0, -5), "time", 0.2f, "delay", 0.72f, "easetype", iTween.EaseType.easeOutSine));

		ladderComboCopy = (GameObject)Instantiate(ladderCombo, new Vector3(9, -2, -5), Quaternion.identity);
		iTween.MoveTo(ladderComboCopy, iTween.Hash("position", new Vector3(0.1f, -2, -5), "time", 0.5f, "delay", 0.24f, "easetype", iTween.EaseType.easeOutSine));
		iTween.MoveTo(ladderComboCopy, iTween.Hash("position", new Vector3(0.5f, -2, -5), "time", 0.2f, "delay", 0.74f, "easetype", iTween.EaseType.easeOutSine));

		ladderFigSizeCopy = (GameObject)Instantiate(ladderFigSize, new Vector3(9, -4, -5), Quaternion.identity);
		iTween.MoveTo(ladderFigSizeCopy, iTween.Hash("position", new Vector3(0.9f, -4, -5), "time", 0.5f, "delay", 0.26f, "easetype", iTween.EaseType.easeOutSine));
		iTween.MoveTo(ladderFigSizeCopy, iTween.Hash("position", new Vector3(1f, -4, -5), "time", 0.2f, "delay", 0.76f, "easetype", iTween.EaseType.easeOutSine));

		backCopy = (GameObject)Instantiate(back, new Vector3(9, -6, -5), Quaternion.identity);
		iTween.MoveTo(backCopy, iTween.Hash("position", new Vector3(1.1f, -6, -5), "time", 0.5f, "delay", 0.28f, "easetype", iTween.EaseType.easeOutSine));
		iTween.MoveTo(backCopy, iTween.Hash("position", new Vector3(1.5f, -6, -5), "time", 0.2f, "delay", 0.78f, "easetype", iTween.EaseType.easeOutSine));
	}

	public void ShowAchiveErrorMenu(){

		//labelMain = Exp.Label( Localization.GetWord("Selected")+":", 0.2f);
		labelMain = Exp.Label("Internet connection error!", 0.2f);
		labelMain.transform.position = new Vector3(9, 0, -5);
		iTween.MoveTo(labelMain, iTween.Hash("position", new Vector3(-0.4f, 0, -5), "time", 0.5f, "delay", 0.24f, "easetype", iTween.EaseType.easeOutSine));
		iTween.MoveTo(labelMain, iTween.Hash("position", new Vector3(0, 0, -5), "time", 0.2f, "delay", 0.74f, "easetype", iTween.EaseType.easeOutSine));

		backCopy = (GameObject)Instantiate(back, new Vector3(9, -6, -5), Quaternion.identity);
		iTween.MoveTo(backCopy, iTween.Hash("position", new Vector3(-0.4f, -6, -5), "time", 0.5f, "delay", 0.28f, "easetype", iTween.EaseType.easeOutSine));
		iTween.MoveTo(backCopy, iTween.Hash("position", new Vector3(0, -6, -5), "time", 0.2f, "delay", 0.78f, "easetype", iTween.EaseType.easeOutSine));
	}

	public void ShowSelectControlMenu()
	{
		
		swipeCopy = (GameObject)Instantiate(swipe, new Vector3(9, 2, -5), Quaternion.identity);
		iTween.MoveTo(swipeCopy, iTween.Hash("position", new Vector3(-1.4f, 2, -5), "time", 0.5f, "delay", 0.14f, "easetype", iTween.EaseType.easeOutSine));
		iTween.MoveTo(swipeCopy, iTween.Hash("position", new Vector3(-1f, 2, -5), "time", 0.2f, "delay", 0.64f, "easetype", iTween.EaseType.easeOutSine));

		keyboardCopy = (GameObject)Instantiate(keyboard, new Vector3(9, 0, -5), Quaternion.identity);
		iTween.MoveTo(keyboardCopy, iTween.Hash("position", new Vector3(-0.9f, 0, -5), "time", 0.5f, "delay", 0.16f, "easetype", iTween.EaseType.easeOutSine));
		iTween.MoveTo(keyboardCopy, iTween.Hash("position", new Vector3(-0.5f, 0, -5), "time", 0.2f, "delay", 0.66f, "easetype", iTween.EaseType.easeOutSine));

		labelMain = Exp.Label( Localization.GetWord("Selected")+":", 0.2f);
		labelMain.transform.position = new Vector3(9, -2, -5);
		iTween.MoveTo(labelMain, iTween.Hash("position", new Vector3(-0.4f, -2, -5), "time", 0.5f, "delay", 0.24f, "easetype", iTween.EaseType.easeOutSine));
		iTween.MoveTo(labelMain, iTween.Hash("position", new Vector3(0, -2, -5), "time", 0.2f, "delay", 0.74f, "easetype", iTween.EaseType.easeOutSine));

		if (Ini.LoadControl() == "swipe")
		{
			GameObject.Find("swipe_ico").GetComponent<SpriteRenderer>().enabled = true;
		}
		else
		{
			GameObject.Find("keyboard").GetComponent<SpriteRenderer>().enabled = true;
		}

		backCopy = (GameObject)Instantiate(back, new Vector3(9.5f, -6, -5), Quaternion.identity);
		iTween.MoveTo(backCopy, iTween.Hash("position", new Vector3(1.4f, -6, -5), "time", 0.5f, "delay", 0.18f, "easetype", iTween.EaseType.easeOutSine));
		iTween.MoveTo(backCopy, iTween.Hash("position", new Vector3(1f, -6, -5), "time", 0.2f, "delay", 0.68f, "easetype", iTween.EaseType.easeOutSine));		
	}

	public void ShowSelectLanguageMenu()
	{
		ruCopy = (GameObject)Instantiate(ru, new Vector3(9, 4, -5), Quaternion.identity);
		iTween.MoveTo(ruCopy, iTween.Hash("position", new Vector3(-1.9f, 4, -5), "time", 0.5f, "delay", 0.14f, "easetype", iTween.EaseType.easeOutSine));
		iTween.MoveTo(ruCopy, iTween.Hash("position", new Vector3(-1.5f, 4, -5), "time", 0.2f, "delay", 0.64f, "easetype", iTween.EaseType.easeOutSine));
		
		enCopy = (GameObject)Instantiate(en, new Vector3(9, 2, -5), Quaternion.identity);
		iTween.MoveTo(enCopy, iTween.Hash("position", new Vector3(-1.4f, 2, -5), "time", 0.5f, "delay", 0.16f, "easetype", iTween.EaseType.easeOutSine));
		iTween.MoveTo(enCopy, iTween.Hash("position", new Vector3(-1f, 2, -5), "time", 0.2f, "delay", 0.66f, "easetype", iTween.EaseType.easeOutSine));

		deCopy = (GameObject)Instantiate(de, new Vector3(9, 0, -5), Quaternion.identity);
		iTween.MoveTo(deCopy, iTween.Hash("position", new Vector3(-0.9f, 0, -5), "time", 0.5f, "delay", 0.18f, "easetype", iTween.EaseType.easeOutSine));
		iTween.MoveTo(deCopy, iTween.Hash("position", new Vector3(-0.5f, 0, -5), "time", 0.2f, "delay", 0.68f, "easetype", iTween.EaseType.easeOutSine));

		frCopy = (GameObject)Instantiate(fr, new Vector3(9, -2, -5), Quaternion.identity);
		iTween.MoveTo(frCopy, iTween.Hash("position", new Vector3(-0.4f, -2, -5), "time", 0.5f, "delay", 0.20f, "easetype", iTween.EaseType.easeOutSine));
		iTween.MoveTo(frCopy, iTween.Hash("position", new Vector3(0f, -2, -5), "time", 0.2f, "delay", 0.70f, "easetype", iTween.EaseType.easeOutSine));

		backCopy = (GameObject)Instantiate(back, new Vector3(9.5f, -6, -5), Quaternion.identity);
		iTween.MoveTo(backCopy, iTween.Hash("position", new Vector3(1.4f, -6, -5), "time", 0.5f, "delay", 0.22f, "easetype", iTween.EaseType.easeOutSine));
		iTween.MoveTo(backCopy, iTween.Hash("position", new Vector3(1f, -6, -5), "time", 0.2f, "delay", 0.72f, "easetype", iTween.EaseType.easeOutSine));		
	}
}
