using System;
using Audio;
using UnityEngine;
using Zenject;

namespace UI.Buttons
{
    public class CustomButton : MonoBehaviour
    {
        public event Action OnClick;

        public virtual bool HasSound { get; set; } = true;
        protected virtual AudioClip CustomClickClip { get; set; }
        protected IAudioService AudioService { get; set; }
        
        public UnityEngine.UI.Button Button { get; private set; }
        
        [Inject]
        private void Construct(AudioService audioService) => AudioService = audioService;

        protected virtual void Awake()
        {
            Button = gameObject.GetComponent<UnityEngine.UI.Button>();
            
            if(!Button)
                Button = gameObject.AddComponent<UnityEngine.UI.Button>();
            
            Button.onClick.AddListener(() =>
            {
                OnClick?.Invoke();
                
                if(!HasSound)
                    return;
                
                var clickClip = CustomClickClip ? CustomClickClip : AudioService.ClickClip;
                AudioService.PlayGlobalFx(clickClip);
            });
        }
    }
}