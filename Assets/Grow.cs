using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grow : MonoBehaviour {
    public float treeHeightStart=1;
    public float treeWidthStart=0.25f;
    public float growthRateHeight = 0.5f; // Average Rate at which length growth happens
    public float growthRateWidth = 0.05f; // Average Rate at which width growth happens
    public float growthRateVariance = 0.1f; //Variance from growth Rate individual branches will grow
    public float branchStartLength=0.75f; //Tree will begin to branch once its reached this Length
    public int minLeafBuds=3; //Minimum Buds in a Leaf Bud Cluster
    public int maxLeafBuds=5; //Maximum Buds in a Leaf Bud Cluster
    public float fullLeafSize=.2f; //Full Size a leaf will grow to in Meters (.2m = 20cm ~= 8inches)
    public float chanceOfBud = .05f; //percent chance of a Bud
    public float buddingRate = 5f; //Every 5 seconds bud leaves.
    public float budMaxGrowthAngle = 45f; //Maximum angle from parallel new buds will sprout
    public float budGrowthRate = .01f; //How much the bud grows per update;
    public float leafStopLength=2; //branch will stop making leaves at this Length //CONSIDER having this happen when the trunk starts to get big
    public float trunkStartHeight=2; //Or width? //At this Length/Width the branch is considered a Trunk growth will switch to slower length growth
    public float trunkGrowthModifier=0.5f; //Once a tree is a Trunk slow length growth by this amount
    public float trunkMinGrowthAngle = 15; //Minimum angle from parallel new growth will sprout
    public float trunkMaxGrowthAngle=30; //Maximum angle from parallel new growth will sprout
    public int maxBranches=2;
    public float chanceOfBranch = .05f; //percent chance of Branch
    public bool isTrunk=false;

    public GameObject rootPoint;
    private GameObject budPoint;

    public GameObject branchModel;
    public GameObject leafBudModel;
    public GameObject leafModel;
    //public GameObject flowerModel;

    private int branchID=0;
    private int numBranches=0;
    private Transform parentBranch;
    private float nextBudTime=0;
    

	// Use this for initialization
	void Start ()
    {
        branchModel = transform.root.GetComponentInChildren<Grow>().branchModel;
    }
	
	// Update is called once per frame
	void Update ()
    {
        /*
        if(isTrunk && rootPoint==null)
        {
            rootPoint = new GameObject("RootPoint");
            rootPoint.transform.parent = gameObject.transform.parent.transform;

        }
        */

        ///////GROW THIS BRANCH
        GrowBranch();

        //////GROW NEW BRANCH
        SproutNewBranch();

        ///////GROW LEAF BUDS
        if (Random.Range(0f, 100f) < chanceOfBud)
        {
            SproutLeafBuds();

            //SproutQuickBuds();
        }
        ///////
        if (Time.time>=nextBudTime)
        {
            //Debug.Log("Budding at time: " + Time.time);
            //Do Bud stuff
            //nextBudTime = Time.time + buddingRate;
        }
    }
    private void SproutQuickBuds()
    {
        Debug.Log("Sprout Quick Buds");


    }
    private void SproutLeafBuds()
    {
        int numBuds = Random.Range(minLeafBuds, maxLeafBuds);
        Vector3 budOffset = Random.Range(0.85f, 1f) * gameObject.transform.localScale.y * rootPoint.transform.up;
        for (int i = 0; i < numBuds; i++)
        {


            GameObject thisBud = Instantiate(leafBudModel);
            thisBud.GetComponent<BudGrow>().budGrowthRate = budGrowthRate;

            thisBud.GetComponent<BudGrow>().budPoint = new GameObject("budPoint");
            thisBud.GetComponent<BudGrow>().budPoint.transform.parent = rootPoint.transform;
            thisBud.GetComponent<BudGrow>().budPoint.transform.position = rootPoint.transform.position;
            thisBud.GetComponent<BudGrow>().budPoint.transform.rotation = rootPoint.transform.rotation;

            thisBud.GetComponent<BudGrow>().budPoint.transform.position += budOffset; //NEED to Fix budPoint Location

            //Debug.Log(rootRotationVector);
            Vector3 budRotationVector = new Vector3(Random.Range(15f, budMaxGrowthAngle), Random.Range(0f, 360f), 0);
            thisBud.GetComponent<BudGrow>().budPoint.transform.localRotation = Quaternion.Euler(budRotationVector);

            thisBud.name = ("Bud");
            thisBud.transform.localScale = Vector3.zero;


            thisBud.transform.parent = gameObject.transform.parent;
            thisBud.GetComponent<BudGrow>().budPoint.transform.parent = gameObject.GetComponentInChildren<MeshRenderer>().transform;
        }
    }

    private void GrowBranch()
    {
        gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x + (growthRateWidth / 365), gameObject.transform.localScale.y + (growthRateHeight / 365), gameObject.transform.localScale.z + (growthRateWidth / 365));
        if (rootPoint)
        {
            gameObject.transform.parent.position = rootPoint.transform.position;
            gameObject.transform.parent.rotation = rootPoint.transform.rotation;
        }
    }
    private void SproutNewBranch()
    {
        if (gameObject.transform.localScale.y >= branchStartLength && numBranches < maxBranches && Random.Range(0f, 100f) <= chanceOfBranch)
        {
            //Debug.Log("growing new branch because this branch has reached " + gameObject.transform.localScale.y + "m long");
            Vector3 rootRotationVector = new Vector3(Random.Range(trunkMinGrowthAngle, trunkMaxGrowthAngle), Random.Range(0f, 360f), 0);
            GameObject thisTrunk = Instantiate(branchModel);
            GameObject thisBranch = (thisTrunk.GetComponentInChildren<Grow>().gameObject);

            thisBranch.GetComponent<Grow>().rootPoint = new GameObject("RootPoint");

            //thisBranch.GetComponent<Grow>().rootPoint.transform.localScale = Vector3.one;
            thisBranch.GetComponent<Grow>().rootPoint.transform.parent = rootPoint.transform;
            thisBranch.GetComponent<Grow>().rootPoint.transform.position = rootPoint.transform.position;
            thisBranch.GetComponent<Grow>().rootPoint.transform.rotation = rootPoint.transform.rotation;

            thisBranch.GetComponent<Grow>().rootPoint.transform.position += Random.Range(0.5f, 1f) * gameObject.transform.localScale.y * rootPoint.transform.up; //NEED to Fix RootPoint Location

            //Debug.Log(rootRotationVector);
            thisBranch.GetComponent<Grow>().rootPoint.transform.localRotation = Quaternion.Euler(rootRotationVector);

            thisBranch.GetComponent<Grow>().branchID = branchID + 1;
            thisTrunk.name = ("BranchBase " + branchID);
            thisBranch.transform.localScale = Vector3.zero;


            thisTrunk.transform.parent = gameObject.transform.parent;
            thisBranch.GetComponent<Grow>().rootPoint.transform.parent = gameObject.GetComponentInChildren<MeshRenderer>().transform;

            ++numBranches;
            //Debug.Log(numBranches+"/"+maxBranches);
            if (numBranches >= maxBranches)
            {
                Debug.Log("In Loop");
                growthRateHeight *= 0.1f;
                growthRateWidth *= 1f;
            }
        }
    }
}
