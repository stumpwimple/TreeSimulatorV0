using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafBudScript : MonoBehaviour {
    public GameObject budPrefab;
    public GameObject flowerPrefab; 
    public GameObject leafPrefab;
    public int minBuds;
    public int maxBuds;
    public float fullLeafSize;




	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SpawnBuds()
    {
        int numBuds = Random.Range(minBuds, maxBuds);
        for (int i = 0; i < numBuds; i++)
        {
            GameObject thisBud = Instantiate(budPrefab);
            

        }

    }
}
