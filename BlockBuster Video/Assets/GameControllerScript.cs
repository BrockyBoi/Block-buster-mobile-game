using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class GameControllerScript : MonoBehaviour {

    float sizes;
    float totalSize;
    float totalSizeX;
    float totalSizeY;

    Vector2 screenSize;

    List<PhysicsBlockScript> blockList;
	// Use this for initialization
	void Start () {

        screenSize = CameraScript.screenSize;
	}
	
	// Update is called once per frame
	void Update () {
        totalSize = PhysicsBlockScript.totalSize;
        float size = PhysicsBlockScript.size;
        blockList = PhysicsBlockScript.blockList;

        for(int i = 0; i < blockList.Count; i++)
        {
          sizes = 0;
          sizes +=  blockList[i].getSize();
        }
       totalSize = sizes;
        
        if (Input.GetKeyDown("space"))
        {
            Debug.Log("Total size: " + totalSize + "Size: " + size);
        }
        if (PhysicsBlockScript.totalSize > screenSize.x * screenSize.y)
        {
            Debug.Log("fucked it");
        }
	}
}
