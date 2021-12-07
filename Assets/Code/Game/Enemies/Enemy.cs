using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SMB.Game.Enemies
{
    internal abstract class Enemy : MonoBehaviour
    {
        protected Animation[] _Animations;
        protected BoxCollider2D _Col2D;
        protected int _HitPoints;
        protected Rigidbody2D _RB;

        protected virtual void Awake()
        {

        }

        protected virtual void Start()
        {
            _RB = this.GetComponent<Rigidbody2D>();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {

        }

        internal void DamageEnemy(int hitPointDamage = 1)
        {
            _HitPoints -= hitPointDamage;
            if (_HitPoints <= 0)
            {
                _HitPoints = 0;
                _OnKilled();
            }
        }

        protected abstract void _OnKilled();
    }
}