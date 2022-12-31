using System;
using Player;
using UnityEngine;

namespace Movement
{
    public interface ICoreMovement
    {
        Core Core { get; }
        Settings MovementSettings { get; }
        void SetMoveBehaviour(IMovable moveBehaviour);
        
        [Serializable]
        public class Settings
        {
            [field: SerializeField] public float MovementSpeed { get; private set; }   
        }
    }
}