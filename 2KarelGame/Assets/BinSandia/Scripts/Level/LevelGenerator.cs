using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public Texture2D map;
    public ColorMapping[] mappings;
    [Space]
    public GameObject codeEditor;
    public List<GameObject> generatedObjectsInLevel = new List<GameObject>();

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
                Vector3 position = new Vector3(x * 3, 0, y * 3);
                foreach(GameObject prefab in mapin.prefabs)
                {
                    GameObject ins = Instantiate(prefab, position, Quaternion.identity, transform);
                    ObjectPropieties prop = ins.GetComponent<ObjectPropieties>();
                    prop.xCoord = x;
                    prop.yCoord = y;
                    if (prefab.name == "Player")
                    {
                        codeEditor.GetComponent<CodeRendering>().player = ins;
                        // El player lleva un KarelPlayer puesto ya en el prefab,
                        // Y ahora assignamos al gameobject KarelPlayer que sea capaz de acceder a este script
                        // y usar la funcion getObjectCoord
                        ins.GetComponent<KarelPlayer>().levelGenerator = gameObject;
                    }
                    generatedObjectsInLevel.Add(ins);
                }
            }
        }
    }

    public GameObject getObjectCoord(int x, int y)
    {
        // Falta optimización
        foreach(GameObject g in generatedObjectsInLevel)
        {
            if(g.GetComponent<ObjectPropieties>().xCoord == x && g.GetComponent<ObjectPropieties>().yCoord == y)
            {
                return g;
            }
        }
        return null;
    }
}
