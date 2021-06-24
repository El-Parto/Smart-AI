using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GuiScript : MonoBehaviour
{
    private float speed = 5;
    [SerializeField]
    private Transform spin;
    [SerializeField]
    private Transform spin1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        spin.Rotate(Vector3.up, speed * .5f);
        spin1.Rotate(Vector3.right, speed * .5f);
    }


    public void PlayGame()
    {
        SceneManager.LoadScene("AiScene");
    }

    public void QuitGame()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif

    }
}
