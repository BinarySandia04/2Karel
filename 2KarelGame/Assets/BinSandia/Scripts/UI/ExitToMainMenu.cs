using UnityEngine;
using UnityEngine.SceneManagement;
using static GameManager;

public class ExitToMainMenu : MonoBehaviour
{
    public void exit()
    {
        foreach(GameObject g in SceneManager.GetActiveScene().GetRootGameObjects())
        {
            if(g.name == "Managers")
            {
                // Mirar si hay codigo guardado entonces sobreescribir
                CodeData delete = new CodeData();
                bool deleting = false;
                foreach(CodeData gcd in g.transform.Find("Game Manager").GetComponent<GameManager>().codeData)
                {
                    if(gcd.level == g.transform.Find("Game Manager").GetComponent<GameManager>().currentLevel)
                    {
                        delete = gcd;
                        deleting = true;
                    }
                }
                if(deleting) g.transform.Find("Game Manager").GetComponent<GameManager>().codeData.Remove(delete);
                // Añadir el nuevo
                CodeData cd = new CodeData();
                cd.code = g.transform.Find("Game Manager").GetComponent<GameManager>().GetTheCodeCode();
                cd.level = g.transform.Find("Game Manager").GetComponent<GameManager>().currentLevel;
                g.transform.Find("Game Manager").GetComponent<GameManager>().codeData.Add(cd);

                g.transform.Find("Game Manager").GetComponent<GameManager>().ShowMainMenu();
                // TODO: Hacer que al salir al menu principal poner el
                g.transform.Find("Game Manager").GetComponent<GameManager>().Content.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
                // rectTransform de Content la Y a 0 y del
                // Scrollbar Vertical el value a 1
                g.transform.Find("Game Manager").GetComponent<GameManager>().scroll.value = 1;
                // De esta manera, al salir el dropdown estara siempre puesto al primer item y el usuario no
                // tendra que subir. En fin, buenas noches. Estamos a 06/02/2019
                return;
            }
        }
    }
}
