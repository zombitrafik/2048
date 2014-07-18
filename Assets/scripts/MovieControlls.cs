using UnityEngine;
using System.Collections;

public class MovieControlls : MonoBehaviour {

	public static float marginX = -4.5f;
	public static float marginY = -4.5f;

	public static bool MoveItemsLeft(int[,] arr)
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

	public static bool MoveItemsRight(int[,] arr)
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


	public static bool MoveItemsDown(int[,] arr)
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
					iTween.MoveTo(item, iTween.Hash("position", new Vector3(i + marginX, itemsCount + marginY, 0), "time", 0.3f, "easetype", iTween.EaseType.easeOutSine));

					itemsCount++;
				}
			}
		}
		return res;
	}

	public static bool MoveItemsUp(int[,] arr)
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
}
