using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CircleChunkLoader: ChunkLoader {

	public override Vector2[] GetChunkPositions (float chunkSize) {
		Vector2 position = this.position;
		Vector2 chunkPosition;
		List<Vector2> result = new List<Vector2> ();

		for (int x = -renderDistance; x <= renderDistance; ++x) {
			for (int y = -renderDistance; y <= renderDistance; ++y) {
				chunkPosition = new Vector2 (
					gameObject.transform.position.x + x * chunkSize,  
					gameObject.transform.position.z + y * chunkSize
				);
				if (Vector2.Distance (chunkPosition, position) <= renderDistance * chunkSize) {
					result.Add (chunkPosition);
				}
			}
		}
		return result.ToArray ();
	}

	public override bool CanRelaseChunk (float chunkSize, Vector2 chunkPosition) {
		return Vector2.Distance (chunkPosition, position) > renderDistance * chunkSize;
	}
}