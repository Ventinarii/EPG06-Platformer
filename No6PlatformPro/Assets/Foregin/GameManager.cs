using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public Text text;
    // Start is called before the first frame update

    private void Awake()
    {
        if (instance == null)
            instance = gameObject.GetComponent<GameManager>();
        else
            Destroy(gameObject);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static GameManager GetInstance()
    {
        return instance;
    }

    public void GameOver()
    {
        text.text = "Game Over";
    }
}
