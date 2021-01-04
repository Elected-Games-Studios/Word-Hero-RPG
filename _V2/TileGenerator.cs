using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGenerator : MonoBehaviour
{
    private List<int[]> AllHeros;
    //private List<GameObject> HeroButtons = new List<GameObject>();
    [SerializeField]
    private GameObject tile;
    private List<Vector3> positions = new List<Vector3>();

    private void Awake()
    {

        positions.Add(new Vector3(-350, 504, 0));
        positions.Add(new Vector3(0, 504, 0));
        positions.Add(new Vector3(350, 504, 0));
        positions.Add(new Vector3(-350, 100, 0));
        positions.Add(new Vector3(0, 100, 0));
        positions.Add(new Vector3(350, 100, 0));
        positions.Add(new Vector3(-350, -300, 0));
        positions.Add(new Vector3(0, -300, 0));
        positions.Add(new Vector3(350, -300, 0));

        CanvasDisplayManager.GenerateHeroTiles += GenerateTiles;
    }
    private void GenerateTiles()
    {
        AllHeros = CharectorStats.UnlockedCharectors();

       for (int i=0; i<AllHeros.Count; i++)
        {
            Instantiate(tile, positions[i], Quaternion.identity);
        }
    }


    private void OnDisable()
    {
        AllHeros.Clear();
        CanvasDisplayManager.GenerateHeroTiles -= GenerateTiles;
    }
}
