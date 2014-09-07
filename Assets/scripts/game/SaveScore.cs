using UnityEngine;
using System.Collections;
using System.IO;

public class SaveScore : MonoBehaviour
{
		protected string filename = "SavedScore";
		protected string extension = ".txt";
		protected string savedScore = "";
		protected StreamWriter writer;
		private static SaveScore instance;
		
		
		
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

		public static SaveScore Instance {
				get {
						if (instance == null) {
								instance = GameObject.FindObjectOfType<SaveScore> ();
								
								//Le dice a unity queno destruya el objeto cuando cargue otra escena
								DontDestroyOnLoad (instance.gameObject);
						}
			
						return instance;
				}
		}

		// Use this for initialization
		void Start ()
		{
				
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}
		public string GetFileName ()
		{
				return Application.dataPath + "\\" + filename + extension;
		}

		public bool WriteFile (string pts)
		{
								
				int n;
				if (!int.TryParse (GetSavedScore (GetFileName ()), out n)) {
						writer = new StreamWriter (GetFileName ());
						writer.WriteLine (pts);
						writer.Flush ();
						writer.Close ();
						return true;
						
				}
				if (int.Parse (pts) <= int.Parse (GetSavedScore (GetFileName ())) && int.Parse (pts) >= 0)
						return false;
				
				if (int.Parse (pts) < 0) {
						pts = "0";
				}
						
				writer = new StreamWriter (GetFileName ());
				writer.WriteLine (pts);
				writer.Flush ();
				writer.Close ();
				return true;
		}

		public string GetSavedScore (string filename)
		{
				savedScore = "";
				if (!File.Exists (filename))
						return null;
				StreamReader sr = File.OpenText (filename);
				string input = "";
				while (true) {
						input = sr.ReadLine ();
						if (input == null)
								break;
						savedScore += input;
				}
				sr.Close ();
				return savedScore;
		}
}
