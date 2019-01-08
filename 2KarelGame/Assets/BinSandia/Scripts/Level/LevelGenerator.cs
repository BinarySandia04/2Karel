using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public Texture2D map;
    public ColorMapping[] mappings;

    [System.Serializable]
    public class ColorMapping
    {
        public Color color;
        public GameObject[] prefabs;
    }

    void Start()
    {
        GenerateLevel();
    }

    void GenerateLevel()
    {
        for(int x = 0; x < map.width; x++)
        {
            for(int y = 0; y < map.height; y++)
            {
                GenerateTile(x, y);
            }
        }
    }

    void GenerateTile(int x, int y)
    {
        Color pc = map.GetPixel(x, y);
        if (pc.a == 0) return; // Empty
        
        foreach(ColorMapping mapin in mappings)
        {
            
            if (mapin.color.Equals(pc))
            {
                Debug.Log("Pixel");
                Vector3 position = new Vector3(x * 3, 0, y * 3);
                foreach(GameObject prefab in mapin.prefabs)
                {
                    Instantiate(prefab, position, Quaternion.identity, transform);
                }
            }
        }
    }
}
