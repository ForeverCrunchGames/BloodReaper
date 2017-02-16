using UnityEngine;
using System.Collections;

public class CloudSpawner : MonoBehaviour {

	public GameObject cloud;


	// Use this for initialization
	void Start () {
		StartCoroutine (TheSpawn ());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator TheSpawn()
	{
		while (true) {

			yield return new WaitForSeconds(Random.Range(1,6));

			cloud.transform.localScale = new Vector2(Random.Range(0.5f,2f),Random.Range(0.5f,2f));

			GameObject clone = (GameObject) Instantiate(cloud, transform.position,Quaternion.identity);
			clone.GetComponent<Rigidbody2D>().velocity = Vector2.right* Random.Range(.5f,3f);

			Destroy(clone,20f);

				}

	}

}
