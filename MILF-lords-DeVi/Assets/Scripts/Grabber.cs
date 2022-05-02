using UnityEngine;

namespace Deck
{
    public class Grabber : MonoBehaviour
    {

        [SerializeField]
        private Camera gameCamera;

        private GameObject selectedObject;
        private Vector3 initialPosition;
        private Quaternion initialRotation;
        private int player;
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
                        if (selectedObject.transform.position.z < 0) player = 1;
                        else player = 2;
                        Cursor.visible = false;
                    }
                }

            }

            if (Input.GetMouseButtonUp(0))
            {
                if (selectedObject != null)
                {
                    Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, gameCamera.WorldToScreenPoint(selectedObject.transform.position).z);
                    Vector3 worldPosition = gameCamera.ScreenToWorldPoint(position);
                    if (player==1 && worldPosition.x>-10 && worldPosition.x < 10 && worldPosition.z <=0 && worldPosition.z > -10)
                    {
                        selectedObject.transform.position = new Vector3(worldPosition.x, 0f, worldPosition.z);
                    }
                    else if (player == 2 && worldPosition.x > -10 && worldPosition.x < 10 && worldPosition.z >= 0 && worldPosition.z < 10)
                    {
                        selectedObject.transform.position = new Vector3(worldPosition.x, 0f, worldPosition.z);
                        selectedObject.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
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
                Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, gameCamera.WorldToScreenPoint(selectedObject.transform.position).z);
                Vector3 worldPosition = gameCamera.ScreenToWorldPoint(position);
                selectedObject.transform.position = new Vector3(worldPosition.x, 1f, worldPosition.z);
            }
        }

        private RaycastHit CastRay()
        {
            Vector3 screenMousePosFar = new Vector3(Input.mousePosition.x, Input.mousePosition.y, gameCamera.farClipPlane);
            Vector3 screenMousePosNear = new Vector3(Input.mousePosition.x, Input.mousePosition.y, gameCamera.nearClipPlane);

            Vector3 worldMousePosFar = gameCamera.ScreenToWorldPoint(screenMousePosFar);
            Vector3 worldMousePosNear = gameCamera.ScreenToWorldPoint(screenMousePosNear);
            RaycastHit hit;
            Physics.Raycast(worldMousePosNear, worldMousePosFar - worldMousePosNear, out hit);
            return hit;
        }
    }
}
