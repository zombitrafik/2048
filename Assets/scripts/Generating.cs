
using UnityEngine;
using System.Collections;

public class Generating : MonoBehaviour {

	private static Generating instance;
	public static Generating Instance
	{
		get
		{
			return instance;
		}
	}

	public int activeItems = 0;
	private int startCount = 3;
	public int[,] arr;

	public Sprite redSprite;
	public Sprite greenSprite;
	public Sprite blueSprite;
	public Sprite yellowSprite;
	public Sprite purpleSprite;

	void Start()
	{
		instance = this;
	}

	public void Init(int[,] mas)
	{

		arr = mas;
	}

	public int[,] GeneratePositions()
	{
		int color = 0;
		int posX = 0, posY = 0;
		int i = 0;
		
		if (activeItems + startCount >= 100)
		{
			GameOver.Instance.Over();
			MainClass.Instance.isGameOverMenu = true;
			return new int[10,10];
		}

		while (i < startCount)
		{
			color = UnityEngine.Random.Range(1, 6);
			posX = UnityEngine.Random.Range(0, 10);
			posY = UnityEngine.Random.Range(0, 10);

			if (arr[9 - posY, posX] != 0) continue;
			SetItem(9 - posY, posX, color);
			i++;
			activeItems++;
		}
		iTween.MoveTo(gameObject, iTween.Hash("position", gameObject.transform.position, "time", 0f, "delay", 0.3f, "onComplete", "RunEnd", "onCompleteTarget", gameObject));
		return arr;
	}
	public void SetItem(int posY, int posX, int color)
	{
		arr[posY, posX] = color;
		Sprite selectedSprite;
		switch (color)
		{
			case 1: selectedSprite = redSprite;
				break;
			case 2: selectedSprite = greenSprite;
				break;
			case 3: selectedSprite = blueSprite;
				break;
			case 4: selectedSprite = yellowSprite;
				break;
			case 5: selectedSprite = purpleSprite;
				break;
			default: selectedSprite = redSprite;
				break;

		}
		GameObject item = new GameObject();
		item.AddComponent<SpriteRenderer>();
		item.GetComponent<SpriteRenderer>().sprite = selectedSprite;

		PushItems(item, new Vector3(MovieControlls.marginX + posX, MovieControlls.marginY + posY, -1));

		item.name = posX + "_" + posY;

	}

	public void PushItems(GameObject obj, Vector3 pos)
	{
		Vector2 startPos = pos;
		startPos += new Vector2(0.3f, 0.3f);
		obj.transform.position = startPos;
		iTween.MoveTo(obj, iTween.Hash("position", pos, "time", 0.7f, "easetype", iTween.EaseType.easeOutSine));
	}

	void RunEnd()
	{
		Boom.Instance.Init(arr, activeItems);
		arr = Boom.Instance.Exploid();
	}
}
