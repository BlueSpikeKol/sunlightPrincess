using UnityEngine;
using PlatformerPro;

public class LiquidArea : MonoBehaviour
{
    public event System.EventHandler<CharacterEventArgs> CharacterEntered;

    public event System.EventHandler<CharacterEventArgs> CharacterExited;

    private void OnTriggerEntered(Character character)
    {
        if(CharacterEntered != null)
        {
            CharacterEntered(this, new CharacterEventArgs(character));
        }
    }

    private void OnTriggerExited(Character character)
    {
        if(CharacterExited != null)
        {
            CharacterExited(this, new CharacterEventArgs(character));
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Floatable fl = other.GetComponent<Floatable>();

        if(fl != null)
        {
            fl.ActivateFloating(true, transform.position.y);
        }

        var characterRef = other.gameObject.GetComponent<ICharacterReference>();

        if(characterRef != null)
        {
            OnTriggerEntered(characterRef.Character);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        Floatable fl = other.GetComponent<Floatable>();

        if(fl != null)
        {
            fl.ActivateFloating(true, transform.position.y);
        }

        var characterRef = other.gameObject.GetComponent<ICharacterReference>();

        if(characterRef != null)
        {
            OnTriggerEntered(characterRef.Character);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Floatable fl = other.GetComponent<Floatable>();

        if(fl != null)
        {
            fl.ActivateFloating(false, transform.position.y);
        }

        var characterRef = other.gameObject.GetComponent<ICharacterReference>();

        if(characterRef != null)
        {
            OnTriggerExited(characterRef.Character);
        }
    }

    private void OnDisable()
    {
        var collider = GetComponent<Collider2D>();

        var filter = new ContactFilter2D();
        filter.useTriggers = true;

        var results = new Collider2D[100];
        var qte = collider.OverlapCollider(filter, results);

        for(int i = 0; i < qte; i++)
        {
            var fl = results[i].GetComponent<Floatable>();

            if(fl != null)
            {
                fl.ActivateFloating(false, transform.position.y);
            }

            var characterRef = results[i].gameObject.GetComponent<ICharacterReference>();

            if(characterRef != null)
            {
                OnTriggerExited(characterRef.Character);
            }
        }
    }
}