using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App
{
	public class TriggerArea : MonoBehaviour
	{
		public List<Collider2D> Colliders = new List<Collider2D>();

		public bool LogMessages;

		public bool Triggered => Colliders.Count > 0;

		public event ColliderEvent TriggerEnter;

		public event ColliderEvent TriggerExit;

		public event ColliderEvent CollisionEnter;

		public event ColliderEvent CollisionExit;

		public event ColliderEvent ColliderStay;

		public void Reset()
		{
			Colliders = new List<Collider2D>();
		}

		private void FixedUpdate()
		{
			Colliders.RemoveAll((Collider2D d) => d == null || !d.gameObject.activeInHierarchy);
			if (ColliderStay != null)
			{
				Colliders.ForEach(delegate(Collider2D d) { ColliderStay(this, d); });
			}
		}

		private void OnCollisionEnter2D(Collision2D collision)
		{
			if (LogMessages)
			{
				Debug.Log(string.Concat("OnCollisionEnter2D: ", collision.collider, " / ",
					Colliders.Contains(collision.collider).ToString()));
			}

			if (!collision.collider.isTrigger && !Colliders.Contains(collision.collider))
			{
				Colliders.Add(collision.collider);
				if (CollisionEnter != null)
				{
					CollisionEnter(this, collision.collider);
				}
			}
		}

		private void OnCollisionExit2D(Collision2D collision)
		{
			if (LogMessages)
			{
				Debug.Log(string.Concat("OnCollisionExit2D: ", collision.collider, " / ",
					Colliders.Contains(collision.collider).ToString()));
			}

			if (Colliders.Contains(collision.collider))
			{
				Colliders.Remove(collision.collider);
				if (CollisionExit != null)
				{
					CollisionExit(this, collision.collider);
				}
			}
		}

		private void OnTriggerEnter2D(Collider2D collider)
		{
			if (LogMessages)
			{
				Debug.Log(string.Concat("OnTriggerEnter2D: ", collider, " / ",
					Colliders.Contains(collider).ToString()));
			}

			if (!collider.isTrigger && !Colliders.Contains(collider))
			{
				Colliders.Add(collider);
				if (TriggerEnter != null)
				{
					TriggerEnter(this, collider);
				}
			}
		}

		private void OnTriggerExit2D(Collider2D collider)
		{
			if (LogMessages)
			{
				Debug.Log(string.Concat("OnTriggerExit2D: ", collider, " / ", Colliders.Contains(collider).ToString()));
			}

			if (Colliders.Contains(collider))
			{
				Colliders.Remove(collider);
				if (TriggerExit != null)
				{
					TriggerExit(this, collider);
				}
			}
		}

		public void Despawn()
		{
		}

		public void Respawn()
		{
			Colliders.Clear();
		}
	}
}
