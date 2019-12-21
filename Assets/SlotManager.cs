using PlatformerPro;
using UnityEngine;
using UnityEngine.UI;

public class SlotManager : MonoBehaviour
{
    public void Fill(ItemData itemData, int index, Text textComponent)
    {
        var slotData = new SlotData(itemData, index);

        textComponent.text = slotData.slotId + " : " + slotData.count;
    }
}


public class SlotData
{
    public string slotId;
    public int count;

    public SlotData(ItemData itemData, int index)
    {
       slotId =  itemData.stackableItemCountsIds[index];
       count = itemData.stackableItemCountsCounts[index];
    }
}
