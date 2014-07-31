//----------------------------------------------
//            NGUI: Next-Gen UI kit
// Copyright © 2011-2012 Tasharen Entertainment
//----------------------------------------------

using UnityEngine;

/// <summary>
/// Simple example of an OnDrop event accepting a game object. In this case we check to see if there is a DragDropObject present,
/// and if so -- create its prefab on the surface, then destroy the object.
/// </summary>

[AddComponentMenu("NGUI/Examples/Drag and Drop Surface")]
public class DragDropSurface : MonoBehaviour
{
	public bool rotatePlacedObject = false;
	//Add this variable to get our power containerd
	public GameObject powersContainer;
	
	void OnDrop (GameObject go)
	{	
		DragDropItem ddo = go.GetComponent<DragDropItem>();
		if (ddo != null)
		{
			RecreateDragDropItem();
				
			GameObject child = NGUITools.AddChild(gameObject, ddo.prefab);
			Transform trans = child.transform;
			
			//Add this line to set the new Power
//			GameManager.SetPower(child.GetComponent<PowerItem>().type);
			
			if (rotatePlacedObject) trans.rotation = Quaternion.LookRotation(UICamera.lastHit.normal) * Quaternion.Euler(90f, 0f, 0f);
			Destroy(go);	
		}
	}
	
	
	void RecreateDragDropItem() {
		//If there already is a Power selected
/*		if(GameManager.SelectedPower != PowerItem.Type.None)
		{
			//Get its PowerItem component:
			PowerItem currentPowerItem;
			currentPowerItem = transform.GetChild(0).GetComponent<PowerItem>();
			
			//Recreate the currently selected Power's Draggable Item:
			NGUITools.AddChild(powersContainer, currentPowerItem.itemPrefab as GameObject);
			
			//We can now destroy the currentPower:
			Destroy(currentPowerItem.gameObject);
		}*/
	}
	
	void OnClick() 
	{
		/*if(GameManager.SelectedPower != PowerItem.Type.None)
		{
			RecreateDragDropItem();
			//Set the SelectedPower to None
			GameManager.SetPower(PowerItem.Type.None);
			//Ask the UITable to recalculate positions
			powersContainer.GetComponent<UITable>().Reposition();
		}*/
	}
}