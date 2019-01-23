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

    public enum Orientation
    {
        North,
        South,
        East,
        West
    }

    [Space]
    public Orientation or = Orientation.North;


    // ALERTA: Codigo bastante largo y aburrido
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

        } else if(or == Orientation.South)
        {

        } else if(or == Orientation.East)
        {

        } else if(or == Orientation.West){

        } else
        {

        }
    }

    public void KarelTurnLeft()
    {
        if (or == Orientation.North)
        {

        }
        else if (or == Orientation.South)
        {

        }
        else if (or == Orientation.East)
        {

        }
        else if (or == Orientation.West)
        {

        }
        else
        {

        }
    }

    public void KarelTurnRight()
    {
        if (or == Orientation.North)
        {

        }
        else if (or == Orientation.South)
        {

        }
        else if (or == Orientation.East)
        {

        }
        else if (or == Orientation.West)
        {

        }
        else
        {

        }
    }
}
