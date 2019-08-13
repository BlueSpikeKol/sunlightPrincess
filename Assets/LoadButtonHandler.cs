using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadButtonHandler : MonoBehaviour {

    public void LoadHealth()
    {
        GameControl.control.Load();
    }
}
