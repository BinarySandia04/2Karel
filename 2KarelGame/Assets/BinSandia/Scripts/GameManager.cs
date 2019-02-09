using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [System.Serializable]
    public struct Map
    {
        public Texture2D mapPng;
        public Sprite mapSprite; // Imagen para el level manager xD
        public string name;
        public float difficulty; // En plan osu, de 1 a 10. Sin tener en cuenta cosas de GamePlay
        // Osea, lo tedioso que son!
        public int mapId; // Id que identifica el mapa
        public KarelPlayer.CompilingPropieties mapCompilingPropieties; // Propiedades al compilar codigo en el mapa
    }

    public struct CodeData
    {
        public string code;
        public int level;
    }

    public bool displayedMainMenu = true;
    public int currentLevel = -1;
    public Map currentLevelMap;
    [Space]
    [Header("Maps")]
    public List<Map> maps = new List<Map>();
    public List<CodeData> codeData = new List<CodeData>();
    [Space]
    [Header("Ui elements")]
    public GameObject InGameMenu;
    public GameObject InGameMenuPrefab;
    [Space]
    public GameObject MainMenu;
    public GameObject Content;
    public Scrollbar scroll;
    public GameObject LevelExamplePrefab;
    public GameObject ContinueExamplePrefab;
    public GameObject LevelManager;
    public GameObject Canvas;

    public void SetTheCodeCode(string code)
    {
        InGameMenu.transform.Find("Right Ingame Menu").Find("Windows").Find("Code").Find("Code Editor").GetComponent<TMP_InputField>().text = code;
    }

    public string GetTheCodeCode()
    {
        return InGameMenu.transform.Find("Right Ingame Menu").Find("Windows").Find("Code").Find("Code Editor").GetComponent<TMP_InputField>().text;
    }

    // Start is called before the first frame update
    void Start()
    {
        ShowMainMenu();
        AddLevelMaps();
    }

    public void HideMainMenu()
    {
        // TODO: Insertar aqui transicion de desvanecimiento de ejemplo
        InGameMenu.transform.Find("Right Ingame Menu").Find("Windows").GetComponent<MenuManagerInGame>().OpenMenuInstant("Code");
        MainMenu.SetActive(false);
        InGameMenu.SetActive(true);
        displayedMainMenu = false;
    }

    public void ShowMainMenu()
    {
        LevelManager.GetComponent<LevelGenerator>().codeEditor = InGameMenu.transform.Find("Right Ingame Menu").Find("Windows").Find("Code").Find("Code Editor").gameObject;
        // OPTIMIZAR
        AddLevelMaps();
        MainMenu.SetActive(true);
        InGameMenu.SetActive(false);
        displayedMainMenu = true;
    }

    public Map getMap(int orderNumber)
    {
        return maps[orderNumber];
    }

    // Se acepta optimizacion
    public Map getMapById(int id)
    {
        foreach(Map map in maps)
        {
            if (map.mapId == id) return map;
        }
        return new Map();
    }

    public void MakeGameMenu()
    {
        GameObject inGameMenu = Instantiate(InGameMenu);
        inGameMenu.gameObject.name = "inGameMenu";
        LevelManager.GetComponent<LevelGenerator>().codeEditor = inGameMenu.transform.Find("Right Ingame Menu").Find("Windows").Find("Code").Find("Code Editor").gameObject;
        inGameMenu.transform.SetParent(Canvas.transform);

        inGameMenu.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
        inGameMenu.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
    }

    public void DisableGameMenu()
    {
        if(Canvas.transform.Find("inGameMenu") != null) Destroy(Canvas.transform.Find("inGameMenu").gameObject);
    }

    private void ClearLevelMaps()
    {
        foreach(Transform tran in Content.transform)
        {
            Destroy(tran.gameObject);
        }
    }

    private void AddLevelMaps()
    {
        ClearLevelMaps();

        int levelToHighLight = 0;
        // HAY QUE OPTIMIZAR ESTO! UNITY PASA DEMASIADO RATO AQUI!
        // Poner primero el continuar
        foreach(Map map in maps)
        {
            if (levelToHighLight == currentLevel)
            {
                GameObject newButton = Instantiate(ContinueExamplePrefab);
                newButton.transform.SetParent(Content.transform);
                LevelSelectorButtonManager lBut = newButton.GetComponent<LevelSelectorButtonManager>();

                lBut.transform.Find("LevelText").GetComponent<TextMeshProUGUI>().text = "Continue";
                lBut.transform.Find("LevelDiff").GetComponent<TextMeshProUGUI>().text = map.name + " | Difficulty: " + map.difficulty;
                lBut.isContinueTarget = true;
                lBut.levelManager = LevelManager.GetComponent<LevelGenerator>();
                lBut.gameManager = this;
            }
            levelToHighLight++;
        }

        levelToHighLight = 0;
        // Ya los demas
        Map finalMap;
        foreach(Map map in maps)
        {
            // TODO: Boton de continuar
            if(levelToHighLight != currentLevel){
                GameObject newButton = Instantiate(LevelExamplePrefab);
                newButton.transform.SetParent(Content.transform);
                LevelSelectorButtonManager lBut = newButton.GetComponent<LevelSelectorButtonManager>();
                // Asignar cosas que hacen los botones a los botones que se crean a partir de botones
                finalMap = map;
                finalMap.mapId = levelToHighLight;
                lBut.map = finalMap;

                lBut.levelManager = LevelManager.GetComponent<LevelGenerator>();
                lBut.gameManager = this;
                lBut.isContinueTarget = false;

                lBut.generateUI();
            }
            levelToHighLight++;
        }
    }
}
