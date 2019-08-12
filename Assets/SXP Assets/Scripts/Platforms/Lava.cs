using UnityEngine;
using PlatformerPro;

public class Lava : GenericResponder 
{
	public const string WATER_DEFMAT_NAME = "WaterSprite_Default";
    public const string SURFACE_DEFMAT_NAME = "SurfaceSprite_Default";
    public const string INSTANCE_MAT_PATH = "Assets/SXP Assets/Sprites/Materials/";
    
    public Vector2 size = Vector2.one;

    public SpriteRenderer lavaRenderer;
    public SpriteRenderer surfaceRenderer;

    new Transform transform;
    new BoxCollider2D collider;

    #region GETTERS

    public Transform GetTransform
    {
        get
        {
            if (transform == null)
            {
                transform = GetComponent<Transform>();
            }

            return transform;
        }
    }

    public BoxCollider2D GetCollider
    {
        get
        {
            if (collider == null)
            {
                collider = GetComponent<BoxCollider2D>();
            }

            return collider;
        }
    }
    
    #endregion
    
    
    #region ENGINE OVERRIDE
    
    void OnTriggerEnter2D(Collider2D other)
    {
        Floatable fl = other.GetComponent<Floatable>();

        if (fl != null)
        {
            fl.ActivateFloating(true, GetTransform.position.y);
        }

        var characterRef = other.gameObject.GetComponent<ICharacterReference>();

        if(characterRef != null)
        {
            KillCharacter(characterRef.Character);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        Floatable fl = other.GetComponent<Floatable>();

        if (fl != null)
        {
            fl.ActivateFloating(true, GetTransform.position.y);
        }

        var characterRef = other.gameObject.GetComponent<ICharacterReference>();

        if(characterRef != null)
        {
            KillCharacter(characterRef.Character);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Floatable fl = other.GetComponent<Floatable>();

        if (fl != null)
        {
            fl.ActivateFloating(false, GetTransform.position.y);
        }
    }

    #endregion


    void KillCharacter(Character character)
    {
        if(character != null)
        {
            EventResponse response = new EventResponse();
            response.responseType = EventResponseType.KILL;
            response.targetComponent = character.GetComponent<CharacterHealth>();
            DoImmediateAction(response, null);
        }
    }

    public void RefreshLava()
    {
        GetCollider.size = new Vector2(size.x, size.y + 0.5f);
        GetCollider.offset = new Vector2(size.x / 2f, -size.y / 2f + 0.25f);

        if(lavaRenderer != null)
        {
            lavaRenderer.material = new Material(Shader.Find("Sprites/Default"));
            lavaRenderer.drawMode = SpriteDrawMode.Tiled;
            lavaRenderer.size = size;
            lavaRenderer.transform.localPosition = new Vector3(size.x / 2f, -size.y / 2f);
            lavaRenderer.transform.localScale = Vector3.one;
        }

        if(surfaceRenderer != null)
        {
            surfaceRenderer.material = new Material(Shader.Find("Sprites/Default"));
            surfaceRenderer.drawMode = SpriteDrawMode.Tiled;
            surfaceRenderer.size = new Vector2(size.x, 1);
            surfaceRenderer.transform.localPosition = new Vector3(size.x / 2f, 0.5f);
            surfaceRenderer.transform.localScale = Vector3.one;
        }

    }	
}
