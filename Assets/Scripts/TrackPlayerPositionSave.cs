using UnityEngine;

public class TrackPlayerPositionSave : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        float[] p = PlayerRetention.LoadedPlayerData.positions;
        transform.position = new Vector3(p[0], p[1], p[2]);

    }

}
