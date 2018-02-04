using UnityEngine;

public abstract class ChunkGenerator: MonoBehaviour {
	
	public float chunkSize;

	public abstract GameObject CreateChunkAt(Vector3 position);
}

