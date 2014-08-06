using UnityEngine;
using System.Collections;

public class EnvironmentController : MonoBehaviour
{

		protected PlayerControllerGraph player;
		protected AudioSource audioSource;
		protected float bottomLimitPitch = .65f;
		protected float pitchChangeSpeed = .1f;
		

		// Use this for initialization
		void Start ()
		{
				player = PlayerControllerGraph.Instance;
				audioSource = gameObject.GetComponent<AudioSource> ();
		}
	
		// Update is called once per frame
		void Update ()
		{
				
				if (player.isHungry) 
						DownPitch ();
				else if (!player.isHungry)
						UpPitch ();
			
		}
		
		void DownPitch ()
		{
				if (audioSource.pitch >= bottomLimitPitch)
						audioSource.pitch -= Time.deltaTime * pitchChangeSpeed;
				
			
		}
		void UpPitch ()
		{
				if (audioSource.pitch < 1.0f)
						audioSource.pitch += Time.deltaTime;
			
		}
		
}
