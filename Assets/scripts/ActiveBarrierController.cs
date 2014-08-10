using UnityEngine;
using System.Collections;

public class ActiveBarrierController : MonoBehaviour
{

		//necesitamos el slider y el objeto para la localizacion del label
		private UISlider slider;
		private UILocalize loc;
		private bool built = false;

		
		//cogeomos los metodos necesarios en awake
		
		void Awake ()
		{
				slider = GetComponentInChildren<UISlider> ();
				loc = GetComponentInChildren<UILocalize> ();
		}
		// Use this for initialization

		void Start ()
		{
				//asignaremos el viewport al evento uiforward de la barrera activa
				GetComponent<UIForwardEvents> ().target = transform.parent.gameObject;
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}

		public IEnumerator Build (float buildTime)
		{
				while (slider.value < 1) {
						slider.value += (Time.deltaTime / buildTime);
						yield return null;

				}
				BuildFinished ();
			
		}
		
		private void BuildFinished ()
		{
				//nos aseguramos que tiene valor 1
				slider.value = 1;
				//steamos l akey para la localizacion  y la actualizamos
				loc.key = "Barrier";
				loc.Localize ();
				built = true;
				collider.enabled = true;
		}
		public void HitByEnemy (EnemyController enemy)
		{
				//si la barrera no esta construida, no hacemos nada
				if (!built)
						return;

				//de lo contrario, destruimos el enemigo
				StartCoroutine (enemy.Kill ());
				// y la barrera
				StartCoroutine (RemoveBarrier ());
		}

		IEnumerator RemoveBarrier ()
		{
				//haemos un tween para ua desaparicion suave
				TweenScale.Begin (gameObject, 0.2f, Vector3.zero);
			
				//notificamos al viewpor que la barreraa ha sido destruida
				transform.parent.SendMessage ("BarrierRemoved");
				
				//esperamos al final del tween y entonces esperamos al objeto
				yield return new WaitForSeconds (0.2f);
				Destroy (gameObject);

		}
}
