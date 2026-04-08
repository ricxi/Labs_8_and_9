using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RandomMap : MonoBehaviour
{
    [SerializeField] private int width;
    [SerializeField] private int depth;
    [SerializeField] private List<GameObject> prefabTilesList;
    [SerializeField] private Transform parentMap;
    [SerializeField] private Transform startPoint;
    [SerializeField] private GameObject[,] map;
    [SerializeField] private List<List<GameObject>> listMap = new List<List<GameObject>>();
    [SerializeField] private float perlinScale;

    private InputAction _rebuildPerlin;
    private float _xOffset;
    private float _zOffset;

    private void Awake()
    {
        _rebuildPerlin = InputSystem.actions.FindAction("Player/RebuildPerlin");
    }

    private void OnEnable()
    {
        _rebuildPerlin.performed += RebuildPerlin;
    }

    private void OnDisable()
    {
        _rebuildPerlin.performed -= RebuildPerlin;
    }

    private void Start()
    {
        map = new GameObject[width, depth];
        _xOffset = Random.Range(1000, 5000);
        _zOffset = Random.Range(-1000, -5000);
        BuildPerlinNoiseMap();
        //BuildRandomMap();
    }

    private void BuildRandomMap()
    {
        for (int row = 0; row < depth; row++)
        {
            List<GameObject> listRow = new List<GameObject>();
            for (int col = 0; col < width; col++)
            {
                if (row == 0 && col == 0) continue;

                Vector3 position = new Vector3(col * 50, 0f, row * 50);
                GameObject tile = Instantiate(prefabTilesList[Random.Range(0, prefabTilesList.Count)], position, Quaternion.identity, parentMap);
                listRow.Add(tile);
                map[col, row] = tile;
            }
            listMap.Add(listRow);
        }
    }

    private void RebuildPerlin(InputAction.CallbackContext _)
    {
        Debug.Log("M Key Pressed. Rebuiling perlin...");
        RebuildPerlinMap();
    }

    private void RebuildPerlinMap()
    {
        listMap.Clear();

        for (int row = 0; row < depth; row++)
        {
            for (int col = 0; col < width; col++)
                Destroy(map[col, row]);
        }

        BuildPerlinNoiseMap();
    }

    private void BuildPerlinNoiseMap()
    {
        for (int row = 0; row < depth; row++)
        {
            List<GameObject> listRow = new List<GameObject>();
            for (int col = 0; col < width; col++)
            {
                if (row == 0 && col == 0) continue; 
                float perlinNoiseValue = GetPerlinNoise(col, row);
                Vector3 pos = new Vector3(col * 50, 0f, row * 50);
                GameObject tile = Instantiate(GenerateTileOnPerlinNoise(perlinNoiseValue), pos, Quaternion.identity, parentMap);
                listRow.Add(tile);
                map[col, row] = tile;
            }
            listMap.Add(listRow);
        }
    }

    private float GetPerlinNoise(float x, float z)
    {
        float xCoord = (x + _xOffset) / (width * perlinScale);
        float zCoord = (z + _zOffset) / (depth * perlinScale);
        return Mathf.Clamp01(Mathf.PerlinNoise(xCoord, zCoord));
    }

    private GameObject GenerateTileOnPerlinNoise(float noiseValue)
    {
        switch (noiseValue)
        {
            case <= 0.2f: return prefabTilesList[0]; // Water
            case <= 0.4f: return prefabTilesList[1]; // Grass
            case <= 0.6f: return prefabTilesList[2]; // Road
            case <= 0.8f: return prefabTilesList[3]; // Ground
            case <= 1f:   return prefabTilesList[4]; // Lava
            default: return prefabTilesList[1]; 
        }
    }
}
