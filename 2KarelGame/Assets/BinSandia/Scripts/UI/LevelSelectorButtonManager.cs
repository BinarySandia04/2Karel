using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static GameManager;

public class LevelSelectorButtonManager : MonoBehaviour
{
    public LevelGenerator levelManager;
    public GameManager gameManager;
    public GameObject messageManager;
    [Space]
    public Map map;
    [Space]
    public bool isContinueTarget;
    public string cacheCode; // TODO: Al cargar otro nivel, hacer que el codigo guardado de la ultima sesion
    // sea el que abandono el usuario antes. Facil xd

    // PD: El Pau Pedrals m'ha dit que aixó optimitza.
    Transform trans;

    void Awake()
    {
        trans = transform;
    }
    // TODO: Soluciona este caos porfavor!
    public void generateUI()
    {
        trans.Find("LevelText").GetComponent<TextMeshProUGUI>().text = map.name;
        trans.Find("LevelDiff").GetComponent<TextMeshProUGUI>().text = "Difficulty: " + map.difficulty;
        trans.Find("Image").GetComponent<Image>().sprite = map.mapSprite;
    }

    public void StartLevel()
    {
        if (isContinueTarget)
        {
            gameManager.HideMainMenu();
            gameManager.currentLevelMap = map;

            MessageManager.MakeAMessage(map.name + '\n' + "Difficulty: " + map.difficulty, Color.yellow, 3f);
        } else
        {
            bool noExistsSavedCode = true;
            foreach(CodeData cd in gameManager.codeData)
            {
                if(cd.level == map.mapId)
                {
                    // Importar codigo
                    gameManager.SetTheCodeCode(cd.code);
                    noExistsSavedCode = false;
                }
            }
            if(noExistsSavedCode) gameManager.SetTheCodeCode("");
            gameManager.HideMainMenu();
            gameManager.currentLevel = map.mapId;
            KarelPlayer.CurrentCompilingPropieties = map.mapCompilingPropieties;
            levelManager.GenerateLevel(map.mapPng);
            MessageManager.MakeAMessage(map.name + '\n' + "Difficulty: " + map.difficulty, Color.yellow, 3f);
        }
    }
}
