using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MessageManager : MonoBehaviour
{
    // VERSION FINAL DE ESTE SCRIPT! YAY! :D

    [Header("Instances")]
    public GameObject messagePrefab;
    [Space]
    [Header("Objects to define")]
    public Transform canvas;

    // Estructura de datos de las targetas!
    public struct KMessage
    {
        public string text;
        public Color color;
        public float time;

        public KMessage(string text)
        {
            this.text = text;
            this.color = Color.white;
            this.time = 5f;
        }

        public KMessage(string text, Color color)
        {
            this.text = text;
            this.color = color;
            this.time = 5f;
        }

        public KMessage(string text, Color color, float time)
        {
            this.text = text;
            this.color = color;
            this.time = time;
        }
    }

    [SerializeField]
    public static List<KMessage> _messageQueue = new List<KMessage>();

    
        public static void MakeAMessage(string messagetext)
        {
            _messageQueue.Add(new KMessage(messagetext));
        }

        public static void MakeAMessage(string messagetext, Color messageColor)
        {
            _messageQueue.Add(new KMessage(messagetext, messageColor));
        }

        public static void MakeAMessage(string messagetext, Color messageColor, float messageTime)
        {
            _messageQueue.Add(new KMessage(messagetext, messageColor, messageTime));
        }


    void Awake()
    {
        StartCoroutine(ProcessMessages());
    }

    void Start()
    {
        // MsgManager.MakeAMessage("Dolor ipsum tie aemet!", Color.red, 10f);
        // MsgManager.MakeAMessage("Dolor ipsum tie aemet!", Color.green, 50f);
    }

    IEnumerator ProcessMessages()
    {
        while (true)
        {
            // Codigo que se ejecuta todo el rato continuamente sin dar error al bucle infinito ejem ejem unity
            if(_messageQueue.Count > 0)
            {
                yield return StartCoroutine(HandleMessage(_messageQueue[0]));
                _messageQueue.RemoveAt(0);
            }

            yield return null;
        }
    }

    IEnumerator HandleMessage(KMessage message)
    {
        // Instantear prefab
        GameObject target = Instantiate(messagePrefab);
        // Establecer posicion
        target.transform.SetParent(canvas);
        target.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        // Conseguir cambiar el texto
        TextMeshProUGUI tmpro = target.transform.Find("Solid").Find("Text").GetComponent<TextMeshProUGUI>();
        tmpro.text = message.text;
        // Conseguir cambiar el color
        target.transform.Find("Solid").GetComponent<Image>().color = message.color;
        target.transform.Find("Solid").Find("Slider").Find("Background").GetComponent<Image>().color = message.color;
        // Conseguir functionar la cross xd
        Button but = target.transform.Find("Solid").Find("Cross").GetComponent<Button>();
        but.onClick.AddListener(() => DesactiveAnObject(target));
        // Ahora a hacer animacion
        Slider sli = target.transform.Find("Solid").Find("Slider").GetComponent<Slider>();
        sli.maxValue = message.time;
        for(float i = 0f; i < message.time; i += 0.02f)
        {
            sli.value = i;
            if (!target.activeSelf) break;
            yield return new WaitForFixedUpdate();
        }
        
        Destroy(target);

        // Espera bonita para no estresar
        for(int i = 0; i < 50; i++) yield return new WaitForFixedUpdate();
    }

    public void DesactiveAnObject(GameObject obj)
    {
        obj.SetActive(false);
    }
}
