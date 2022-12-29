using System;
using Model;
using UnityEngine;
using View;
using Zenject;

namespace Presenter
{
    public class UnitUnitPresenter : IUnitPresenter, IInitializable, IDisposable
    {
        public Vector3 Position
        {
            get => View.Rb.position;
            set => View.Rb.position = value;
        }

        public Vector3 Velocity
        {
            get => View.Rb.velocity;
            set => View.Rb.velocity = value;
        }

        public Quaternion Rotation
        {
            get => View.Rb.rotation;
            set => View.Rb.rotation = value;
        }

        protected readonly IUnitModel UnitModel;
        protected readonly IUnitView View;

        public UnitUnitPresenter(IUnitModel unitModel, IUnitView view)
        {
            View = view;
            UnitModel = unitModel;
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