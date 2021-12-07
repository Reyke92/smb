using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SMB.Game.Blocks
{
    internal abstract class ActionBlock : Block
    {
        private static Sprite _EMPTY_BLOCK_SPRITE;

        protected bool _IsEmpty;

        protected virtual void Awake()
        {
            base.Awake();

            if (_EMPTY_BLOCK_SPRITE == null)
            {
                _EMPTY_BLOCK_SPRITE = Resources.Load<Sprite>("Sprites/EmptyBlock");
            }
        }

        protected void _DeclareEmpty()
        {
            this._SpriteRenderer.sprite = _EMPTY_BLOCK_SPRITE;
            this._IsEmpty = true;
        }
    }
}