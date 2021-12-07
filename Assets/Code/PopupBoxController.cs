using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SMB
{
    public class PopupBoxController : MonoBehaviour
    {
        [SerializeField]
        private Text _PopupText;

        [SerializeField]
        private Button _CloseButton;

        private void Start()
        {
            _CloseButton.onClick.AddListener(_HandleCloseButtonClicked);
        }

        public void SetText(string text)
        {
            _PopupText.text = text;
        }

        private void _HandleCloseButtonClicked()
        {
            // Close the pop-up box.
            GameObject.Destroy(this.gameObject);
        }
    }
}
