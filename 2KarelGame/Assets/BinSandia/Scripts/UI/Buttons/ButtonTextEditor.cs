using System.Collections;
using UnityEngine;

public class ButtonTextEditor : MonoBehaviour
{
    public bool showed = true;
    public bool coroutining = false;
    public GameObject codeEditor;

    private float speedMultiplier;

    void Start()
    {
        if (codeEditor == null) return;
        speedMultiplier = codeEditor.GetComponent<MenuUISizeChanger>().speedMultiplier;
    }

    // Start is called before the first frame update
    public void AlternateColor()
    {
        if (!coroutining)
        {
            if (showed)
            {
                StartCoroutine(hideMenu());
            }
            else
            {
                StartCoroutine(showMenu());
            }
            showed = !showed;
        }

    }

    IEnumerator showMenu()
    {
        coroutining = true;
        RectTransform rt = GetComponent<RectTransform>();
        float i;
        for (i = -90; i <= 90; i += (speedMultiplier * Time.deltaTime * 0.5f))
        {
            GetComponent<RectTransform>().rotation = Quaternion.Euler(0,0,i);
            yield return new WaitForSecondsRealtime(0.001f);
        }
        i = 90;
        GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, i);
        yield return new WaitForSecondsRealtime(0.001f);

        coroutining = false;
        yield return null;
    }

    IEnumerator hideMenu()
    {
        coroutining = true;
        RectTransform rt = GetComponent<RectTransform>();
        float i;
        for (i = 90; i >= -90; i -= (speedMultiplier * Time.deltaTime * 0.5f))
        {
            GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, i);
            yield return new WaitForSecondsRealtime(0.001f);
        }
        i = -90;
        GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, i);
        yield return new WaitForSecondsRealtime(0.001f);

        coroutining = false;
        yield return null;

    }
}
