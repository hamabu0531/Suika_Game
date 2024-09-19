using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{    
    public int score;
    public Text scoreText;
    public GameObject[] fruits;
    public float leftBound, rightBound;
    private float moveSpeed = 5;
    private Rigidbody2D[] rBs;
    private bool isNextFruit = true, isGameOver = false;
    private GameObject nextFruit, viewFruit;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        nextFruit = fruits[Random.Range(0, 5)];
        viewFruit = Instantiate(nextFruit, this.transform.position + Vector3.left * 0.96f, nextFruit.transform.rotation, this.transform);
        viewFruit.GetComponent<Rigidbody2D>().simulated = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameOver)
        {
            scoreText.text = score.ToString();

            Debug.Log(Input.touchCount);

            Touch[] touches = Input.touches;
            ///*
            // 移動 (PC操作用)
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                if (transform.position.x > leftBound)
                {
                    transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
                }
            }
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                if (transform.position.x < rightBound)
                {
                    transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
                }
            }
            if (Input.GetKeyDown(KeyCode.Space) && isNextFruit)
            {
                Destroy(viewFruit);
                Instantiate(nextFruit, this.transform.position + Vector3.left * 0.96f, nextFruit.transform.rotation);
                StartCoroutine(nextCycle());
            }
            //*/

            
            if (touches.Length != 0)
            {
                // 移動
                Vector2 newPos = new Vector2((touches[0].position.x) / 135f - 2.1f, 2.9f);

                if (newPos.x < leftBound)
                {
                    newPos.x = leftBound;
                }
                if (newPos.x > rightBound)
                {
                    newPos.x = rightBound;
                }

                transform.position = newPos;

                // フルーツ落下
                if (touches[0].phase == TouchPhase.Ended && isNextFruit)
                {
                    Destroy(viewFruit);
                    Instantiate(nextFruit, this.transform.position + Vector3.left * 0.96f, nextFruit.transform.rotation);
                    StartCoroutine(nextCycle());
                }
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
        // ゲームオーバー処理を後ほど記述
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
            viewFruit = Instantiate(nextFruit, this.transform.position + Vector3.left * 0.96f, nextFruit.transform.rotation, this.transform);
            viewFruit.GetComponent<Rigidbody2D>().simulated = false;
        }
    }
}
