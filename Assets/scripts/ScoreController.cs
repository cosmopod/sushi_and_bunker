using UnityEngine;
using System.Collections;

public class ScoreController : MonoBehaviour
{

		protected int points = 0;
		public static ScoreController Instance;
		protected UILabel uiLabel;
		protected PlayerControllerGraph player;
		
		void Awake ()
		{
				Instance = this;
				
		}

		// Use this for initialization
		void Start ()
		{
				uiLabel = gameObject.GetComponent<UILabel> ();
				player = PlayerControllerGraph.Instance;
		}
	
		// Update is called once per frame
		void Update ()
		{
				UpdatePoints ();
		}

		public void  UpdatePoints ()
		{
				if (points != player.GetPoints ()) {
						points = player.GetPoints ();
						StartCoroutine (TweenScore ());
				}
				
				uiLabel.text = points.ToString () + " points";
				
				
		}
		IEnumerator TweenScore ()
		{
				uiLabel.color = Color.yellow;
				TweenScale.Begin (gameObject, 0.2f, new Vector3 (1.5f, 1.5f, 1.5f));
				yield return new WaitForSeconds (0.2f);
				uiLabel.color = Color.white;
				TweenScale.Begin (gameObject, 0.2f, new Vector3 (1.0f, 1.0f, 1.0f));
		}
		
	
}
