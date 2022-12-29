using UnityEngine;
using Zenject;

namespace Player
{
    public class PlayerMovement : ITickable
    {
        public void Tick()
        {
            PlayerInput.IsMovingLeft = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A);
            PlayerInput.IsMovingRight = Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D);
            PlayerInput.IsMovingUp = Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W);
            PlayerInput.IsMovingDown = Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S);

            PlayerInput.IsShootingBullets = Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0);
            PlayerInput.IsShootingLaser = Input.GetKey(KeyCode.X) || Input.GetMouseButton(1);
        }
    }
}