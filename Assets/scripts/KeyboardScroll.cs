using UnityEngine;
using System.Collections;

public class KeyboardScroll : MonoBehaviour
{

		//declaramos las variables para las barras de scroll
		UIScrollBar hScrollbar;
		UIScrollBar vScrollBar;

		public float keyboardSensitivity = 1;

		void Awake ()
		{
				///asignamos ambas scrollbars en el metodo awake
				hScrollbar = GetComponent<UIDraggablePanel> ().horizontalScrollBar;
				vScrollBar = GetComponent<UIDraggablePanel> ().verticalScrollBar;
		}
		
		// Use this for initialization
		void Start ()
		{
	
		}
	
		// Update is called once per frame
		void Update ()
		{
				//Cogemos los inputs axiales del teclado
				Vector2 keyDelta = Vector2.zero;
				keyDelta.Set (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"));
				//volvemos a vector zzero si no presionamos las flechas del teclado
				if (keyDelta == Vector2.zero)
						return;
				//hacemos el movimiento indepndiente del frame rate y lo ajustamos a la sensibilidad
				keyDelta *= Time.deltaTime * keyboardSensitivity;
				
				//ajustamos el scroll al valor de las barras de scroll
				hScrollbar.value += keyDelta.x;
				vScrollBar.value -= keyDelta.y;
			
				
		}
}
