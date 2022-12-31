using DialogueSystem;
using LoadingSystem.Loading.Operations.Home;
using UI.Buttons;
using UnityEngine;
using Zenject;

namespace UI.Dialogues
{
    public class DefeatDialogue : Dialogue
    {
        [field: SerializeField] private CustomButton ContinueButton { get; set; }
        
        private HomeSceneLoadingContext _context;

        [Inject]
        private void Construct(HomeSceneLoadingContext context) => _context = context;

        protected override void Awake()
        {
            base.Awake();
            
            ContinueButton.OnClick += OnContinueButtonClick;
        }

        private void OnContinueButtonClick()
        {
            Close();
            _context.SceneLoadingService.RestartCurrentLocation();
        }
    }
}