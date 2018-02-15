using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour {

    public float speed;

    public float height = 5.0f;
    private float minX, maxX, minZ, maxZ;

	// Use this for initialization
	void Start () {
        MapGrid map = GameObject.Find("Map Grid").GetComponent<MapGrid>();

        minX = map.GetCell(0).transform.position.x;
        minZ = map.GetCell(0).transform.position.z - height;

        //scale the camera's view limits to the map grid
        maxX = map.GetCell(map.width * map.height - 1).transform.position.x;
        maxZ = map.GetCell(map.width * map.height - 1).transform.position.z - height;

        Debug.Log("minX = " + minX);
        Debug.Log("minZ = " + minZ);

        Debug.Log("maxX = " + maxX);
        Debug.Log("maxZ = " + maxZ);
        
        transform.position = new Vector3(minX, map.GetCell(0).transform.position.y + height, minZ);
	}
	
	// Update is called once per frame
	void Update () {

        //handle arrow key input
        if (Input.GetKey(KeyCode.RightArrow) && transform.position.x < maxX)
        {
            transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
        }
        if (Input.GetKey(KeyCode.LeftArrow) && transform.position.x > minX)
        {
            transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0));
        }
        if (Input.GetKey(KeyCode.DownArrow) && transform.position.z > minZ)
        {
            transform.Translate(new Vector3(0, 0, -speed * Time.deltaTime), Space.World);
        }
        if (Input.GetKey(KeyCode.UpArrow) && transform.position.z < maxZ)
        {
            transform.Translate(new Vector3(0, 0, speed * Time.deltaTime), Space.World);
        }

	}

}
