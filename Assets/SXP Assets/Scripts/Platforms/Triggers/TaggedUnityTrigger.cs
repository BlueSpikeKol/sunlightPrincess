using UnityEngine;
using System.Collections;

namespace PlatformerPro
{

	/// <summary>
	/// Trigger class that uses Unity triggers. Requires a collider on the character.
	/// </summary>
	[ExecuteInEditMode]
	public class TaggedUnityTrigger : Trigger {

        [TagSelector]
        public string otherColliderTag;

		/// <summary>
		/// Unity enable hook
		/// </summary>

		void OnEnable()
		{
			if (!Application.isPlaying)
			{
				if (GetComponent<Collider2D>() == null)
				{
					BoxCollider2D boxCollider = gameObject.AddComponent<BoxCollider2D>();
					boxCollider.isTrigger = true;
				}
			}
		}

		/// <summary>
		/// Unity start hook.
		/// </summary>
		void Start () {
			Init();
		}

		/// <summary>
		/// Unity 2D trigger hook
		/// </summary>
		/// <param name="other">Other.</param>
		void OnTriggerEnter2D(Collider2D other)
		{
            if(!string.IsNullOrEmpty(otherColliderTag) && other.tag != otherColliderTag) return;

			Character character = null;
			CharacterReference characterReference = other.GetComponent<CharacterReference> ();
			if (characterReference != null) character = characterReference.Character;
			if (character == null) character = other.GetComponentInParent<Character> ();

			if (character != null)
			{
				Debug.Log("Enter Trigger :: " + other.name);
				EnterTrigger(character);
			}
			else if(other.GetComponent<Rigidbody2D>() != null)
			{
				EnterTrigger(null);
			}
		}

		/// <summary>
		/// Unity 2D trigger hook
		/// </summary>
		/// <param name="other">Other.</param>
		void OnTriggerExit2D(Collider2D other)
		{
            if(!string.IsNullOrEmpty(otherColliderTag) && other.tag != otherColliderTag) return;

            Character character = null;
			CharacterReference characterReference = other.GetComponent<CharacterReference> ();
			if (characterReference != null) character = characterReference.Character;
			if (character == null) character = other.GetComponentInParent<Character> ();

			if(character != null)
			{
				LeaveTrigger(character);
			}
			else if(other.GetComponent<Rigidbody2D>() != null)
			{
				LeaveTrigger(null);
			}
		}

		/// <summary>
		/// Unity gizmo hook, draw the connection.
		/// </summary>
		void OnDrawGizmos()
		{
			if (receivers != null)
			{
				Gizmos.color = Trigger.GizmoColor;

				foreach (TriggerTarget receiver in receivers) 
				{
					if (receiver != null && receiver.receiver != null) Gizmos.DrawLine(transform.position, receiver.receiver.transform.position);
				}
			}
		}

	}
}