using UnityEngine;

public class GameManagerController : MonoBehaviour
{

    GameManager gameManager;
    public GameObject cellPrefab;
    float cellSpacing = 1.1f;


    /// <summary>
    /// Start is called once before the first execution of Update after the MonoBehaviour is created
    /// </summary>
    void Start()
    {
        gameManager = new GameManager(10, 10);
        gameManager.StartGame();
        //Instantiate(cellPrefab, new Vector3(1,0,0), Quaternion.identity);

        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                Instantiate(cellPrefab, new Vector3(j* cellSpacing - 4.95f, -i*cellSpacing + 4.95f, 0), Quaternion.identity);
            }
        }

    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        
    }
}
