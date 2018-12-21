using UnityEngine;

public class Swarm : MonoBehaviour
{
	public Transform boidPrefab;
	public int swarmCount = 100;


	void Start()
	{
		for (var i = 0; i < swarmCount; i++)
		{
			var boid = Instantiate(boidPrefab, Random.insideUnitSphere * 25, Quaternion.identity) as Transform;
			boid.parent = transform;
		}
	}
}
