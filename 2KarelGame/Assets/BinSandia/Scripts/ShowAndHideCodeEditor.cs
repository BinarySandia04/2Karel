using System.Collections;
using UnityEngine;

public class ShowAndHideCodeEditor : MonoBehaviour
{
    private bool showed = true;

    // Start is called before the first frame update
    public void AlternateColor()
    {
        if (showed)
        {
            StartCoroutine(hideMenu());
        } else
        {
            StartCoroutine(showMenu());
        }
    }

    IEnumerator showMenu()
    {
        yield return null;
    }

    IEnumerator hideMenu()
    {
        yield return null;
    }
}
