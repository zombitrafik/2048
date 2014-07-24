using UnityEngine;
using System.Collections;
using System;

public class Boom : MonoBehaviour {

    private const string ACHIEVE_NOWICE_BUILDER = "achievementNoviceBuilder";
    private const string ACHIEVE_ADVANCED_BUILDER = "achievementAdvancedBuilder";
    private const string ACHIEVE_MASTER_BUILDER = "achievementMasterBuilder";


	private static Boom instance;
	public static Boom Instance
	{
		get
		{
			return instance;
		}
	}

	Hashtable hash = new Hashtable();
	private int[,] figures = new int[10, 10];
	private int comboValue = 0;
	public int minFigureSize = 0;


	public int[,] arr;
	public int activeItems;

	void Start()
	{
		minFigureSize = Ini.LoadMinBoom();
		instance = this;
	}

	public void Init(int[,] mas, int active_items)
	{
		arr = mas;
		activeItems = active_items;
	}

	public int[,] Exploid()
	{
		Copy();
		FindByColor();
		return arr;
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
                    isBoom = DestroyFigs(col)?true:isBoom;
					RepairIndexes(col);
					hash.Clear();
				}
			}
		}

		if (!isBoom) comboValue = 1;
		Exp.UpdateCombo(comboValue);
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
		}catch (Exception){}

		try
		{
			if (figures[i + 1, j] == col)
			{
				FindNearests(i + 1, j, col);
			}
		}catch (Exception){}

		try
		{
			if (figures[i, j + 1] == col)
			{
				FindNearests(i, j + 1, col);
			}
		}catch (Exception){}

		try
		{
			if (figures[i, j - 1] == col)
			{
				FindNearests(i, j - 1, col);
			}
		}catch (Exception){}

	}

	private void Exploision(GameObject obj)
	{
		GameObject.Find("Drip").GetComponent<AudioSource>().volume = Sound.volumeValue;
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

        CheckAchieve(count);

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
						Generating.Instance.activeItems = activeItems;
					}
				}
			}
			int value = Factorial(count);
			value *= comboValue;
			Exp.newExp(value, GetPosFromList(), GetColorById(col));
			Exp.UpdateScore(value);
			comboValue++;
		}
        return isBoom;
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
			case 1: res = Color.red;
				break;
			case 2: res = Color.green;
				break;
			case 3: res = Color.blue;
				break;
			case 4: res = Color.yellow;
				break;
			case 5: res = new Color(159, 0, 197);
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

	private int Factorial(int x)
	{
		return (x == 1) ? 1 : x + Factorial(x - 1);
	}

    private void CheckAchieve(int count)
    {
        if (count >= 5)
        {
            GooglePlayServices.Instance.UpdateAchieveProgress(ACHIEVE_NOWICE_BUILDER, 100);
        }
        if (count >= 7)
        {
            GooglePlayServices.Instance.UpdateAchieveProgress(ACHIEVE_ADVANCED_BUILDER, 100);
        }
        if (count >= 10)
        {
            GooglePlayServices.Instance.UpdateAchieveProgress(ACHIEVE_MASTER_BUILDER, 100);
        }
    }
}
