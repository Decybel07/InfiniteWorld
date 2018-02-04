using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class ChunkLoader: MonoBehaviour {

	public int renderDistance = 3;

	protected Vector2 position {
		get {
			return new Vector2 (gameObject.transform.position.x, gameObject.transform.position.z);
		}
	}

	public abstract Vector2[] GetChunkPositions (float chunkSize);
	public abstract bool CanRelaseChunk (float chunkSize, Vector2 chunkPosition);
}
