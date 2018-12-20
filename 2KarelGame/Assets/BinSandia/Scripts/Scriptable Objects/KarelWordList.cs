using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewKarelWordList", menuName = "Sandia/Karel Word List")]
public class KarelWordList : ScriptableObject
{

    [Serializable]
    public struct Sentence
    {
        public string word;
        public Color color;
    }
    
    public List<Sentence> wordList;

    /*
     * bool allthelinecolor, etc
     */
}
