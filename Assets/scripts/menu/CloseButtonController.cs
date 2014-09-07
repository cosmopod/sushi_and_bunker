using UnityEngine;
using System.Collections;

public class CloseButtonController : MonoBehaviour
{
		protected UISprite uiSprite;
		protected Color color;

		void Awake ()
		{
				uiSprite = GetComponent<UISprite> ();
		}

		// Use this for initialization
		void Start ()
		{
				uiSprite.alpha = 0.0f;
		}
	
		// Update is called once per frame
		void Update ()
		{
				float mouseSpeed = Input.GetAxis ("Mouse X");
				if (mouseSpeed != 0.0f) {
						
						IncreaseAlpha (uiSprite, true);
				} else {
						IncreaseAlpha (uiSprite, false);	
				}
				
				
		}

		void OnClick ()
		{
				Application.LoadLevel ("menu");
		}

		void OnHover ()
		{
				uiSprite.alpha = 1.0f;
		}

		void IncreaseAlpha (UISprite ui, bool move)
		{
				float alphaFactor = -0.5f;
				if (move)
						alphaFactor = 0.5f;
				color = ui.color;
				color.a += alphaFactor * Time.deltaTime;
				color.a = KeepAlphaMargin (color.a);
				ui.color = color;
					
				
				
		}
		private float KeepAlphaMargin (float a)
		{
				float alpha = a;
			
				if (alpha >= 1.0)
						alpha = 1.0f;
				if (alpha <= 0.0f)
						alpha = 0.0f;

				return alpha;
		}
}
