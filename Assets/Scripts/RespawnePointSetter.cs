using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlatformerPro;

public class RespawnePointSetter : MonoBehaviour {

                                       
    void Awake () {

        LevelManager.Instance.ActivateRespawnPoint(LevelManager.PreviousLevel);
    }
    
}
