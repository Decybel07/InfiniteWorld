using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour {

	public GameObject player;
	public ChunkGenerator chunkGenerator;

	private Dictionary<Vector2, GameObject> chunkMap = new Dictionary<Vector2, GameObject>();

	void Start () {
		LoadChunks (GetChunkLoaders ());
	}

	void Update () {
		ChunkLoader[] chunkLoaders = GetChunkLoaders ();
		LoadChunks (chunkLoaders);
		RelaseChunks (chunkLoaders);
	}

	private ChunkLoader[] GetChunkLoaders() {
		List<ChunkLoader> result = new List<ChunkLoader> ();
		ChunkLoader chunkLoader;

		foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("ChunkLoader")) {
			chunkLoader = gameObject.GetComponent<ChunkLoader> ();
			if (chunkLoader != null) {
				result.Add (chunkLoader);
			}
		}
		return result.ToArray ();
	}

	private void LoadChunks(ChunkLoader[] chunkLoaders) {
		foreach (ChunkLoader chunkLoader in chunkLoaders) {
			foreach (Vector2 chunkPosition in chunkLoader.GetChunkPositions(chunkGenerator.chunkSize)) {
				MakeChunkAt (chunkPosition);
			}
		}
	}

	private void RelaseChunks(ChunkLoader[] chunkLoaders) {
		Queue<Vector2> chunkKeysToDelete = new Queue<Vector2> ();
		Vector2 position;
		GameObject chunk;
		bool canRelaseChunk;

		foreach (Vector2 chunkPosition in chunkMap.Keys) {
			canRelaseChunk = true;
			foreach (ChunkLoader chunkLoader in chunkLoaders) {
				canRelaseChunk &= chunkLoader.CanRelaseChunk (chunkGenerator.chunkSize, chunkPosition);
			}
			if (canRelaseChunk) {
				chunkKeysToDelete.Enqueue (chunkPosition);
			}
		}

		while (chunkKeysToDelete.Count > 0) {
			position = chunkKeysToDelete.Dequeue ();
			chunk = chunkMap [position];
			chunkMap.Remove (position);
			Destroy (chunk);
		}
	}

	private void MakeChunkAt(Vector2 position) {
		position = new Vector2(RoundToChunkSize(position.x), RoundToChunkSize(position.y));

		if (!chunkMap.ContainsKey (position)) {
			GameObject chunk = chunkGenerator.CreateChunkAt (new Vector3 (position.x, 0.0f, position.y));
			chunk.transform.parent = gameObject.transform;
			chunkMap.Add (position, chunk);
		}
	}

	private float RoundToChunkSize(float value) {
		return Mathf.RoundToInt (value / chunkGenerator.chunkSize) * chunkGenerator.chunkSize;
	}
}
