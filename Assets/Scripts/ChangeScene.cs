using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    /// <summary> �ǂݍ��ރV�[���� </summary>
    public string sceneName;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// �V�[����ǂݍ���
    /// </summary>
    public void Load()
    {
        SceneManager.LoadScene(sceneName);
    }
}
