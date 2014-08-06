using UnityEngine;
using System.Collections;

public class BellControllerGraph : MonoBehaviour
{

	
		public float speed = 5.0f;
		protected Bounds bound;
		public const int BELL_POINTS = 20;
		public GameObject explosion;
		protected SpriteRenderer spriteRenderer;
		public bool isTrap = false;

		public float trapChance = 2.0f;
	
		// Use this for initialization
		void Start ()
		{		
				
				if (trapChance >= 100.0f * Random.value)
						isTrap = true;
				spriteRenderer = gameObject.GetComponent<SpriteRenderer> ();
				bound = gameObject.GetComponent<SpriteRenderer> ().bounds;
								
		}
	
		// Update is called once per frame
		void Update ()
		{
				if (isTrap)
						ChangeColor ();
				Movement ();
		}
	
		public void Movement ()
		{
		
				transform.Translate (0, -speed * Time.deltaTime, 0);
				Dissapear (transform);
		}
	
		public Bounds GetBoundsObject ()
		{		
		
				return bound;
		}
	
		void OnTriggerEnter (Collider collider)
		{
		
				PlayerControllerGraph player = collider.GetComponent<PlayerControllerGraph> ();
				if (player != null && !isTrap) {
			
						player.AddPoints (BELL_POINTS);
						player.IncreaseHeight ();
						ScoreController.Instance.UpdatePoints ();
						Instantiate (explosion, transform.position, transform.rotation);
						DestroyObject (this.gameObject);

				} else if (player != null && isTrap) {
						Instantiate (explosion, transform.position, transform.rotation);
						DestroyObject (this.gameObject);
				}

		
				if (collider.gameObject.name == "Suelo") {
						StartCoroutine (SmoothDissapear ());
				}
		
		
		
		}
	
		void Dissapear (Transform obj)
		{
				Vector3 pos = Camera.main.WorldToViewportPoint (obj.position);
				if (pos.y < 0.0f)
						Destroy (gameObject);
		
		}
		public IEnumerator SmoothDissapear ()
		{
				TweenScale.Begin (gameObject, 0.5f, Vector3.zero);
				yield return new WaitForSeconds (0.5f);
				Destroy (gameObject);
		}

		public void ChangeColor ()
		{		
				spriteRenderer.color = new Color (1.0f, 1.0f, Random.Range (0.5f, 1.0f));
				
		}
		
		public void IncreaseSpeed ()
		{
				this.speed += .5f;
		}

		public void ResetSpeed ()
		{
				this.speed = 5.0f;
		}
}
