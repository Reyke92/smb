using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SMB.Game.Blocks
{
    internal abstract class Block : MonoBehaviour
    {
        protected BoxCollider2D _Col2D;
        protected SpriteRenderer _SpriteRenderer;

        protected virtual void Awake()
        {
            _Col2D = this.GetComponent<BoxCollider2D>();
            _SpriteRenderer = this.GetComponent<SpriteRenderer>();
        }

        protected abstract void _OnHitByPlayer();

        private void OnCollisionEnter2D(Collision2D col)
        {
            CollisionSide colSide = CollisionHelper.GetCollisionSide(col);

            if (col.gameObject.tag == Tags.PLAYER && colSide == CollisionSide.Bottom)
            {
                _OnHitByPlayer();
            }
        }
    }
}