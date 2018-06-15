using UnityEngine;

namespace Map
{
    public static class HexMetrics
    {
        public const float Scale = 10f;
        public const float BlendingFactor = 0.25f;
        public const float SolidFactor = 1 - BlendingFactor;
        public const float OuterRadius = Scale;
        public const float InnerRadius = Scale * 0.866025404f;
        public const int ChunkSizeX = 5;
        public const int ChunkSizeZ = 5;

        public static Vector3[] Vertices = {
            new Vector3(0f, 0f, OuterRadius), 
            new Vector3(InnerRadius, 0f, 0.5f * OuterRadius), 
            new Vector3(InnerRadius, 0f, -0.5f * OuterRadius),
            new Vector3(0f, 0f, -OuterRadius), 
            new Vector3(-InnerRadius, 0f, -0.5f * OuterRadius), 
            new Vector3(-InnerRadius, 0f, 0.5f * OuterRadius),
        };

        public static Vector3 Center(HexCoordinates coordinates)
        {
            return new Vector3(InnerRadius * (2 * coordinates.X + coordinates.Z) , 0f, 1.5f * OuterRadius * coordinates.Z);
        }

        public static Vector3 FirstVertex(HexCoordinates coordinates, HexDirection direction)
        {
            return Center(coordinates) + Vertices[(int)direction % Vertices.Length] * SolidFactor;
        }

        public static Vector3 SecondVertex(HexCoordinates coordinates, HexDirection direction)
        {
            return Center(coordinates) + Vertices[((int)direction + 1) % Vertices.Length] * SolidFactor;
        }
    }

    public enum HexDirection
    {
        NE, E, SE, SW, W, NW
    }

    public static class HexDirectionExtensions
    {
        public static HexDirection Opposite(this HexDirection direction)
        {
            return (HexDirection) (((int) direction + 3) % 6);
        }
    }
}
