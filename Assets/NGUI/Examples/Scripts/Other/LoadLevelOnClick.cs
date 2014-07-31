using UnityEngine;

[AddComponentMenu("NGUI/Examples/Load Level On Click")]
public class LoadLevelOnClick : MonoBehaviour
{
	public string levelName;
	
	public UIInput nicknameInput; 
	
	public GameObject menuContainer;

	void OnClick ()
	{
		if (!string.IsNullOrEmpty(levelName))
		{
			//If the nickname input is empty...
			if(string.IsNullOrEmpty(nicknameInput.value))
			{
				//...Show a Nickname error notification for 2.5 sec...
				//StartCoroutine(NotificationManager.instance.Show(NotificationManager.Type.Nickname, 2.5f));
				//...and don't go further
				return;
			}
			
			/*//If no Power is selected...
			if(GameManager.SelectedPower == PowerItem.Type.None)
			{
				//...Show a Power error notification for 2.5 sec...
				NotificationManager.ShowNotification(NotificationManager.Type.Power, 2.5f);
				//...and don't go further
				return;
			}*/
			
			//If everything is alright, save nickname and load game
			PlayerPrefs.SetString("Nickname", nicknameInput.value);
			menuContainer.SendMessage("CloseMenu");
			Invoke("LaunchNow", 0.5f);
		}
	}
	
	void LaunchNow()
	{
		Application.LoadLevel(levelName);
	}
}