namespace DialogueSystem
{
    public interface IDialoguesService
    {
        T OpenDialogue<T>(DialogueOpenMode mode = DialogueOpenMode.Enqueue) where T : Dialogue;
        void CloseAllDialogues();
        bool IsOpenDialog();
        bool IsDialogTypeQueued<T>() where T : Dialogue;
    }
}