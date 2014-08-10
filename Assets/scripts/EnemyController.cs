using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
		//variables para la autodestruccion de la nave enemiga
		public bool hacked = false;
		
		//el label con el codigo para autodestruccion
		private UILabel codeLabel;
		
		// el eslider del hackeo
		private UISlider hackSlider;

		//variable para almacenar elcodigo de hackeo
		public string destructCode = "";
		
		// la velocidad de hackeo
		public float hackSpeed = 0.2f;


		public void Initialize (float movementDuration)
		{
				
				//obtenemos el tamanyo del background del viewport
				Vector2 bgSize = transform.parent.parent.FindChild ("Background").GetComponent<UISprite> ().localSize;
				
				//obtenemos el tamanyo del sprite de este objeto
				Vector2 enemySpriteSize = transform.FindChild ("Sprite").GetComponent<UISprite> ().localSize;
				
				//seteamos su posicion aleatoriamente en el eje X
				transform.localPosition = new Vector3 (Random.Range (enemySpriteSize.x * 0.5f, bgSize.x - enemySpriteSize.x * 0.5f), -(enemySpriteSize.y * 0.5f), 0);
				
				//hacemos un tween a su posicion para que avance hacia la parte mas baja del background
				TweenPosition.Begin (gameObject, movementDuration, new Vector3 (transform.localPosition.x, -bgSize.y + (enemySpriteSize.y * 0.5f), 0));
				
				//Seteamos el viewport como target del forward event
				GetComponent<UIForwardEvents> ().target = transform.parent.parent.gameObject;
			
				//setamos las variables del label del cdigo de autodestruccion  el slider de hackeo
				hackSlider = transform.FindChild ("DestructCode").GetComponent<UISlider> ();
				codeLabel = hackSlider.transform.FindChild ("Label").GetComponent<UILabel> ();
		}

		// Use this for initialization
		void Start ()
		{
	
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}

		public IEnumerator Kill ()
		{
				//hacemos un tween para su desaparicion
				TweenScale.Begin (gameObject, 0.2f, Vector3.zero);
				//Desactivamos el collider
				collider.enabled = false;
				//esperamos al final del tween para destruir el objeto
				yield return new WaitForSeconds (0.2f);
				EnemySpawnController.instance.EnemyDestroyed (this);
				Destroy (gameObject);
		}

		void OnTriggerEnter (Collider other)
		{
				if (other.CompareTag ("DamageZone")) {
						//quitamos puntos de vida al jugador
						HealthbarController.Instance.Damage (30f);
						//eliminamos el enemigo
						StartCoroutine (Kill ());
						return;
				}
				//guardamos en una variable el controllador de la barrera
				ActiveBarrierController barrierController = other.GetComponent<ActiveBarrierController> ();
				//si es de un active barrier, llamamos a su metodo HitByEnemy
				if (barrierController != null) {
						barrierController.HitByEnemy (this);
				}
		}

		public void SetDestructCode (string randomWordkey)
		{
				//comprobamos que la palabra clave no seqa vacia
				if (randomWordkey != "") {
						//cogemos el codigo de localizacion
						destructCode = Localization.instance.Get (randomWordkey);
						//seteamos el label a "Codigo encriptado"
						codeLabel.text = Localization.instance.Get ("CodeEncrypted");
					
				} else
						hackSlider.gameObject.SetActive (false);
				//si la palabra clave es vacia, dectivamos el slider del hackeo



		}
		IEnumerator Hack ()
		{
				//seteamos el label a "hacking"
				codeLabel.text = Localization.instance.Get ("Hacking");
				//mientras el slider de hackeo no esta completo
				while (hackSlider.value < 1) {
						//incrementamos su valor, independientemente del framerate
						hackSlider.value += Time.deltaTime * hackSpeed;
						//esperamos al siguiente frame
						yield return null;
				}
				//nos aseguramos de que el valor de slide es 1
				hackSlider.value = 1;
				//seteamos la variable de hackeo a 1
				hacked = true;
				//motramos el codigo de hackeo	
				codeLabel.text = "[99FF99]" + destructCode;
		}

		//cremo un evento click para iniciar el hackeo
		void OnClick ()
		{
				//si el enemigo tiene un codigo de hackeo, lo lanzamos
				if (!string.IsNullOrEmpty (destructCode))
						StartCoroutine (Hack ());
		}
		

}
