using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawnController : MonoBehaviour
{

		//necesitamos una instancia de enemigo para instanciar
		public Object enemyPrefab;
		// variales para el control del random
		public int firstEnemyDelay = 1;
		//intervalo maximo y minimo entre dos spawns de enemigos
		public int minInterval = 4;
		public int maxInterval = 15;
		
		//rapidez maxima y minima del enemigo
		public float minMovementTime = 20.0f;
		public float maxMovementTime = 50.0f;

		//probabilidd e que un enemigo tenga un codigo de hackeo
		public float destructCodeChance = 60f;
		//array donde guardaremos los codigos de autodestruccion
		public string[] wordkeys;
		//creamos una lista de enemigos
		private  List<EnemyController> enemies;
		//declaramos una instancia de este controlador
		public static EnemySpawnController instance;
		//variable para almacenar el codigo introducido por el jugador
		public string currentWord = "";

		void Awake ()
		{
				//almacenamos la instancia de este script
				instance = this;
				//inicializamos la lista
				enemies = new List<EnemyController> ();
		}
	
		// Use this for initialization
		void Start ()
		{
				//iniciamos la corrutina de regeneracion de enemigos
				StartCoroutine (SpawnEnemy ());
		}
	
		// Update is called once per frame
		void Update ()
		{
				//si el jugador ha introducido alguna palabra
				if (!string.IsNullOrEmpty (Input.inputString)) {
						//añadimos el caracter tecleado a la palabra actual
						currentWord += Input.inputString;
						//comprobamos que la cadena actual coincide con al menos un enemigo
						bool codeMatches = false;
						//checkeamos el codigo de los enemigos
						foreach (EnemyController enemy in enemies) {
								if (enemy.destructCode != "" && enemy.hacked) {
										//comprobamos si la palabra instroducida contiene el codigo
										if (currentWord.Contains (enemy.destructCode)) {
												StartCoroutine (enemy.Kill ());
												codeMatches = true;
										}	
								}
						}
						if (codeMatches) {
								//en el caso de que averiguemos el codigo, reseteamos la palabra introducida
								currentWord = "";
						}
				}


		}

		IEnumerator SpawnEnemy ()
		{
				//seteamos el delay del enemigo
				float delay = firstEnemyDelay;
				//loopeamos mienras eljuego esta en marcha
				while (true) {
						//esperamos el delay
						yield return new WaitForSeconds (delay);
						//creamos un nuevo enemigo
						EnemyController newEnemy = NGUITools.AddChild (gameObject, enemyPrefab as GameObject).GetComponent<EnemyController> ();
						//lo inicialiazamos con una velocidad aleatoria
						newEnemy.Initialize (Random.Range (minMovementTime, maxMovementTime));
						//seteamos el proximo delay
						delay = Random.Range (minInterval, maxInterval);
						
						//creamos un nuevo codigo vacio
						string randomCode = "";
						//si el random entra dentro de las posibilidades fijadas, cogemos la palabra aleatoria
						if (Random.Range (0f, 100f) < destructCodeChance) {
								randomCode = GetRandomWord ();
						}
						newEnemy.SetDestructCode (randomCode);
						//añadimos un enemigo a la lista
						enemies.Add (newEnemy);
					
				}
			
		}

		private string GetRandomWord ()
		{
				//devlvemos una palabra aleatoria de la lista
				return wordkeys [Random.Range (0, wordkeys.Length)];
		}

		public void EnemyDestroyed (EnemyController destroyedEnemy)
		{
				//quitamos el enemigo de la lista de enemigos
				enemies.Remove (destroyedEnemy);
		
		}
}
