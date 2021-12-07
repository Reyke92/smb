using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SMB.Tweening
{
    public class UIButtonTween : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField]
        public UITweenBehavior Behavior;

        [SerializeField]
        [Range(0f, 5f)]
        public float Duration;

        [SerializeField]
        public LeanTweenType EaseType;

        [SerializeField]
        [Range(0f, 10f)]
        public float ScaleValue;

        private Button _Button;

        private Vector3 _OriginalScale;
        private RectTransform _RectTf;
        private LTDescr _ScaleTD;

        public UIButtonTween()
        {
            Behavior = UITweenBehavior.ScaleOnHover;
            Duration = 0.5f;
            EaseType = LeanTweenType.easeOutExpo;
            ScaleValue = 1.15f;
        }

        public void Start()
        {
            _RectTf = GetComponent<RectTransform>();
            _OriginalScale = _RectTf.localScale;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (Behavior == UITweenBehavior.ScaleOnHover)
            {
                if (_ScaleTD != null) LeanTween.cancel(_ScaleTD.uniqueId);

                Vector3 newScale = _OriginalScale;
                newScale.x *= ScaleValue;
                newScale.y *= ScaleValue;

                _ScaleTD = LeanTween.scale(_RectTf, newScale, Duration).setEase(EaseType);
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (Behavior == UITweenBehavior.ScaleOnHover)
            {
                if (_ScaleTD != null) LeanTween.cancel(_ScaleTD.uniqueId);

                _ScaleTD = LeanTween.scale(_RectTf, _OriginalScale, Duration).setEase(EaseType);
            }
        }
    }
}
