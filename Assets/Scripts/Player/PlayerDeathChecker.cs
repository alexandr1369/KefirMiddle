using Player.Movement;

namespace Player
{
    public class PlayerDeathChecker
    {
        private readonly IPlayerMovement _movement; 
        
        private PlayerDeathChecker(Player player, IPlayerMovement movement)
        {
            _movement = movement;
            player.Presenter.OnDestroyed += OnDestroyed;
        }

        private void OnDestroyed()
        {
            _movement.SetMoveBehaviour(new PlayerDestroyedMoveBehaviour());
            
            // TODO: restart game with ISceneLoader
        }
    }
}