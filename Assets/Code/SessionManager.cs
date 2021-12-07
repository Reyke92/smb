using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using SMB.Api;

namespace SMB
{
    internal class SessionManager : MonoBehaviour
    {
        internal static SessionManager Instance { get; private set; }
        internal static ApiClient ApiClient { get; private set; }
        internal static GameSave GameSave;

        static SessionManager()
        {
            ApiClient = new ApiClient();
        }

        private void Awake()
        {
            GameObject.DontDestroyOnLoad(this.gameObject);
        }
    }
}