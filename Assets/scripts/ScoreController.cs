using UnityEngine;
using System.Collections;

public class ScoreController : MonoBehaviour
{

		protected int points = 0;
		public static ScoreController Instance;
		protected UILabel uiLabel;
		
		
		void Awake ()
		{
				Instance = this;
		}

		// Use this for initialization
		void Start ()
		{
				uiLabel = gameObject.GetComponent<UILabel> ();
				
		}
	
		// Update is called once per frame
		void Update ()
		{
				UpdatePoints ();
		}

		public void  UpdatePoints ()
		{
				//points = PlayerController.Instance.GetPoints ();
				points = PlayerControllerGraph.Instance.GetPoints ();
				uiLabel.text = points.ToString () + " points";
				
		}
		



}
