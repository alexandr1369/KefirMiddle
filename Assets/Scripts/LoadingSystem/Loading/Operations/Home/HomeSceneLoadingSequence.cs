using UnityEngine.SceneManagement;

namespace LoadingSystem.Loading.Operations.Home
{
    public class HomeSceneLoadingSequence : LoadingSequence
    {
        public HomeSceneLoadingSequence(HomeSceneLoadingContext context)
        {
            Add(new LoadSceneOperation("HomeScene", LoadSceneMode.Single));
            Add(new LoadSceneOperation("HudScene", LoadSceneMode.Additive));
            Add(new InitPlayerOperation(context));
            Add(new InitEnemiesOperation(context));
            Add(new InitHomeMusicOperation(context));
        }
    }
}