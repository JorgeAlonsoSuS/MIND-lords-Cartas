using UnityEngine;

namespace Deck
{
    public class Grabber : MonoBehaviour
    {
        private GameObject selectedObject;
        private Vector3 initialPosition;
        private Quaternion initialRotation;

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (selectedObject == null)
                {
                    RaycastHit hit = CastRay();

                    if (hit.collider != null)
                    {
                        if (!hit.collider.CompareTag("Drag"))
                        {
                            return;
                        }

                        selectedObject = hit.collider.gameObject;
                        initialPosition = selectedObject.transform.position;
                        initialRotation = selectedObject.transform.rotation;

                        Cursor.visible = false;
                    }
                }

            }

            if (Input.GetMouseButtonUp(0))
            {
                if (selectedObject != null)
                {
                    Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(selectedObject.transform.position).z);
                    Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);
                    if (worldPosition.x>-10&& worldPosition.x < 10 && worldPosition.z <=0)
                    {
                        selectedObject.transform.position = new Vector3(worldPosition.x, 0f, worldPosition.z);
                    }
                    else
                    {
                        selectedObject.transform.position = initialPosition;
                        selectedObject.transform.rotation = initialRotation;
                    }

                    selectedObject = null;
                    Cursor.visible = true;
                }
            }

            if (selectedObject != null)
            {
                Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(selectedObject.transform.position).z);
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);
                selectedObject.transform.position = new Vector3(worldPosition.x, 0.5f, worldPosition.z);
            }
        }

        private RaycastHit CastRay()
        {
            Vector3 screenMousePosFar = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.farClipPlane);
            Vector3 screenMousePosNear = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane);

            Vector3 worldMousePosFar = Camera.main.ScreenToWorldPoint(screenMousePosFar);
            Vector3 worldMousePosNear = Camera.main.ScreenToWorldPoint(screenMousePosNear);
            RaycastHit hit;
            Physics.Raycast(worldMousePosNear, worldMousePosFar - worldMousePosNear, out hit);

            return hit;
        }
    }
}
