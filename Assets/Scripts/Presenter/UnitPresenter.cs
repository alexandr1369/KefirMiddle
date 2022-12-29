using System;
using Model;
using UnityEngine;
using View;
using Zenject;

namespace Presenter
{
    public class UnitPresenter : IPresenter, IInitializable, IDisposable
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

        protected readonly IModel Model;
        protected readonly UnitView View;

        public UnitPresenter(IModel model, UnitView view)
        {
            View = view;
            Model = model;
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

    public interface IPresenter
    {
        Vector3 Position { get; }
        Vector3 Velocity { get; }
        Quaternion Rotation { get; }
    }
}