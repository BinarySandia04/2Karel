using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectorButtonManager : MonoBehaviour
{
    public GameObject levelManager;
    public GameObject gameManager;
    public GameObject messageManager;
    [Space]
    public string mapName;
    public Sprite png;
    public float dif;
    public Texture2D map;
    public int levelId;
    [Space]
    public bool isContinueTarget;
    public string cacheCode; // TODO: Al cargar otro nivel, hacer que el codigo guardado de la ultima sesion
    // sea el que abandono el usuario antes. Facil xd

    public void generateUI()
    {
        transform.Find("LevelText").GetComponent<TextMeshProUGUI>().text = mapName;
        transform.Find("LevelDiff").GetComponent<TextMeshProUGUI>().text = "Difficulty: " + dif;
        transform.Find("Image").GetComponent<Image>().sprite = png;
    }

    public void StartLevel()
    {
        if (isContinueTarget)
        {
            gameManager.GetComponent<GameManager>().HideMainMenu();
            gameManager.GetComponent<GameManager>().currentLevel = levelId;
            MessageManager.MakeAMessage(mapName + '\n' + "Difficulty: " + dif, Color.yellow, 3f);
        } else
        {
            gameManager.GetComponent<GameManager>().HideMainMenu();
            gameManager.GetComponent<GameManager>().currentLevel = levelId;
            levelManager.GetComponent<LevelGenerator>().GenerateLevel(map);
            MessageManager.MakeAMessage(mapName + '\n' + "Difficulty: " + dif, Color.yellow, 3f);
        }
    }
}
