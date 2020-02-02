using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class load : MonoBehaviour
{

    public string text;

    public void hello()
    {
        SceneManager.LoadScene(text);
    }
}
