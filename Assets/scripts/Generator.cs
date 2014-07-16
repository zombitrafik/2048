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
			//MoveItemsLeft();
			GeneratePositions();
		}
		if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			MoveItemsRight();
			//GeneratePositions();
		}
		if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			//MoveItemsUp();
			GeneratePositions();
		}
		if (Input.GetKeyDown(KeyCode.DownArrow))
		{
			//MoveItemsDown();
			GeneratePositions();
		}
	}

	void GeneratePositions()
	{
		int color = 0;
		int posX = 0, posY = 0;
		int i=0;
		while(i<startCount){
			color = Random.Range(1, 6);
			posX = Random.Range(0, 10);
			posY = Random.Range(0, 10);
			if (arr[posX, posY] != 0) continue;
			SetItem(posX,posY,color);
			i++;
		}
	}
	void SetItem(int posX, int posY, int color)
	{
		arr[posX, posY] = color;
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
		item.transform.position = new Vector2(marginX + posX, marginY + posY);
		item.name = posX + "_" + posY;
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
					if (j == 9 - itemsCount)
					{
						//Debug.Log("if proc " + j);
						//itemsCount++;
						//continue;
					}
					arr[i, 9 - itemsCount] = arr[i, j];
					GameObject item = GameObject.Find(i + "_" + j);

					item.name = i + "_" + (9 - itemsCount);

					item.transform.position = new Vector2((9 - itemsCount) + marginX, j + marginY);
					arr[i, j] = 0;
					itemsCount++;
				}
			}
			Debug.Log(i + " " + itemsCount) ;
		}
	}


}
