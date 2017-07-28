using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawnerScript : MonoBehaviour {

    public GameObject[] blockPrefabs;

    public void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("SPAWNING BLOCK");
            SpawnNewBlock();
            SpawnNewBlock();
            SpawnNewBlock();
            SpawnNewBlock();
            SpawnNewBlock();
            SpawnNewBlock();
            SpawnNewBlock();
            SpawnNewBlock();
        }
    }
    public void SpawnNewBlock()
    {
        // Spawn the block right here (and let rigidbody physics toss it)
        int whichPrefab = Random.Range(0, blockPrefabs.Length-1);
        GameObject.Instantiate(blockPrefabs[whichPrefab], this.transform.position, this.transform.rotation);
    }
}
