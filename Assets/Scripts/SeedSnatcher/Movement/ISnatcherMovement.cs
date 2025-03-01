using UnityEngine;

namespace SeedSnatcher.Movement
{
    public interface ISnatcherMovement
    {
        public void Init(Vector3 startPos, Vector3 endPos);
        public void Loop();
    }
}