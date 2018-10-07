using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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