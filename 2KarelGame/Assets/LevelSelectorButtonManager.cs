using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectorButtonManager : MonoBehaviour
{
    public GameObject levelManager;
    public GameObject gameManager;
    public GameObject messageManager;

    public string mapName;
    public Sprite png;
    public float dif;
    public Texture2D map;

    public void generateUI()
    {
        transform.Find("LevelText").GetComponent<TextMeshProUGUI>().text = mapName;
        transform.Find("LevelDiff").GetComponent<TextMeshProUGUI>().text = "Difficulty: " + dif;
        transform.Find("Image").GetComponent<Image>().sprite = png;
    }

    public void StartLevel()
    {
        gameManager.GetComponent<GameManager>().HideMainMenu();
        levelManager.GetComponent<LevelGenerator>().GenerateLevel(map);
        MessageManager.MakeAMessage(mapName + '\n' + "Difficulty: " + dif, Color.yellow, 3f);
    }
}
