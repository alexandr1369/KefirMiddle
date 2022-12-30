using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace DialogueSystem
{
    [RequireComponent(typeof(Canvas))]
    public class DialoguesService : MonoBehaviour
    {
        [field: SerializeField] private DialogueBackground BackgroundPrefab { get; set; }
        
        private readonly List<Dialogue> _dialogsQueue = new();
        private DiContainer _diContainer;
        // private Dialogues _dialogues;
        private DialogueBackground _dialogueBackground;
        private float _secondsPastSinceLastDialogClosing = DialogueBackground.FADE_TIME;
        private bool _isFading;

        // [Inject]
        // private void Construct(DiContainer diContainer, Dialogues dialogues)
        // {
        //     _diContainer = diContainer;
        //     _dialogues = dialogues;
        // }

        private void Update()
        {
            if (!_isFading)
                return;

            _secondsPastSinceLastDialogClosing += Time.deltaTime;
            
            if(_secondsPastSinceLastDialogClosing - DialogueBackground.FADE_TIME >= 0) 
                _isFading = false;
        }

        // public T OpenDialog<T>(DialogueOpenMode mode = DialogueOpenMode.Enqueue) where T : Dialogue
        // {
        //     var dialog = _diContainer.InstantiatePrefabForComponent<T>(_dialogues.GetAsset<T>(), transform);
        //     dialog.SetOnCloseAction(() => OnClosed(dialog));
        //
        //     if (IsQueueEmpty())
        //     {
        //         _dialogsQueue.Add(dialog);
        //         OpenPreviousDialog();
        //         
        //         Debug.Log("[Dialogues Service] Queue is empty!");
        //     }
        //     else
        //     {
        //         switch (mode)
        //         {
        //             case DialogueOpenMode.Enqueue:
        //                 _dialogsQueue.Add(dialog);
        //
        //                 break;
        //             case DialogueOpenMode.PushKeepPrevious:
        //                 _dialogsQueue.Insert(0, dialog);
        //                 OpenPreviousDialog();
        //                 _dialogueBackground.transform.SetSiblingIndex(transform.childCount - 2);
        //
        //                 break;
        //             case DialogueOpenMode.PushHidePrevious:
        //                 if (_dialogsQueue.Count > 0)
        //                     _dialogsQueue[0].Hide(() => { });
        //
        //                 _dialogsQueue.Insert(0, dialog);
        //                 OpenPreviousDialog();
        //
        //                 break;
        //             case DialogueOpenMode.PushClosePrevious:
        //                 if (_dialogsQueue.Count > 0)
        //                     _dialogsQueue[0].Close();
        //
        //                 _dialogsQueue.Insert(0, dialog);
        //
        //                 break;
        //         }
        //     }
        //     
        //     return dialog;
        // }

        public void CloseAllDialogs()
        {
            if (IsQueueEmpty())
                return;

            foreach (var dialog in _dialogsQueue.FindAll(t => !t.gameObject.activeSelf).ToList())
            {
                _dialogsQueue.Remove(dialog);
                Destroy(dialog.gameObject);
            }

            foreach (var dialog in _dialogsQueue.ToList())
            {
                _dialogsQueue.Remove(dialog);
                dialog.Close();
            }
        }

        private void OpenPreviousDialog()
        {
            if (IsQueueEmpty())
            {
                _dialogueBackground.Hide(() => Destroy(_dialogueBackground.gameObject));
                return;
            }
            
            if (!_dialogueBackground) 
                _dialogueBackground = Instantiate(BackgroundPrefab, transform);

            _dialogueBackground.Show();
            
            var dialog = _dialogsQueue[0];
            dialog.transform.SetSiblingIndex(transform.childCount - 1);
            dialog.Show();
        }

        public bool IsOpenDialog() => _dialogsQueue.Count > 0;

        public bool IsDialogTypeQueued<T>() where T : Dialogue => 
            _dialogsQueue.Find(t => t.GetType() == typeof(T));

        private bool IsQueueEmpty() => _dialogsQueue.Count <= 0;

        private void OnClosed(Dialogue dialogue)
        {
            _isFading = true;
            _secondsPastSinceLastDialogClosing = 0;
            
            _dialogsQueue.Remove(dialogue);
            OpenPreviousDialog();
        }
    }
}