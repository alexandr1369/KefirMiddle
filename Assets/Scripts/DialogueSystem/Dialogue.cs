using System;
// using UI.Buttons;
using UnityEngine;
using UnityEngine.UI;

namespace DialogueSystem
{
    [RequireComponent(typeof(DialogueAnimationController))]
    public class Dialogue : MonoBehaviour
    {
        [field: Header("Dialogue")]
        [field: SerializeField] protected Button MisstapCatcher { get; set; }
        // [field: SerializeField] protected CustomButton CloseButton { get; set; }
        
        public bool IsHiding => !_isShown;

        private DialogueAnimationController _dialogueAnimationController;
        private Action _onHiding;
        private Action _onPreclose;
        private Action _onClose;
        private bool _isShown;
        
        protected virtual void Awake()
        {
            // if (CloseButton) 
            //     CloseButton.OnClick += Close;

            if (MisstapCatcher) 
                MisstapCatcher.onClick.AddListener(Close);

            _dialogueAnimationController = GetComponent<DialogueAnimationController>();
            gameObject.SetActive(false);
        }
        
        public void Show()
        {
            _isShown = true;
            gameObject.SetActive(true);
            _dialogueAnimationController.PlayOpeningAnimation();
        }

        public void Hide(Action onHidden)
        {
            _isShown = false;
            _onHiding?.Invoke();
            
            _dialogueAnimationController.PlayClosingAnimation(() =>
            {
                gameObject.SetActive(false);
                onHidden?.Invoke();
            });
        }
        
        public void Close() => Close(null);

        private void Close(Action onClose)
        {
            _onPreclose?.Invoke();

            if (_isShown)
                Hide(() =>
                {
                    DoClose(onClose);
                });
            else
                DoClose(onClose);
        }
        
        public void SetOnCloseAction(Action onClose) => _onClose += onClose;

        public void SetOnPrecloseAction(Action onPreclose) => _onPreclose += onPreclose;

        protected void SetOnHidingAction(Action onHiding) => _onHiding += onHiding;

        private void DoClose(Action onClose)
        {
            onClose?.Invoke();
            _onClose?.Invoke();
            Destroy(gameObject);
        }
    }
}