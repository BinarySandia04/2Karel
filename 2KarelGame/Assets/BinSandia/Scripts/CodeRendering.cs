using System;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class CodeRendering : MonoBehaviour
{
    // Public variables

    public KarelWordList WordList;
    private TMP_InputField input;
    private List<List<int>> coloredWordsInfo = new List<List<int>>(); // First int, PURE WORD location start, second one, end

    // Useful functions

    string colorToHex(Color color)
    {
        return ColorUtility.ToHtmlStringRGB(color);
    }
    
    bool isBetweenOf(int n, int a, int b)
    {
        if (n >= a && n <= b) return true;
        else return false;
    }

    void refreshColorInfo(int offset)
    {
        if (offset < 0)
        {
            // Vaciar
            coloredWordsInfo = new List<List<int>>();
            return;
        }
        int colors = 0;
        for (int i = offset; i < input.text.Length - 1; i++)
        {
            if(input.text[i] == '>')
            {
                List<int> add = new List<int>();
                add.Add(i + 1); // Start index
                int s = 0;
                while (input.text[i + s] != '<')
                {
                    s++;
                    if(s > 100) // ehhhh, ok?
                    {
                        Debug.LogError("Ehhh bucle infinito aqui free");
                        return;
                    }
                }
                add.Add(i + s);
                coloredWordsInfo[colors] = add;
                colors++;
                i += s + 7;
            }
        }
        // Vaciar sobrantes (optimisation)
        for(int i = colors; i + 1 <= coloredWordsInfo.Count; /*Nothing*/)
        {
            coloredWordsInfo.RemoveAt(i);
        }
    }

    int WhereIsCaret(int modifier)
    {
        int cp = input.stringPosition + modifier; // Gets the REAL caret position
        int p = 0;
        
        foreach (List<int> list in coloredWordsInfo)
        {
            if (isBetweenOf(cp, (int) list.ElementAt(0), (int)list.ElementAt(1)))
            {
                return p; // Esta aqui! Qual numero es...
                
            } else
            {
                p++; // No esta aqui, vamos a seguir buscando...
            }
        }
        return -1; // Error no esta en ninguna
    }

    int FirstPureWord(int where)
    {
        // If the caret isn't placed in a color, return -1.
        if (where == -1) return where;
        return coloredWordsInfo.ElementAt(where).ElementAt(0);
    }

    int LastPureWord(int where)
    {
        // If the caret isn't placed in a color, return -1.
        if (where == -1) return where;
        return coloredWordsInfo.ElementAt(where).ElementAt(1);
    }

    // Void Start and Update

    void Start() {
        input = GetComponent<TMP_InputField>();
    }

    void Update()
    {
        if (Input.anyKey)
        {
            ReajustCaret();
            if (input.stringPosition == input.text.Length)
            {
                if (Input.GetKey(KeyCode.Backspace)) TryRenderFromEnd();
                else RenderTextColorsOnWrite();
            } else
            {
                // Now you don't inside or outside?
                if (Input.GetKey(KeyCode.Backspace))
                {
                    if (WhereIsCaret(0) == -1)
                    {
                        TryRemoveColorOutside();
                    }
                    else
                    {
                        TryRemoveColorsInside();
                    }
                }
            }
        }
    }

    public void RenderTextColorsOnWrite()
    {
        input = GetComponent<TMP_InputField>();
        string a = "color=#", b = "/color";
        foreach(KarelWordList.Sentence cw in WordList.wordList)
        {
            // SearchforandAdd
            TryAddNewColors(cw.word, a + colorToHex(cw.color), b);
        }
        
    } // Implementar a inspector DONE

    private void TryRenderFromEnd()
    {
        if (input.text.Length < 12) return;
        if (input.text.Substring(input.stringPosition - 7, 7) == "</color")
        {
            // Vale son tres ...<color=#dddddd>...</color>...
            // Necessitamos saber el principio de la PURE word
            int where = WhereIsCaret(-9);
            string firstSub, secondSub;

            if ((FirstPureWord(where) - "<color=#ddddd> ".Length) < 0)
            {
                firstSub = "";
                secondSub = input.text.Substring(FirstPureWord(where) + 2, LastPureWord(where) - FirstPureWord(where)); // ddd>... no hace falta
            }
            else
            {
                firstSub = input.text.Substring(0, FirstPureWord(where) - "<color=#ddddd> ".Length);  // ...<col
                secondSub = input.text.Substring(FirstPureWord(where), LastPureWord(where) - FirstPureWord(where)); // ddd>... no hace falta
            }
            
            input.text = firstSub + secondSub.Substring(0, secondSub.Length - 1);

            if ((FirstPureWord(where) - "<color=#ddddd> ".Length) < 0) refreshColorInfo(-1);
            else refreshColorInfo(where);
        }
    }

    private void TryRemoveColorOutside()
    {

        try
        {
            // Implementa funcion de limpiar <color> sobrantes y ya esta!
            
            // RIP antiguo sistema. 15/12/2018 - 18/12/2018
        }
        catch (Exception)
        {
        }
    } // Not yet

    private void TryRemoveColorsInside()
    {

    }

    private void ReajustCaret()
    {
        if (input.selectionAnchorPosition != input.selectionFocusPosition)
        {
            ReajustSelectionCaret(); // Bug de que los colores no se seleccionan al pasar por el raton! [BUG #5] JODEEEER ESTOY HARTO
            return; // Soluciona un bug que al pasar por texto colorido la seleccion se bugueaba [BUG #1]
        }
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

    private void ReajustSelectionCaret()
    {
        // Cosas con las que input.text.onSelect() o noseque

        // Al final haz la funcion de actualizar si sobran <color> y eso. Misma funcion que on edit
    }

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
                    _add.Add(m.Index + 15);
                    _add.Add(m.Index + 15 + value.Length);
                    coloredWordsInfo.Add(_add);

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
