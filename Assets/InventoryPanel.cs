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

    void OnEnable()
    {
        var itemManager = pinky.gameObject.GetComponent<ItemManager>();
        var itemData= (ItemData) itemManager.SaveData;

        var slotManagers = GetComponentsInChildren<SlotManager>();

        for (int i=0; i < itemData.stackableItemCountsIds.Count; i++)
        {
            var textComponent = slotManagers[i].GetComponentInChildren<Text>();
            slotManagers[i].Fill(itemData, i, textComponent);
        }
    }
}



