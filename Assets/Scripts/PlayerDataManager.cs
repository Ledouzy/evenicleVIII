using UnityEngine;


public class PlayerDataManager : MonoBehaviour
{

    public float[] position;
    public int health;
    public class InventoryItem
    {
        public string itemName;
        public int quantity;
    }
    public int score;

   public void SaveGame()
    {
        PlayerData data = new PlayerData();

        data.positions = new float[] { position[0], position[1], position[2] };
        data.health = health;
        data.score = score;

        string json = JsonUtility.ToJson(data);
        string path = Application.persistentDataPath + "/savefile.json";
        System.IO.File.WriteAllText(path, json);

    }

    public void LoadGame()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if (System.IO.File.Exists(path))
        {
            string json = System.IO.File.ReadAllText(path);
            PlayerData data = JsonUtility.FromJson<PlayerData>(json);


            //Match current game state to loaded data
            position[0] = data.positions[0];
            position[1] = data.positions[1];
            position[2] = data.positions[2];
            health = data.health;
            score = data.score;
        }
    }
}
