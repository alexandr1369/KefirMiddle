using System;
using UnityEngine;

namespace DialogueSystem
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(CanvasGroup))]
    public class DialogueAnimationController : MonoBehaviour
    {
        private event Action OnCloseAnimationCompleted;

        [field: SerializeField] private RuntimeAnimatorController OpenAnimatorController { get; set;}
        [field: SerializeField] private RuntimeAnimatorController CloseAnimatorController { get; set; }

        private Animator _animator;

        private void Awake() => _animator = GetComponent<Animator>();

        public void PlayOpeningAnimation()
        {
            if (_animator == null || OpenAnimatorController == null)
                return;
            
            _animator.runtimeAnimatorController = OpenAnimatorController;
        }

        public void PlayClosingAnimation(Action animationCompleted)
        {
            if (_animator == null || CloseAnimatorController == null)
            {
                animationCompleted?.Invoke();
                return;
            }

            _animator.runtimeAnimatorController = CloseAnimatorController;
            OnCloseAnimationCompleted += animationCompleted;
        }

        /// <summary>
        /// Animation timeline event.
        /// </summary>
        private void OnDisappearAnimationCompleted()
        {
            OnCloseAnimationCompleted?.Invoke();
            OnCloseAnimationCompleted = null;
        }
    }
}