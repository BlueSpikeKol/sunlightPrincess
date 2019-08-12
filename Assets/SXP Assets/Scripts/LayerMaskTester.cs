using UnityEngine;

[ExecuteInEditMode]
public class LayerMaskTester : MonoBehaviour
{
	void Update ()
    {
	    if (Input.GetKeyDown(KeyCode.K))
	    {
	        Debug.Log(LayerMask.NameToLayer("Ground"));
	    }
	}
}
