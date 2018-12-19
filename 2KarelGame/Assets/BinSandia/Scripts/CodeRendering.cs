using System;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class CodeRendering : MonoBehaviour
{
    // Public variables
    public int debug;
    public KarelWordList WordList;
    private TMP_InputField input;
    private List<List<int>> coloredWordsInfo = new List<List<int>>(1000); // First int, PURE WORD location start, second one, end

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
       
            // Vaciar
            coloredWordsInfo = new List<List<int>>();
            offset = 0;
        
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
                coloredWordsInfo.Add(add);
                colors++;
                i += s + 7;
            }
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

    bool canHaveColor(string what)
    {
        foreach(KarelWordList.Sentence sen in WordList.wordList)
        {
            if (sen.word == what) return true;
        }
        return false;
    }

    void removeInnecessaryColors(int offset)
    {
        string whatWord = "";
        int start = -1, end = -1;
        bool success = false;
        int i;
        for (i = offset; i < input.text.Length - 1; i++)
        {
            if (input.text[i] == '>' && input.text[i - 6] != '/')
            {
                int until = 0;
                while((until + i) < input.text.Length && input.text[until + i] != '<' && until < 1000)
                {
                    until++;
                }
                start = i;
                end = until;
                whatWord = input.text.Substring(start + 1, end - 1);
                if (!canHaveColor(whatWord))
                {
                    success = true;
                    break;
                }
            }
        }

        if (success)
        {
            string firstSub, secondSub, thirdSub;
            firstSub = input.text.Substring(0, start - "<color=#ffffff".Length); // ERROR
            secondSub = input.text.Substring(start + 1, end - 1);
            thirdSub = input.text.Substring(start + end + 8, input.text.Length - (start + end + 8));
            int wherewasbefore = input.stringPosition;
            input.text = firstSub + secondSub + thirdSub;
            input.stringPosition = wherewasbefore - 15;
        }

        refreshColorInfo(-1);
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
            DoSpacing(); // Ajustar espaciado para codigo bueno bonito barato
            if (input.stringPosition == input.text.Length)
            {
                if (Input.GetKey(KeyCode.Backspace)) TryRenderFromEnd();
                else RenderTextColorsOnWrite();
            } else
            {
                // Now you don't inside or outside?
                //RenderTextColorsOnWrite(); // Aaaah pon aqui la function para actualizar colores.
                if(Input.anyKeyDown) removeInnecessaryColors(0);
                if (Input.GetKey(KeyCode.Backspace))
                {
                    if (WhereIsCaret(0) != -1) // La verdad es que solo nos interesa quitar si estamos dentro lool xd
                    {
                        TryRemoveColorsInside();
                    } else
                    {
                        TryRemoveColorsOutside(); // Estamos fuera del color, pero ns
                    }
                } else
                {
                    RenderTextColorsOnWrite();
                }
            }
        }
    }

    void DoSpacing()
    {
        try
        {
            if (input.text[input.stringPosition - 1] == '\n')
            {
                Debug.Log("Salto linea!");
            }
        }
        catch (Exception) { }
        
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
        if (input.stringPosition - 7 < 0) return;
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

    private void TryRemoveColorsInside()
    {
        // Nooot yet
        int whereImAt = WhereIsCaret(0); // Esto no puede ser -1, he hecho un if antes!
        // Encima estamos dentro porque si borras x fuera no puedes basicamente, se ejecutaria la otro lol xd
        removeInnecessaryColors(0);
    }

    private void TryRemoveColorsOutside()
    {
        // Hacer casi el mismo procedimiento de que si estamos editando lol xd
        if (input.stringPosition - 7 < 0) return;
            if (input.text.Substring(input.stringPosition - 7, 7) == "</color")
            {
                // Vale son tres ...<color=#dddddd>...</color>...
                // Necessitamos saber el principio de la PURE word
                int where = WhereIsCaret(-9);
                string firstSub, secondSub, thirdSub; // Ahora hacen falta TRES. Estamos en el medio!

                if ((FirstPureWord(where) - "<color=#ddddd> ".Length) < 0)
                {
                // Bug aqui al editar en medio lol
                    firstSub = "";
                    secondSub = input.text.Substring(FirstPureWord(where) + 2, LastPureWord(where) - (FirstPureWord(where) + 1)); // Facil, lo del medio -1 pq estamos en el final y queremos borrar xd
                    thirdSub = input.text.Substring(LastPureWord(where) + "</color".Length, input.text.Length - (LastPureWord(where) + "</color".Length)); // ddd>... no hace falta
                }
                else
                {
                    firstSub = input.text.Substring(0, FirstPureWord(where) - "<color=#ddddd> ".Length);  // ...<col
                    secondSub = input.text.Substring(FirstPureWord(where), LastPureWord(where) - (FirstPureWord(where) + 1)); // Facil, lo del medio -1 pq estamos en el final y queremos borrar xd
                    thirdSub = input.text.Substring(LastPureWord(where) + "</color".Length, input.text.Length - (LastPureWord(where) + "</color".Length)); // ddd>... no hace falta
                }

                input.text = firstSub + secondSub + thirdSub;
                input.stringPosition -= 23;

                if ((FirstPureWord(where) - "<color=#ddddd> ".Length) < 0) refreshColorInfo(-1); // Lo mismo diria
                else refreshColorInfo(where);
            }
        
        
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

    } // Not done

    private void ReajustSelectionCaret()
    {
        // Cosas con las que input.text.onSelect() o noseque

        // Al final haz la funcion de actualizar si sobran <color> y eso. Misma funcion que on edit
    } // Not done

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

// M'he matat bastant xd