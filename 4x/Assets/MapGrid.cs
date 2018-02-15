using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MapGrid : MonoBehaviour {

    public int width = 10;
    public int height = 10;
    public float GapX = 0.0f;
    public float GapZ = 0.0f;

    //used for gap calculation
    private float tempGapX = 0;
    private float tempGapZ = 0;

    public Cell cellFab;

    Cell[] cells;

    public Text cellLabelFab;

    public Color defaultColor = Color.white;

    Canvas gridCanvas;
    HexMesh hexMesh;

    //grid must be created first
    void Awake()
    {
        gridCanvas = GetComponentInChildren<Canvas>();
        hexMesh = GetComponentInChildren<HexMesh>();

        cells = new Cell[width * height];

        CreateGrid();
    }

    // Use this for initialization
    void Start () {
        hexMesh.Triangulate(cells);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ColorCell (Vector3 pos, Color color)
    {
        pos = transform.InverseTransformPoint(pos);
        HexCoordinates coord = HexCoordinates.FromPosition(pos);
        //Debug.Log("touched at " + coord.ToString());
        //Debug.Log("coord x: " + coord.X + ", coord z: " + coord.Z + ", width: " + width + ", coord.z/2: " + coord.Z / 2);

        int index = coord.X + coord.Z * width + coord.Z / 2;

        //Debug.Log("coloring index " + index);
        if (index >= 0 && index < width * height)
        {
            Cell cell = cells[index];
            cell.color = color;
            hexMesh.Triangulate(cells);
        }
    }

    //spawns the map grid primitives
    void CreateGrid()
    {
        int n = 0;
        for (int z = 0; z < height; z++)
        {
            for (int x = 0; x < width; x++)
            {
                Vector3 pos;
                pos.x = (x + z * 0.5f - z / 2) * HexUtilities.innerRadius * 2f;
                pos.y = 0f;
                pos.z = z * HexUtilities.outerRadius * 1.5f;

                //add spacing
                pos.x += tempGapZ;
                pos.z += tempGapX;

                Cell cellInst = cells[n] = Instantiate<Cell>(cellFab);
                cellInst.transform.SetParent(transform, false);
                cellInst.transform.localPosition = pos;
                cellInst.coords = HexCoordinates.FromOffsetCoordinates(x, z);
                cellInst.color = defaultColor;

                Text label = Instantiate<Text>(cellLabelFab);
                label.rectTransform.SetParent(gridCanvas.transform, false);
                label.rectTransform.anchoredPosition = new Vector2(pos.x, pos.z);
                label.text = cellInst.coords.ToStringNewLine();

                Debug.Log("hex with coords " + cellInst.coords.ToString() + " is at index " + n);

                tempGapZ += GapZ;
                n++;
            }
            tempGapX += GapX;
            tempGapZ = 0;
        }
    }

    public Cell GetCell(int index)
    {
        return cells[index];
    }
}
