using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SMB.Game.Items;
using SMB.Game.Enemies;

namespace SMB.Game
{
    internal class Player : MonoBehaviour
    {
        internal static Player Instance { get; private set; }

        [SerializeField]
        private PlayerType _PlayerType;

        [SerializeField]
        private BoxCollider2D _GroundedCol;

        [SerializeField]
        private BoxCollider2D _HitboxCol;

        [SerializeField]
        private float _HorizontalSpeed;

        [SerializeField]
        private float _HorizontalFastSpeed;

        [SerializeField]
        private float _InitialJumpHeight;

        [SerializeField]
        private float _ProgressiveJumpHeightSmall;

        [SerializeField]
        private float _ProgressiveJumpHeightSuper;

        [SerializeField]
        private float _ProgressiveJumpMaxTime;

        [SerializeField]
        private float _LongJumpDelayTime;

        [SerializeField]
        private float _BounceHeight;

        [SerializeField]
        private PowerUpType _PowerUp;

        private Animator _Animator;
        private bool _HasRedMushroom;
        private bool _IsGrounded;
        private bool _IsLongJumping;
        private float _JumpTimer;
        private float _LongJumpDelayTimer;
        private Rigidbody2D _RB;
        private Vector2 _PreviousPos;
        private Direction _PlayerFacing;
        private CharacterSize _PlayerSize;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            _PlayerFacing = Direction.Right;
            _PreviousPos = new Vector2(this.transform.position.x, this.transform.position.y);
        }

        private void OnDestroy()
        {
            Instance = null;
        }

        internal void ResetPlayer()
        {
            this._IsGrounded = true;
            this._IsLongJumping = false;
            this._HasRedMushroom = false;
            this._PlayerFacing = Direction.Right;
            this._PlayerSize = CharacterSize.Small;
            this.gameObject.SetActive(true);
        }

        private void Start()
        {
            _RB = this.GetComponent<Rigidbody2D>();
            _Animator = this.GetComponent<Animator>();
        }

        internal void BounceBackToGround()
        {
            // First, move the character to his previous position in the last (FixedUpdate()) frame.
            this.transform.position = _PreviousPos;

            // If the player is in the air and moving up, cause them to bounce back to the ground.
            if (!_IsGrounded && _RB.velocity.y > 0.0f)
            {
                _IsLongJumping = false;

                // Then, reverse his y-axis direction.
                _RB.velocity = new Vector2(_RB.velocity.x, -1f * _RB.velocity.y);
            }
        }

