using UnityEngine;
using System.Collections;

public class TooltipManager : MonoBehaviour
{
		//enum par definir que tipo de texto aclaratorio poner
		public enum Type
		{
				Bomb,
				Tiempo
		}

		public Type type;

		// Use this for initialization
		void Start ()
		{
	
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}

		void OnTooltip (bool state)
		{
				//si el estado el verdadero, creamo un nuevo tooltip que dependera del tipo
				if (state) {
						UITooltip.ShowText (Localization.instance.Get (type.ToString () + "Tooltip"));
				} else {
						UITooltip.ShowText ("");
				}
		}
}
