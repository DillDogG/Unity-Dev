using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : Interactable
{
    [SerializeField] float health;
    [SerializeField] GameObject pickupPrefab;

	public override void OnInteractActive(GameObject gameObject)
	{
		//if (gameObject.TryGetComponent(out PlayerShip player))
		//{
		//	player.ApplyHealth(health);
		//	if (pickupPrefab != null) Instantiate(pickupPrefab, transform.position, Quaternion.identity);
		//	Debug.Log("active");
		//	Destroy(gameObject);
		//}
	}

	public override void OnInteractEnd(GameObject gameObject)
	{
		//throw new System.NotImplementedException();
	}

	public override void OnInteractStart(GameObject gameObject)
	{
		if (gameObject.TryGetComponent(out PlayerShip player))
		{
			player.ApplyHealth(health);
			if (pickupPrefab != null) Instantiate(pickupPrefab, transform.position, Quaternion.identity);
			Destroy(gameObject);
		}
	}

	public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayerShip player))
        {
            player.ApplyHealth(health);
            if (pickupPrefab != null) Instantiate(pickupPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

}
