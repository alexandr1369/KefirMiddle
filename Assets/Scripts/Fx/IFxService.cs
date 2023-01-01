using UnityEngine;

namespace Fx
{
    public interface IFxService
    {
        void PlayAt(FxType type, Vector3 position = default, Vector3 scale = default);
    }
}