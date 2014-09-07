using UnityEngine;
using System.Collections;

public class VolumeController : MonoBehaviour
{
		UISlider uiSlider;

		void Awake ()
		{
				uiSlider = GetComponent<UISlider> ();
				uiSlider.value = NGUITools.soundVolume;
		}

		// Use this for initialization
		void Start ()
		{
	
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}

		public void OnVolumeChange ()
		{
				NGUITools.soundVolume = UISlider.current.value;

				AudioListener.volume = UISlider.current.value;
		}
}
