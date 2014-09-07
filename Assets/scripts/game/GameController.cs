using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
		//GameController vars
		public enum GameState
		{
				playing,
				gameover
		}
		;
		
		
		public GameState gameState = GameState.playing;
		public float timeScrolling = 0.0f;
		public static float limitTimeScrolling = 3.0f;
		protected float scrollDownCamspeed = 50.0f;
		protected bool isSaving = false;
		public int nextlevelScore = 100;
		public static int leveScoreStep = 100;
		protected int fibonacci1 = 1;
		protected int fibonacci2 = 1;
		protected int lastFibonacci = 0;
		
		//gameplay gameobjects
		public GameObject score;	
		public GameObject bestScore;	


		//world gameobjects
		public GameObject player;
		public GameObject sushi;
		public GameObject bellHolderController;
		
		//gameobject controllers
		public static GameController Instance;
		protected PlayerControllerGraph pc;
		public BellHolderController bhc;
		protected BestScore bs;
		protected BellControllerGraph sushiController;
		//mejorar
		protected Animator playerAnimator;


		void Awake ()
		{
				Instance = this;
		}
		// Use this for initialization
		void Start ()
		{
				pc = player.GetComponent<PlayerControllerGraph> ();
				bs = bestScore.GetComponent<BestScore> ();
				sushiController = sushi.GetComponent<BellControllerGraph> ();
				bhc = BellHolderController.Instance;
				playerAnimator = player.GetComponent<Animator> ();
		}
	
		// Update is called once per frame
		void Update ()
		{
				
				
				IsGameOver (player.transform);

				if (gameState == GameState.gameover) {
						
						bhc.DestroyBells ();
						ResetFibonnacci ();
						sushiController.ResetSpeed ();
						

						if (!isSaving) {
								bs.SetLabelScore (SavedScore (pc.GetPoints ().ToString ()));
								isSaving = true;
						}
					
						if (pc != null) {
								
								player.transform.position = Vector3.Lerp (player.transform.position, pc.GetStartPosition (), 1.0f);
								playerAnimator.SetBool ("IsHungry", false);
								playerAnimator.SetBool ("IsRunning", false);
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
								if (playerAnimator != null)
										playerAnimator.enabled = true;
								
								TweenScale.Begin (score, .25f, new Vector3 (1.0f, 1.0f, 1.0f));
								
				
						}
								

				} else if (gameState == GameState.playing) {

						IncreaseLevel (pc.GetPoints ());

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
				Application.LoadLevel ("menu");

		}
		public void SetGameToPlay ()
		{
				this.gameState = GameState.playing;
		}

		public string SavedScore (string points)
		{
				
				SaveScore.Instance.WriteFile (points);
				string score = SaveScore.Instance.GetSavedScore (SaveScore.Instance.GetFileName ());
				if (score == null)
						score = "";
				
				return score;
						
		}
		
		protected int IncreaseFibonacci ()
		{
				lastFibonacci = fibonacci1 + fibonacci2;
				fibonacci1 = fibonacci2;
				fibonacci2 = lastFibonacci;
				return lastFibonacci;
				
		}

		protected void ResetFibonnacci ()
		{
				fibonacci1 = 1;
				fibonacci2 = 1;
				lastFibonacci = 0;
		}
		public void IncreaseLevel (int actualScore)
		{
				if (actualScore >= nextlevelScore) {
						
						IncreaseFibonacci ();
						nextlevelScore = leveScoreStep * lastFibonacci;
						sushiController.IncreaseSpeed ();
				}	
		}
	
		
}
