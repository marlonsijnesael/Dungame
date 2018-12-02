
using UnityEngine;


//this script enables the user to manipulate the minimap camera by using the scrollwheel and left-shift.
//It changes the size of the orthographic camera in order to create a zoom effect
public class MiniMap : MonoBehaviour {

    public Transform player;
    public Camera cam;
    private void Start() {
        cam = GetComponent<Camera>();
        }

    private void LateUpdate() {
        float scroll = Input.mouseScrollDelta.y;
        if (Input.GetKey(KeyCode.LeftShift)) {
            if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
    {
                cam.orthographicSize++;
                } else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
                  {
                cam.orthographicSize--;
                }
            }
        }
    }