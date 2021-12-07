using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SMB
{
    internal class PopupBoxManager : MonoBehaviour
    {
        internal static PopupBoxManager Instance { get; private set; }

        private Object _PopupBoxPrefab;

        private void Awake()
        {
            Instance = this;
            _PopupBoxPrefab = Resources.Load("Prefabs/PopupBox");
        }

        internal PopupBoxController CreatePopup(string text)
        {
            GameObject popup = GameObject.Instantiate(_PopupBoxPrefab, this.transform) as GameObject;
            PopupBoxController controller = popup.GetComponent<PopupBoxController>();
            controller.SetText(text);
            return controller;
        }
    }
}
