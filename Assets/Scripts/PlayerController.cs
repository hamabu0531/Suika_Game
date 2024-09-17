using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{    
    public int score;
    public Text scoreText;
    public GameObject[] fruits;
    private float moveSpeed = 5;
    private Rigidbody2D[] rBs;
    private bool isNextFruit = true, isGameOver = false;
    private GameObject nextFruit, viewFruit;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        nextFruit = fruits[Random.Range(0, 5)];
        viewFruit = Instantiate(nextFruit, this.transform.position + Vector3.left * 1.2f, nextFruit.transform.rotation, this.transform);
        viewFruit.GetComponent<Rigidbody2D>().simulated = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameOver)
        {
            scoreText.text = score.ToString();
            // 移動
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                if (transform.position.x > -1.2f)
                {
                    transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
                }
            }
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                if (transform.position.x < 3.6f)
                {
                    transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
                }
            }

            // フルーツ落下
            if (Input.GetKeyDown(KeyCode.Space) && isNextFruit)
            {
                Destroy(viewFruit);
                Instantiate(nextFruit, this.transform.position + Vector3.left * 1.2f, nextFruit.transform.rotation);
                StartCoroutine(nextCycle());
            }
        }    
    }

    public void GameOver()
    {
        isGameOver = true;
        rBs = FindObjectsByType<Rigidbody2D>(FindObjectsSortMode.None);
        foreach (Rigidbody2D rB in rBs)
        {
            rB.simulated = false;
        }
        Debug.Log("Game Over");
    }
    IEnumerator nextCycle()
    {
        isNextFruit = false;
        yield return new WaitForSeconds(1);
        if (!isGameOver)
        {
            nextFruit = fruits[Random.Range(0, 5)];
            isNextFruit = true;

            // 雲の横に次のフルーツを表示
            viewFruit = Instantiate(nextFruit, this.transform.position + Vector3.left * 1.2f, nextFruit.transform.rotation, this.transform);
            viewFruit.GetComponent<Rigidbody2D>().simulated = false;
        }
    }
}
