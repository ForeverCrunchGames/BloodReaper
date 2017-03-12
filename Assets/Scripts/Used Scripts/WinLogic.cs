using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLogic : MonoBehaviour {

    PlayerMOD Player;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMOD>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            //SceneManager.LoadScene ("Menu");
            Player.isLevelEnded = true;
            Cursor.visible = true;
            Debug.Log("Win");
        }

    }
}
