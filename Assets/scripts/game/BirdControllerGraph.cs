using UnityEngine;
using System.Collections;

public class BirdControllerGraph : MonoBehaviour
{

		public const int BIRD_POINTS = 50;
		public float speed = 10.0f;
		protected bool direction = true;
		public GameObject explosion;
		public float timeToShit = 3.0f;
		public float lastShit = 0.0f;
		public float chanceToShit = 25.0f;
		
		//GameObject 
		public GameObject shit;
		
	
		// Use this for initialization
		void Start ()
		{

		}
	
		// Update is called once per frame
		void Update ()
		{
				
				Shit ();
				Movement (Reverse (transform));
				Dissapear (transform);
		}
	
	
		void Movement (bool direction)
		{
		
				if (direction) {
						transform.position += Vector3.right * speed * Time.deltaTime;
				} else {
						transform.position += Vector3.right * -speed * Time.deltaTime;
				}
		
		}
	
		bool Reverse (Transform obj)
		{

				Vector3 playerSize = renderer.bounds.size;
		
				//definimos los limites del viewport
				float distance = (transform.position - Camera.main.transform.position).z;
		
				float leftBorder = Camera.main.ViewportToWorldPoint (new Vector3 (0f, 0f, distance)).x + (playerSize.x * .5f);
				float rightBorder = Camera.main.ViewportToWorldPoint (new Vector3 (1f, 0f, distance)).x - (playerSize.x * .5f);
				
				
				obj.position = (new Vector3 (Mathf.Clamp (obj.position.x, leftBorder, rightBorder), obj.position.y, obj.position.z));
				if (obj.position.x <= leftBorder) {
						if (!direction) {
								direction = true;
								obj.Rotate (0.0f, 180.0f, 0.0f);
						}
				} else if (obj.position.x >= rightBorder) {
						if (direction) {
								direction = false;
								obj.Rotate (0.0f, 180.0f, 0.0f);
						}
				}

				return direction;

		}
	
		void OnTriggerEnter (Collider collider)
		{
		
				PlayerControllerGraph player = collider.GetComponent<PlayerControllerGraph> ();
				if (player != null) {
			
						player.AddPoints (BIRD_POINTS);
						player.IncreaseHeight ();
						Instantiate (explosion, transform.position, transform.rotation);
						DestroyObject (this.gameObject);
				}
		
		
		
		}
	
		void Dissapear (Transform obj)
		{
				Vector3 pos = Camera.main.WorldToViewportPoint (obj.position);
				if (pos.y < 0)
						Destroy (gameObject);
		
		}

		public void Shit ()
		{
				
				lastShit += Time.deltaTime;
				if (lastShit >= timeToShit) {
						if (chanceToShit >= 100 * Random.value) {
								Instantiate (shit, transform.position, transform.rotation);
								lastShit = 0.0f;
						}
				}
		}

}
