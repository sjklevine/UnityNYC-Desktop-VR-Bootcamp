using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KittenSpawner : MonoBehaviour {
	public GameObject kittenPrefab;

	public void SpawnKitten() {
		Vector3 randomDistance = new Vector3 (Random.insideUnitCircle.x, 0, Random.insideUnitCircle.y);
		Quaternion RotationRandomY = Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0));
		GameObject.Instantiate (kittenPrefab, this.transform.position + randomDistance, RotationRandomY);
	}
}
