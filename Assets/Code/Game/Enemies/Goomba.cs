using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SMB.Game.Enemies
{
    internal class Goomba : Enemy
    {
        [SerializeField]
        private float _HorizontalSpeed;

        [SerializeField]
        [InspectorName("X Move Direction (-1 for Left, 1 for Right)")]
        private int _XMoveDirection;

        internal Goomba()
        {
            _HitPoints = 1;
        }

        protected override void Awake()
        {
            base.Awake();
        }

        protected override void Start()
        {
            base.Start();
        }

        private void Update()
        {
            _RB.velocity = new Vector2(_XMoveDirection, 0) * _HorizontalSpeed;
        }
        
        private void OnCollisionEnter2D(Collision2D col)
        {
            CollisionSide colSide = CollisionHelper.GetCollisionSide(col);

            // If the Goomba was hit on the left side and was moving left, switch directions.
            // If the Goomba was hit on the right side and was moving right, switch directions also.
            if ((colSide == CollisionSide.Left && _XMoveDirection == -1) ||
                (colSide == CollisionSide.Right && _XMoveDirection == 1))
            {
                _XMoveDirection *= -1;
            }
        }

        protected override void _OnKilled()
        {
            SoundManager.Instance.Play(SoundEffect.Stomp);
            GameResources.Instance.Points += GameResources.PointsWorthPerEnemy[typeof(Goomba)];

            GameObject.Destroy(this.gameObject);
        }
    }
}