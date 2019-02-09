// IN PROGRESS

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class KarelPlayer : MonoBehaviour
{
    [HideInInspector]
    public KarelWordList reservedWordList;
    [HideInInspector]
    public LevelGenerator levelGenerator;
    [HideInInspector]
    public static CompilingPropieties CurrentCompilingPropieties;

    // ERROR HANDLING
    private string _errorMessage;
    private bool _triggeredError;

    // Ok aqui estan todos los bools de condiciones que el usuario puede conseguir escribiendo su codigo
    [Header("Exit")]
    [Space]
    [Header("Realtime propieties")] // Ironico pero no se porque en el inspector se intercanvian xd
    public bool noExitPresent;
    public bool exitPresent;
    /*
    [Header("Position")] Esto esta en GetComponent<ObjectPropieties>
    public int x = 0;
    public int y = 0;
    */
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
    [Header("Position")]
    [SerializeField]
    public Position pos;

    public enum Orientation
    {
        North,
        South,
        East,
        West
    }

    // TODO: Mover estos structs a otro sitio para estar ordenado
    [System.Serializable]
    public struct CompilingPropieties
    {
        public int recursiveFunctionCheckLimit;
    }

    public struct Function
    {
        public string funcName;
        public string funcContent;
        public bool main;
    }

    public struct Action
    {
        int xPos;
        int yPos;
        Orientation currentOrientation;
        bool hasMoved;
        bool hasRotated;
        bool hasPickedBeeper;
        bool hasLeftBeeper;
        bool exited;
    }

    [Space]
    public Orientation or = Orientation.North;

    public void translateCode()
    {
        _triggeredError = false;
        /*
         * --------------- INICIO DE UN LARGO COMENTARIO ---------------------------
         * Esta es la main function que convierte todo lo que hay en buffer code directamente ya a los
         * coroutines para ejecutar los comandos que ha escrito el jugador.
         * 
         * La estrategia sera la siguiente:
         * 1 - "Compilar" el codigo
         *   Lo que haremos sera quitar todos los espacios. I todos los tabs.
         *   Una vez hecho esto haremos que todas las funciones las definiremos como "macros", y las substituiremos
         *   por lo que haya dentro del function main(). Osea:
         *   Si tenemos:
         *   --------------------------------------------------------------------------
         *   // fig(1)
         *   function main(){
         *      cosa();
         *      turnLeft();
         *      cosa();
         *      turnRight();
         *      while(noExitPresent()){
         *         move();
         *      }
         *      exit();
         *   }
         *   function cosa(){
         *      repeat(8){
         *         move();
         *      }
         *   }
         *   ------------------------------------------------------------------------------
         *   Lo convertiremos en:
         *   ------------------------------------------------------------------------------
         *   // fig(2)
         *   function main(){
         *      repeat(8){
         *         move();
         *      }
         *      turnLeft();
         *      repeat(8){
         *          move();
         *      }
         *      turnRight();
         *      while(noExitPresent()){
         *          move();
         *      }
         *      exit();
         *   }
         *   -------------------------------------------------------------------------------
         *   Asi definiremos las funciones.
         *   Con los repeats y los whiles los compilaremos después. Haciendo el while hasta que se cumpla la condicion
         *   calculando el estado actual del Karel. Si el while se pasa de iteraciones podria saltar un error?
         *   
         *   Imaginemos que despues del turnRight la salida esta a 12 casillas:
         *   -------------------------------------------------------------------------------
         *   // fig(3)
         *   function main(){
         *      move();
         *      move();
         *      move();
         *      move();
         *      move();
         *      move();
         *      move();
         *      move();
         *      turnLeft();
         *      move();
         *      move();
         *      move();
         *      move();
         *      move();
         *      move();
         *      move();
         *      move();
         *      turnRight();
         *      move();
         *      move();
         *      move();
         *      move();
         *      move();
         *      move();
         *      move();
         *      move();
         *      move();
         *      move();
         *      move();
         *      move();
         *      exit();
         *  }
         *  ---------------------------------------------------------------------------------------
         *  Esto seria el codigo final.
         *  Como que el terreno de juego puede cambiar cada vez, (en plan si estamos multijugador),
         *  deberia hacer un sistema capaz de ir actualizando esta compilacion después de cada instruccion.
         *  
         *  Asi que definiremos una List<Action>, que se actualizara con los resultados de la compilacion final (fig. 3),
         *  cada vez que se ejecute una accion, se volvera a compilar todo el codigo inicial, y depende de la linea en que estemos,
         *  sera nuestro punto de partida para la siguiente accion
         *  --------- FIN DE UN LARGO COMENTARIO --------------------------------------------------------
         */

        if(CurrentCompilingPropieties.Equals(default(CompilingPropieties)))
        {
            Debug.LogWarning("No se han definido las opciones de compilacion");
            return;
        }

        // Paso 1 - quitar espacios i saltos de linea
        string compiledCode = removeSpaces(bufferCode); // Tenemos el codigo sin espacios
        // Comprobar gramatica

        // Paso 2 - definir functiones
        List<Function> funcions = getFunctions(compiledCode); // Tenemos en la lista funcions el contenido de todas las funciones y sus nombres
        // Vamos a comprobar que tenemos la funcion main
        // Antes vamos a ver si tenemos errores!
        checkForErrors();
        bool isMainHere = false;
        Function mainFunction = new Function();
        foreach(Function func in funcions)
        {
            if (func.funcName == "main")
            {
                mainFunction = func;
                isMainHere = true;
            }
        }

        for(int i = 0; i < CurrentCompilingPropieties.recursiveFunctionCheckLimit; i++)
        {
            foreach (Function func in funcions)
            {
                if (func.funcName != "main")
                {
                    mainFunction.funcContent = mainFunction.funcContent.Replace(func.funcName + "();", func.funcContent);
                }
            }
        }
        foreach (Function func in funcions)
        {
            if (func.funcName != "main")
            {
                mainFunction.funcContent = mainFunction.funcContent.Replace(func.funcName + "();", "###ERROR##ENDOFRECURSIVEFUNC###");
            }
        }

        if (!isMainHere)
        {
            _errorMessage = "No hay definido un punto de entrada";
            _triggeredError = true;
        }
        // Ahora hay que limpiar lo que sobra de function main
        compiledCode = mainFunction.funcContent;
        // Ahora deberiamos tener la fig(2)
        Debug.Log(compiledCode);
        // Hasta aqui todo TESTEADO Y CORRECTO
        // Y me da palo seguir. Esto TODO

        // Final: Mirar si hay errores:
        checkForErrors();
    }

    private bool checkForErrors()
    {
        if (_triggeredError)
        {
            MessageManager.MakeAMessage("ERROR: " + _errorMessage, Color.red, 3);
            Debug.LogError(_errorMessage);
            return true;
        } else {
            return false;
        }
    }

    // ALERTA: Funcion bastante larga y aburrida
    void checkBools()
    {
        int playerX = pos.xCoord;
        int playerY = pos.yCoord;

        GameObject standingIn = LevelGenerator.getObjectCoord(playerX, playerY);

        GameObject gnorth = LevelGenerator.getObjectCoord(playerX, playerY + 1);
        GameObject gsouth = LevelGenerator.getObjectCoord(playerX, playerY - 1);
        GameObject geast = LevelGenerator.getObjectCoord(playerX - 1, playerY);
        GameObject gwest = LevelGenerator.getObjectCoord(playerX + 1, playerY);

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
        }
        else if (or == Orientation.South)
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
        }
        else if (or == Orientation.East)
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
        }
        else
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
        pos.xCoord += x;
        pos.yCoord += y;
    }
    public bool imNotInAWallLol()
    {
        if (LevelGenerator.getObjectCoord(pos.xCoord, pos.yCoord).name != "Wall") return true;
        else return false;
    }

    // FUNCIONES QUE DESPUÉS SERAN INTERPRETADAS A CODIGO!
    public bool KarelMove() // Devuelve si se ha podido hacer
    {
        if(or == Orientation.North)
        {
            translateCurrentPosition(0, 1);
        } else if(or == Orientation.South)
        {
            translateCurrentPosition(0, -1);
        } else if(or == Orientation.East)
        {
            translateCurrentPosition(1, 0);
        } else if(or == Orientation.West)
        {
            translateCurrentPosition(-1, 0);
        } else
        {
            Debug.LogWarning("Orientation undefined when moving Karel");
        }

        if (imNotInAWallLol())
        {
            return true;
        } else
        {
            return false;
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
    public string removeSpaces(string code)
    {
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
        return code;
    }
    public List<Function> getFunctions(string code)
    {
        // Ya esta
        // TODO: Testing intensivo [YA ESTA Y FUNCIONA]
        List<Function> functions = new List<Function>();
        int funcNumber = Regex.Matches(code, "function").Count;
        int pointing = 0;
        code.Replace("function", "");
        for(int func = 0; func < funcNumber; func++)
        {
            Function finalFunc = new Function();
            // main(){move();repeat(3){move();]exit();} [EJEMPLO]
            int firstPoint;
            try
            {
                for (firstPoint = pointing; code[pointing] != '(' && pointing != code.Length; pointing++) { }
            }
            catch (Exception)
            {
                _errorMessage = "Funciones no definidas correctamente. Estan bien escritas?";
                _triggeredError = true;
                return null;
            }
            
            string finalFuncName = substrWithInt(code, firstPoint + 8, pointing - 1);
            if (isReservedWord(finalFuncName))
            {
                // ERROR: Hay una funcion definida como una palabra reservada.
                _errorMessage = "Hay una funcion definida como una palabra reservada";
                _triggeredError = true;
                return null;
            }
            else
            {
                finalFunc.funcName = finalFuncName;
            }
            pointing += 4; // Principio de funcion
            // Ahora incluir hasta final de funcion
            firstPoint = pointing;
            for(int brackets = 1; brackets > 0 && pointing != code.Length; pointing++)
            {
                if (code[pointing] == '{') brackets++;
                if (code[pointing] == '}') brackets--;
            }
            finalFunc.funcContent = substrWithInt(code, firstPoint - 1, pointing - 2);
            functions.Add(finalFunc);
        }
        
        return functions;
    }

    // Se aceptan optimizaciones
    private bool isReservedWord(string finalFuncName)
    {
        foreach(KarelWordList.Sentence sen in reservedWordList.wordList)
        {
            if (sen.word == finalFuncName && sen.word != "main") return true;
        }
        return false;
    }

    private List<int> findSubstr(string str, string find, bool final)
    {
        List<int> positions = new List<int>();
        for (int i = 1; i < str.Length; i++)
        {
            if(str.Substring(i, find.Length) == find)
            {
                if (final) positions.Add(i + find.Length);
                else positions.Add(i);
            }
        }
        return positions;
    }
    private string substrWithInt(string str, int start, int end)
    {
        string res = "";
        if (start >= end) return res;
        for (; start <= end; start++) res += str[start];
        return res;
    }
}
