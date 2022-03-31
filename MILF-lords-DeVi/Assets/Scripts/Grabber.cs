using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deck
{
    public class Grabber : MonoBehaviour
    {
        private GameObject selectedObject;
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if(selectedObject == null)
                {
                    RaycastHit hit = CastRay();

                    if(hit.collider != null)
                    {
                        if (!hit.collider.CompareTag("Drag"))
                        {
                            return;
                        }

                        selectedObject = hit.collider.gameObject;
                        Cursor.visible = false;
                    }
                }
                else
                {
                    Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(selectedObject.transform.position).z);
                    Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);
                    selectedObject.transform.position = new Vector3(worldPosition.x, 0f, worldPosition.z);

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
