using UnityEngine;
using System.Collections;

public class ExplosionController : MonoBehaviour
{
		
		//public AudioClip explosion;
		public AudioClip explosion;
		
		
		// Use this for initialization
		void Start ()
		{
				NGUITools.PlaySound (explosion);
				//audio.PlayOneShot (explosion);
				Invoke ("Dissapear", 3);
				
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}

		void  Dissapear ()
		{			
				Destroy (gameObject);
		}


	
}
