using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SMB.Game.Blocks
{
    internal class CoinBlock : ActionBlock
    {
        [SerializeField]
        private int _NumCoinsContained;

        private void Start()
        {
            if (_NumCoinsContained == 0) _DeclareEmpty();
        }

        protected override void _OnHitByPlayer()
        {
            if (_NumCoinsContained > 0)
            {
                _NumCoinsContained -= 1;
                GameResources.Instance.Coins += 1;
                SoundManager.Instance.Play(SoundEffect.CoinCollected);

                // If we are now at 0 coins left in the block, declare that it is empty.
                if (_NumCoinsContained == 0) _DeclareEmpty();
            }
        }
    }
}