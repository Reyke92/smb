using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SMB.Game.Blocks
{
    internal class PowerUpBlock : ActionBlock
    {
        [SerializeField]
        private PowerUpType _PowerUp;

        private void Start()
        {
            if (_PowerUp == PowerUpType.None) _DeclareEmpty();
        }

        protected override void _OnHitByPlayer()
        {
            if (!_IsEmpty)
            {
                _DeclareEmpty();
                SoundManager.Instance.Play(SoundEffect.PowerUpAppears);
            }
        }
    }
}