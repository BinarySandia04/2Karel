using UnityEngine;

public class MenuManagerInGame : MonoBehaviour
{
    public GameObject[] gameObjects;
    public int showedpos = 0;
    public int hidedpos = -200;

    void Start()
    {

    }
    public void OpenMenu(string whatMenu){
        foreach(GameObject g in gameObjects){
          Debug.Log(g.name);
            if(g.name == whatMenu){
                // Es este.
                Debug.Log(g.name + " este se debe mostrar");
                StartCoroutine(goto(showedpos, g));
            } else {
                // Pon en posicion inicial
                Debug.Log(g.name + " este se debe ocultar");
                StartCoroutine(goto(hidedpos, g));
            }
        }
    }

    IEnumerator goto(int where, GameObject go){
      yield return null;
    }
}
