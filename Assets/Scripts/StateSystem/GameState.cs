using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace StateSystem
{
    [Serializable]
    public class GameState
    {
        [field: SerializeField] public UserState.UserState UserState { get; private set; }
        [field: SerializeField] public InventoryState InventoryState { get; private set; }
        [field: SerializeField] public List<AwaitingItemState> AwaitingItemStates { get; private set; }

        /// <summary>
        /// The very first game state initialization (is called per once after reset).
        /// </summary>
        public GameState()
        {
            UserState = new UserState.UserState();
            InventoryState = new InventoryState();
            AwaitingItemStates = new List<AwaitingItemState>();
        }
        
        public AwaitingItemState GetAwaitingItemStateForId(string id)
        {
            foreach (var state in AwaitingItemStates.Where(state => string.Equals(state.Id, id)))
                return state;
            
            var newAwaitingItemState = new AwaitingItemState(id);
            AwaitingItemStates.Add(newAwaitingItemState);
            
            return newAwaitingItemState;
        }
    }
}
