using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HexMapEditor : MonoBehaviour {

    public Color[] colors;

    public MapGrid grid;

    private Color activeColor;

    void Awake()
    {
        SelectColor(0);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            HandleInput();
        }
    }

    void HandleInput()
    {
        Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(inputRay, out hit))
        {
            grid.ColorCell(hit.point, activeColor);
        }
    }

    public void SelectColor(int ind)
    {
        activeColor = colors[ind];
    }
}
