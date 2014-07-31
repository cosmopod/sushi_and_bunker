using UnityEngine;
using System.Collections;

public class BellHolderController : MonoBehaviour
{
		public const float BELL_ELAPSE = .75f;
		public float bellTime = 0f;
		public float bellLimit;
		public int bellTop = -50;
		public int bellTotal = 0;
		public float bellLastXCoor = 0f;
		protected Bounds bellSize;
		public float spaceBetweenBells = 10f;
		public float chanceToBird = 3.0f;
		protected float lastBirdYCoor = 0f;
		protected float singleBirdInterval = 20f;
		
		public static BellHolderController Instance;
	

		public GameObject bell;
		public GameObject bird;


		void Awake ()
		{
				Instance = this;
		}
		// Use this for initialization
		void Start ()
		{
				bellTime = BELL_ELAPSE;

		}
	
		// Update is called once per frame
		void Update ()
		{
				
				bellTime = Time.time;
				
				
				if (bellTime >= bellLimit) {				
						CreateBird (transform.position);
						bellTotal++;
						if (bellTotal == 1) {
								bellLastXCoor = RndWorldXCoordPos ();
						} else {
								float newBellXCoord = RndWorldXCoordPos ();
								if (newBellXCoord > bellLastXCoor + spaceBetweenBells) {
										newBellXCoord = bellLastXCoor + spaceBetweenBells;
								} else if (newBellXCoord < bellLastXCoor - spaceBetweenBells) {
										newBellXCoord = bellLastXCoor - spaceBetweenBells;
								}
								bellLastXCoor = newBellXCoord;
						}

						Vector3 bellPosition = new Vector3 (bellLastXCoor, transform.position.y, transform.position.z);
						CreateBell (bellPosition);
						bellTotal++;
						bellLimit = bellTime + BELL_ELAPSE;
				
				}
		}


		public void CreateBell (Vector3 pos)
		{	

				Vector3 position = new Vector3 (pos.x, pos.y, 0f);
				Instantiate (bell, position, transform.rotation);
			
				
		}
		
		public void CreateBird (Vector3 pos)
		{	
				if (pos.y > lastBirdYCoor + singleBirdInterval) {
						Vector3 position = new Vector3 (0f, pos.y, 0f);
						if (chanceToBird >= Random.value * 100) {
								Instantiate (bird, position, transform.rotation);
								lastBirdYCoor = position.y;
						}
				} else {
						return;
				}

		}
		
		public float RndWorldXCoordPos ()
		{
				Vector3 pos = Camera.main.ViewportToWorldPoint (new Vector3 (Random.value, 0, 0));
				return pos.x;
		}

		
}
