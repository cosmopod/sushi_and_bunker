using UnityEngine;
using System.Collections;

public class AppearFromAbove : MonoBehaviour
{

		// Use this for initialization
		void Start ()
		{
				//primero, steamos la posicion 'y' del menu para que aparezca fuera del menu
				this.transform.localPosition = new Vector3 (0, 1080, 0);
				//comenzamos el tweening de un segundo hacia la posicion {0,0,0}
				TweenPosition tween = TweenPosition.Begin (this.gameObject, 1.5f, Vector3.zero);
				//añadimos un delay
				tween.delay = 1f;
				
				//añadimos una leve desaceleracion de la animacion antes de terminar
				tween.method = UITweener.Method.EaseInOut;

	
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}

		void CloseMenu ()
		{
				//hacemos un tween para escalar el menu a cero
				TweenScale.Begin (this.gameObject, 0.5f, Vector3.zero);
		}

}