using Cysharp.Threading.Tasks.Triggers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    public static PlayerController instance;

    /// <summary> Rigidbody2D型の変数 </summary>
    Rigidbody2D rbody;
    /// <summary> 入力用変数 </summary>
    private float _axisH = 0.0f;
    /// <summary> 移動変数 </summary>
    public float _speed = 3.0f;
    /// <summary> ジャンプ変数 </summary>
    public float _jump = 9.0f;
    /// <summary> 着地できるレイヤー </summary>
    public LayerMask groundLayer;
    /// <summary> ジャンプ開始フラグ </summary>
    bool _gojump = false;
    /// <summary> 地面に立っているフラグ </summary>
    bool _onGround = false;

    public GameObject targetMoveBlock;
    public GameObject TargetMoveBlock;
    public GameObject TArgetMoveBlock;
    public bool moveblock = false;

    Animator animator;
    public string stopAnime = "PlayerStop";
    public string moveAnime = "PlayerMove";
    public string blockAnime = "PlayerBlock";
    public string deadAnime = "PlayerOver";
    string nowAnime = "";
    string oldAnime = "";

    /// <summary> ゲームの状態 </summary>
    public static string _gameState = "playing";


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("スタート");
        // Rigidbody2Dをとる
        rbody = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();
        nowAnime = stopAnime;
        oldAnime = stopAnime;

        // ゲーム中にする
        _gameState = "playing";
    }

    // Update is called once per frame
    void Update()
    {
        // ゲーム中以外の判定でメソッド内の以下を実行しない
        if (_gameState != "playing")
        {
            return;
        }
        //　水平方向の入力を確認する
        _axisH = Input.GetAxisRaw("Horizontal");
        //　向きの調整
        if (_axisH > 0.0f)
        {
            // 右方向の向き
            transform.localScale = new Vector2(1, 1);
        }
        else if (_axisH < 0.0f)
        {
            // 左に移動
            // 左右に反転させる
            transform.localScale = new Vector2(-1, 1);
        }
        // Playerをジャンプさせる
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
        // 動かす
        if (Input.GetButtonDown("Fire3"))
        {
            moveblock = true;
        }
        if (Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    /// <summary>
    /// 速度更新
    /// </summary>
    void FixedUpdate()
    {
        // ゲーム中以外はメソッド内の以下の判定を実行しない
        if (_gameState != "playing")
        {
            return;
        }
        // 地上判定
        _onGround = Physics2D.Linecast(transform.position, transform.position - (transform.up * 1.1f), groundLayer);
        // 地面の上もしくは速度が0ではない
        if (_onGround || _axisH != 0)
        {
            // 速度を更新する
            rbody.velocity = new Vector2(_axisH * _speed, rbody.velocity.y);
        }
        // 地面の上でジャンプキーが押された
        if (_onGround && _gojump)
        {
            Debug.Log("ジャンプボタンをおした");
            // ジャンプ可能
            Vector2 jumpPw = new Vector2(0, _jump);         //じゃんぷさせるためのメソッド
            rbody.AddForce(jumpPw, ForceMode2D.Impulse);    //瞬間的な力を加える
            _gojump = false;
        }
        if (_onGround)
        {
            if (_axisH == 0)
            {
                nowAnime = stopAnime;
            }
            else
            {
                nowAnime = moveAnime;
            }
        }

        if (nowAnime != oldAnime)
        {
            oldAnime = nowAnime;
            animator.Play(nowAnime);
        }
    }

    /// <summary>
    /// ジャンプするためのメソッド
    /// </summary>
    public void Jump()
    {

        Debug.Log("ボタン推した");
        _gojump = true;         //ジャンプフラグを建てる
    }

    /// <summary>
    /// 接触開始
    /// </summary>
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Goal")
        {
            // ゴールメソッドを呼ぶ
            Goal();
        }
        else if (collision.gameObject.tag == "Dead")
        {
            // ゲームオーバーメソッドを呼ぶ
            GameOver();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "MoveBlock" && moveblock)
        {
            MoveBlock movblock = targetMoveBlock.GetComponent<MoveBlock>();
            MoveBlock movblock1 = TargetMoveBlock.GetComponent<MoveBlock>();
            MoveBlock movblock2 = TArgetMoveBlock.GetComponent<MoveBlock>();
            animator.Play(blockAnime);
            movblock.Move();
            movblock1.Move();
            movblock2.Move();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "MoveBlock")
        {
            moveblock = false;
            Debug.Log("止めましょう");
            MoveBlock movblock = targetMoveBlock.GetComponent<MoveBlock>();
            MoveBlock movblock1 = TargetMoveBlock.GetComponent<MoveBlock>();
            MoveBlock movblock2 = TArgetMoveBlock.GetComponent<MoveBlock>();
            movblock.Stop();
            movblock1.Stop();
            movblock2.Stop();
        }
    }

    /// <summary>
    /// ゴールメソッド
    /// </summary>
    public void Goal()
    {
        Debug.Log("Goal");
        _gameState = "gameclear";
        // ゲーム停止メソッドを呼ぶ
        SceneManager.LoadScene("Goal Scene");
    }

    /// <summary>
    /// ゲームオーバーメソッド
    /// </summary>
    public void GameOver()
    {
        Debug.Log("しんだ");
        _gameState = "gameover";
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        // プレイヤーを少し上に上げる
        rbody.AddForce(new Vector2(0, 5), ForceMode2D.Impulse);
    }

    /// <summary>
    /// ゲーム停止メソッド
    /// </summary>
    void GameStop()
    {
        // Rigidbody2Dをとってくる
        Rigidbody2D rbody = GetComponent<Rigidbody2D>();
        // 速度0で強制停止
        rbody.velocity = new Vector2(0, 0);
    }
}
