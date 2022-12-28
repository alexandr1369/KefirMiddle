using System;
using Model;
using UnityEngine;
using View;
using Zenject;

namespace Presenter
{
    public class UnitPresenter : IInitializable, IDisposable
    {
        private readonly IUnit _model;
        private readonly UnitView _view;

        public UnitPresenter(IUnit model, UnitView view)
        {
            _view = view;
            _model = model;
            
            Debug.Log("Player presenter");
        }

        void IInitializable.Initialize()
        {
            Debug.Log("[Player Presenter] Initializing!");
        }

        void IDisposable.Dispose()
        {
            Debug.Log("[Player Presenter] Disposing!");
        }
    }
}