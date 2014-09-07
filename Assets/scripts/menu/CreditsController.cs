using UnityEngine;
using System.Collections;
using System;

public class CreditsController : MonoBehaviour
{
		
		protected UILabel uiLabel;
		protected string[] arrayCredits;
		protected int maxArrayIndex;
		protected int actualIndex = 0;
		protected Animator animator;
		

		void Awake ()
		{
				uiLabel = GetComponent<UILabel> ();
				animator = GetComponent<Animator> ();
		}
		// Use this for initialization
		void Start ()
		{
					
				string credits = MenuController.Instance.GetCredits ();
				arrayCredits = credits.Split (new Char [] {'|'});
				maxArrayIndex = arrayCredits.Length;
				ChangeCredit ();
				
				
			
		}
	
		// Update is called  per frame
		void Update ()
		{
				
		}


		public void ChangeCredit ()
		{
				if (!StopCredit ()) {
						
						uiLabel.text = arrayCredits [actualIndex % maxArrayIndex];
						actualIndex++;
				} 
		}

		public bool StopCredit ()
		{
				if (actualIndex > maxArrayIndex - 1)
						animator.enabled = false;

				return !animator.enabled;

		}


}
