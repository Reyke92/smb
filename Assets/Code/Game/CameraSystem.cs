using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SMB.Game
{
    internal class CameraSystem : MonoBehaviour
    {
        [SerializeField]
        private float _XMin;

        [SerializeField]
        private float _XMax;

        [SerializeField]
        private float _YMin;

        [SerializeField]
        private float _YMax;

        private GameObject _PlayerGO;

        // Start is called before the first frame update
        private void Start()
        {
            _PlayerGO = GameObject.FindGameObjectWithTag(Tags.PLAYER);
        }

        // Update is called once per frame
        private void LateUpdate()
        {
            float x = Mathf.Clamp(_PlayerGO.transform.position.x, _XMin, _XMax);
            float y = Mathf.Clamp(_PlayerGO.transform.position.y, _YMin, _YMax);
            this.gameObject.transform.position = new Vector3(x, y, this.gameObject.transform.position.z);
        }
    }
}
