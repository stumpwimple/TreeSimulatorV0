using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BudGrow : MonoBehaviour {
    public float budGrowthRate;
    public GameObject budPoint;
    public GameObject leafPrefab;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        ///////GROW THIS BUD
        if (gameObject.transform.localScale.x < 1f)
        {
            gameObject.transform.localScale += Vector3.one * budGrowthRate;
        }
        else if(gameObject.transform.localScale.x >= 1f)
        {
            Debug.Log("Spawn Leaf");
            GameObject thisLeaf = Instantiate(leafPrefab);
            thisLeaf.transform.position = budPoint.transform.position;
            thisLeaf.transform.rotation = budPoint.transform.rotation;
            thisLeaf.transform.parent = budPoint.transform;
        }

        if (budPoint)
        {
            gameObject.transform.position = budPoint.transform.position;
            gameObject.transform.rotation = budPoint.transform.rotation;
        }
    }
}
