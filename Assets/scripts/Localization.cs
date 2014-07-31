using UnityEngine;
using System.Collections;
using SmartLocalization;

public class Localization : MonoBehaviour {

	public static LanguageManager languageManager = LanguageManager.Instance;

	public static string GetWord(string val){
		if (languageManager.IsLanguageSupported(languageManager.GetSupportedSystemLanguage()))
		{
			languageManager.ChangeLanguage(languageManager.GetSupportedSystemLanguage());
		}
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

			default: return "Different key";
		}
	}

}
