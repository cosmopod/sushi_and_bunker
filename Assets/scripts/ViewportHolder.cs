using UnityEngine;
using System.Collections;

public class ViewportHolder : MonoBehaviour
{
		//Necesitamos dos variables par almacenar los prefabs
		public Object barrierObjectPrefab;
		public Object activeBarrierPrefab;

		//declaramos una variable para el contenedor del barrierObject
		public GameObject barrierContainer;
		public int barrierCount = 0;
		
		// Use this for initialization
		void Start ()
		{
	
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}

		void OnDrop (GameObject droppedObj)
		{
				//obtenemos el objeto object controller añadido a las barreras
				BarrierObjectController barrierObj = droppedObj.GetComponent<BarrierObjectController> ();
				if (barrierObj != null) {
						Destroy (droppedObj);
						RecreateBarrierObject ();
						CreateActiveBarrier (droppedObj.transform);
				}

		}

		void RecreateBarrierObject ()
		{
				//add a barrier object to the container
				Transform newBarrierTrans = NGUITools.AddChild (barrierContainer, barrierObjectPrefab as GameObject).transform;
				//reset its localPosition to zero
				newBarrierTrans.localPosition = Vector3.zero;
				//inicamos el lapsus con co-rutina llamando al metodo cooldown
				StartCoroutine (newBarrierTrans.GetComponent<BarrierObjectController> ().CoolDown ((barrierCount + 1) * 3));
		}

		void CreateActiveBarrier (Transform dropObj)
		{
				//añadimos una barrera al viewport
				Transform newActiveBarrierTrans = NGUITools.AddChild (gameObject, activeBarrierPrefab as GameObject).transform;
				//la colocamos en la posicion en la que hemos dropeado el objeto
				newActiveBarrierTrans.position = dropObj.position;
				
				//actualizamos  el timepo de la barrera
				barrierCount++;
				//cremos una co-rutina para iniciar el slide de la barrera
				StartCoroutine (newActiveBarrierTrans.GetComponent<ActiveBarrierController> ().Build (barrierCount * 2));
		}

		void BarrierRemoved ()
		{
				//quitamos uno a la cuenta de barreras construidas
				barrierCount--;
		}
}
