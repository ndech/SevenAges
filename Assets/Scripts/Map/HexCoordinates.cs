using System;
using UnityEngine;

namespace Map
{
    [Serializable]
    public class HexCoordinates
    {
        [SerializeField]
        private int _x, _z;

        public int X => _x;
        public int Y => -X - Z;
        public int Z => _z;

        public HexCoordinates(int x, int z)
        {
            _x = x;
            _z = z;
        }

        public override string ToString() =>
            $"X: {X}, Y: {Y}, Z: {Z}";

        public string ToStringOnSeparateLines() =>
            $"X: {X}{Environment.NewLine}Y: {Y}{Environment.NewLine}Z: {Z}";

        public static HexCoordinates FromOffset(int x, int z)
        {
            return new HexCoordinates(x - (z / 2), z);
        }

        public static HexCoordinates FromPosition(Vector3 position)
        {
            float z = position.z / (HexMetrics.OuterRadius * 1.5f);
            float x = position.x / (HexMetrics.InnerRadius * 2f) - z/2;
            float y = -x - z;
            int iX = Mathf.RoundToInt(x);
            int iY = Mathf.RoundToInt(y);
            int iZ = Mathf.RoundToInt(z);
            if (iX + iY + iZ != 0)
            {
                float dX = Mathf.Abs(x - iX);
                float dY = Mathf.Abs(y - iY);
                float dZ = Mathf.Abs(-x - y - iZ);
                if (dX > dY && dX > dZ)
                    iX = -iY - iZ;
                else if (dZ > dY)
                    iZ = -iX - iY;
            }
            return new HexCoordinates(iX, iZ);
        }
    }
}