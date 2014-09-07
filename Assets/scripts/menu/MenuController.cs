using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour
{


		private static MenuController instance;
		protected string fileName = "Credits";
		protected string extension = ".txt";
		

		void Awake ()
		{
				if (instance == null) {
						
						//Si es la primera instancia, hacemos un singleton
						instance = this;	
						DontDestroyOnLoad (this);
				} else {
						
						//Si un singleton ya existe y encontramos otra referencia
						//en la escena, la destruimos
						if (this != instance)
								Destroy (this.gameObject);
				}
		}
			
		public static MenuController Instance {
			
				get {

						if (instance == null) {
								instance = GameObject.FindObjectOfType<MenuController> ();
				
								//Le dice a unity queno destruya el objeto cuando cargue otra escena
								DontDestroyOnLoad (instance.gameObject);
						}
			
						return instance;
				}
		}

		void Start ()
		{

		}
	
		// Update is called once per frame
		void Update ()
		{
				
		}
		
		public void  ChangeScene (string scene = null)
		{
				StartCoroutine (PauseBeforeLoad (scene));
			
		}

		IEnumerator PauseBeforeLoad (string scene)
		{
				yield return new WaitForSeconds (1);
				if (scene == null)
						Application.Quit ();
				else
						Application.LoadLevel (scene);
		} 
	
		public bool ResetRecord ()
		{
				return SaveScore.Instance.WriteFile ("-1");
		}

		public string GetRecord ()
		{
			
				return SaveScore.Instance.GetSavedScore (SaveScore.Instance.GetFileName ());
				
		}

		public string GetCredits ()
		{
				return SaveScore.Instance.GetSavedScore (Application.dataPath + "\\" + fileName + extension);
		}

}
