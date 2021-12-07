using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SMB.Game.Items
{
    internal class Fireball : MonoBehaviour
    {
        private Direction _Direction;
        private BoxCollider2D _Col2D;
        private Rigidbody2D _RB;

        internal Fireball() { }
        internal Fireball(Direction direction)
        {
            this._Direction = direction;
        }

        private void Start()
        {

        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            
        }

        private void Update()
        {

        }
    }
}
