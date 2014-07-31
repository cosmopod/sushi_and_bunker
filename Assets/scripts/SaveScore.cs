using UnityEngine;
using System.Collections;
using System.IO;

public class SaveScore : MonoBehaviour
{
		protected string filepath;
		protected string filename = "SavedScore";
		protected string extension = ".txt";
		protected string savedScore = "";
		protected StreamWriter writer;
		
		

		// Use this for initialization
		void Start ()
		{
				filepath = Application.dataPath + "\\";
				
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}
		public string GetFileName ()
		{
				return filepath + filename + extension;
		}

		public bool WriteFile (string pts)
		{
								
				int n;
				if (!int.TryParse (GetSavedScore (GetFileName ()), out n)) {
						writer = new StreamWriter (filepath + filename + extension);
						writer.WriteLine (pts);
						writer.Flush ();
						writer.Close ();
						return true;
						
				}
				if (int.Parse (pts) <= int.Parse (GetSavedScore (GetFileName ())))
						return false;

				writer = new StreamWriter (filepath + filename + extension);
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
