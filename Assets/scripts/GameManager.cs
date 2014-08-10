using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
	
	
		public enum Difficulties
		{
				Facil,
				Normal,
				Dificil
		}
	
		public static Difficulties difficulty = Difficulties.Normal;

		//esta variable almacenara el tipo de elemento dragueado
		public static Power.Type SelectedPower = Power.Type.None;

		//este metodo cambia el valor de SelectedPower
		public static void SetPower (Power.Type newPower)
		{
				SelectedPower = newPower;
		}

		// Use this for initialization
		void Start ()
		{
		
		}
	
		// Update is called once per frame
		void Update ()
		{
		
		}
	
		public void ExitPressed ()
		{
				//Exit application
				Invoke ("QuitNow", 0.5f);
		}
		
		public void OnDifficultyChange ()
		{
				string listValue = UIPopupList.current.value;

				switch (listValue) {
				case "Facil":
						difficulty = Difficulties.Facil;
						break;
				case "Normal":
						difficulty = Difficulties.Normal;
						break;
				case "Dificil":
						difficulty = Difficulties.Dificil;
						break;
				}
			
		}
		void QuitNow ()
		{
				Application.Quit ();
		}
		
}
