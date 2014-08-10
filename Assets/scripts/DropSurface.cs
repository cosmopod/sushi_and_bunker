using UnityEngine;
using System.Collections;

public class DropSurface : MonoBehaviour
{

		public GameObject dragItemsContainer;

		// Use this for initialization
		void Start ()
		{
	
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}

		// llamamos a este metodo cuando dopreamos un objeto sobre este objeto
		public void OnDrop (GameObject droppped)
		{

				//cogemos el objeto DragItem del objeto que hemos dropeado
				DragItem dragItem = droppped.GetComponent<DragItem> ();
			
				//comprbamos que no venga null
				if (dragItem == null)
						return;

				RecreateDragItem ();
			
				//instanciamos en caso contrario el objeto arrastrado
				GameObject newPower = NGUITools.AddChild (this.gameObject, dragItem.createOnDrop as GameObject);

				GameManager.SetPower (newPower.GetComponent<Power> ().type);
				//finalmente destruimos el objeto dropeado
				Destroy (droppped);
	
		}

		void RecreateDragItem ()
		{
				if (GameManager.SelectedPower != Power.Type.None) {
						//cogemos el script power del objeto power
						Power selectedPowerScript = transform.GetChild (0).GetComponent<Power> ();
				
						//añadimos el poder dragueado al grid
						NGUITools.AddChild (dragItemsContainer, selectedPowerScript.createOnDestroy as GameObject);
						//deestruimos el poder seleccionado actualmente
						Destroy (selectedPowerScript.gameObject);
				}
		}

		void OnClick ()
		{
				RecreateDragItem ();
				//reseteamos el poder a nulo
				GameManager.SetPower (Power.Type.None);
				//Forzamos la ordenacion del la rehilla de poderes
				dragItemsContainer.GetComponent<UIGrid> ().Reposition ();
		}

}