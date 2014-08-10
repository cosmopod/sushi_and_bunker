using UnityEngine;
using System.Collections;

public class NotificationManager : MonoBehaviour
{


		// creamos una enumeracion para el tipo de validacion necesario
		public enum Type
		{
				Nickname,
				Power,
				BarrierAvailable
		}
		//almacenamos la notifiacion para acceder a ella mediante metodos estaticos
		public static NotificationManager instance;

		public UILocalize local;
		public Type type;

		void Awake ()
		{
				instance = this;
				//desactivamos el objeto notificacion
				gameObject.SetActive (false);
		}



		// Use this for initialization
		void Start ()
		{
	
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}

		void OnEnable ()
		{

				//empezamos con un tweenscale de 0.5 segundos hacia la escala {0,0,1}
				TweenScale tween = TweenScale.Begin (this.gameObject, 0.5f, new Vector3 (1, 1, 1));
				//añadimos un efecto easing  in and out a nuestro tween
				tween.method = UITweener.Method.EaseInOut;
				//seteamos la key para la localizacion
				local.key = type.ToString () + "Notification";
				//forzamos la actualizacion de UILocalize con la nueva key
				local.Localize ();

		}

		public void Show (Type notificationType, float duration)
		{
				//si no hay notificacion
				if (!gameObject.activeInHierarchy) {
						//seteamos el tipo de notificacion
						type = notificationType;
				
						//activamos el tipo de notificacion
						gameObject.SetActive (true);
						//empleamos una co-rutina para eliminar la notificacion despues de haber permanecido un tiempo
						StartCoroutine (Remove (duration));
				}
		}

		public IEnumerator Remove (float duration)
		{
				//esperamos el tiempo de la notificacion
				yield return new WaitForSeconds (duration);
				//comenzamos  el efecto tween para hacer desaparecer la notificacion
				TweenScale.Begin (gameObject, 0.5f, new Vector3 (0, 0, 1));
				//esperamos a la duracion de la desaparicon del efecto tween
				yield return new WaitForSeconds (0.5f);
				//desactivamos el objeto de la notificacion
				gameObject.SetActive (false);

		}
}


