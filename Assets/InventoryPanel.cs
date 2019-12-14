using System.Collections;
using System.Collections.Generic;
using PlatformerPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPanel : MonoBehaviour
{
    [SerializeField] private GameObject coinSlot;
    [SerializeField] private GameObject gemSlot;
    [SerializeField] private GameObject artifactSlot;

    [SerializeField] private GameObject pinky;

    void Fill(ItemManager itemManager)
    {
        // todo: rendre générique
        Text coinSlotText = coinSlot.GetComponentInChildren<Text>();
        ItemData items = (ItemData) itemManager.SaveData;
        int coinsCount = items["Coin"];
        coinSlotText.text = "Coin: " + coinsCount;



    }

    // Use this for initialization
    void Start ()
    {
        

		
	}

    void OnEnable()
    {
        var itemManager = pinky.gameObject.GetComponent<ItemManager>();
        Fill(itemManager);
    }

    // Update is called once per frame
    void Update () {
       

    }
}



