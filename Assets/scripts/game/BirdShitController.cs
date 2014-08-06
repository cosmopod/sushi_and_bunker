using UnityEngine;
using System.Collections;

public class BirdShitController : MonoBehaviour
{
		public float speed = 9.0f;
		PlayerControllerGraph player;
		

		// Use this for initialization
		void Start ()
		{
	
		}
	
		// Update is called once per frame
		void Update ()
		{
				Movement ();
		}


		public void Movement ()
		{
		
				transform.Translate (0, -speed * Time.deltaTime, 0);
				Dissapear (transform);
		}

		void Dissapear (Transform obj)
		{
				Vector3 pos = Camera.main.WorldToViewportPoint (obj.position);
				if (pos.y < 0.0f)
						Destroy (gameObject);
		
		}

		void OnTriggerEnter (Collider collider)
		{
		
				player = collider.GetComponent<PlayerControllerGraph> ();
				if (player != null) {
						player.FallIll ();
						Destroy (gameObject);
				}

		}
}