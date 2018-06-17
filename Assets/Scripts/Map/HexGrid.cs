using Map.CellFeatures;
using UnityEngine;
using UnityEngine.UI;

namespace Map
{
    class HexGrid : MonoBehaviour
    {
        public int ChunkCountX = 5;
        public int ChunkCountZ = 5;
        public HexCell CellPrefab;
        public HexGridChunk ChunkPrefab;
        public Text TextPrefab;
        private HexGridChunk[] _chunks;
        private HexCell[] _cells;
        private int CellCountX => ChunkCountX * HexMetrics.ChunkSizeX;
        private int CellCountZ => ChunkCountZ * HexMetrics.ChunkSizeZ;

        void Awake()
        {
            CreateChunks();
            CreateCells();
        }

        void Start()
        {
            foreach (var chunk in _chunks)
                chunk.Refresh();
        }

        private void CreateChunks()
        {
            _chunks = new HexGridChunk[ChunkCountX * ChunkCountZ];
            for (int z = 0, i = 0; z < ChunkCountZ; z++)
            for (int x = 0; x < ChunkCountX; x++)
                _chunks[i++] = Instantiate(ChunkPrefab, transform);
        }

        private void CreateCells()
        {
            _cells = new HexCell[CellCountX * CellCountZ];
            for (int z = 0, i = 0; z < CellCountZ; z++)
            for (int x = 0; x < CellCountX; x++)
                _cells[i] = CreateCell(x, z, i++);
        }

        private HexCell CreateCell(int x, int z, int i)
        {
            HexCoordinates coordinates = HexCoordinates.FromOffset(x, z);
            Vector3 center = HexMetrics.Center(coordinates);
            HexCell cell = Instantiate(CellPrefab, center, Quaternion.identity, transform);
            cell.Initialize(coordinates, TerrainType.Ocean);
            AssignToChunk(x, z, cell);
            SetNeighbours(x, z, i, cell);
            return cell;
        }

        private void AssignToChunk(int x, int z, HexCell cell)
        {
            int chunkX = x / HexMetrics.ChunkSizeX;
            int chunkZ = z / HexMetrics.ChunkSizeZ;
            HexGridChunk chunk = _chunks[chunkX + chunkZ * ChunkCountX];

            int localX = x - chunkX * HexMetrics.ChunkSizeX;
            int localZ = z - chunkZ * HexMetrics.ChunkSizeZ;
            chunk.RegisterCell(cell, localX + localZ * HexMetrics.ChunkSizeX, TextPrefab);
        }

        private void SetNeighbours(int x, int z, int i, HexCell cell)
        {
            if (x > 0)
                cell.SetNeighbour(_cells[i - 1], HexDirection.W);
            if (z == 0) return;
            if (z % 2 == 0)
            {
                cell.SetNeighbour(_cells[i - CellCountX], HexDirection.SE);
                if(x > 0)
                    cell.SetNeighbour(_cells[i - CellCountX - 1], HexDirection.SW);
            }
            else
            {
                cell.SetNeighbour(_cells[i - CellCountX], HexDirection.SW);
                if(x < CellCountX - 1)
                    cell.SetNeighbour(_cells[i - CellCountX + 1], HexDirection.SE);
            }
        }

        public HexCell GetCell(Vector3 position)
        {
            position = transform.InverseTransformPoint(position);
            HexCoordinates coordinates = HexCoordinates.FromPosition(position);
            return _cells[(coordinates.X + coordinates.Z / 2) + coordinates.Z * CellCountX];
        }
    }
}
