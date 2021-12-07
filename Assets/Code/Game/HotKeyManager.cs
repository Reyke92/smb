using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SMB.Game
{
    internal class HotKeyManager : MonoBehaviour
    {
        internal static HotKeyManager Instance { get; private set; }

        private Dictionary<KeyCode, Action<KeyCode>> _RegisteredHotKeys;

        private void Awake()
        {
            if (Instance == null) Instance = this;

            _RegisteredHotKeys = new Dictionary<KeyCode, Action<KeyCode>>();
            this.enabled = false;
        }

        private void OnDestroy()
        {
            Instance = null;
        }

        private void Start()
        {

        }

        internal void RegisterHotKey(KeyCode key, Action<KeyCode> callback)
        {
            _RegisteredHotKeys.Add(key, callback);
            this.enabled = true;
        }

        internal void UnRegisterHotKey(KeyCode key)
        {
            _RegisteredHotKeys.Remove(key);
            if (_RegisteredHotKeys.Count == 0) this.enabled = false;
        }

        internal void UnRegisterAllHotKeys()
        {
            this.enabled = false;
            _RegisteredHotKeys.Clear();
        }

        private void Update()
        {
            foreach (KeyValuePair<KeyCode, Action<KeyCode>> entry in _RegisteredHotKeys)
            {
                if (Input.GetKeyDown(entry.Key))
                {
                    entry.Value.Invoke(entry.Key);
                }
            }
        }
    }
}