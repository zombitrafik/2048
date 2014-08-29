using UnityEngine;
using System.Collections;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;
using System.Net;
using System.IO;
public class GooglePlayServices : MonoBehaviour {

    private bool isFirstTry = true;
    private bool isAuth = false;
    private static GooglePlayServices instance;

    public int test;

    public const string ACHIEVE_NOWICE_BUILDER = "CgkI8PPD1pcBEAIQDA";
    public const string ACHIEVE_ADVANCED_BUILDER = "CgkI8PPD1pcBEAIQDQ";
    public const string ACHIEVE_MASTER_BUILDER = "CgkI8PPD1pcBEAIQDg";
    public const string ACHIEVE_EPIC_FAIL = "CgkI8PPD1pcBEAIQBw";
    public const string ACHIEVE_SOCIALLY_ACTIVE = "CgkI8PPD1pcBEAIQDw";
    public const string ACHIEVE_1000_POINTS = "CgkI8PPD1pcBEAIQAg";
    public const string ACHIEVE_FIRST_STEP = "CgkI8PPD1pcBEAIQAQ";
    public const string ACHIEVE_10000_POINTS = "CgkI8PPD1pcBEAIQAw";
    public const string ACHIEVE_100000_POINTS = "CgkI8PPD1pcBEAIQAw";
    public const string ACHIEVE_1000000_POINTS = "CgkI8PPD1pcBEAIQBQ";
    public const string ACHIEVE_FIRST_COMBO = "CgkI8PPD1pcBEAIQCA";
    public const string ACHIEVE_X10_COMBO = "CgkI8PPD1pcBEAIQCQ";
    public const string ACHIEVE_X15_COMBO = "CgkI8PPD1pcBEAIQCg";
    public const string ACHIEVE_X20_COMBO = "CgkI8PPD1pcBEAIQCw";

    public const string FIGURE_SIZE_LEADER_BOARD = "CgkI8PPD1pcBEAIQEQ";
    public const string COMBO_LEADER_BOARD = "CgkI8PPD1pcBEAIQEA";
    public const string SCORE_LEADER_BOARD = "CgkI8PPD1pcBEAIQBg";

    //private Hashtable resMap = new Hashtable(){
    //    {"achievementFirstStep","CgkI8PPD1pcBEAIQAQ"},
    //    {"achievement1000Points","CgkI8PPD1pcBEAIQAg"},
    //    {"achievement10000Points","CgkI8PPD1pcBEAIQAw"},
    //    {"achievement100000Points","CgkI8PPD1pcBEAIQBA"},
    //    {"achievementHeyStopItd","CgkI8PPD1pcBEAIQBQ"},
    //    {"achievementEpicFail","CgkI8PPD1pcBEAIQBw"},
    //    {"achievementFirstCombo","CgkI8PPD1pcBEAIQCA"},
    //    {"achievementX10Combo","CgkI8PPD1pcBEAIQCQ"},
    //    {"achievementX15Combo","CgkI8PPD1pcBEAIQCg"},
    //    {"achievementCcccomboBreaker","CgkI8PPD1pcBEAIQCw"},
    //    {"achievementNoviceBuilder","CgkI8PPD1pcBEAIQDA"},
    //    {"achievementAdvancedBuilder","CgkI8PPD1pcBEAIQDQ"},
    //    {"achievementMasterBuilder","CgkI8PPD1pcBEAIQDg"},
    //    {"achievementSociallyActive","CgkI8PPD1pcBEAIQDw"},
    //    {"leaderboardMoveCrushLeaderboard","CgkI8PPD1pcBEAIQBg"},
    //    {"leaderboardComboLeaderboard","CgkI8PPD1pcBEAIQEA"},
    //    {"leaderboardFigureSizeLeaderboard","CgkI8PPD1pcBEAIQEQ"}
    //};
    public static GooglePlayServices Instance
    {
        get
        {
            return instance;
        }
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    
	void Start () {
        instance = this;
        isAuth = Authenticate();
	}

   

    public void UpdateRecord(string leaderBoardName, int score)
    {
        string name = leaderBoardName;
        if (isAuth)
        {
            Social.ReportScore(score, name, (bool success) =>
            {

            });
        }
        else
        {
            if (isFirstTry)
            {
                isFirstTry = false;
                Authenticate();
                UpdateRecord(leaderBoardName, score);
            }
        }
    }

    public bool Authenticate()
    {
        if (CheckConnection("http://google.com") == "")
        {
            return false;
        }

        PlayGamesPlatform.Activate();
        
        bool res=true;

        Social.localUser.Authenticate((bool success) =>
        {
            res = success;
        });
        return res;
    }

    public void ShowLeaderBoard(string leaderBoardName)
    {
        string name = leaderBoardName;
        ((PlayGamesPlatform)Social.Active).ShowLeaderboardUI(name);
    }

    public void UpdateAchieveProgress(string achName, double progress)
    {
        progress += GetAchieveProgress(achName);
        progress = progress > 100 ? 100 : progress;
        Social.ReportProgress(achName, progress, (bool success) =>{});
    }

    private double GetAchieveProgress(string achName)
    {
        double res = -1;
        Social.LoadAchievements(achievements =>
        {
            if (achievements.Length == 0){
                return;
            }
            foreach (IAchievement achievement in achievements)
            {
                if (achievement.id == achName)
                {
                    res= achievement.percentCompleted;
                }
            }
        });
        return res;
    }

    public void ShowAchievements()
    {
        ((PlayGamesPlatform)Social.Active).ShowAchievementsUI();
    }

    public string CheckConnection(string resource)
    {
        string html = string.Empty;
        HttpWebRequest req = (HttpWebRequest)WebRequest.Create(resource);
        try
        {
            using (HttpWebResponse resp = (HttpWebResponse)req.GetResponse())
            {
                bool isSuccess = (int)resp.StatusCode < 299 && (int)resp.StatusCode >= 200;
                if (isSuccess)
                {
                    using (StreamReader reader = new StreamReader(resp.GetResponseStream()))
                    {
                        //We are limiting the array to 80 so we don't have
                        //to parse the entire html document feel free to 
                        //adjust (probably stay under 300)
                        char[] cs = new char[80];
                        reader.Read(cs, 0, cs.Length);
                        foreach (char ch in cs)
                        {
                            html += ch;
                        }
                    }
                }
            }
        }
        catch
        {
            return "";
        }
        return html;
    }

}
