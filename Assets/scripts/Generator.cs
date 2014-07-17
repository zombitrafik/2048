using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Generator : MonoBehaviour {
	
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

    private int SwipeID;
    private Vector2 StartPos;
    private float buttonCooler;
    private float minMovement = 30;
    private float swipeCD = 0.7f;
    private float lastSwipe = 0;

	Hashtable hash = new Hashtable();

	// Use this for initializationd
	void Start () {
		GeneratePositions();
	}
	
	// Update is called once per frame
	void Update()
	{
        if (Time.time-lastSwipe>=swipeCD)
        {
            
            Swipes();
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                lastSwipe = Time.time;
                if (MoveItemsLeft()) GeneratePositions();
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                lastSwipe = Time.time;
                if (MoveItemsRight()) GeneratePositions();
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                lastSwipe = Time.time;
                if (MoveItemsUp()) GeneratePositions();
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                lastSwipe = Time.time;
                if (MoveItemsDown()) GeneratePositions();
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
                            if (MoveItemsRight()) GeneratePositions();
                        }
                        else
                        {
                            if (MoveItemsLeft()) GeneratePositions();
                        }
                    }
                }
                else
                {
                    if (delta.y > 0)
                    {
                        if (MoveItemsUp()) GeneratePositions();
                    }
                    else
                    {
                        if (MoveItemsDown()) GeneratePositions();
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

	public void GeneratePositions()
	{
		int color = 0;
		int posX = 0, posY = 0;
		int i = 0;
		while (i < startCount)
		{
			//color = 1;
			color = UnityEngine.Random.Range(1, 6);
			posX = UnityEngine.Random.Range(0, 10);
			posY = UnityEngine.Random.Range(0, 10);


			if (arr[9 - posY, posX] != 0) continue;
			SetItem(9 - posY, posX, color);
			i++;
		}
		//Exploid();
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

		PushItems(item, new Vector3(marginX + posX, marginY + posY, 0));

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

                    iTween.MoveTo(item, iTween.Hash("position", new Vector3(itemsCount + marginX, i + marginY, 0), "time", 0.3f, "easetype", iTween.EaseType.easeOutSine, "onComplete", "RunEnd", "onCompleteTarget", gameObject));

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

                    iTween.MoveTo(item, iTween.Hash("position", new Vector3((9 - itemsCount) + marginX, i + marginY, 0), "time", 0.3f, "easetype", iTween.EaseType.easeOutSine, "onComplete", "RunEnd", "onCompleteTarget", gameObject));

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
                    iTween.MoveTo(item, iTween.Hash("position", new Vector3(i + marginX, itemsCount + marginY, 0), "time", 0.3f, "easetype", iTween.EaseType.easeOutSine, "onComplete", "RunEnd", "onCompleteTarget", gameObject));

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

                    iTween.MoveTo(item, iTween.Hash("position", new Vector3(i + marginX, (9 - itemsCount) + marginY, 0), "time", 0.3f, "easetype", iTween.EaseType.easeOutSine, "onComplete", "RunEnd", "onCompleteTarget", gameObject));

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
		FindByColor(RED);
		FindByColor(GREEN);
		FindByColor(BLUE);
		FindByColor(YELLOW);
		FindByColor(PURPLE);	
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

	private void FindByColor(int col)
	{
		int count = 0;
		for (int i = 0; i < 10; i++)
		{
			for (int j = 0; j < 10; j++)
			{
				if (figures[i, j] == col)
				{
					count = FindNearests(i, j, col);
					if (count >= minFigureSize)
					{
						DestroyFigs();
						int value = Factorial(count);
						Exp.newExp(value, GetPosFromList(), GetColorById(col));
						UpdateScore(value);
						
					}
					RepairIndexes(col);
					hash.Clear();
					
					
				}
			}
		}
	}

	private int FindNearests(int i, int j, int col)
	{
		int count = 0;
		figures[i, j] = -1;
		try
		{
			try
			{
				if (figures[i - 1, j] == col)
				{
					count += FindNearests(i - 1, j, col);
					//Debug.Log("Count =" + count);
				}
			}
			catch (Exception)
			{

			}

			try
			{
				if (figures[i + 1, j] == col)
				{
					count += FindNearests(i + 1, j, col);
					//Debug.Log("Count =" + count);
				}
			}
			catch (Exception)
			{

			}

			try
			{
				if (figures[i, j + 1] == col)
				{
					count += FindNearests(i, j + 1, col);
					//Debug.Log("Count =" + count);
				}
			}
			catch (Exception)
			{

			}

			try
			{
				if (figures[i, j - 1] == col)
				{

					count += FindNearests(i, j - 1, col);
					//Debug.Log("Count =" + count);
				}
			}
			catch (Exception)
			{

			}
			
		}
		catch (Exception)
		{

		}
		
		count++;
		hash.Add(count, new Vector2(i, j));
		return count;
	}

    private void Exploision(GameObject obj)
    {
        Vector3 newScale = obj.transform.localScale + new Vector3(0.3f, 0.3f, 0.3f);
        iTween.ScaleTo(obj, iTween.Hash("scale", newScale, "time", 0.2f));
        iTween.ScaleTo(obj, iTween.Hash("scale", Vector3.zero, "time", 0.4f, "delay", 0.2f));
    }

	private void DestroyFigs()
	{
		for (int i = 0; i < 10; i++)
		{
			for (int j = 0; j < 10; j++)
			{
				if (figures[i, j] == -1)
				{
					figures[i, j] = 0;
					arr[i, j] = 0;

                    Exploision(GameObject.Find(j + "_" + i));
                    Destroy(GameObject.Find(j + "_" + i), 0.4f);
				}
			}
		}
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
	}

	int Factorial(int x)
	{
		return (x == 0) ? 1 : x * Factorial(x - 1);
	}
}
