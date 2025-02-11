using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_GameManager : MonoBehaviour
{
    [SerializeField] private Button backButton; 

    // Start is called before the first frame update
    void Start()
    {
        backButton.onClick.AddListener(()=> SceneLoader.Instance.LoadLevel("MainMenu"));
    }
}
