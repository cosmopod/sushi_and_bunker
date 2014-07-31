using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
		public enum GameState
		{
				playing,
				gameover
		}
		;
		public GameObject player;
		public GameObject bellHolderController;
		public GameState gameState = GameState.playing;
		public static GameController Instance;
		public float timeScrolling = 0.0f;
		public static float limitTimeScrolling = 4.0f;
		public GameObject score;	
		public GameObject bestScore;
		protected BestScore bs;
		
		protected float scrollDownCamspeed = 50.0f;
		protected PlayerControllerGraph pc;
		protected SaveScore saveScore;
		protected bool isSaving = false;
		


		void Awake ()
		{
				Instance = this;
		}
		// Use this for initialization
		void Start ()
		{
				pc = player.GetComponent<PlayerControllerGraph> ();
				saveScore = gameObject.GetComponent<SaveScore> ();
				bs = bestScore.GetComponent<BestScore> ();
				
		}
	
		// Update is called once per frame
		void Update ()
		{
				
				Quit ();
				IsGameOver (player.transform);
				if (gameState == GameState.gameover) {

						if (!isSaving) {
								bs.SetLabelScore (SaveScore (pc.GetPoints ().ToString ()));
								isSaving = true;
						}
					
						if (pc != null) {
								
								player.transform.position = Vector3.Lerp (player.transform.position, pc.GetStartPosition (), 1.0f);							
								pc.enabled = false;
								
						}			
						if (Camera.main.transform.position.y > Vector3.zero.y) {
								Camera.main.transform.position -= Vector3.up * Time.deltaTime * scrollDownCamspeed;
								timeScrolling += Time.deltaTime;
								if (timeScrolling >= limitTimeScrolling) {
										Camera.main.transform.position = Vector3.zero;
										timeScrolling = 0.0f;
								}

						}
						if (Camera.main.transform.position.y <= Vector3.zero.y) {
								Camera.main.transform.position = Vector3.zero;
								Animator playerAnimator = player.GetComponent<Animator> ();
								if (playerAnimator != null)
										playerAnimator.enabled = true;
								
								TweenScale.Begin (score, .25f, new Vector3 (1.0f, 1.0f, 1.0f));
								
				
						}
								

				} else if (gameState == GameState.playing) {

						isSaving = false;
						
						TweenScale.Begin (score, .25f, new Vector3 (0.0f, 0.0f, 1.0f));
						
						pc.enabled = true;
						
			

				}
		}

		public void IsGameOver (Transform obj)
		{
				Vector3 pos = Camera.main.WorldToViewportPoint (obj.position);
				if (pos.y < -0.01f) {
						GameObject.Find ("CatSprite").SendMessage ("SetMaxHeightToZero");
						gameState = GameState.gameover;
						
				}					
		}
		
		public void Quit ()
		{
				Application.Quit ();

		}
		public void SetGameToPlay ()
		{
				this.gameState = GameState.playing;
		}

		public string SaveScore (string points)
		{
				
				saveScore.WriteFile (points);
				string score = saveScore.GetSavedScore (saveScore.GetFileName ());
				if (score == null)
						score = "";
				
				return score;
						
		}
	
		
}
