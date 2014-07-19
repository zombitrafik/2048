using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public GameObject play;
	private GameObject playCopy;

	public GameObject mode;
	private GameObject modeCopy;

	public GameObject settings;
	private GameObject settingsCopy;

	public GameObject tutorial;
	private GameObject tutorialCopy;

	public GameObject about;
	private GameObject aboutCopy;

	public GameObject exit;
	private GameObject exitCopy;

	private GameObject activeClicked;

	void Start()
	{
		ShowBackMenu();
	}

	public void ShowBackMenu()
	{

		playCopy = (GameObject)Instantiate(play, new Vector3(8, 6, -5), Quaternion.identity);
		modeCopy = (GameObject)Instantiate(mode, new Vector3(8, 4, -5), Quaternion.identity);
		settingsCopy = (GameObject)Instantiate(settings, new Vector3(8, 2, -5), Quaternion.identity);
		tutorialCopy = (GameObject)Instantiate(tutorial, new Vector3(8, 0, -5), Quaternion.identity);
		aboutCopy = (GameObject)Instantiate(about, new Vector3(8, -2, -5), Quaternion.identity);
		exitCopy = (GameObject)Instantiate(exit, new Vector3(8, -4, -5), Quaternion.identity);


		iTween.MoveTo(playCopy, iTween.Hash("position", new Vector3(-2, 6, -5), "time", 0.5f, "delay", 0.5f, "easetype", iTween.EaseType.easeOutSine));
		iTween.MoveTo(modeCopy, iTween.Hash("position", new Vector3(-1.5f, 4, -5), "time", 0.5f, "delay", 0.52f, "easetype", iTween.EaseType.easeOutSine));
		iTween.MoveTo(settingsCopy, iTween.Hash("position", new Vector3(-1f, 2, -5), "time", 0.5f, "delay", 0.54f, "easetype", iTween.EaseType.easeOutSine));
		iTween.MoveTo(tutorialCopy, iTween.Hash("position", new Vector3(-0.5f, 0, -5), "time", 0.5f, "delay", 0.56f, "easetype", iTween.EaseType.easeOutSine));
		iTween.MoveTo(aboutCopy, iTween.Hash("position", new Vector3(0, -2, -5), "time", 0.5f, "delay", 0.58f, "easetype", iTween.EaseType.easeOutSine));
		iTween.MoveTo(exitCopy, iTween.Hash("position", new Vector3(0.5f, -4, -5), "time", 0.5f, "delay", 0.6f, "easetype", iTween.EaseType.easeOutSine));


		iTween.MoveTo(playCopy, iTween.Hash("position", new Vector3(-1.6f, 6, -5), "time", 0.2f, "delay", 1f, "easetype", iTween.EaseType.easeOutSine));
		iTween.MoveTo(modeCopy, iTween.Hash("position", new Vector3(-1.1f, 4, -5), "time", 0.2f, "delay", 1.02f, "easetype", iTween.EaseType.easeOutSine));
		iTween.MoveTo(settingsCopy, iTween.Hash("position", new Vector3(-0.6f, 2, -5), "time", 0.2f, "delay", 1.04f, "easetype", iTween.EaseType.easeOutSine));
		iTween.MoveTo(tutorialCopy, iTween.Hash("position", new Vector3(-0.1f, 0, -5), "time", 0.2f, "delay", 1.06f, "easetype", iTween.EaseType.easeOutSine));
		iTween.MoveTo(aboutCopy, iTween.Hash("position", new Vector3(0.4f, -2, -5), "time", 0.2f, "delay", 1.08f, "easetype", iTween.EaseType.easeOutSine));
		iTween.MoveTo(exitCopy, iTween.Hash("position", new Vector3(0.9f, -4, -5), "time", 0.2f, "delay", 1.1f, "easetype", iTween.EaseType.easeOutSine));

	}

	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

			if (hit.collider != null)
			{
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
			}
		}

		if (Input.GetMouseButtonUp(0))
		{
			if (activeClicked == null) return;

			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

			if (hit.collider != null)
			{
				if (activeClicked.GetComponent<Button>().GetName() == "play" && hit.transform.gameObject.name == "play_down(Clone)")
				{
					Application.LoadLevel("main");
				}
				if (activeClicked.GetComponent<Button>().GetName() == "mode" && hit.transform.gameObject.name == "mode_down(Clone)")
				{

				}
				if (activeClicked.GetComponent<Button>().GetName() == "settings" && hit.transform.gameObject.name == "settings_down(Clone)")
				{

				}
				if (activeClicked.GetComponent<Button>().GetName() == "tutorial" && hit.transform.gameObject.name == "tutorial_down(Clone)")
				{

				}
				if (activeClicked.GetComponent<Button>().GetName() == "about" && hit.transform.gameObject.name == "about_down(Clone)")
				{

				}
				if (activeClicked.GetComponent<Button>().GetName() == "exit" && hit.transform.gameObject.name == "exit_down(Clone)")
				{
					Application.Quit();
				}
			}
			activeClicked.GetComponent<Button>().SetButtonUp();
			activeClicked = null;
		}
	}

}
