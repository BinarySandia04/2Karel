using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MessageManager;

public class MenuManagerInGame : MonoBehaviour
{
    public GameObject windows;
    public int animationSpeed = 4;

    [SerializeField]
    private List<bool> _bprocess = new List<bool>();
    [SerializeField]
    private List<GameObject> _gprocess = new List<GameObject>();
    void Start() {
        StartCoroutine(processUI());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private void OnEnable()
    {
        StartCoroutine(processUI());
    }

    public void OpenMenu(string whatMenu){
        foreach (Transform child in windows.transform) {
            
            GameObject g = child.gameObject;
            if (g.name == whatMenu) {
                // Es este.
                if(!imOcultado(g)) addToProcessUI(false, g);
                
            } else {
                // Pon en posicion inicial

                if (!imMostrado(g)) addToProcessUI(true, g);
                
            }
        }
    }

    public void OpenMenuInstant(string whatMenu)
    {
        foreach (Transform child in windows.transform)
        {

            GameObject g = child.gameObject;
            if (g.name == whatMenu)
            {
                if (!imOcultado(g)) GoToInstant(false, g);
            }
            else
            {
                // Pon en posicion inicial

                if (!imMostrado(g)) GoToInstant(true, g);

            }
        }
    }

    private void addToProcessUI(bool show, GameObject go)
    {
        _gprocess.Add(go);
        _bprocess.Add(show);
    }
    private bool imMostrado(GameObject g)
    {
        RectTransform rect = g.GetComponent<RectTransform>();
        if (rect.anchoredPosition.x == 500)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private bool imOcultado(GameObject g)
    {
        RectTransform rect = g.GetComponent<RectTransform>();
        if (rect.anchoredPosition.x == 0)
        {
            return true;
        } else
        {
            return false;
        }
    }

    IEnumerator processUI()
    {
        while (true)
        {
            while (_gprocess.Count > 0)
            {
                // Va de 2 and 2
                yield return StartCoroutine(GoTo(_bprocess[0], _gprocess[0]));
                _gprocess.RemoveAt(0);
                _bprocess.RemoveAt(0);
            }
            yield return null;
        }
    }

    IEnumerator GoTo(bool show, GameObject go){

        RectTransform rect = go.GetComponent<RectTransform>();
        if (show){
            // EMPIEZA: rect.left = 0, rect.right = 0;
            // ACABA:   rect.left = 500, rect.right = 0;
            
            Debug.Log(rect.anchoredPosition);
            for(int i = 0; i < 500; i++)
            {
                Vector2 newPos = new Vector2(i, 0);
                rect.anchoredPosition = newPos;
                if(i % animationSpeed == 0) yield return new WaitForFixedUpdate();
            }
            rect.anchoredPosition = new Vector2(500, 0);
        }
        else
        {
            // EMPIEZA: rect.left = 500, rect.right = 0;
            // ACABA:   rect.left = 0, rect.right = 0;
            for (int i = 500; i > 0; i--)
            {
                Vector2 newPos = new Vector2(i, 0);
                rect.anchoredPosition = newPos;
                if (i % animationSpeed == 0) yield return new WaitForFixedUpdate();
            }
            rect.anchoredPosition = new Vector2(0, 0);
        }
        yield return null;
    }

    void GoToInstant(bool show, GameObject go)
    {
        RectTransform rect = go.GetComponent<RectTransform>();
        if (show)
        {
            Debug.Log(rect.anchoredPosition);
            rect.anchoredPosition = new Vector2(500, 0);
        }
        else
        {
            Debug.Log(rect.anchoredPosition);
            rect.anchoredPosition = new Vector2(0, 0);
        }
    }
}
