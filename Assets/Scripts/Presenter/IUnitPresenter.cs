using UnityEngine;

namespace Presenter
{
    public interface IUnitPresenter
    {
        Vector3 Position { get; }
        Vector3 Velocity { get; }
        Quaternion Rotation { get; }
    }
}