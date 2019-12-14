using UnityEngine;

public class PanelManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (UnityEngine.Input.GetKeyDown(KeyCode.E))
        {
            var inventoryPanel = transform.Find("InventoryPanel").gameObject;

            inventoryPanel.SetActive( !inventoryPanel.activeSelf );
        }
    }
}
