using System.Collections;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    public GameObject ThrowingObject;//投げるもの

    public float interval;//発射間隔
    public float speed;//発射速度
    public float angle;//発射角度
    public float count;//カウント
    public float length;//索敵範囲
    GameObject player; //索敵対象
    bool isShooting = false; //発射してない

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");　//プレイヤーをタグで検索して取得
    }

    // Update is called once per frame
    void Update()
    {
     
    }
}
