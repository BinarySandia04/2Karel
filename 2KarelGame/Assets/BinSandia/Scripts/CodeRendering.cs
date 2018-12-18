using System;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using System.Collections.Generic;

public class CodeRendering : MonoBehaviour
{

    // Structs

    [Serializable]
    public struct ColoredWord
    {
        public string word;
        public Color color;
    }

    // Public variables

    public List<ColoredWord> colorDictionary;
    private TMP_InputField input;
    private List<List<int>> coloredWordsInfo = new List<List<int>>(); // First int, pure word location start, second one, end

    // Useful functions

    string colorToHex(Color color)
    {
        return ColorUtility.ToHtmlStringRGB(color);
    }

    // Void Start and Update

    void Start() { }

    void Update()
    {
        if (Input.anyKey)
        {
            RenderTextColors();
            if (Input.GetKey(KeyCode.Backspace))
            {
                TryRemoveColor();
            }
        }
    }

    public void RenderNumberColor()
    {
        // WIP
    } // Ojala?

    public void RenderTextColors()
    {
        input = GetComponent<TMP_InputField>();
        string a = "color=#" +
            "", b = "/color";
        foreach(ColoredWord cw in colorDictionary)
        {
            // SearchforandAdd
            TryAddNewColors(cw.word, a + colorToHex(cw.color), b);
        }
        ReajustCaret();
    } // Implementar a inspector DONE

    private void TryRemoveColor()
    {

        try
        {
            if (input.text[input.stringPosition - 6] == '/')
            {
                input.text = input.text.Substring(0, input.stringPosition - 8) + input.text.Substring(input.stringPosition, input.text.Length - input.stringPosition);
            }
            if (input.text[input.stringPosition - 8] == '#')
            {
                input.text = input.text.Substring(0, input.stringPosition - 15) + input.text.Substring(input.stringPosition, input.text.Length - input.stringPosition);
            }
            if (input.text[input.stringPosition - 14] == '<')
            {
                input.text = input.text.Substring(0, input.stringPosition - 14) + input.text.Substring(input.stringPosition, input.text.Length - input.stringPosition);
            }
        }
        catch (Exception)
        {
        }
    } // Done

    private void ReajustCaret()
    {
        if (input.selectionAnchorPosition != input.selectionFocusPosition) return; // Soluciona un bug que al pasar por texto colorido la seleccion se bugueaba [BUG #1]
        try
        {
            int newCaretPos = 0;
            newCaretPos = input.stringPosition;
            if (input.text[input.stringPosition] == '>')
            {
                if (input.text[input.stringPosition - 1] == 'r')
                {
                    newCaretPos -= 8;
                }
                else
                {
                    newCaretPos -= 15;
                }
                input.stringPosition = newCaretPos;
                return;
            }
            if (input.text[input.stringPosition] == '<')
            {
                if (input.text[input.stringPosition + 1] == '/')
                {
                    newCaretPos += 8;
                }
                else
                {
                    newCaretPos += 15;
                }
                input.stringPosition = newCaretPos;
                return;
            }
        }
        catch (Exception)
        {
        }

    } // Done

    private void TryAddNewColors(string value, string before, string after)
    {
        string colored;
        MatchCollection match = Regex.Matches(input.text, value); // numero de values en la string
        foreach (Match m in match)
        {
            try
            {
                if (input.text[m.Index - 1] == '>')
                {
                    // Osea, si hay en este caso detras "<color=#noseqe> y delante </color>, esto se triggerea
                }
                else
                {
                    colored = input.text.Insert(m.Index, '<' + before + '>').Insert(m.Index + before.Length + 2 + value.Length, "<" + after + ">");
                    // This colors a word!

                    // Ara la palabra esta en m.Index + 13
                    // El final de la palabra esta en m.Index + 13 + value.Length
                    List<int> _add = new List<int>();
                    _add.Add(m.Index + 13);
                    _add.Add(m.Index + 13 + value.Length);
                    Debug.Log(m.Index + 13);
                    coloredWordsInfo.Add(_add);
                    foreach (List<int> l in coloredWordsInfo)
                    {
                        Debug.Log("First index: " + l[0]);
                        Debug.Log("Last word: " + l[1]);
                    }

                    input.text = colored;
                    input.stringPosition += before.Length + 2;
                }
            }
            catch (Exception)
            {
                colored = input.text.Insert(m.Index, '<' + before + '>').Insert(m.Index + before.Length + 2 + value.Length, "<" + after + ">");

                // Ara la palabra esta en m.Index + 13
                // El final de la palabra esta en m.Index + 13 + value.Length
                List<int> _add = new List<int>();
                _add.Add(m.Index + 13);
                _add.Add(m.Index + 13 + value.Length);
                coloredWordsInfo.Add(_add);

                input.text = colored;
                input.stringPosition += before.Length + 2;
            }
        }
    }  // TODO Much work, rompe esto en fracciones
}

/* // TODO: Hacer lo siguiente:
 *
 * - Detectar si lo que se esta editando esta dentro del string, i no en el final
 *   - Al ser así, al editar algo con color, borrar esta entrada del array de ints coloredWordsInfo
 *   - Actualizar todas las demas entradas a partir de esta editada con sus nuevas posiciones
 *   - Se evitara el lag, y todos seremos felices :D!
 * // Cuando hagas funcionar este desastre, optimizalo un poco. Hay muchos bucles. Ya sabes lo que quiero decir!
*/
