using UnityEngine;
using System.Collections;

public class ButtonController : MonoBehaviour
{

		protected UILabel uiLabel;

		void Awake ()
		{
				uiLabel = GameObject.FindGameObjectWithTag ("RecordLabel").GetComponent<UILabel> ();
		}
		// Use this for initialization
		void Start ()
		{
				
				GetRecord ();
		}
	
		// Update is called once per frame
		void Update ()
		{
				
		}

		public void LoadGame ()
		{
				MenuController.Instance.ChangeScene ("game");
		}
		
		public void LoadCredits ()
		{
				MenuController.Instance.ChangeScene ("credits");	
		}

		public void QuitGame ()
		{
				
				MenuController.Instance.ChangeScene (); 
				
		}

		public void ResetRecord ()
		{
				MenuController.Instance.ResetRecord ();
				uiLabel.text = "0";
		}

		public void GetRecord ()
		{
				if (MenuController.Instance.GetRecord () != null) {
						uiLabel.text = MenuController.Instance.GetRecord ();//uiLabel.text = MenuController.Instance.GetRecord ();
				} else {
						uiLabel.text = "0";
				}
						

		}
}
