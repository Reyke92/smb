using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SMB.Game.Enemies;

namespace SMB.Game
{
    internal class GameResources : MonoBehaviour
    {
        internal static readonly Dictionary<Type, int> PointsWorthPerEnemy = new Dictionary<Type, int>()
        {
            { typeof(Goomba),       100 },
            { typeof(GreenTurtle),  100 },
            { typeof(RedTurtle),    100 }
        };

        internal static GameResources Instance { get; private set; }

        private int _Coins;
        internal int Coins
        {
            get { return _Coins; }
            set
            {
                // If there is no change in value, do nothing.
                if (value == _Coins) return;

                if (value >= 100)
                {
                    int numLivesEarned = value / 100; // Integer division.
                    value = value % 100;

                    _Lives += numLivesEarned;
                }

                _Coins = value;
                _OnCoinsChanged(new ValueChangedEventArgs(value));
            }
        }
        internal event EventHandler<ValueChangedEventArgs> CoinsChanged;

        internal int LevelNum;

        private int _Lives;
        internal int Lives
        {
            get { return _Lives; }
            set
            {
                // If there is no change in value, do nothing.
                if (value == _Lives) return;

                _Lives = value;
                _OnLivesChanged(new ValueChangedEventArgs(value));
            }
        }
        internal event EventHandler<ValueChangedEventArgs> LivesChanged;

        private int _Points;
        internal int Points
        {
            get { return _Points; }
            set
            {
                // If there is no change in value, do nothing.
                if (value == _Points) return;

                _Points = value;
                _OnPointsChanged(new ValueChangedEventArgs(value));
            }
        }
        internal event EventHandler<ValueChangedEventArgs> PointsChanged;

        private int _TimeLeft;
        internal int TimeLeft
        {
            get { return _TimeLeft; }
            set
            {
                // If there is no change in value, do nothing.
                if (value == _TimeLeft) return;

                _TimeLeft = value;
                _OnTimeLeftChanged(new ValueChangedEventArgs(value));
            }
        }
        internal event EventHandler<ValueChangedEventArgs> TimeLeftChanged;

        internal int WorldNum;

        private bool _Inited;

        private void Awake()
        {
            _Init();
        }

        private void _Init()
        {
            if (_Inited) return;

            GameObject.DontDestroyOnLoad(this);
            Instance = this;
            _Inited = true;
        }

        private void _OnCoinsChanged(ValueChangedEventArgs e)
        {
            if (this.CoinsChanged != null)
            {
                this.CoinsChanged(this, e);
            }
        }

        private void _OnLivesChanged(ValueChangedEventArgs e)
        {
            if (this.LivesChanged != null)
            {
                this.LivesChanged(this, e);
            }
        }

        private void _OnPointsChanged(ValueChangedEventArgs e)
        {
            if (this.PointsChanged != null)
            {
                this.PointsChanged(this, e);
            }
        }

        private void _OnTimeLeftChanged(ValueChangedEventArgs e)
        {
            if (this.TimeLeftChanged != null)
            {
                this.TimeLeftChanged(this, e);
            }
        }
    }

    internal class ValueChangedEventArgs : EventArgs
    {
        internal int Value;

        internal ValueChangedEventArgs(int value)
        {
            this.Value = value;
        }
    }
}