using UnityEngine;
using System.Collections;

public class Power : MonoBehaviour
{

		public enum Type
		{
				None,
				Tiempo,
				Bomb
		}

		public Type type;

		//esta variable nos servira para almacenar el tipo de elemento draguable que hay que recrear
		public Object createOnDestroy;


		// Use this for initialization
		void Start ()
		{
	
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}
}
