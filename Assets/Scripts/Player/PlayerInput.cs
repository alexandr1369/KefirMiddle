using UnityEngine;

namespace Player
{
    public static class PlayerInput
    {
        public static bool IsMovingLeft => Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A);
        public static bool IsMovingRight => Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D);
        public static bool IsMovingUp => Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W);
        public static bool IsMovingDown => Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S);
        
        public static bool IsShootingBullets => Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0);
        public static bool IsShootingExtra => Input.GetKey(KeyCode.X) || Input.GetMouseButton(1);
    }
}