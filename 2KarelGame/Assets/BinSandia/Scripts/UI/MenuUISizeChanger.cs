using System.Collections;
using UnityEngine;

public class MenuUISizeChanger : MonoBehaviour
{
    public float multiplier = 5;
    public float speedMultiplier;

    public int beforeSize = -250;
    public int nextSize = 250;
    public bool coroutining = false;
    public int startSize = -250;
    public int endSize = 250;
    

    public void SetSizeTo(int size)
    {
        nextSize = size;
        speedMultiplier = size / 100 * multiplier;
        if (!coroutining)
        {
            StartCoroutine(_SetSize());
        }
    }

    IEnumerator _SetSize()
    {
        coroutining = true;
        RectTransform rt = GetComponent<RectTransform>();
        if (beforeSize < nextSize)
        {
            float i;
            for (i = beforeSize; i <= nextSize; i += (speedMultiplier * Time.deltaTime))
            {
                rt.anchoredPosition = new Vector3(-i / 2, 0, 0); // No
                rt.sizeDelta = new Vector2(i, rt.sizeDelta.y);
                yield return new WaitForSecondsRealtime(0.001f);
            }
            i = nextSize;
            rt.anchoredPosition = new Vector3(-i / 2, 0, 0); // No
            rt.sizeDelta = new Vector2(i, rt.sizeDelta.y);
            yield return new WaitForSecondsRealtime(0.001f);
        }
        if(beforeSize > nextSize)
        {
            float i;
            for (i = beforeSize; i >= nextSize; i -= (speedMultiplier * Time.deltaTime))
            {
                rt.anchoredPosition = new Vector3(-i / 2, 0, 0); // No
                rt.sizeDelta = new Vector2(i, rt.sizeDelta.y);
                yield return new WaitForSecondsRealtime(0.001f);
            }
            i = nextSize;
            rt.anchoredPosition = new Vector3(-i / 2, 0, 0); // No
            rt.sizeDelta = new Vector2(i, rt.sizeDelta.y);
            yield return new WaitForSecondsRealtime(0.001f);
        }
        beforeSize = nextSize;
        coroutining = false;
        yield return null;
    }

    public bool showed = true;

    // Start is called before the first frame update
    public void AlternateColor()
    {
        if (!coroutining){
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
        for (i = endSize; i >= startSize; i -= (speedMultiplier * Time.deltaTime))
        {
            rt.anchoredPosition = new Vector3(i, 0, 0); // No
            yield return new WaitForSecondsRealtime(0.001f);
        }
        i = startSize;
        rt.anchoredPosition = new Vector3(i, 0, 0); // No
        yield return new WaitForSecondsRealtime(0.001f);

        coroutining = false;
        yield return null;
    }

    IEnumerator hideMenu()
    {
        coroutining = true;
        RectTransform rt = GetComponent<RectTransform>();
        float i;
        for (i = startSize; i <= endSize; i += (speedMultiplier * Time.deltaTime))
        {
            rt.anchoredPosition = new Vector3(i, 0, 0); // No
            yield return new WaitForSecondsRealtime(0.001f);
        }
        i = endSize;
        rt.anchoredPosition = new Vector3(i, 0, 0); // No
        yield return new WaitForSecondsRealtime(0.001f);

        coroutining = false;
        yield return null;
        
    }
}
