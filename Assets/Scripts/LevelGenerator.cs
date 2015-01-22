using UnityEngine;
using System.Collections;

public class LevelGenerator : MonoBehaviour {
   
    public Transform player;
    public GameObject levelObject1;
    public GameObject levelObject2;
    public GameObject levelObject3;
    public GameObject levelObject4;

    public int distance;

    GameObject lastInstantiatedGameObject;
	
    void Start () {
        levelObject1.transform.position = new Vector3(player.position.x, player.position.y-2, player.position.z);
        Instantiate(levelObject1);
        lastInstantiatedGameObject = levelObject1;
	}
	
	void Update () {
     
	  if(player.transform.position.x > lastInstantiatedGameObject.transform.position.x-10 )
      {
          GenerateObject(Random.Range(1, 5));
          GenerateObject(Random.Range(1, 5));
      }
      if(Input.GetKeyDown(KeyCode.R))
      {
          Application.LoadLevel(Application.loadedLevel);
      }
      
	}
    void GenerateObject(int nr)
    {
        switch(nr)
        {
            case 1:
                levelObject1.transform.position = new Vector3(lastInstantiatedGameObject.transform.position.x + Random.Range(15, 16), lastInstantiatedGameObject.transform.position.y + (Random.Range(0, 3)), player.position.z);
                Instantiate(levelObject1);
                lastInstantiatedGameObject = levelObject1;
                break;
            case 2:
                levelObject2.transform.position = new Vector3(lastInstantiatedGameObject.transform.position.x + Random.Range(15, 16), lastInstantiatedGameObject.transform.position.y + (Random.Range(0, 3)), player.position.z);
                Instantiate(levelObject2);
                lastInstantiatedGameObject = levelObject2;
                break;
            case 3:
                levelObject3.transform.position = new Vector3(lastInstantiatedGameObject.transform.position.x + Random.Range(15, 16), lastInstantiatedGameObject.transform.position.y + (Random.Range(0, 3)), player.position.z);
                Instantiate(levelObject3);
                lastInstantiatedGameObject = levelObject3;
                break;
            case 4:
                levelObject4.transform.position = new Vector3(lastInstantiatedGameObject.transform.position.x + Random.Range(15, 16), lastInstantiatedGameObject.transform.position.y + (Random.Range(0, 3)), player.position.z);
                Instantiate(levelObject4);
                lastInstantiatedGameObject = levelObject4;
                break;
        }

    }
}
