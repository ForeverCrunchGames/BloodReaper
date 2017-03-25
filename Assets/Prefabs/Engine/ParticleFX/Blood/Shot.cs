using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Shot : MonoBehaviour
{

    [SerializeField]
    private ParticleSystem blood;



    [SerializeField]
    private int minDrops = 3;
    [SerializeField]
    private int maxDrops = 10;

    private Text text;
    private AudioSource shot;

    // Use this for initialization
    void Start()
    {
        text = GetComponent<Text>();
        shot = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            blood.Emit(Random.Range(minDrops, maxDrops));
            shot.Play();
            text.enabled = false;
        }
    }
}
