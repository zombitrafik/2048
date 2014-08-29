using UnityEngine;
using System.Collections;
using SmartLocalization;

public class Localization : MonoBehaviour {

	private static LanguageManager languageManager = LanguageManager.Instance;

    public static void Initialize()
    {
        string lang = Ini.LoadLanguage();
        if (languageManager.IsLanguageSupported(lang))
        {
            languageManager.ChangeLanguage(lang);
        }
        else
        {
            if (languageManager.GetSupportedSystemLanguage() != null)
            {
                languageManager.ChangeLanguage(languageManager.GetSupportedSystemLanguage());
            }
            else
            {
                languageManager.ChangeLanguage("en");
            }
            Ini.SaveLanguage(languageManager.LoadedLanguage);
        }
    }

	public static string GetWord(string val){
		switch(val) {
			case "Play": return languageManager.GetTextValue("MainMenu.Play");
			case "Settings": return languageManager.GetTextValue("MainMenu.Settings");
			case "Difficult": return languageManager.GetTextValue("MainMenu.Difficult");
			case "Tutorial": return languageManager.GetTextValue("MainMenu.Tutorial");
			case "Ratings": return languageManager.GetTextValue("MainMenu.Ratings");
			case "Rate": return languageManager.GetTextValue("MainMenu.Rate");
			case "Back": return languageManager.GetTextValue("MainMenu.Back");
			case "Control": return languageManager.GetTextValue("MainMenu.Control");
			case "Language": return languageManager.GetTextValue("MainMenu.Language");
			case "Swipe": return languageManager.GetTextValue("MainMenu.Swipe");
			case "Buttons": return languageManager.GetTextValue("MainMenu.Buttons");
			case "Retry": return languageManager.GetTextValue("Game.Retry");
			case "Main menu": return languageManager.GetTextValue("Game.MainMenu");
			case "Score": return languageManager.GetTextValue("Game.Score");
			case "You best score": return languageManager.GetTextValue("Game.YBScore");
			case "Selected": return languageManager.GetTextValue("MainMenu.Selected");
			case "Share": return languageManager.GetTextValue("Game.Share");
			case "Set difficulty": return languageManager.GetTextValue("MainMenu.Set_difficulty");
			case "Easy": return languageManager.GetTextValue("MainMenu.Easy");
			case "Normal": return languageManager.GetTextValue("MainMenu.Normal");
			case "Hard": return languageManager.GetTextValue("MainMenu.Hard");
            case "Repost text": return languageManager.GetTextValue("Game.Repost_text");
            case "Repost title": return languageManager.GetTextValue("Game.Repost_title");
            case "Repost link": return languageManager.GetTextValue("Game.Repost_link");
            case "Repost tags": return languageManager.GetTextValue("Game.Repost_tags");
			case "Achievements": return languageManager.GetTextValue("MainMenu.Achive");
			case "Combo": return languageManager.GetTextValue("MainMenu.LadderCombo");
			case "Figure size": return languageManager.GetTextValue("MainMenu.LadderFigSize");
			case "tutorial_1": return languageManager.GetTextValue("Tutorial1");
			case "tutorial_2": return languageManager.GetTextValue("Tutorial2");
			case "tutorial_3": return languageManager.GetTextValue("Tutorial3");
			case "tutorial_4": return languageManager.GetTextValue("Tutorial4");
			case "tutorial_5": return languageManager.GetTextValue("Toturial5");
			case "Deutsch": return languageManager.GetTextValue("Deutsch");
			case "English": return languageManager.GetTextValue("English");
			case "Français": return languageManager.GetTextValue("French");
			case "Русский": return languageManager.GetTextValue("Russian");
            case "internet_required": return languageManager.GetTextValue("Intenet_requied");

			default: return "Different key";
		}
	}

    public static void ChangeLanguage(string langStr)
    {
        if (languageManager.IsLanguageSupported(langStr))
        {
            languageManager.ChangeLanguage(langStr);
            Ini.SaveLanguage(langStr);
            Debug.Log("lang changed");
            Application.LoadLevel(Application.loadedLevel);
        }
    }

}
