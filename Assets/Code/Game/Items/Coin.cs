using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SMB.Game.Items
{
    internal class Coin : MonoBehaviour
    {
        private BoxCollider2D _Col2D;

        private void Start()
        {
            _Col2D = this.GetComponent<BoxCollider2D>();
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.tag == Tags.PLAYER)
            {
                SoundManager.Instance.Play(SoundEffect.CoinCollected);
                GameResources.Instance.Coins += 1;
                GameObject.Destroy(this.gameObject);
            }
        }
    }
}
