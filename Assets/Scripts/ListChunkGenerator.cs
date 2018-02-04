using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListChunkGenerator : ChunkGenerator {

	public GameObject[] chunkPrefabs;

	public override GameObject CreateChunkAt(Vector3 position) {
		int size = (int)Mathf.Sqrt (chunkPrefabs.Length);

		int x = (size + (int)(position.x / chunkSize) % size) % size;
		int y = (size + (int)(position.z / chunkSize) % size) % size;

		int index = y * size + x;
		return Instantiate (chunkPrefabs [index], position, Quaternion.identity);
	}
}
