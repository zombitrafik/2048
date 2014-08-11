using UnityEngine;
using System.Collections;
using System;

public class Debuff : MonoBehaviour {

    private float xPos = -4.5f;
    private float yPos = 6;
    private int minGen = 3;

    public Sprite redSprite;
    public Sprite greenSprite;
    public Sprite blueSprite;
    public Sprite yellowSprite;
    public Sprite purpleSprite;
    public Sprite greySprite;

    private Sprite selectedSprite;

    private static Debuff instance;
    public int debuffValue;
    private int genVal;
    public static Debuff Instance
    {
        get
        {
            return instance;
        }
    }

	void Start () {
        genVal = Ini.LoadGeneratingCount();
        instance = this;
        debuffValue = Ini.LoadDebuff();
        DrawStartBackGround();
        DrawStartDebuff();
	}

    public void Increment()
    {
        DrawNewDebuff();
        debuffValue++;
        if (debuffValue > Ini.LoadGeneratingCount())
        {
            DrawNewBackGround();
            RemoveAllDebufs();
            setGenerating(1);
            debuffValue = 0;
        }
    }

    public void Decrement()
    {
        
        debuffValue--;
        if (debuffValue < 0)
        {
            if(genVal>minGen){
                setGenerating(-1);
                debuffValue = Ini.LoadGeneratingCount();
                RemoveBackGround();
                DrawAllDebufs();
            }
            else
            {
                debuffValue = 0;
            }
        }
        else
        {
            RemoveDebuff();
        }
    }
    
    private void setGenerating(int value)
    {
        genVal += value;
        selectedSprite = SelectSprite(genVal);
        Ini.SaveGeneratingCount(genVal);
        Exp.genCoeff = genVal;
        Generating.Instance.UpdateGeneratingCount(genVal);
    }

    private void DrawStartBackGround()
    {
        for (int i = 0; i < genVal; i++)
        {
            DrawTitle(greySprite, i,1, "debBack_");
        }
    }

    private void DrawStartDebuff()
    {
        for (int i = 0; i < debuffValue; i++)
        {
            DrawTitle(selectedSprite, i, -1, "debAct_");
        }
        selectedSprite = SelectSprite(genVal);
    }

    void RemoveAllDebufs()
    {
        for (int i = 0; i < debuffValue; i++)
        {
            Exploision(GameObject.Find("debAct_" + i));
        }
    }

    void DrawAllDebufs()
    {
        for (int i = 0; i < debuffValue; i++)
        {
            DrawTitle(selectedSprite, i, -1, "debAct_");
        }
    }

    private void Exploision(GameObject obj)
    {
        Vector3 newScale = obj.transform.localScale + new Vector3(0.3f, 0.3f, 0.3f);
        iTween.ScaleTo(obj, iTween.Hash("scale", newScale, "time", 0.2f));
        iTween.ScaleTo(obj, iTween.Hash("scale", Vector3.zero, "time", 0.4f, "delay", 0.2f));
        Destroy(obj, 0.4f);
        
    }

    void RemoveBackGround()
    {
        Debug.Log("debBack_" + genVal);
        Exploision(GameObject.Find("debBack_" + genVal));
    }


    private void RemoveDebuff()
    {
        Exploision(GameObject.Find("debAct_" + debuffValue));
    }

    private void DrawNewDebuff()
    {
        Debug.Log("drawNewDeb");
        DrawTitle(selectedSprite, debuffValue,-1, "debAct_");
    }

    private void DrawNewBackGround()
    {
        DrawTitle(greySprite, genVal, 1, "debBack_");
    }

    void DrawTitle(Sprite sprite, float xMargin, float zPos, string name)
    {
        GameObject item = new GameObject();
        item.AddComponent<SpriteRenderer>();
        item.GetComponent<SpriteRenderer>().sprite = sprite;
        PushItems(item,new Vector3(xPos+xMargin, yPos,zPos));
        item.name = name+xMargin;
    }

    public void PushItems(GameObject obj, Vector3 pos)
    {
        Vector2 startPos = pos;
        startPos += new Vector2(0.3f, 0.3f);
        obj.transform.position = startPos;
        iTween.MoveTo(obj, iTween.Hash("position", pos, "time", 0.7f, "easetype", iTween.EaseType.easeOutSine));
    }


    private Sprite SelectSprite(int value){
        Sprite selectedSprite;
        switch(value-2){
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
        return selectedSprite;
    }

}
