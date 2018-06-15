using Terrain;
using UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Map
{
    class HexMapEditor : MonoBehaviour
    {
        public HexGrid HexGrid;
        public Unit UnitPrefab;
        private TerrainType _activeTerrain;

        void Awake()
        {
            _activeTerrain = TerrainType.Ocean;
        }

        void Update()
        {
            if(EventSystem.current.IsPointerOverGameObject()) return;
            if (Input.GetMouseButton(0))
                GetActiveCell()?.SetTerrain(_activeTerrain);
            if (Input.GetKeyDown(KeyCode.U))
            {
                if (Input.GetKey(KeyCode.LeftShift))
                    GetActiveCell()?.Unit?.Kill();
                else
                    CreateUnit(GetActiveCell());
            }
        }

        private void CreateUnit(HexCell cell)
        {
            if(cell == null || cell.Unit != null) return;
            Unit unit = Instantiate(UnitPrefab, HexGrid.transform);
            unit.Location = cell;
            unit.Orientation = Random.Range(0f, 360f);
        }


        public HexCell GetActiveCell()
        {
            Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(inputRay, out hit))
                return HexGrid.GetCell(hit.point);
            return null;
        }

        public void SelectTerrain(TerrainTypeContainer container)
        {
            _activeTerrain = container.TerrainType;
        }
    }
}
