using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DeathBarrierController : MonoBehaviour
{
    int currentTarget;

    public GameObject deathRemnant;

    List<GameObject> remnants;

    GameObject generator;
    List<GameObject> corridors;
    List<Vector3> positions = new List<Vector3>();

    void Start()
    {
        currentTarget = 0;
        generator = GameObject.FindGameObjectWithTag("Generator");
        corridors = generator.GetComponent<HallGenerator>().generatedCorridors;
        foreach (var trans in corridors) {
            positions.Add(trans.transform.position);
        }
        positions.Insert(0, Vector3.zero);
        remnants = new List<GameObject>();
    }


    void Update()
    {
        GameManager.instance.deathRoom = currentTarget - 1;
        
        if (currentTarget > positions.Count) {
            return;
        }

        float moveSpeed = GameManager.instance.fractureSpeedModifer;

        CorridorController bossCheck;
        if (currentTarget >= 1) {
            bossCheck = corridors[currentTarget - 1].GetComponent<CorridorController>();
            if (bossCheck.isBoss && !bossCheck.destroyed) {
                moveSpeed *= .2f;
            }
        }

        

        

        Vector3 targetPosition = positions[currentTarget];
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(targetPosition, transform.position) <= 0.01f) {
            currentTarget++;
            GameObject remnant = Instantiate(deathRemnant, targetPosition, Quaternion.identity);
            remnant.transform.parent = transform.parent;
            remnants.Add(remnant);
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        Health health = collision.gameObject.GetComponent<Health>();
        if (health != null && collision.gameObject.tag == "Player") {
            health.ChangeHealth(-5 * Time.deltaTime * 10);
        }
    }
}
