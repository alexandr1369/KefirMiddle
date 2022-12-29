using Model;
using UnityEngine;
using View;

namespace Presenter
{
    public interface IUnitPresenter
    {
        IUnitModel Model { get; }
        IUnitView View { get; }
        Vector3 Position { get; }
        Vector3 Velocity { get; }
        Quaternion Rotation { get; }
        bool IsActive { set; }
        void TakeDamage(int damage);
    }
}