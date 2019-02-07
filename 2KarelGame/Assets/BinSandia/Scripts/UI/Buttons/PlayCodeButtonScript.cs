using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayCodeButtonScript : MonoBehaviour
{
    public void renderCode()
    {
        GameObject player = null;
        string code = "";
        foreach(GameObject g in SceneManager.GetActiveScene().GetRootGameObjects())
        {
            if(g.name == "Managers"){
                player = g.transform.Find("Level Manager").Find("Player(Clone)").gameObject;
            }

            if(g.name == "Ui - Canvas")
            {
                code = g.transform.Find("inGameMenu").Find("Right Ingame Menu").Find("Windows").Find("Code").Find("Code Editor").GetComponent<TMP_InputField>().text;
            }
        }
        if(player != null)
        {
            player.GetComponent<KarelPlayer>().bufferCode = code;
            player.GetComponent<KarelPlayer>().translateCode();
        } else
        {
            Debug.LogWarning("No hay player para ejecutar codigo!");
        }
    }
}
