using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    /// <summary> �摜������gameObject </summary>
    public GameObject mainImage;
    /// <summary> �Q�[���I�[�o�[�摜 </summary>
    public Sprite gameOverSpr;
    /// <summary> �Q�[���N���A�摜 </summary>
    public Sprite gameClearSpr;
    /// <summary> �p�l�� </summary>
    public GameObject panel;
    /// <summary> restart�{�^�� </summary>
    public GameObject restartButton;
    /// <summary> next�{�^�� </summary>
    public GameObject nextButton;

    /// <summary> �摜��\�����Ă���C���[�W�R���|�[�l���g </summary>
    Image titleImage;

    // Start is called before the first frame update
    void Start()
    {
        // �摜���\���ɂ���
        Invoke("InactiveImage", 1.0f);
        // �{�^���i�p�l���j
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
