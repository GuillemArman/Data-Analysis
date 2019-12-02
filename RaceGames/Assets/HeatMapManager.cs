using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatMapManager : MonoBehaviour
{
    private static HeatMapManager _instance;

    private Vector2 world_size;

    [Range(1, 100)]
    [SerializeField]
    private int rows;

    [Range(1, 100)]
    [SerializeField]
    private int columns;

    [Range(10, 100)]
    [SerializeField]
    float scale = 10;

    private Vector2Int map_size;


    private Vector2 cell_size;

    Gradient gradient;
    GradientColorKey[] colorKey;
    GradientAlphaKey[] alphaKey;

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
        dimensions = GameObject.Find("Terrain").GetComponent<Terrain>().terrainData.size ;

        world_size.x = dimensions.x;
        world_size.y = dimensions.z;

        if (rows != 0) map_size.x = rows;
        else map_size.x = 10;
        if (columns != 0) map_size.y = columns;
        else map_size.y = 10;



        cell_size.x = world_size.x / map_size.x;
        cell_size.y = world_size.y / map_size.y;

        float[,] heat_map_2D_array = new float[map_size.x, map_size.y];



        float highest_value = 0;

        for (int i = 0; i < positions.Count; i++)
        {
            Vector2 pos_2d = new Vector2(positions[i].pos.x, positions[i].pos.z);
            Vector2Int pos_in_grid = WorldToMap(pos_2d);
            float current_cell_value = heat_map_2D_array[pos_in_grid.x, pos_in_grid.y];

            heat_map_2D_array[pos_in_grid.x, pos_in_grid.y] = current_cell_value + 1;

            if (current_cell_value > highest_value) highest_value = current_cell_value;


        }

        for (int i = 0; i < map_size.x; i++)
        {
            for (int j = 0; j < map_size.y; j++)
            {
                float iterator_cell_value = heat_map_2D_array[i, j];
                if (iterator_cell_value > highest_value) highest_value = iterator_cell_value;
            }
        }


        for (int i = 0; i < map_size.x; i++)
        {
            for (int j = 0; j < map_size.y; j++)
            {
                float iterator_cell_value = heat_map_2D_array[i, j];
                float normalized_cell_value = 0;

                if (iterator_cell_value > 0) normalized_cell_value = (float)(iterator_cell_value / highest_value);

                heat_map_2D_array[i, j] = normalized_cell_value;
               
            }
        }

        for (int i = 0; i < map_size.x; i++)
        {
            for (int j = 0; j < map_size.y; j++)
            {


                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

                //Get the Renderer component from the new cube
                var cubeRenderer = cube.GetComponent<Renderer>();

                float iterator_cell_value = heat_map_2D_array[i, j];

                gradient = new Gradient();

                // Populate the color keys at the relative time 0 and 1 (0 and 100%)
                colorKey = new GradientColorKey[2];
                colorKey[0].color = Color.blue;
                colorKey[0].time = 0.0f;
                colorKey[1].color = Color.red;
                colorKey[1].time = 1.0f;

                alphaKey = new GradientAlphaKey[1];
                alphaKey[0].alpha = 1.0f;
                alphaKey[0].time = 0.0f;


                gradient.SetKeys(colorKey, alphaKey);

                // What's the color at the relative time 0.25 (25 %) ?


                cube.transform.position = new Vector3((cell_size.x * i) + cell_size.x/2, 0, (cell_size.y *j)+ cell_size.y / 2);
                cube.transform.localScale = new Vector3(cell_size.x, 1, cell_size.y);
                if (iterator_cell_value > 0)
                cube.transform.localScale = new Vector3(cell_size.x, scale * iterator_cell_value, cell_size.y);

                //Call SetColor using the shader property name "_Color" and setting the color to red
                cubeRenderer.material.SetColor("_Color", gradient.Evaluate(iterator_cell_value));
                //if (iterator_cell_value > 0)


            }
        }

        Debug.Log(cell_size.x);

        
        Debug.Log(cell_size.x);
    }

    Vector2Int WorldToMap(Vector2 world_coords)
    {
        Vector2Int map_coords = new Vector2Int();

        map_coords.x = (int)(world_coords.x / cell_size.x);
        map_coords.y = (int)(world_coords.y / cell_size.x);


        return map_coords;
    }
}