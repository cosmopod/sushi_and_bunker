using UnityEngine;
using System.Collections;

public class UpdatePanel : MonoBehaviour
{

		// Use this for initialization
		void Start ()
		{
	
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}

		public void UpdateNow ()
		{
				//fuerza un dragueo de  {0,0,0} para actualizar el panel
				GetComponent<UIDraggablePanel> ().MoveRelative (Vector3.zero);
		}
}
