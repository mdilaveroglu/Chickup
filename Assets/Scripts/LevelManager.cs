using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject player;
    public GameObject levelStart;
    public GameObject levelEnd;

    // Start is called before the first frame update

    private static LevelManager instance;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        } 
        else
        {
            Destroy(gameObject);
        }

        SpawnPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void SpawnPlayer()
    {
        Vector2 playerPosition = (Vector2) instance.levelStart.transform.position;
        Instantiate(instance.player, playerPosition, Quaternion.identity);
    }


}
