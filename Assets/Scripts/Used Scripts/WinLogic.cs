using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLogic : MonoBehaviour {

    public bool ignoreTrigger;

    public PlayerMOD Player;

    void OnTriggerEnter2D(Collider2D other)
    {

        if (ignoreTrigger)
            return;

        if (other.tag == "Player")
        {
            //SceneManager.LoadScene ("Menu");
            Player.isLevelEnded = true;
            Debug.Log("Win");
        }

    }
}
