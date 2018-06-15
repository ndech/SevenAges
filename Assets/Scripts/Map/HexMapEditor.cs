using UnityEngine;
using UnityEngine.EventSystems;

namespace Map
{
    class HexMapEditor : MonoBehaviour
    {
        public Color[] Colors;
        public HexGrid HexGrid;
        public Unit UnitPrefab;
        private Color _activeColor;

        void Awake()
        {
            SelectColor(0);
        }

        void Update()
        {
            if(EventSystem.current.IsPointerOverGameObject()) return;
            if (Input.GetMouseButton(0))
                GetActiveCell()?.SetColor(_activeColor);
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

        public void SelectColor(int index)
        {
            _activeColor = Colors[index];
        }
    }
}
