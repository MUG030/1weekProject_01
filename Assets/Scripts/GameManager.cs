using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    /// <summary> 画像を持つgameObject </summary>
    public GameObject mainImage;
    /// <summary> ゲームオーバー画像 </summary>
    public Sprite gameOverSpr;
    /// <summary> ゲームクリア画像 </summary>
    public Sprite gameClearSpr;
    /// <summary> パネル </summary>
    public GameObject panel;
    /// <summary> restartボタン </summary>
    public GameObject restartButton;
    /// <summary> nextボタン </summary>
    public GameObject nextButton;

    /// <summary> 画像を表示しているイメージコンポーネント </summary>
    Image titleImage;

    // Start is called before the first frame update
    void Start()
    {
        // 画像を非表示にする
        Invoke("InactiveImage", 1.0f);
        // ボタン（パネル）
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
