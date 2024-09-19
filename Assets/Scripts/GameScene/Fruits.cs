using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.RenderGraphModule;

public class Fruits : MonoBehaviour
{
    public bool isDestroyed = false;
    public int fruitsIndex;
    public GameObject[] fruitPrefab;
    private int mergePoints;
    private float fruitsMass;
    private bool canUpdate = false;
    private PlayerController pController;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // 質量を決める数学的関数
        fruitsMass = 100.0f / (fruitsIndex + 3);
        this.GetComponent<Rigidbody2D>().mass = fruitsMass;
        mergePoints = fruitsIndex * 10;
        this.tag = "fruits" + fruitsIndex;
        pController = GameObject.Find("Player").GetComponent<PlayerController>();
        
    }

    // Update is called once per frame
    private void Update()
    {
        if (!canUpdate) return;

        if (this.transform.position.y > 1.9f)
        {
            pController.GameOver();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        canUpdate = true;
        Fruits otherFruitScript = collision.gameObject.GetComponent<Fruits>();

        // どちらかがすでに破壊されていたら何もしない
        if (isDestroyed || (otherFruitScript != null && otherFruitScript.isDestroyed))
        {
            return;
        }

        // くっついた時(片方)
        if (collision.gameObject.CompareTag("fruits" + fruitsIndex))
        {
            Vector3 fruits1Pos = gameObject.transform.localPosition;
            Vector3 fruits2Pos = collision.gameObject.transform.localPosition;
            Vector3 instantiatePos = (fruits1Pos + fruits2Pos) / 2;

            // スコア更新
            pController.score += mergePoints;

            // 元の2つを削除
            isDestroyed = true;  // フラグを立てておく
            Destroy(gameObject);
            Destroy(collision.gameObject);

            // スイカの場合
            if (collision.gameObject.CompareTag("fruits11"))
            {
                pController.score += 100;
                return;
            }

            // 1ランク上のフルーツを生成
            Instantiate(fruitPrefab[fruitsIndex], instantiatePos, fruitPrefab[fruitsIndex].transform.rotation);
        }
    }
}
