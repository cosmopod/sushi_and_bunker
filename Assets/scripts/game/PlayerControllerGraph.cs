using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerControllerGraph : MonoBehaviour
{		
		public float speed = 25.0f;
		public int depth = 5;
		public int jumpHeight = 1300; //como de rapido es capaz de saltar
		public bool isGrounded = false;
		public float maxHeight;
		protected Vector3 maxHeightCamPosition;
		public AudioClip jumpSound;
		public static PlayerControllerGraph Instance;
		protected int points = 0;
		public bool isRight = false;
		protected Animator animator;
		public bool isRunning;
		public bool isHungry;
		protected float waitingTime = 0.0f;
		protected float timeToHungry = 6.5f;
		public bool isFalling;
		public Sprite[] sprites;
		protected Vector3 startPosition;
		protected bool isIll = false;
		protected Color lerpedColor = Color.green;
		protected float illTime = 0.0f;
		
		protected SpriteRenderer spriteRenderer;


		void Awake ()
		{
				Instance = this;
				spriteRenderer = gameObject.GetComponent<SpriteRenderer> ();
				
		}
	
		// Use this for initialization
		void Start ()
		{
						
				maxHeight = 0;
				startPosition = gameObject.transform.position;
				animator = gameObject.GetComponent<Animator> ();
		}
	
	
		// Update is called once per frame
		void Update ()
		{

				if (isIll)
						Heal ();
				
				
				if (transform.position.y >= maxHeight) {
						maxHeight = transform.position.y;
						maxHeightCamPosition = Camera.main.transform.position;
				}
				EnableAnimator (isGrounded);
				FollowCursor ();
				PushObjectBackInFrustum (transform);
				if (IsFalling ()) {
						
						SetSprite (sprites [1]);
				}
		
		}

		void FixedUpdate ()
		{
				isGrounded = !Physics.Linecast (transform.position, GameObject.Find ("Suelo").transform.position);
		
		
				if (Input.GetButtonDown ("Jump") && isGrounded) {
						audio.PlayOneShot (jumpSound);
						Jump ();
			
				}
		
		
		}
	
		void   LateUpdate ()
		{
				
				Camera.main.transform.position = (new Vector3 (0, maxHeight, 0));
		}
	
		public void FollowCursor ()
		{
				
				Vector3 mousePos = Input.mousePosition;
				Vector3 wantedPos = Camera.main.ScreenToWorldPoint (new Vector3 (mousePos.x, mousePos.y, depth));
				if (wantedPos.x > transform.localPosition.x + 1) {
						waitingTime = 0.0f;
						if (!isRight) {
								transform.Rotate (0.0f, -180.0f, 0.0f);
								isRight = true;
						}
						if (!isRunning) {
								
								isRunning = true;
								isHungry = false;
						}
						animator.SetBool ("IsHungry", isHungry);
						animator.SetBool ("IsRunning", isRunning);
						transform.position += Vector3.right * SetSpeed (isGrounded) * Time.deltaTime;
				} else if (wantedPos.x < transform.localPosition.x - 1) {
						waitingTime = 0.0f;
						if (isRight) {
								transform.Rotate (0.0f, 180.0f, 0.0f);
								isRight = false;
						}
						if (!isRunning) {
								isRunning = true;
								isHungry = false;
						}
						animator.SetBool ("IsHungry", isHungry);
						animator.SetBool ("IsRunning", isRunning);
						transform.position -= Vector3.right * SetSpeed (isGrounded) * Time.deltaTime;
				} else {
						if (isRunning) {
								isRunning = false;
						}
						waitingTime += Time.deltaTime;
						if (waitingTime >= timeToHungry) {
								isHungry = true;
								
						}
						animator.SetBool ("IsHungry", isHungry);
						animator.SetBool ("IsRunning", isRunning);
						transform.Translate (Vector3.zero);
			
				}
		
		}
		public void Jump ()
		{
				isHungry = false;
				waitingTime = 0.0f;
				
				animator.enabled = false;
				
				if (!isGrounded) 				
						return;
				
				isGrounded = false;
				SetSprite (sprites [0]);
				rigidbody.AddForce (new Vector3 (0, jumpHeight * Time.deltaTime, 0), ForceMode.VelocityChange);
				
				
		}
	

	
		public void IncreaseHeight ()
		{
				SetSprite (sprites [0]);
				rigidbody.velocity = Vector3.zero;		
				rigidbody.AddForce (new Vector3 (0, (jumpHeight * 0.75f) * Time.deltaTime, 0), ForceMode.VelocityChange);
		
		
		}
	
		public  void AddPoints (int pts)
		{
				this.points += pts;
		}
		
		public void SetPointsToZero ()
		{
				this.points = 0;
		}
		public void SetMaxHeightToZero ()
		{
				this.maxHeight = 0.0f;
		}
		public int GetPoints ()
		{
				return this.points;
		}
		
		public Vector3 GetStartPosition ()
		{
				return this.startPosition;
		}

		public Vector3 GetMaxHeightCamPosition ()
		{
				return this.maxHeightCamPosition;
		}
	
		public bool IsFalling ()
		{
				isFalling = false;
				if (rigidbody.velocity.y < -0.1F) 
						isFalling = true;
				return isFalling;
		}
		public float SetSpeed (bool isGrounded)
		{
				float setSpeed = speed;
				if (isGrounded) 
						setSpeed = speed * 0.600f;
		
				return setSpeed;
		}
		public void SetSprite (Sprite sprite)
		{
				spriteRenderer.sprite = sprite;
		}
	
		public void PushObjectBackInFrustum (Transform obj)
		{
				Vector3 playerSize = renderer.bounds.size;
		
				// Here is the definition of the boundary in world point
				float distance = (transform.position - Camera.main.transform.position).z;
		
				float leftBorder = Camera.main.ViewportToWorldPoint (new Vector3 (0f, 0f, distance)).x + (playerSize.x * .5f);
				float rightBorder = Camera.main.ViewportToWorldPoint (new Vector3 (1f, 0f, distance)).x - (playerSize.x * .5f);
		
				// Mentenemos la posicion del personaje dentro de los margenes de la camara
				obj.position = (new Vector3 (Mathf.Clamp (obj.position.x, leftBorder, rightBorder), obj.position.y, obj.position.z));

		}

		public void EnableAnimator (bool isGrounded)
		{
				
				animator.enabled = false;
				if (isGrounded)
						animator.enabled = true;
				
		}
		
		public void FallIll ()
		{
				
				isIll = true;
				illTime = 0.0f;
				rigidbody.velocity = Vector3.zero;
				spriteRenderer.color = Color.grey;
	
	
		}

		public void Heal ()
		{
				illTime += Time.deltaTime * 0.25f;
				lerpedColor = Color.Lerp (Color.green, Color.white, illTime);
				spriteRenderer.color = lerpedColor;
			
		}

	
}
