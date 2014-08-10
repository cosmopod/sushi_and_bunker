using UnityEngine;
using System.Collections;

public class DragItem : MonoBehaviour
{

		public Object createOnDrop;

		// Use this for initialization
		void Start ()
		{
	
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}

		//vamos a desabilitar el collider mientrs es presionado para que no se inserponga en el raycast que
		//va de la camara a la drop surface

		//usamos el metodo OnPress

		void OnPress (bool pressed)
		{
				collider.enabled = !pressed;

				//si dejamos de presionar el objeto
				if (!pressed) {
						//obtenemos el ultimo collider
						Collider col = UICamera.lastHit.collider;
						
						//si el collider es nulo o no hay un dropsurface detras del objeto dropeado
						if (col == null || col.GetComponent<DropSurface> () == null) {
								//cogemos el grid de los objetos padre
								UIGrid grid = NGUITools.FindInParents<UIGrid> (gameObject);
								//si encontramos el grid, reposicionamos a donde estaba el elemento dragueado
								if (grid != null) {
										grid.Reposition ();
										

								}
										
						}
				}
		}
}
