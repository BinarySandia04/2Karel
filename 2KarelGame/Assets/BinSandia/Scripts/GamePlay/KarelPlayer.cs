// IN PROGRESS

using UnityEngine;

public class KarelPlayer : MonoBehaviour
{
    // Ok aqui estan todos los bools de condiciones que el usuario puede conseguir escribiendo su codigo
    [HideInInspector]
    public GameObject levelGenerator;
    [Header("Exit")]
    [Space]
    [Header("Bools in realtime")] // Ironico pero no se porque en el inspector se intercanvian xd
    public bool noExitPresent;
    public bool exitPresent;
    [Header("Beepers")]
    public bool beepersPresent;
    public bool noBeepersPresent;
    public bool beepersInBag;
    public bool noBeepersInBag;
    [Header("Front, right, left")]
    public bool frontIsClear;
    public bool frontIsBlocked;
    public bool rightIsClear;
    public bool rightIsBlocked;
    public bool leftIsClear;
    public bool leftIsBlocked;
    [Header("Trays")]
    public bool trayPresent;
    public bool noTrayPresent;
    public bool trayFull;
    public bool trayNotFull;
    [Header("Other")]
    public bool isRemovableWall;
    public bool trayIsMine;
    public bool isTrueExit;
    [Space]
    [Header("Coding")]
    public string bufferCode;
    public bool running;

    public enum Orientation
    {
        North,
        South,
        East,
        West
    }

    [Space]
    public Orientation or = Orientation.North;


    // ALERTA: Funcion bastante larga y aburrida
    void checkBools()
    {
        int playerX = GetComponent<ObjectPropieties>().xCoord;
        int playerY = GetComponent<ObjectPropieties>().yCoord;

        GameObject standingIn = levelGenerator.GetComponent<LevelGenerator>().getObjectCoord(playerX, playerY);

        GameObject gnorth = levelGenerator.GetComponent<LevelGenerator>().getObjectCoord(playerX, playerY + 1);
        GameObject gsouth = levelGenerator.GetComponent<LevelGenerator>().getObjectCoord(playerX, playerY - 1);
        GameObject geast = levelGenerator.GetComponent<LevelGenerator>().getObjectCoord(playerX - 1, playerY);
        GameObject gwest = levelGenerator.GetComponent<LevelGenerator>().getObjectCoord(playerX + 1, playerY);

        if(or == Orientation.North)
        {
            if(gnorth.name == "Wall")
            {
                // FRONT
                frontIsBlocked = true;
                frontIsClear = false;
            } else
            {
                frontIsBlocked = false;
                frontIsClear = true;
            }
            if(geast.name == "Wall")
            {
                // RIGHT
                rightIsBlocked = true;
                rightIsClear = false;
            } else
            {
                rightIsBlocked = false;
                rightIsClear = true;
            }
            if(gwest.name == "Wall")
            {
                // LEFT
                leftIsBlocked = true;
                leftIsClear = false;
            } else
            {
                leftIsBlocked = false;
                leftIsClear = true;
            }
        } else if (or == Orientation.South)
        {
            if (gsouth.name == "Wall")
            {
                // FRONT
                frontIsBlocked = true;
                frontIsClear = false;
            }
            else
            {
                frontIsBlocked = false;
                frontIsClear = true;
            }
            if (geast.name == "Wall")
            {
                // RIGHT
                rightIsBlocked = true;
                rightIsClear = false;
            }
            else
            {
                rightIsBlocked = false;
                rightIsClear = true;
            }
            if (gwest.name == "Wall")
            {
                // LEFT
                leftIsBlocked = true;
                leftIsClear = false;
            }
            else
            {
                leftIsBlocked = false;
                leftIsClear = true;
            }
        } else if (or == Orientation.West)
        {
            if (gwest.name == "Wall")
            {
                // FRONT
                frontIsBlocked = true;
                frontIsClear = false;
            }
            else
            {
                frontIsBlocked = false;
                frontIsClear = true;
            }
            if (gsouth.name == "Wall")
            {
                // RIGHT
                rightIsBlocked = true;
                rightIsClear = false;
            }
            else
            {
                rightIsBlocked = false;
                rightIsClear = true;
            }
            if (gnorth.name == "Wall")
            {
                // LEFT
                leftIsBlocked = true;
                leftIsClear = false;
            }
            else
            {
                leftIsBlocked = false;
                leftIsClear = true;
            }
        } else if (or == Orientation.East)
        {
            if (geast.name == "Wall")
            {
                // FRONT
                frontIsBlocked = true;
                frontIsClear = false;
            }
            else
            {
                frontIsBlocked = false;
                frontIsClear = true;
            }
            if (gnorth.name == "Wall")
            {
                // RIGHT
                rightIsBlocked = true;
                rightIsClear = false;
            }
            else
            {
                rightIsBlocked = false;
                rightIsClear = true;
            }
            if (gsouth.name == "Wall")
            {
                // LEFT
                leftIsBlocked = true;
                leftIsClear = false;
            }
            else
            {
                leftIsBlocked = false;
                leftIsClear = true;
            }
        } else
        {
            // ?
        }

        // Si el jugador esta posicionado en una salida...
        if(standingIn.name == "Exit")
        {
            noExitPresent = false;
            exitPresent = true;
        } else
        {
            noExitPresent = true;
            exitPresent = false;
        }

        // Si el jugador esta con un beeper...
        if(standingIn.name == "Beeper")
        {
            beepersPresent = true;
            noBeepersPresent = false;
        } else
        {
            beepersPresent = false;
            noBeepersPresent = true;
        }

        // Trays...
        if(standingIn.name == "Tray")
        {
            trayPresent = true;
            noTrayPresent = false;
        } else
        {
            trayPresent = false;
            noTrayPresent = true;
        }
    }
    void translateCurrentPosition(int x, int y)
    {
        ObjectPropieties op = GetComponent<ObjectPropieties>();
        op.xCoord += x;
        op.yCoord += y;
    }

