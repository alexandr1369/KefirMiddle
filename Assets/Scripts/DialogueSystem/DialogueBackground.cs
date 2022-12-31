using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.UI;

namespace DialogueSystem
{
    public class DialogueBackground : MonoBehaviour
    {
        public const float FADE_TIME = .3f;
        
        [field: SerializeField] public Image BackgroundImage { get; set; }

        private TweenerCore<Color, Color, ColorOptions> _hidingState;
        private Color _transparentColor;
        private Color _activeColor;
        
        private void Awake()
        {
            _activeColor = BackgroundImage.color;
            _transparentColor = _activeColor;
            _transparentColor.a = 0f;
            BackgroundImage.color = _transparentColor;
        }
        
        public void Show()
        {
            if(_hidingState.IsActive())
                _hidingState.Kill();
            
            BackgroundImage
                .DOColor(_activeColor, FADE_TIME)
                .SetUpdate(true);
        }

        public void Hide(Action onFinish)
        {
            _hidingState = BackgroundImage
                .DOColor(_transparentColor, FADE_TIME)
                .SetUpdate(true)
                .OnComplete(() =>
                {
                    onFinish.Invoke();
                    _hidingState = null;
                });
        }
    }
}