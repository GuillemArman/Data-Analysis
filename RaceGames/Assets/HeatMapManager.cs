using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatMapManager : MonoBehaviour
{
    private static HeatMapManager _instance;

    private Vector2 world_size;
    private Vector2Int map_size;
    private Vector2 cell_size;

    public static HeatMapManager Instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public void CreateHeatMap(List<EventManager.EventPosition> positions)
    {
        Vector3 dimensions;
        dimensions = GameObject.Find("Terrain").GetComponent<Terrain>().terrainData.size;

        world_size.x = dimensions.x;
        world_size.y = dimensions.y;

        map_size.x = 50;
        map_size.y = 60;

        cell_size.x = world_size.x / map_size.x;
        cell_size.y = world_size.y / map_size.y;

        double[,] heat_map_2D_array = new double[map_size.x, map_size.y];



        for (int i = 0; i < positions.Count; i++)
        {
            Vector2 pos_2d = new Vector2(positions[i].pos.x, positions[i].pos.z);

            Vector2Int pos_in_grid = WorldToMap(pos_2d);
        }

        
    }

    Vector2Int WorldToMap(Vector2 world_coords)
    {
        Vector2Int map_coords = new Vector2Int();

        map_coords.x = (int)(world_coords.x / cell_size.x);
        map_coords.y = (int)(world_coords.y / cell_size.x);


        return map_coords;
    } 
}