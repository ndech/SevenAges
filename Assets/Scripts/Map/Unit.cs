using UnityEngine;

namespace Map
{
    class Unit : MonoBehaviour
    {
        private HexCell _location;
        private float _orientation;

        public HexCell Location
        {
            get { return _location; }
            set
            {
                _location = value;
                Vector3 center = HexMetrics.Center(value.Coordinates);
                transform.localPosition = new Vector3(center.x, transform.localPosition.y, center.z);
                value.Unit = this;
            }
        }

        public float Orientation
        {
            get { return _orientation; }
            set
            {
                _orientation = value;
                transform.localRotation = Quaternion.Euler(0f, value, 0f);
            }
        }

        public void Kill()
        {
            _location.Unit = null;
            Destroy(gameObject);
        }
    }
}
