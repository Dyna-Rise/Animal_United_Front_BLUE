using System.Collections;
using UnityEngine;

public class BossController : MonoBehaviour
{
    // ボスの体力
    public int bossHp = 10;

    // 移動関連
    Coroutine assaultCol;      //急襲コルーチン情報の参照用
    Coroutine repositionCol;       // 逃亡コルーチン情報の参照用
    bool isOffScreen;    // 画面外フラグ
    public float targetHeight;      // 上下移動の距離
    bool moveOn;        // 移動フラグ
    Vector3 targetPosition;     // 目的地
    public float bossMoveSpeed = 3.0f;  // 移動スピード
    public float bossDamper = 0.9f;     // 移動力の減衰（戻る力）
    Vector3 velocity = Vector3.zero;    // 計算用


    // ボスのショット関連
    public GameObject bossShotPrefab;       // 弾のプレハブ
    public float bossShotSpeed = 5.0f;      // 弾速
    public float spreadAngle = 15f;         // 弾の広がる角度

    // ボスの近接攻撃関連
    public GameObject bossSlashPrefab;      // 対象スラッシュプレハブ

    // 各攻撃の発生場所
    Vector3 targetPoint;

    GameObject player;      // プレイヤー取得用

    void Start()
    {
        // playerを取得しておく
        player = GameObject.FindGameObjectWithTag("Player");
        // 画面外フラグをオンに
        isOffScreen = true;

    }


    void Update()
    {
        // 画面外フラグがON∧急襲コルーチンがnull
        if (isOffScreen &&  assaultCol == null)
        {
            // 急襲コルーチンの開始
            assaultCol = StartCoroutine(Assault());
        }
        // 画面外フラグがOFF∧逃亡コルーチンがnull
        else if (!isOffScreen && repositionCol == null)
        {
            // 逃亡コルーチンの開始
            repositionCol = StartCoroutine(Reposition());
        }



        // 移動
        if (moveOn)
        {
            // スピードを乗算した目標へのベクトルを作成
            Vector3 force = (targetPosition - transform.position) * bossMoveSpeed;
            // 慣性のように、前のフレームの速度（移動量）を引き継ぎ、フレームでの移動量を加える
            velocity += force * Time.deltaTime;
            // 摩擦のように、速度を常に減衰させる
            velocity *= bossDamper;

            // このフレームでの移動先
            transform.position += velocity * Time.deltaTime;

            // 目標に十分近づいたら近づいたら目標地点で止める、差の絶対値を比較する
            if (Mathf.Abs(transform.position.y - targetPosition.y) < 0.01f)
            {
                transform.position = targetPosition;
                velocity = Vector3.zero;
                moveOn = false; // 到着したらオフにする
            }

        }

    }

    // 急襲コルーチン
    IEnumerator Assault()
    {
        targetPosition = ((transform.position) - new Vector3(0f, targetHeight, 0f));
        moveOn = true;
        // 移動の時間待ち
        yield return new WaitForSeconds(2.0f);

        // 攻撃関連（近ければ近接、遠ければシュート）

        // 隙用の時間
        yield return new WaitForSeconds(2.0f);
        // 


        yield return new WaitForSeconds(2.0f);
        // 画面外フラグOFF、コルーチンを空に
        isOffScreen = false;
        assaultCol = null;


    }

    IEnumerator Reposition()
    {
        targetPosition = ((transform.position) + new Vector3(0f, targetHeight, 0f));
        moveOn = true;
        yield return new WaitForSeconds(2.0f);
        // 画面外フラグON、コルーチンを空に
        isOffScreen = true;
        repositionCol = null;
        // X軸をランダムに移動（固定から選択にする？）
    }



    void BossShoot()
    {
        // Playerと自身のX座標の差、Y座標の差
        float dx = player.transform.position.x - transform.position.x;
        float dy = player.transform.position.y - transform.position.y;

        // 正規化（大きさを1にする）
        Vector3 direction = new Vector3(dx, dy, 0).normalized;

        // 発射場所をプレイヤー側にする
        targetPoint = transform.position + direction;

        // 3つの方向を配列で定義（下、中央、上）
        float[] angles = { -spreadAngle, 0f, spreadAngle };

        foreach (float angle in angles)
        {
            // 方向を変化させる
            Vector3 spreadDirection = Quaternion.Euler(0, 0, angle) * direction;

            // 弾を生成
            GameObject bullet = Instantiate(bossShotPrefab, targetPoint, Quaternion.identity);

            // 弾に速度を与える
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.AddForce(spreadDirection * bossShotSpeed, ForceMode.Impulse);
        }
    }

    void BossSlash()
    {
        // Playerと自身のX座標の差、Y座標の差
        float dx = player.transform.position.x - transform.position.x;
        float dy = player.transform.position.y - transform.position.y;

        // 正規化（大きさを1にする）
        Vector3 direction = new Vector3(dx, dy, 0).normalized;

        // 発射場所をプレイヤー側にする
        targetPoint = transform.position + direction;

        // Slash を生成
        GameObject slash = Instantiate(bossSlashPrefab, targetPoint, Quaternion.identity);

    }
}
