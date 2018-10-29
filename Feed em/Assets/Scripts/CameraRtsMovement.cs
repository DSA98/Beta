using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRtsMovement : MonoBehaviour {

    [SerializeField]
    private float panSpeed = 20f;
    [SerializeField]
    private float panBorderThickness = 10f;
    [SerializeField]
    private Vector2 panLimit;

    [SerializeField]
    private float scrollSpeed=20f;
    [SerializeField]
    private float minY = 20f;
    [SerializeField]
    private float maxY = 120f;

    Vector3 pos;

    private float scroll;

    // Use this for initialization
    void Awake () {
        scroll = Input.GetAxis("Mouse ScrollWheel");
    }
	
	// Update is called once per frame
	void Update () {
        pos = transform.position;

        if (Input.GetKey(KeyCode.W) || Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            pos.z += panSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S) || Input.mousePosition.y <= panBorderThickness)
        {
            pos.z -= panSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D) || Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            pos.x += panSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A) || Input.mousePosition.x <= panBorderThickness)
        {
            pos.x -= panSpeed * Time.deltaTime;
        }

        pos.y -= scroll * scrollSpeed * 20f * Time.deltaTime;

        pos.x = Mathf.Clamp(pos.x, -panLimit.x, panLimit.x);
        pos.y = Mathf.Clamp(pos.y, -minY, maxY);
        pos.z = Mathf.Clamp(pos.z, -panLimit.y, panLimit.y);

        transform.position = pos;
    }
}
