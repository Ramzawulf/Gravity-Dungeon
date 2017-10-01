using UnityEngine;

namespace Assets.Scripts.V1{
public class Platform : MonoBehaviour {

    public GameObject tilePrefab;
    private GameObject[] tileList;

    public int width;
    public int height;

    public void Awake () {
        InitTiles();
	}

    private void InitTiles()
    {
		/* 
		 * z = horizontal
		 * x = vertical
		*/

		int h = Mathf.FloorToInt(width*0.5f);
		int v = Mathf.FloorToInt(height*0.5f);
		int index = 0;

        for (int x = 0; x < height; x++)
        {
            for (int z = 0; z < width; z++)
            {
                GameObject go = Instantiate(tilePrefab);
				go.transform.SetParent (transform);
				go.transform.position =	transform.position	+ new Vector3 (x-v ,0,z-h);
				go.name = (x+z).ToString();
				index++;
            }
        }
    }

    void Update () {
		
	}
}
}