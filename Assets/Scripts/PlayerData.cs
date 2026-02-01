using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]

public class PlayerData
{

    public float[] positions;
    public int health;
    public string sceneName;
    public int score;

    public PlayerData()
    {
        Scene m_Scene = SceneManager.GetActiveScene();
        sceneName = m_Scene.name;
    }
    
    public class InventoryItem
    {
        public string itemName;
        public int quantity;
    }
}
