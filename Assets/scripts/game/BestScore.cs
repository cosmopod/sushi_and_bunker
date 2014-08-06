using UnityEngine;
using System.Collections;

public class BestScore : MonoBehaviour
{

		protected UILabel uiLabel;

		// Use this for initialization
		void Start ()
		{
				uiLabel = gameObject.GetComponent<UILabel> ();
		}
	
		// Update is called once per frame
		void Update ()
		{
				
		}

		public void SetLabelScore (string score)
		{
				uiLabel.text = "Best Score: " + score + " points";
		}

}
