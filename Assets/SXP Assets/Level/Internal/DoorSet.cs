using UnityEngine;

public class DoorSet : MonoBehaviour
{
    public enum DoorSetColorsEnum { Jaune, Bleu, Orange, Vert }

    public DoorSetColorsEnum doorSetColors;
    
    public SpriteRenderer lockerRenderer;
    public SpriteRenderer keyRenderer;
    
    public Doorset_Colors yellowDoorsetColors;
    public Doorset_Colors orangeDoorsetColors;
    public Doorset_Colors blueDoorsetColors;
    public Doorset_Colors greenDoorsetColors;

    bool isOpen;
    float debugTimer;

    public bool PlayerHaveKey { get; set; }
    
    void Start()
    {
        SetDoorsetColors();
    }

    void Update()
    {
        if (PlayerHaveKey && isOpen)
        {
            debugTimer += Time.deltaTime;

            if (debugTimer >= 1)
            {
                Destroy(gameObject);
            }
        }
    }

    public void SetDoorsetColors()
    {
        switch (doorSetColors)
        {
            case DoorSetColorsEnum.Jaune:
                lockerRenderer.sprite = yellowDoorsetColors.LockerSprite;
                keyRenderer.sprite = yellowDoorsetColors.KeySprite;
                break;

            case DoorSetColorsEnum.Bleu:
                lockerRenderer.sprite = blueDoorsetColors.LockerSprite;
                keyRenderer.sprite = blueDoorsetColors.KeySprite;
                break;

            case DoorSetColorsEnum.Orange:
                lockerRenderer.sprite = orangeDoorsetColors.LockerSprite;
                keyRenderer.sprite = orangeDoorsetColors.KeySprite;
                break;

            case DoorSetColorsEnum.Vert:
                lockerRenderer.sprite = greenDoorsetColors.LockerSprite;
                keyRenderer.sprite = greenDoorsetColors.KeySprite;
                break;
        }
    }

    public void TryOpenDoor()
    {
        if (isOpen) return;
        
        if (PlayerHaveKey)
        {
            isOpen = true;
        }
    }
}

[System.Serializable]
public class Doorset_Colors
{
    public Sprite LockerSprite;
    public Sprite KeySprite;
}
