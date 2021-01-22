using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool gameIsPlaying;
    public bool playerIsGrounded;
    void Start()
    {
        if (Instance)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (gameIsPlaying) Cursor.lockState = CursorLockMode.Locked;

    }
}
