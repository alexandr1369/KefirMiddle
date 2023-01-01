using UnityEngine;

namespace Fx
{
    public interface IFx
    {
        void PlayFx(FxType type, Vector3 position, Vector3 scale, float duration);
    }
}