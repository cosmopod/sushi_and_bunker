using UnityEngine;
using System.Collections;

public class HealthbarController : MonoBehaviour
{
		//variable estatica para tener acceso facilmente a la instancia de ste objeto
		public static HealthbarController Instance;
		
		//guardamos una varibale para el slider y la vida

		private UISlider slider;
		private float hp = 100;


		void Awake ()
		{
				//almacenamos la instancia de ete objeto en la variable estatica de instancia
				Instance = this;
				//obtenemos el slider
				slider = GetComponent<UISlider> ();
		}
		// Use this for initialization
		void Start ()
		{
			
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}

		public void Damage (float damageValue)
		{
				hp = Mathf.Clamp (hp - damageValue, 0, 100);
				//actualizamos el valor del slider
				slider.value = hp * 0.01f;
				if (hp <= 0)
						Application.LoadLevel (Application.loadedLevel);
		}
}
 