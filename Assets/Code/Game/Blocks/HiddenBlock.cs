using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SMB.Game.Blocks
{
    internal class HiddenBlock : ActionBlock
    {
        private void Awake()
        {
            base.Awake();
        }

        private void Start()
        {
            this._Col2D.isTrigger = true;
            this._SpriteRenderer.enabled = false;
        }

        private void OnTriggerEnter2D(Collider2D otherCol)
        {
            CollisionSide colSide = CollisionHelper.GetCollisionSideFromTrigger(this._Col2D, otherCol);

            if (otherCol.gameObject.tag == Tags.PLAYER && colSide == CollisionSide.Bottom)
            {
                _OnHitByPlayer();
            }
        }

        protected override void _OnHitByPlayer()
        {
            if (!_IsEmpty)
            {
                Player.Instance.BounceBackToGround();
                _DeclareEmpty();
                this._SpriteRenderer.enabled = true;
                this._Col2D.isTrigger = false; // Turn on regular-block 'collision' mode.
            }
        }
    }
}