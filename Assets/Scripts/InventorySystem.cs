using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour {

    public GameObject item_collectionable;
    public GameObject item_key_blue;
    public GameObject item_key_yellow;
    public GameObject item_key_magenta;

//    int[] positions;
//    public GameObject[] icons;
//
//    public bool[] isPositionFree;
//
//    public Vector3 iconsPos;
//    public float spacer;
//    public int position;
//
    //    struct IconData{}

    // Use this for initialization
	void Start () 
    {
        item_collectionable.SetActive(false); 
        item_key_blue.SetActive(false);
        item_key_yellow.SetActive(false);
        item_key_magenta.SetActive(false);

//        for (int i = 0; i < isPositionFree.Length; i++)
//        {
//            isPositionFree[i] = true;
//        }
	}
	
	// Update is called once per frame
	void Update () 
    {
//        for (int i = 0; i < icons.Length; i++)
//        {
//            if (icons[i].activeSelf == true)
//            {
//                for (int j = 0; j < isPositionFree.Length; j++)
//                {
//                    if (isPositionFree[j] == true)
//                    {
//                        icons[i].transform.position.x = iconsPos.x + spacer * i; 
//                        isPositionFree[j] = false;
//                    }
//                }
//            }
//
//            if (icons[i].activeSelf == false)
//            {
//                
//            }
//        }

	}

    public void ItemColleted(int item)
    {
        if (item == 0)
        {
            item_collectionable.SetActive(true);
        }
        else if (item == 1)
        {
            item_key_blue.SetActive(true);                
        }
        else if (item == 2)
        {
            item_key_yellow.SetActive(true);                
        }
        else if (item == 3)
        {
            item_key_magenta.SetActive(true);                
        }
    }

    public void ItemUsed(int item)
    {
        if (item == 0)
        {
            item_collectionable.SetActive(false);                
        }
        else if (item == 1)
        {
            item_key_blue.SetActive(false);                
        }
        else if (item == 2)
        {
            item_key_yellow.SetActive(false);                
        }
        else if (item == 3)
        {
            item_key_magenta.SetActive(false);               
        }
    }
}
