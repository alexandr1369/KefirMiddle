using UnityEngine.SceneManagement;

namespace LoadingSystem.Loading.Operations.Home
{
    public class HomeSceneLoadingSequence : LoadingSequence
    {
        public HomeSceneLoadingSequence(HomeSceneLoadingContext context)
        {
            Add(new LoadSceneOperation("HomeScene", LoadSceneMode.Single));
            Add(new LoadSceneOperation("HudScene", LoadSceneMode.Additive));
            Add(new InitPlayer(context));
            Add(new InitEnemies(context));
            Add(new InitHomeMusicOperation(context));
        }
    }
}