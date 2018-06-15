using UnityEngine;

namespace Map
{
    class MapCamera : MonoBehaviour
    {
        public Transform Stick;
        public Transform Swivel;
        public float MinZoom = 50;
        public float MaxZoom = 250;
        public float MoveSpeed = 100;
        private float _zoom = 1f;

        void Awake()
        {
            AdjustZoom(0);
        }

        void Update()
        {
            float zoomDelta = Input.GetAxis("Mouse ScrollWheel");
            if (zoomDelta != 0f)
                AdjustZoom(zoomDelta);
            float xDelta = Input.GetAxis("Horizontal");
            float zDelta = Input.GetAxis("Vertical");
            if (xDelta != 0f || zDelta != 0f)
            {
                AdjustPosition(xDelta, zDelta);
            }
        }

        void AdjustZoom(float delta)
        {
            _zoom = Mathf.Clamp01(_zoom - delta);

            float distance = Mathf.Lerp(MinZoom, MaxZoom, _zoom);
            Stick.localPosition = new Vector3(0f, 0f, -distance);
        }
        void AdjustPosition(float xDelta, float zDelta)
        {
            float distance = MoveSpeed * Time.deltaTime;
            Vector3 position = transform.localPosition;
            float damping = Mathf.Max(Mathf.Abs(xDelta), Mathf.Abs(zDelta));
            position += new Vector3(xDelta, 0f, zDelta).normalized * damping * distance;
            transform.localPosition = position;
        }
    }
}
