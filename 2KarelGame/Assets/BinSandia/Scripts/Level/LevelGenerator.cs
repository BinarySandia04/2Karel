﻿using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public Texture2D generatedMap;
    public ColorMapping[] mappings;
    [Space]
    public GameObject codeEditor;
    public static List<GameObject> generatedObjectsInLevel = new List<GameObject>();

    [System.Serializable]
    public class ColorMapping
    {
        public Color color;
        public GameObject[] prefabs;
    }

    public void GenerateLevel(Texture2D map)
    {
        generatedMap = map;
        // Clears the level
        while(generatedObjectsInLevel.Count > 0)
        {
            GameObject trash = generatedObjectsInLevel[0];
            generatedObjectsInLevel.Remove(trash);
            Destroy(trash);
        }

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
        Color pc = generatedMap.GetPixel(x, y);
        if (pc.a == 0) return; // Empty
        
        foreach(ColorMapping mapin in mappings)
        {
            
            if (mapin.color.Equals(pc))
            {
                Vector3 position = new Vector3(x * 3, 0, y * 3);
                foreach(GameObject prefab in mapin.prefabs)
                {
                    GameObject ins = Instantiate(prefab, position, Quaternion.identity, transform);
                    Position prop = ins.GetComponent<ObjectPropieties>().pos;
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

    public static GameObject getObjectCoord(int x, int y)
    {
        // Falta optimización
        foreach(GameObject g in generatedObjectsInLevel)
        {
            if(g.GetComponent<ObjectPropieties>().pos.xCoord == x && g.GetComponent<ObjectPropieties>().pos.yCoord == y)
            {
                return g;
            }
        }
        return null;
    }
}
