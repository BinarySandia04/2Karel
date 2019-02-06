using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitToMainMenu : MonoBehaviour
{
    public void exit()
    {
        foreach(GameObject g in SceneManager.GetActiveScene().GetRootGameObjects())
        {
            if(g.name == "Managers")
            {
                g.transform.Find("Game Manager").GetComponent<GameManager>().ShowMainMenu();
                return;
            }
        }
    }
}
