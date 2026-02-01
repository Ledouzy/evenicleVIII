using UnityEngine;


public class PlayerDataManager : MonoBehaviour
{

    public Transform playerTransform;
    public int health;

    public string sceneName;
    public class InventoryItem
    {
        public string itemName;
        public int quantity;
    }
    public int score;

    void Update()
    {
        //Debug.Log("Player Position: " + playerTransform.position);
    }

    // Save the current game state to a JSON file, does not contain inventory yet, don't touch, currently only stock player data, will store boss data later
   public void SaveGame()
    {
        PlayerData data = new PlayerData();


        data.positions = new float[] { playerTransform.position.x, playerTransform.position.y, playerTransform.position.z };
        data.health = health;
        data.score = score;
        data.sceneName = sceneName;
        string json = JsonUtility.ToJson(data);
        string path = Application.persistentDataPath + "/savefile.json";
        System.IO.File.WriteAllText(path, json);

    }

    // Load the current game state to a JSON file, does not contain inventory yet, don't touch
    public void LoadGame()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if (System.IO.File.Exists(path))
        {
            string json = System.IO.File.ReadAllText(path);
            PlayerData data = JsonUtility.FromJson<PlayerData>(json);


            //Match current game state to loaded data
            PlayerRetention.LoadedPlayerData = data;
            health = data.health;
            score = data.score;
            UnityEngine.SceneManagement.SceneManager.LoadScene("InventoryScene");

        }
    }
}
