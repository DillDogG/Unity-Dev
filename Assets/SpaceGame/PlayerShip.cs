using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : MonoBehaviour, IDamagable
{
	//[SerializeField] private IntEvent scoreEvent;
	[SerializeField] private Inventory inventory;
	//[SerializeField] private IntVariable score;
	[SerializeField] private FloatVariable health;

	[SerializeField] private GameObject hitPrefab;
	[SerializeField] private GameObject destroyPrefab;

    [SerializeField] Event playerDeadEvent = default;

    private void Start()
	{
		//scoreEvent.Subscribe(AddPoints);
		health.value = 100;
	}

	void Update()
	{
		if (Input.GetButtonDown("Jump"))
		{
			inventory.currentItem.Use();
		}
		if (Input.GetButtonUp("Jump"))
		{
			inventory.currentItem.StopUse();
		}
		if (Input.GetButtonDown("Fire2"))
		{
            inventory.SwapCurrent();
        }

		//pathFollower.speed (Input.GetKey(KeyCode.Space)) ? 2 : 1;
	}

	//public void AddPoints(int points)
	//{
	//	score.value += points;
	//	//Debug.Log(score.value);
	//}

	public void ApplyDamage(float damage)
	{
		health.value -= damage;
		if (health <= 0)
		{
			if (destroyPrefab != null)
			{
				Instantiate(destroyPrefab, gameObject.transform.position, Quaternion.identity);
			}
            playerDeadEvent.RaiseEvent();
            //Destroy(gameObject);
        }
		else
		{
			if (hitPrefab != null)
			{
				Instantiate(hitPrefab, gameObject.transform.position, Quaternion.identity);
			}
		}
	}

    public void ApplyHealth(float health)
	{
		this.health.value += health;
		this.health.value = Mathf.Min(this.health, 100);
	}
}
