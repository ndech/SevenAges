using UnityEngine;

namespace Map.MapComponents
{
    abstract class ChunkComponent : MonoBehaviour
    {
        public abstract void Draw(HexCell[] cells);
    }
}