        private void FixedUpdate()
        {
            // Update the previous position.
            _PreviousPos.x = this.transform.position.x;
            _PreviousPos.y = this.transform.position.y;

            float xInputAxis = Input.GetAxis("Horizontal");
            float yInputAxis = Input.GetAxis("Vertical");
            float dx = 0f;

            if (xInputAxis > 0.0f) // If 'move right' is pressed.
            {
                _PlayerFacing = Direction.Right;
                dx = _HorizontalSpeed;
            }
            else if (xInputAxis < 0.0f) // If 'move left' is pressed.
            {
                _PlayerFacing = Direction.Left;
                dx = _HorizontalSpeed;
            }

            if (_IsGrounded && yInputAxis > 0.0f) // If 'jump' is pressed and player is on ground.
            {
                if (_PlayerSize == CharacterSize.Small)
                {
                    _RB.AddForce(Vector2.up * _InitialJumpHeight);
                    SoundManager.Instance.Play(SoundEffect.JumpSmall);
                }
                else /*PlayerSize == CharacterSize.Super*/
                {
                    _RB.AddForce(Vector2.up * _InitialJumpHeight);
                    SoundManager.Instance.Play(SoundEffect.JumpSuper);
                }

                _IsGrounded = false;
                _IsLongJumping = true;
                _JumpTimer = 0f;
                _LongJumpDelayTimer = 0f;
            }
            else if (_IsLongJumping) // If we are currently long-jumping.
            {
                if (yInputAxis > 0.0f)
                {
                    if (_LongJumpDelayTimer < _LongJumpDelayTime) // Delay the long-jump by the set amount of time.
                    {
                        _LongJumpDelayTimer += Time.deltaTime;
                    }
                    else if (_JumpTimer <= _ProgressiveJumpMaxTime) // If we still have time left in our long-jump.
                    {
                        if (_PlayerSize == CharacterSize.Small)
                        {
                            _RB.AddForce(Vector2.up * _ProgressiveJumpHeightSmall);
                        }
                        else /*PlayerSize == CharacterSize.Super*/
                        {
                            _RB.AddForce(Vector2.up * _ProgressiveJumpHeightSuper);
                        }

                        _JumpTimer += Time.deltaTime;
                    }
                }
                else _IsLongJumping = false;
            }

            _RB.velocity = new Vector2(xInputAxis * dx, _RB.velocity.y);

            _Animator.SetFloat("Horizontal", xInputAxis);
            _Animator.SetFloat("Vertical", yInputAxis);
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            CollisionSide colSide = CollisionHelper.GetCollisionSide(col);

            if (col.gameObject.tag == Tags.ENEMY)
            {
                Enemy enemy = col.gameObject.GetComponent<Enemy>();

                if (colSide == CollisionSide.Bottom)
                {
                    enemy.DamageEnemy();
                    _RB.AddForce(Vector2.up * _BounceHeight);
                    _IsLongJumping = false;
                    _IsGrounded = false;
                }
                else
                {
                    SoundManager.Instance.Play(SoundEffect.PlayerDies);
                    SoundManager.Instance.StopBackgroundMusic();
                    GameResources.Instance.Lives -= 1;
                    this.gameObject.SetActive(false);
                }
            }
            else if (col.gameObject.tag == Tags.DEATH_WALL)
            {
                SoundManager.Instance.Play(SoundEffect.PlayerDies);
                SoundManager.Instance.StopBackgroundMusic();
                GameResources.Instance.Lives -= 1;
                this.gameObject.SetActive(false);
            }
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            // Attempt a Raycast on the bottom-center side of the Grounded collider.
            RaycastHit2D hit = Physics2D.Raycast(
                new Vector2(_GroundedCol.bounds.center.x, _GroundedCol.bounds.min.y),
                Vector2.down,
                distance: 1f
            );
            if (hit.collider == null)
            {
                // Attempt a Raycast on the bottom-left side of the Grounded collider.
                hit = Physics2D.Raycast(
                    new Vector2(_GroundedCol.bounds.min.x, _GroundedCol.bounds.min.y),
                    Vector2.down,
                    distance: 1f
                );
                if (hit.collider == null)
                {
                    // Attempt a Raycast on the bottom-right side of the Grounded collider.
                    hit = Physics2D.Raycast(
                        new Vector2(_GroundedCol.bounds.max.x, _GroundedCol.bounds.min.y),
                        Vector2.down,
                        distance: 1f
                    );
                    if (hit.collider == null) return; // If we still haven't found anything, go no further.
                }
            }
            col = hit.collider;

            if (col.gameObject.tag == Tags.ENEMY)
            {
                _IsGrounded = false;
                _IsLongJumping = false;
            }
            else if (col.gameObject.tag == Tags.GROUND ||
                     col.gameObject.tag == Tags.PIPE ||
                     col.gameObject.tag == Tags.ACTION_BLOCK)
            {
                _IsGrounded = true;
                _IsLongJumping = false;
                _RB.velocity = new Vector2(_RB.velocity.x, 0f); // Cancel out any leftover velocity.
            }
        }

        private void OnTriggerExit2D(Collider2D col)
        {
            if (col.gameObject.tag == Tags.GROUND ||
                col.gameObject.tag == Tags.PIPE ||
                col.gameObject.tag == Tags.ACTION_BLOCK)
            {
                _IsGrounded = false;
            }
        }
    }
}

internal enum Direction
{
    Left,
    Right
}

internal enum CollisionSide
{
    Left,
    Right,
    Top,
    Bottom
}

public enum PlayerType
{
    Mario,
    Luigi
}

internal enum PowerUpType
{
    None,
    OneUp,
    RedMushroom,
    Star,
    FirePower
}

internal enum CharacterSize
{
    Small,
    Super
}