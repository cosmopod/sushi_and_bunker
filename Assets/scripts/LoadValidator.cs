using UnityEngine;
using System.Collections;

public class LoadValidator : MonoBehaviour
{

		public UIInput nicknameInput;
		public GameObject menuContainer;

		// Use this for initialization
		void Start ()
		{
	
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}

		void OnClick ()
		{
				//comprobamos si el nockname esta vacio
				if (string.IsNullOrEmpty (nicknameInput.value)) {
						//mostramos una notificacion de error
						NotificationManager.instance.Show (NotificationManager.Type.Nickname, 2.5f);
				}
				//hay nick pero no hay poder elegido
				else if (GameManager.SelectedPower == Power.Type.None) {
						NotificationManager.instance.Show (NotificationManager.Type.Power, 2.5f);
				} else {
						//nos aseguramos de guardar el nick aun sin haber pulsado intro en la caja
						PlayerPrefs.SetString ("Nickname", nicknameInput.value);
						menuContainer.SendMessage ("CloseMenu");
						Invoke ("LaunchNow", 0.5f);
				}
		}

		void LaunchNow ()
		{
				Application.LoadLevel ("Game");
		}
}