    // FUNCIONES QUE DESPUÉS SERAN INTERPRETADAS A CODIGO!
    public void KarelMove()
    {
        if(or == Orientation.North)
        {
            // Animation
            translateCurrentPosition(0, 1);
        } else if(or == Orientation.South)
        {
            translateCurrentPosition(0, -1);
        } else if(or == Orientation.East)
        {
            translateCurrentPosition(1, 0);
        } else if(or == Orientation.West){
            translateCurrentPosition(-1, 0);
        } else
        {
            Debug.LogWarning("Orientation undefined when moving Karel");
        }
    }
    public void KarelTurnLeft()
    {
        if (or == Orientation.North)
        {
            or = Orientation.West;
        }
        else if (or == Orientation.South)
        {
            or = Orientation.East;
        }
        else if (or == Orientation.East)
        {
            or = Orientation.North;
        }
        else if (or == Orientation.West)
        {
            or = Orientation.South;
        }
        else
        {
            Debug.LogWarning("Orientation undefined when turning left karel");
        }
    }
    public void KarelTurnRight()
    {
        if (or == Orientation.North)
        {
            or = Orientation.East;
        }
        else if (or == Orientation.South)
        {
            or = Orientation.West;
        }
        else if (or == Orientation.East)
        {
            or = Orientation.South;
        }
        else if (or == Orientation.West)
        {
            or = Orientation.North;
        }
        else
        {
            Debug.LogWarning("Orientation undefined when turning right karel");
        }
    }

    // Magia
    public string translateCode(string code)
    {
        // Aqui convertiremos nuestro codigo en identificadores especiales para que después
        // en la función extractCode sea más fácil programarla
        /*
         * En esta funcion se escribira con los prefijos de más abajo separados por comas.
         * La string final no deveria de encontrar repeats ni cosas extrañas.
         * Aqui hacemos toda la transformacion. Primero comprobando de que no hayan espacios
         * ni saltos de línea eliminando estos.
         */
        /*
         * ------- FUNCIONES ---------
         * mv - Move
         * tl - TurnLeft
         * tr - TurnRight
         * wh<> - while (acepta bool)
         * ex - exit
         * ------- BOOLEANOS ---------
         * ep - no / exit present
         * bp - no / beepers present
         * bg - no / beepers in bag
         * fb - front blocked / clear
         * rb - right blocked / clear
         * lb - left blocked / clear
         * tp - no / tray present
         * tf - no / tray full
         * ----------------------------
         * 
         * NOTA: Hay que iterar de manera recursiva los whiles.
         */

        // FASE 1: Eliminar colores, espacios i saltos de linea
        string clear = "";
        for (int i = 0; i < code.Length; i++)
        {
            if (code[i] == '<')
            {
                // No añadir hasta tal
                int of = 0;
                while (code[i + of] != '>')
                {
                    of++;
                    if ((i + of) >= code.Length) break;
                }
                i = i + of;
            }
            else
            {
                if(code[i] != ' ' && code[i] != '\n') clear += code[i];
            }
        }
        code = clear;
        // FASE 2: Interpretar (hard TUDU WIP)
        string resulting = "";
        /*
         * Primero: Definir funciones
         * Segundo: Entonces definir repeats
         * Tercero: ggwp
         */

        return code;
    }
    public void extractCode(string code)
    {

    }
}
