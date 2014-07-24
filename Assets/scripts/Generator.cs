using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;

public class Generator : MonoBehaviour {

	public Texture2D blur;
	public GUIStyle style;

	private int comboValue = 0;

	public int startCount = 3;
    public int minFigureSize = 3;
	public Sprite redSprite;
	public Sprite greenSprite;
	public Sprite blueSprite;
	public Sprite yellowSprite;
	public Sprite purpleSprite;

	const int RED = 1;
	const int GREEN = 2;
	const int BLUE = 3;
	const int YELLOW = 4;
	const int PURPLE = 5;
	const float marginX = -4.5f;
	const float marginY = -4.5f;

	int[,] arr = new int[10, 10];
	int[,] figures = new int[10, 10];

    private int SwipeID = -1;
    private Vector2 StartPos;
    private float buttonCooler;
    private float minMovement = Screen.width/ 15;
    private float swipeCD = 0.4f;
    private float lastSwipe = 0;

	private int activeItems = 0;

	private bool isGameOverMenu = false;
	private bool isPauseMenu = false;

	Hashtable hash = new Hashtable();

	// Use this for initializationd
	void Start () {
		gameObject.GetComponent<Blur>().enabled = false;
		GeneratePositions();
		LoadScore();
	}
	
	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if(isPauseMenu)	isPauseMenu = false; else isPauseMenu = true;
		}

        if (Time.time-lastSwipe>=swipeCD && !isGameOverMenu)
        {
            
            Swipes();
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                lastSwipe = Time.time;
				
				//GameObject.Find("CLICK").GetComponent<AudioSource>().Play();
				if (MoveItemsLeft()) GeneratePositions(); else NeonLight("left"); 
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                lastSwipe = Time.time;
				//GameObject.Find("CLICK").GetComponent<AudioSource>().Play();
				if (MoveItemsRight()) GeneratePositions(); else NeonLight("right");
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                lastSwipe = Time.time;
				//GameObject.Find("CLICK").GetComponent<AudioSource>().Play();
				if (MoveItemsUp()) GeneratePositions(); else NeonLight("up");
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                lastSwipe = Time.time;
				//GameObject.Find("CLICK").GetComponent<AudioSource>().Play();
				if (MoveItemsDown()) GeneratePositions(); else NeonLight("down");
            }
        }
	}

	public void Swipes()
	{
		foreach (var T in Input.touches)
		{
			var P = T.position;
			var C = T.tapCount;
			if (T.phase == TouchPhase.Began && SwipeID == -1)
			{
				SwipeID = T.fingerId;
				StartPos = P;
			}
			else if (T.fingerId == SwipeID)
			{
				var delta = P - StartPos;
				if (T.phase == TouchPhase.Moved && delta.magnitude > minMovement)
				{
					SwipeID = -1;
					if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
					{
						if (delta.x > 0)
						{
							lastSwipe = Time.time;
							if (MoveItemsRight()) GeneratePositions(); else NeonLight("right"); 
						}
						else
						{
							lastSwipe = Time.time;
							if (MoveItemsLeft()) GeneratePositions(); else NeonLight("left"); 
						}
					}
					else
					{
						if (delta.y > 0)
						{
							lastSwipe = Time.time;
							if (MoveItemsUp()) GeneratePositions(); else NeonLight("up"); 
						}
						else
						{
							lastSwipe = Time.time;
							if (MoveItemsDown()) GeneratePositions(); else NeonLight("down"); 
						}
					}
				}
				else if (T.phase == TouchPhase.Canceled || T.phase == TouchPhase.Ended)
				{
					SwipeID = -1;
					if (buttonCooler > 0 && C >= 2)
					{
						//двойной тап
					}
					else
					{
						buttonCooler = 0.5f;
						C += 1;
					}
				}
			}
		}
	}

	public void GeneratePositions()
	{
		
		int color = 0;
		int posX = 0, posY = 0;
		int i = 0;

		//startCount = 100 - activeItems >= startCount ? startCount : 100 - activeItems;

		if (activeItems + startCount >= 100) {
			GameOver();
			return;
			}

		while (i < startCount)
		{
			//color = 1;
			color = UnityEngine.Random.Range(1, 6);
			posX = UnityEngine.Random.Range(0, 10);
			posY = UnityEngine.Random.Range(0, 10);


			if (arr[9 - posY, posX] != 0) continue;
			SetItem(9 - posY, posX, color);
			i++;
			activeItems++;
		}

		if (activeItems == 100) GameOver();
		iTween.MoveTo(gameObject, iTween.Hash("position", gameObject.transform.position,"time", 0f, "delay", 0.3f, "onComplete", "RunEnd", "onCompleteTarget", gameObject));
		
	}
	public void SetItem(int posY, int posX, int color)
	{
		arr[posY, posX] = color;
		Sprite selectedSprite;
		switch(color){
			case RED: selectedSprite = redSprite;
				break;
			case GREEN: selectedSprite = greenSprite;
				break;
			case BLUE: selectedSprite = blueSprite;
				break;
			case YELLOW: selectedSprite = yellowSprite;
				break;
			case PURPLE: selectedSprite = purpleSprite;
				break;
			default: selectedSprite = redSprite;
				break;
				
		}
		GameObject item = new GameObject();
		item.AddComponent<SpriteRenderer>();
		item.GetComponent<SpriteRenderer>().sprite = selectedSprite;

		PushItems(item, new Vector3(marginX + posX, marginY + posY, -1));

		item.name = posX + "_" + posY;

	}

	public void PushItems(GameObject obj, Vector3 pos)
	{
		Vector2 startPos = pos;
		startPos += new Vector2(0.3f, 0.3f);
		obj.transform.position = startPos;
		iTween.MoveTo(obj, iTween.Hash("position", pos, "time", 0.7f, "easetype", iTween.EaseType.easeOutSine));
	}

	bool MoveItemsLeft()
	{
		
        bool res = false;
		for (int i = 9; i >= 0; i--)
		{
			int itemsCount = 0;
			for (int j = 0; j < 10; j++)
			{
				if (arr[i, j] != 0)
				{
					if (j < itemsCount + 1)
					{
						itemsCount++;
						continue;
					}
                    res = true;
					GameObject item = GameObject.Find(j + "_" + i);
					arr[i, itemsCount] = arr[i, j];
					arr[i, j] = 0;
					item.name = itemsCount + "_" + i;

                    iTween.MoveTo(item, iTween.Hash("position", new Vector3(itemsCount + marginX, i + marginY, 0), "time", 0.3f, "easetype", iTween.EaseType.easeOutSine));

					itemsCount++;
				}
			}
		}
		 return res;
	}

	bool MoveItemsRight()
	{
        bool res = false;
		for (int i = 9; i >= 0; i--)
		{
			int itemsCount = 0;
			for (int j = 9; j >= 0; j--)
			{
				if (arr[i, j] != 0)
				{
					if (j > 8 - itemsCount)
					{
						itemsCount++;
						continue;
					}
                    res = true;
					GameObject item = GameObject.Find(j + "_" + i);
					arr[i, 9 - itemsCount] = arr[i, j];
					arr[i, j] = 0;
					item.name = (9 - itemsCount) + "_" + i;

                    iTween.MoveTo(item, iTween.Hash("position", new Vector3((9 - itemsCount) + marginX, i + marginY, 0), "time", 0.3f, "easetype", iTween.EaseType.easeOutSine));

					itemsCount++;
				}
			}
		}
		  return res;
	}


	bool MoveItemsDown()
	{
        bool res = false;
		for (int i = 9; i >= 0; i--)
		{
			int itemsCount = 0;
			for (int j = 0; j < 10; j++)
			{
				if (arr[j, i] != 0)
				{
					if (j < itemsCount + 1)
					{
						itemsCount++;
						continue;
					}
                    res = true;
					GameObject item = GameObject.Find(i + "_" + j);
					arr[itemsCount, i] = arr[j, i];
					arr[j, i] = 0;
					item.name = i + "_" + itemsCount;
					//item.transform.position = new Vector3(i + marginX, itemsCount + marginY, 0);
                    iTween.MoveTo(item, iTween.Hash("position", new Vector3(i + marginX, itemsCount + marginY, 0), "time", 0.3f, "easetype", iTween.EaseType.easeOutSine));

					itemsCount++;
				}
			}
		}
		 return res;
	}

	bool MoveItemsUp()
	{
        bool res = false;
		for (int i = 0; i < 10; i++)
		{
			int itemsCount = 0;
			for (int j = 9; j >= 0; j--)
			{
				if (arr[j, i] != 0)
				{
					if (j == 9 - itemsCount)//8-itemsCount)
					{
						itemsCount++;
						continue;
					}
                    res = true;
					GameObject item = GameObject.Find(i + "_" + j);
					arr[9 - itemsCount, i] = arr[j, i];
					arr[j, i] = 0;
					item.name = i + "_" + (9 - itemsCount);

                    iTween.MoveTo(item, iTween.Hash("position", new Vector3(i + marginX, (9 - itemsCount) + marginY, 0), "time", 0.3f, "easetype", iTween.EaseType.easeOutSine));

					itemsCount++;
				}
			}
		}
		
        return res;
	}

    public void RunEnd()
    {
        Exploid();
    }

	private void Exploid()
	{
		Copy();
		FindByColor();
	}

	private void Copy()
	{
		for (int i = 0; i < 10; i++)
		{
			for (int j = 0; j < 10; j++)
			{
				figures[i, j] = arr[i, j];
			}
		}
	}

	private void FindByColor()
	{
		bool isBoom = false;
		for (int i = 0; i < 10; i++)
		{
			for (int j = 0; j < 10; j++)
			{
				int col = figures[i, j];
				if (col != 0)
				{
					FindNearests(i, j, col);
					bool ForBoom = DestroyFigs(col);
					if (ForBoom) isBoom = true;
					RepairIndexes(col);
					hash.Clear();
				}
			}
		}

		if (!isBoom) comboValue = 0;
		UpdateCombo(comboValue);
	}

	private void FindNearests(int i, int j, int col)
	{
		figures[i, j] = -1;
		
		try
		{
			if (figures[i - 1, j] == col)
			{
				FindNearests(i - 1, j, col);
			}
		}
		catch (Exception)
		{

		}

		try
		{
			if (figures[i + 1, j] == col)
			{
				FindNearests(i + 1, j, col);
			}
		}
		catch (Exception)
		{

		}

		try
		{
			if (figures[i, j + 1] == col)
			{
				FindNearests(i, j + 1, col);
			}
		}
		catch (Exception)
		{

		}

		try
		{
			if (figures[i, j - 1] == col)
			{

				FindNearests(i, j - 1, col);
			}
		}
		catch (Exception)
		{

		}

	}

    private void Exploision(GameObject obj)
    {
		GameObject.Find("Drip").GetComponent<AudioSource>().Play();
        Vector3 newScale = obj.transform.localScale + new Vector3(0.3f, 0.3f, 0.3f);
        iTween.ScaleTo(obj, iTween.Hash("scale", newScale, "time", 0.2f));
        iTween.ScaleTo(obj, iTween.Hash("scale", Vector3.zero, "time", 0.4f, "delay", 0.2f));
    }

	private bool DestroyFigs(int col)
	{
		int count = 0;
		for (int i = 0; i < 10; i++)
		{
			for (int j = 0; j < 10; j++)
			{
				if (figures[i, j] == -1)
				{
					count++;
				}
			}
		}
		int hashCounter = 1;
		bool isBoom = false;
		if (count >= minFigureSize)
		{
			for (int i = 0; i < 10; i++)
			{
				for (int j = 0; j < 10; j++)
				{
					if (figures[i, j] == -1)
					{
						isBoom = true;
						figures[i, j] = 0;
						arr[i, j] = 0;
						hash.Add(hashCounter++, new Vector2(i, j));
						Exploision(GameObject.Find(j + "_" + i));
						Destroy(GameObject.Find(j + "_" + i), 0.4f);
						activeItems--;
					}
				}
			}
			int value = Factorial(count);
			value *= comboValue + 1;
			Exp.newExp(value, GetPosFromList(), GetColorById(col));
			UpdateScore(value);
			comboValue++;
		}
		if (isBoom) return true; else return false;
	}

	private void RepairIndexes(int col)
	{
		for (int i = 0; i < 10; i++)
		{
			for (int j = 0; j < 10; j++)
			{
				if (figures[i, j] == -1)
				{
					figures[i, j] = col;
				}
			}
		}
	}


	private Color GetColorById(int color)
	{
		Color res = Color.black;
		switch (color)
		{
			case RED: res = Color.red;
				break;
			case GREEN: res = Color.green;
				break;
			case BLUE: res = Color.blue;
				break;
			case YELLOW: res = Color.yellow;
				break;
			case PURPLE: res = new Color(159, 0, 197);
				break;
			default: res = Color.black;
				break;
		}
		return res;
	}

	private Vector3 GetPosFromList()
	{
		Vector2 t = (Vector2)hash[(int)(hash.Count / 2 + 0.5f)];
		return GameObject.Find(t.y + "_" + t.x).transform.position;
	}

	private void UpdateScore(int newVal)
	{
		
		int oldValue = Convert.ToInt32(GameObject.Find("Score").GetComponent<TextMesh>().text.Replace("Score: ", ""));
		GameObject.Find("Score").GetComponent<TextMesh>().text = "Score: " + (oldValue + newVal);

		if ((newVal+oldValue) > Convert.ToInt32(GameObject.Find("BestScore").GetComponent<TextMesh>().text.Replace("Your best score: ", "")))
		{
			GameObject.Find("BestScore").GetComponent<TextMesh>().text = "Your best score: " + (oldValue + newVal);
		}
	}

	int Factorial(int x)
	{
		return (x == 0) ? 1 : x * Factorial(x - 1);
	}

	private void GameOver()
	{
		FadeEnd();
		iTween.ValueTo(gameObject, iTween.Hash("from", 0, "to", 1.5f, "time", .5f, "delay", 0.5f, "onupdate", "changeMotionBlur"));
		iTween.ValueTo(gameObject, iTween.Hash("from", 0, "to", 3.5f, "time", .5f, "delay", 0.5f, "onupdate", "changeMotionBlurBlurSize"));
		iTween.ValueTo(gameObject, iTween.Hash("from", 3, "to", 3f, "time", .5f, "delay", 0.5f, "onupdate", "changeMotionBlurBlurIterations"));
	}

	void changeMotionBlurDownSample(float newValue)
	{
		gameObject.GetComponent<Blur>().downsample = (int)newValue;
	}
	void changeMotionBlurBlurSize(float newValue)
	{
		gameObject.GetComponent<Blur>().blurSize = newValue;
	}
	void changeMotionBlurBlurIterations(float newValue)
	{
		gameObject.GetComponent<Blur>().blurIterations = (int)newValue;
	}

	void OnGUI()
	{
		if (isGameOverMenu)
		{
			int score = Convert.ToInt32(GameObject.Find("Score").GetComponent<TextMesh>().text.Replace("Score: ", ""));
			int bestScore = Convert.ToInt32(GameObject.Find("BestScore").GetComponent<TextMesh>().text.Replace("Your best score: ", ""));
			GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 240, 100, 40), "Score: " + score.ToString(), style);
			GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 180, 200, 40), "Best score: " + bestScore.ToString(), style);

			if (GUI.Button(new Rect(Screen.width / 2 - 150, Screen.height / 2 , 100, 50), "Retry"))
			{
				Application.LoadLevel("main");
			}
			if (GUI.Button(new Rect(Screen.width / 2 + 50, Screen.height / 2, 100, 50), "Exit"))
			{
				Application.LoadLevel("main_menu");
			}
			if (score == bestScore) SaveRecord(bestScore);
		}
		if (isPauseMenu)
		{
			if (GUI.Button(new Rect(Screen.width / 2 - 150, Screen.height / 2, 100, 50), "Back"))
			{
				isPauseMenu = false;
			}
			if (GUI.Button(new Rect(Screen.width / 2 + 50, Screen.height / 2, 100, 50), "Exit"))
			{
				Application.LoadLevel("main_menu");
			}
		}
		
	}

	void FadeEnd()
	{
		gameObject.GetComponent<Blur>().enabled = true;
		isGameOverMenu = true;
	}
	
	private void LoadScore()
	{
		StreamReader fileReader;
		string fileName = Application.persistentDataPath + "/bestScore.txt";
		if (File.Exists(fileName))
		{
			fileReader = File.OpenText(fileName);
			GameObject.Find("BestScore").GetComponent<TextMesh>().text = "Your best score: " +fileReader.ReadLine();
			fileReader.Close();
		}
	}
	
	private void SaveRecord(int value)
	{
		StreamWriter fileWriter;
		string fileName = Application.persistentDataPath + "/bestScore.txt";
		File.WriteAllText(fileName, String.Empty);
		fileWriter = File.CreateText(fileName);
		fileWriter.WriteLine(value);
		fileWriter.Close();
	}

	private void NeonLight(string side)
	{
		
		switch (side)
		{
			case "left":
				iTween.ValueTo(gameObject, iTween.Hash("from", 0, "to", 1, "time", .2f, "onupdate", "NeonChangeLeft"));
				iTween.ValueTo(gameObject, iTween.Hash("from", 1, "to", 0, "time", .2f, "delay", .2f, "onupdate", "NeonChangeLeft"));
				break;
			case "right":
				iTween.ValueTo(gameObject, iTween.Hash("from", 0, "to", 1, "time", .2f, "onupdate", "NeonChangeRight"));
				iTween.ValueTo(gameObject, iTween.Hash("from", 1, "to", 0, "time", .2f, "delay", .2f, "onupdate", "NeonChangeRight"));
				break;
			case "up":
				iTween.ValueTo(gameObject, iTween.Hash("from", 0, "to", 1, "time", .2f, "onupdate", "NeonChangeUp"));
				iTween.ValueTo(gameObject, iTween.Hash("from", 1, "to", 0, "time", .2f, "delay", .2f, "onupdate", "NeonChangeUp"));
				break;
			case "down":
				iTween.ValueTo(gameObject, iTween.Hash("from", 0, "to", 1, "time", .2f, "onupdate", "NeonChangeDown"));
				iTween.ValueTo(gameObject, iTween.Hash("from", 1, "to", 0, "time", .2f, "delay", .2f, "onupdate", "NeonChangeDown"));
				break;
			default: break;
		}
	}

	private void NeonChangeLeft(float newValue)
	{
		GameObject.Find("mirrLeft").GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, newValue);
	}

	private void NeonChangeRight(float newValue)
	{
		GameObject.Find("mirrRight").GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, newValue);
	}

	private void NeonChangeUp(float newValue)
	{
		GameObject.Find("mirrUp").GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, newValue);
	}

	private void NeonChangeDown(float newValue)
	{
		GameObject.Find("mirrDown").GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, newValue);
	}

	private void UpdateCombo(int value)
	{
		GameObject.Find("Combo").GetComponent<TextMesh>().text = "x " + value;
		if (value <= 1) return;
		Exp.newCombo(value, new Vector3(0, 5.5f, 1f), Color.white);
	}
	
	 
}

