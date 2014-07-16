using UnityEngine;
using System.Collections;

public class Generator : MonoBehaviour {
	
	public int startCount = 3;
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
	int[,] arr = new int[10,10];
	// Use this for initialization
	void Start () {
		GeneratePositions();
	}
	
	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			MoveItemsLeft();
			GeneratePositions();
		}
		if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			MoveItemsRight();
			GeneratePositions();
		}
		if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			MoveItemsUp();
			GeneratePositions();
		}
		if (Input.GetKeyDown(KeyCode.DownArrow))
		{
			MoveItemsDown();
			GeneratePositions();
		}
	}

	public void GeneratePositions()
	{
		int color = 0;
		int posX = 0, posY = 0;
		int i = 0;
		while (i < startCount)
		{
			color = Random.Range(1, 6);
			posX = Random.Range(0, 10);
			posY = Random.Range(0, 10);


			if (arr[9 - posY, posX] != 0) continue;
			SetItem(9 - posY, posX, color);
			i++;
		}
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

	void MoveItemsLeft()
	{
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
					Debug.Log(j + "_" + i);
					GameObject item = GameObject.Find(j + "_" + i);
					arr[i, itemsCount] = arr[i, j];
					arr[i, j] = 0;
					item.name = itemsCount + "_" + i;

					iTween.MoveTo(item, iTween.Hash("position", new Vector3(itemsCount + marginX, i + marginY, 0), "time", 0.3f, "easetype", iTween.EaseType.easeOutSine));

					itemsCount++;
				}
			}
		}
	}

	void MoveItemsRight()
	{
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

					GameObject item = GameObject.Find(j + "_" + i);
					arr[i, 9 - itemsCount] = arr[i, j];
					arr[i, j] = 0;
					item.name = (9 - itemsCount) + "_" + i;

					iTween.MoveTo(item, iTween.Hash("position", new Vector3((9 - itemsCount) + marginX, i + marginY, 0), "time", 0.3f, "easetype", iTween.EaseType.easeOutSine));

					itemsCount++;
				}
			}
		}
	}


	void MoveItemsDown()
	{
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

					GameObject item = GameObject.Find(i + "_" + j);
					arr[itemsCount, i] = arr[j, i];
					arr[j, i] = 0;
					item.name = i + "_" + itemsCount;

					iTween.MoveTo(item, iTween.Hash("position", new Vector3(i + marginX, itemsCount + marginY, 0), "time", 0.3f, "easetype", iTween.EaseType.easeOutSine));

					itemsCount++;
				}
			}
		}
	}

	void MoveItemsUp()
	{
		for (int i = 0; i < 10; i++)
		{
			int itemsCount = 0;
			for (int j = 9; j >= 0; j--)
			{
				if (arr[j, i] != 0)
				{
					if (j == 9 - itemsCount)//8-itemsCount)
					{
						Debug.Log("if proc " + j + " " + (8 + itemsCount));
						itemsCount++;
						continue;
					}

					GameObject item = GameObject.Find(i + "_" + j);
					arr[9 - itemsCount, i] = arr[j, i];
					arr[j, i] = 0;
					Debug.Log(i + "_" + (9 - itemsCount) + "     ");
					item.name = i + "_" + (9 - itemsCount);

					iTween.MoveTo(item, iTween.Hash("position", new Vector3(i + marginX, (9 - itemsCount) + marginY, 0), "time", 0.3f, "easetype", iTween.EaseType.easeOutSine));

					itemsCount++;
				}
			}
		}
	}
}
