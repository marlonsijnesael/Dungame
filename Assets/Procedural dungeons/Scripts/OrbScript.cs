using UnityEngine;

/// <summary>
/// simple script to make an object move along a sinewave
/// </summary>
public class OrbScript : MonoBehaviour {

    public float moveSpeed;
    public float startPosition;
    public Transform center;

    private void Update() {
        transform.position = new Vector3(transform.position.x,  Mathf.Sin(center.position.y + (Time.time *moveSpeed)), transform.position.z);
        }
    }
