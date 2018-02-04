using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SquareChunkLoader: ChunkLoader {

	public override Vector2[] GetChunkPositions (float chunkSize) {
		Vector2 position = this.position;
		List<Vector2> result = new List<Vector2> ();

		for (int x = -renderDistance; x <= renderDistance; ++x) {
			for (int y = -renderDistance; y <= renderDistance; ++y) {
				result.Add (new Vector2 (
					gameObject.transform.position.x + x * chunkSize,  
					gameObject.transform.position.z + y * chunkSize
				));
			}
		}
		return result.ToArray ();
	}

	public override bool CanRelaseChunk (float chunkSize, Vector2 chunkPosition) {
		return Mathf.Abs(gameObject.transform.position.x - chunkPosition.x) > chunkSize * renderDistance
			|| Mathf.Abs(gameObject.transform.position.z - chunkPosition.y) > chunkSize * renderDistance;
	}
}