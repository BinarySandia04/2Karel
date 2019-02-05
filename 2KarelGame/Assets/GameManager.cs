using System.Collections.Generic;
using UnityEngine;

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
    }

    public bool displayedMainMenu = true;
    [Space]
    [Header("Maps")]
    public List<Map> maps = new List<Map>();
    [Space]
    [Header("Ui elements")]
    public GameObject InGameMenu;
    public GameObject MainMenu;
    public GameObject Content;
    public GameObject LevelExamplePrefab;
    public GameObject LevelManager;
    public GameObject Canvas;

    // Start is called before the first frame update
    void Start()
    {
        ShowMainMenu();
        AddLevelMaps();
    }

    public void HideMainMenu()
    {
        // TODO: Insertar aqui transicion de desvanecimiento de ejemplo
        MainMenu.SetActive(false);
        MakeGameMenu();
        displayedMainMenu = false;
    }

    public void ShowMainMenu()
    {
        MainMenu.SetActive(true);
        DisableGameMenu();
        displayedMainMenu = true;
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

    private void AddLevelMaps()
    {
        foreach(Map map in maps)
        {
            GameObject newButton = Instantiate(LevelExamplePrefab);
            newButton.transform.SetParent(Content.transform);
            LevelSelectorButtonManager lBut = newButton.GetComponent<LevelSelectorButtonManager>();
            // Asignar cosas que hacen los botones a los botones que se crean a partir de botones

            lBut.dif = map.difficulty;
            lBut.map = map.mapPng;
            lBut.mapName = map.name;
            lBut.png = map.mapSprite;

            lBut.levelManager = LevelManager;
            lBut.gameManager = gameObject;

            lBut.generateUI();
        }
    }
}
