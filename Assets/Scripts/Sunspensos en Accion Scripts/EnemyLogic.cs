using UnityEngine;
using System.Collections;

public class EnemyLogic : MonoBehaviour 
{

    public Animator anim;
    bool isDead;
    public float counter = 1;
    public GameObject enemy;
    public GameObject enemyCollisions;

	// Use this for initialization
	void Start () 
    {
        //anim = GetComponent<Animator>();
        isDead = false;
	}
	
	// Update is called once per frame
	void Update () 
    {

	}

    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.tag == "Attack1")
            {
                anim.SetTrigger("RayDie");
                enemyCollisions.SetActive(false);
            }
    }
        
}
