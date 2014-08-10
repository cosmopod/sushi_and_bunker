using UnityEngine;
using System.Collections;

public class BarrierObjectController : MonoBehaviour
{
		//necesitamos el boton y un label
		private UIButton button;
		private UILabel label;
		
		void Awake ()
		{
				//inicamos el boton y el label
				button = GetComponentInChildren<UIButton> ();
				label = GetComponentInChildren<UILabel> ();
		}

		// Use this for initialization
		void Start ()
		{
			
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}

		void OnPress (bool pressed)
		{
				//invertimos el estado del collider
				this.collider.enabled = !pressed;
				

				//si ha sido dropeado
				if (!pressed) {
						
						//cogemos el collider del objetivo (en este caso el del viewport)
						Collider col = UICamera.lastHit.collider;
						if (col == null || col.GetComponent<ViewportHolder> () == null) {
								//reset its local position to zero
								transform.localPosition = Vector3.zero;
								
						}
				}

		}

		public IEnumerator CoolDown (int cooldown)
		{
				//desactivamos el boton de crear una nueva barrera y actualizamos  el color a disabled
				button.isEnabled = false;
				button.UpdateColor (false, true);
				while (cooldown > 0) {
						//actualizamos el label cada segundo
						label.text = Localization.instance.Get ("Wait") + " " + cooldown.ToString () + "s";
						cooldown -= 1;
						//esperamos un segundo y volvemos a iniciar el bucle while
						yield return new WaitForSeconds (1);
				}
				if (cooldown <= 0) {
						CooldownFinished ();
				}
		}

		protected void CooldownFinished ()
		{
				//Reseteamos el label de la barrera
				label.text = Localization.instance.Get ("Barrier");
				//reactivamos el boton y le damos un color normal
				button.isEnabled = true;
				button.UpdateColor (true, true);
				//ponemos la escala del boton a cero
				transform.localScale = Vector3.zero;
				//hacemos un tween para incrementar su escala suavemente
				TweenScale.Begin (gameObject, 0.3f, new Vector3 (1, 1, 1));
				//informamos al jugador de una nueva barrera disponible
				NotificationManager.instance.Show (NotificationManager.Type.BarrierAvailable, 1.5f);
					
		}
}
