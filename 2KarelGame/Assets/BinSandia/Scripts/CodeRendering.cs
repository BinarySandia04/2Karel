using System;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

public class CodeRendering : MonoBehaviour
{

    public TMP_InputField input;
    private string nums = "1234567890";
    private string[] colorWords = new string[] {"function", "pickBeeper", "putBeeperInTray",
            "move", "repeat", "turnLeft", "turnRight", "while",
            "if", "else", "frontIsBlocked", "frontIsClear",
            "rightIsBlocked", "rightIsClear",
            "leftIsBlocked", "leftIsClear", "trayNotFull", "noExitPresent", "beepersInBag" };

    bool isNumbers()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0) ||
            Input.GetKeyDown(KeyCode.Alpha2) ||
            Input.GetKeyDown(KeyCode.Alpha3) ||
            Input.GetKeyDown(KeyCode.Alpha4) ||
            Input.GetKeyDown(KeyCode.Alpha5) ||
            Input.GetKeyDown(KeyCode.Alpha6) ||
            Input.GetKeyDown(KeyCode.Alpha7) ||
            Input.GetKeyDown(KeyCode.Alpha8) ||
            Input.GetKeyDown(KeyCode.Alpha9) ||
            Input.GetKeyDown(KeyCode.Alpha0)
            )
        {
            return true;
        }
        return false;
    }

    void Update()
    {
        if (Input.anyKey)
        {
            RenderTextColors();
            if (Input.GetKey(KeyCode.Backspace))
            {
                TryRemoveColor();
            }
            if (isNumbers())
            {
                RenderNumberColor();
            }
        }
    }
    public void RenderNumberColor()
    {
        // WIP
    }

    private void SearchForAndAddNumber(string value, string before, string after)
    {
        string actualText = input.text;
        MatchCollection match = Regex.Matches(actualText, value); // numero de values en la string
        Match m;
        try
        {
            m = match[match.Count - 1];
        }
        catch (Exception ex)
        {
            return; // Numero no implementado
        }
        if (m.Index != input.stringPosition - 1) return;
        string colored = actualText.Insert(m.Index, '<' + before + '>').Insert(m.Index + before.Length + 2 + value.Length, "<" + after + ">");
        input.text = colored;
        input.stringPosition += before.Length + 2;

    }

    public void RenderTextColors()
    {
        input = GetComponent<TMP_InputField>();
        SearchForAndAdd("function", "color=#F4A442", "/color");
        SearchForAndAdd("pickBeeper", "color=#6B0FFF", "/color");
        SearchForAndAdd("putBeeperInTray", "color=#6B0FFF", "/color");
        SearchForAndAdd("move", "color=#6B0FFF", "/color");
        SearchForAndAdd("repeat", "color=#F4A442", "/color");
        SearchForAndAdd("turnLeft", "color=#33FF33", "/color");
        SearchForAndAdd("turnRight", "color=#33FF33", "/color");
        SearchForAndAdd("while", "color=#F4A442", "/color");
        SearchForAndAdd("if", "color=#F4A442", "/color");
        SearchForAndAdd("else", "color=#F4A442", "/color");
        SearchForAndAdd("frontIsBlocked", "color=#0F4FDB", "/color");
        SearchForAndAdd("frontIsClear", "color=#0F4FDB", "/color");
        SearchForAndAdd("rightIsBlocked", "color=#0F4FDB", "/color");
        SearchForAndAdd("rightIsClear", "color=#0F4FDB", "/color");
        SearchForAndAdd("leftIsBlocked", "color=#0F4FDB", "/color");
        SearchForAndAdd("leftIsClear", "color=#0F4FDB", "/color");
        SearchForAndAdd("trayNotFull", "color=#0F4FDB", "/color");
        SearchForAndAdd("noExitPresent", "color=#0F4FDB", "/color");
        SearchForAndAdd("beepersInBag", "color=#0F4FDB", "/color");

        ReajustCaret();
    }

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
        catch (Exception ex)
        {
        }
    }

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
                    Debug.Log("A");
                }
                else
                {
                    newCaretPos += 15;
                }
                input.stringPosition = newCaretPos;
                return;
            }
        }
        catch (Exception ex)
        {
        }

    }

    private void SearchForAndAdd(string value, string before, string after)
    {
        string colored;
        bool changed = false;
        string actualText = input.text;
        MatchCollection match = Regex.Matches(actualText, value); // numero de values en la string
        foreach (Match m in match)
        {
            // m.Index dice donde se encuentra el string encontrado.
            // Aqui hace un loop por todos
            if (nums.Contains(value))
            {
                // Ok estamos tratando de colorear un número!
                // TODO comprobar si en 1,2,3,4,5 o 6 posiciones atras hay un '#'
                try
                {

                    if (!(actualText[m.Index + 1] != '>' && actualText[m.Index + 2] != '>' &&
                    actualText[m.Index + 3] != '>' && actualText[m.Index + 4] != '>' &&
                    actualText[m.Index + 5] != '>' && actualText[m.Index + 6] != '>'))
                    {
                        // Esta fuera de un color!
                        try
                        {
                            if (actualText[m.Index - 1] == '>')
                            {
                                // Osea, si hay en este caso detras "<color=#noseqe> y delante </color>, esto se triggerea
                            }
                            else
                            {
                                colored = actualText.Insert(m.Index, '<' + before + '>').Insert(m.Index + before.Length + 2 + value.Length, "<" + after + ">");

                                input.text = colored;
                                input.stringPosition += before.Length + 2;
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                    else
                    {
                    }
                }
                catch (Exception ex)
                {
                }

            }
            try
            {
                if (actualText[m.Index - 1] == '>')
                {
                    // Osea, si hay en este caso detras "<color=#noseqe> y delante </color>, esto se triggerea
                }
                else
                {
                    colored = actualText.Insert(m.Index, '<' + before + '>').Insert(m.Index + before.Length + 2 + value.Length, "<" + after + ">");

                    input.text = colored;
                    input.stringPosition += before.Length + 2;
                }
            }
            catch (Exception ex)
            {
                colored = actualText.Insert(m.Index, '<' + before + '>').Insert(m.Index + before.Length + 2 + value.Length, "<" + after + ">");

                input.text = colored;
                input.stringPosition += before.Length + 2;
            }
        }
        // Encontrar colores que ya no pueden existir [BUG #2]
        match = Regex.Matches(actualText, before);
        foreach (Match m in match)
        {
            bool noHas = true;
            string whatIsIt = "NULL";
            for (int i = 0; i < colorWords.Length; i++)
            {
                try
                {
                    Debug.Log("Comparando " + input.text.Substring(m.Index + before.Length + 1, colorWords[i].Length) + " con " + colorWords[i]);
                    if (input.text.Substring(m.Index + before.Length + 1, colorWords[i].Length) == colorWords[i])
                    {
                        noHas = false;
                        whatIsIt = colorWords[i];
                        break;
                    }
                }
                catch (Exception ex)
                {
                    input.text += " ";
                }

            }
            if (noHas) // El substring deberia devolver, si es <color=#FFFFF>tes</color>, deveria devolver tes<, i si eso no es igual a test...
            {// TODO
                int lengthError = 0;
                try
                {
                    // Falta arreglar esto, y de bugs vamos done! :D
                    for (int i = 0; input.text[i + m.Index + before.Length] != '<'; i++)
                    {
                        lengthError++;
                    }
                    Debug.Log("LengthError: " + lengthError);
                    Debug.Log("No es lo mismo " + input.text.Substring(m.Index + before.Length + 1, lengthError) + " que ningun noseque");
                    input.text = input.text.Substring(0, m.Index) + input.text.Substring(m.Index + lengthError, input.text.Length - (m.Index + before.Length - 3));
                    input.stringPosition -= 6;
                }
                catch (Exception ex)
                {
                    // Añadir substring del principio?
                    int i = input.text.Length - 1;
                    while (input.text[i] != '>')
                    {
                        i--;
                    }
                    input.text = input.text.Substring(i + 1, input.text.Length - i - 1);
                    i = input.text.Length - 1;
                    while (input.text[i] == ' ')
                    {
                        i--;
                    }
                    input.text = input.text.Substring(0, i + 1);
                }

            }
        }
    }
}

// TODO:

    // Cuando hagas funcionar este desastre, optimizalo un poco. Hay muchos bucles. Ya sabes lo que quiero decir!