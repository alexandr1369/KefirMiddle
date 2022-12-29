using Location;
using UnityEngine;
using Utils;

namespace Enemy
{
    public class EnemiesManagerData
    {
        public static Vector3 GetRandomLocalScale(float settingsMinScale, float settingsMaxScale) => 
            Random.Range(settingsMinScale, settingsMaxScale).SetVector3();

        public static Vector3 GetRandomEnemyPosition(float scale, ISceneBoundsService service)
        {
            var side = (Side)Random.Range(0, (int)Side.Count);
            var rand = Random.Range(0.0f, 1.0f);

            return side switch
            {
                Side.Bottom => new Vector3(service.Left + rand * service.Width, service.Bottom - scale, 0),
                Side.Right => new Vector3(service.Right + scale, service.Bottom + rand * service.Height, 0),
                Side.Left => new Vector3(service.Left - scale, service.Bottom + rand * service.Height, 0),
                _ => new Vector3(service.Left + rand * service.Width, service.Top + scale, 0)
            };
        }

        public static Vector3 GetRandomEnemyVelocity()
        {
            var theta = Random.Range(0, Mathf.PI * 2.0f);
            
            return new Vector3(Mathf.Cos(theta), Mathf.Sin(theta), 0);
        }

        private enum Side
        {
            Top,
            Bottom,
            Left,
            Right,
            Count
        }
    }
}