using Cysharp.Threading.Tasks.Triggers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    public static PlayerController instance;

    /// <summary> Rigidbody2D�^�̕ϐ� </summary>
    Rigidbody2D rbody;
    /// <summary> ���͗p�ϐ� </summary>
    private float _axisH = 0.0f;
    /// <summary> �ړ��ϐ� </summary>
    public float _speed = 3.0f;
    /// <summary> �W�����v�ϐ� </summary>
    public float _jump = 9.0f;
    /// <summary> ���n�ł��郌�C���[ </summary>
    public LayerMask groundLayer;
    /// <summary> �W�����v�J�n�t���O </summary>
    bool _gojump = false;
    /// <summary> �n�ʂɗ����Ă���t���O </summary>
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

    /// <summary> �Q�[���̏�� </summary>
    public static string _gameState = "playing";


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("�X�^�[�g");
        // Rigidbody2D���Ƃ�
        rbody = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();
        nowAnime = stopAnime;
        oldAnime = stopAnime;

        // �Q�[�����ɂ���
        _gameState = "playing";
    }

    // Update is called once per frame
    void Update()
    {
        // �Q�[�����ȊO�̔���Ń��\�b�h���̈ȉ������s���Ȃ�
        if (_gameState != "playing")
        {
            return;
        }
        //�@���������̓��͂��m�F����
        _axisH = Input.GetAxisRaw("Horizontal");
        //�@�����̒���
        if (_axisH > 0.0f)
        {
            // �E�����̌���
            transform.localScale = new Vector2(1, 1);
        }
        else if (_axisH < 0.0f)
        {
            // ���Ɉړ�
            // ���E�ɔ��]������
            transform.localScale = new Vector2(-1, 1);
        }
        // Player���W�����v������
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
        // ������
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
    /// ���x�X�V
    /// </summary>
    void FixedUpdate()
    {
        // �Q�[�����ȊO�̓��\�b�h���̈ȉ��̔�������s���Ȃ�
        if (_gameState != "playing")
        {
            return;
        }
        // �n�㔻��
        _onGround = Physics2D.Linecast(transform.position, transform.position - (transform.up * 1.1f), groundLayer);
        // �n�ʂ̏�������͑��x��0�ł͂Ȃ�
        if (_onGround || _axisH != 0)
        {
            // ���x���X�V����
            rbody.velocity = new Vector2(_axisH * _speed, rbody.velocity.y);
        }
        // �n�ʂ̏�ŃW�����v�L�[�������ꂽ
        if (_onGround && _gojump)
        {
            Debug.Log("�W�����v�{�^����������");
            // �W�����v�\
            Vector2 jumpPw = new Vector2(0, _jump);         //�����Ղ����邽�߂̃��\�b�h
            rbody.AddForce(jumpPw, ForceMode2D.Impulse);    //�u�ԓI�ȗ͂�������
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
    /// �W�����v���邽�߂̃��\�b�h
    /// </summary>
    public void Jump()
    {

        Debug.Log("�{�^��������");
        _gojump = true;         //�W�����v�t���O�����Ă�
    }

    /// <summary>
    /// �ڐG�J�n
    /// </summary>
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Goal")
        {
            // �S�[�����\�b�h���Ă�
            Goal();
        }
        else if (collision.gameObject.tag == "Dead")
        {
            // �Q�[���I�[�o�[���\�b�h���Ă�
            GameOver();
        }
    }

    /*private void OnCollisionStay2D(Collision2D collision)
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
    }*/

    /*private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "MoveBlock")
        {
            moveblock = false;
            Debug.Log("�~�߂܂��傤");
            MoveBlock movblock = targetMoveBlock.GetComponent<MoveBlock>();
            MoveBlock movblock1 = TargetMoveBlock.GetComponent<MoveBlock>();
            MoveBlock movblock2 = TArgetMoveBlock.GetComponent<MoveBlock>();
            movblock.Stop();
            movblock1.Stop();
            movblock2.Stop();
        }
    }*/

    /// <summary>
    /// �S�[�����\�b�h
    /// </summary>
    public void Goal()
    {
        Debug.Log("Goal");
        _gameState = "gameclear";
        // �Q�[����~���\�b�h���Ă�
        SceneManager.LoadScene("Goal Scene");
    }

    /// <summary>
    /// �Q�[���I�[�o�[���\�b�h
    /// </summary>
    public void GameOver()
    {
        Debug.Log("����");
        _gameState = "gameover";
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        // �v���C���[��������ɏグ��
        rbody.AddForce(new Vector2(0, 5), ForceMode2D.Impulse);
    }

    /// <summary>
    /// �Q�[����~���\�b�h
    /// </summary>
    void GameStop()
    {
        // Rigidbody2D���Ƃ��Ă���
        Rigidbody2D rbody = GetComponent<Rigidbody2D>();
        // ���x0�ŋ�����~
        rbody.velocity = new Vector2(0, 0);
    }
}
