using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

public class HallGenerator : MonoBehaviour
{
    public GameObject straightCorridor, cornerCorridor, bossCorridor, endingCorridor;
    public int corridorLength = 10, segmentLength = 5;
    public float chanceToTurn = 0.1f;
    public LayerMask corridorMask;

    //public List<Vector3> pathPositions = new List<Vector3>();
    public List<GameObject> generatedCorridors = new List<GameObject>();
    Vector3 direction, lastDirection;
    Vector3 currentPosition;
    
    int failedAttempts = 0;

    Vector3 exitPosition;
    
    void Start()
    {
        GenerateHall();
    }


    public void GenerateHall() {
        ClearDungeon();

        GameManager.instance.maxRooms = corridorLength;
        GameManager.instance.enemeyHealths.Clear();

        direction = Vector3.right;
        currentPosition = transform.position + direction * segmentLength; //skip spawn
        //pathPositions.Add(currentPosition);
        int[] bossRooms = {
            corridorLength / 3,
            corridorLength / 3 * 2,
            corridorLength
        };

        int hallLevel = 0;

        for (int i = 0; i < corridorLength; i++) {
            bool corner = false;

            if (failedAttempts > 100) {
                Debug.LogError("Generation failed at position: " + i);
                GenerateHall();
            }

            if (Random.value < chanceToTurn && !bossRooms.Contains(i + 1)) {
                corner = ChangeDirection();
            }

            GameObject corridor;
            if (bossRooms.Contains(i + 1)) {
                corridor = Instantiate(bossCorridor, currentPosition, Quaternion.Euler(new Vector3(0, 0, StraightRotationFromDirection())));
                corridor.GetComponent<CorridorController>().hallLevel = hallLevel;
                hallLevel++;
            } else if (corner == true) {
                corridor = Instantiate(cornerCorridor, currentPosition, Quaternion.Euler(new Vector3(0, 0, CornerRotationFromDirection())));
                corridor.GetComponent<CorridorController>().hallLevel = hallLevel;
            } else {
                corridor = Instantiate(straightCorridor, currentPosition, Quaternion.Euler(new Vector3(0, 0, StraightRotationFromDirection())));
                corridor.GetComponent<CorridorController>().hallLevel = hallLevel;
            }

            corridor.transform.parent = transform;
            generatedCorridors.Add(corridor);
            //pathPositions.Add(currentPosition);
            corridor.GetComponent<CorridorController>().roomNumber = i;

            currentPosition = currentPosition + direction * segmentLength;
        }

        GameObject end = Instantiate(endingCorridor, currentPosition, Quaternion.Euler(new Vector3(0, 0, StraightRotationFromDirection())));
        generatedCorridors.Add(end);
        //pathPositions.Add(currentPosition);
        end.transform.parent = transform;
        end.GetComponent<CorridorController>().roomNumber = corridorLength;

        exitPosition = currentPosition;
    }

    float StraightRotationFromDirection() {
        if (direction == Vector3.right) return 0f;
        if (direction == Vector3.left) return 180f;
        if (direction == Vector3.up) return 90f;
        if (direction == Vector3.down) return -90f;

        return 0f;
    }

    float CornerRotationFromDirection() {
        if (lastDirection == Vector3.right) {
            if (direction == Vector3.up) {
                return 180f; 
            } else if (direction == Vector3.down) {
                return -90f; 
            }
        }
        else if (lastDirection == Vector3.left) {
            if (direction == Vector3.up) {
                return 90f; 
            } else if (direction == Vector3.down) {
                return 0f; //
            }
        } 
        else if (lastDirection == Vector3.up) {
            if (direction == Vector3.right) {
                return 0f; 
            } else if (direction == Vector3.left) {
                return -90f; 
            }
        } 
        else if (lastDirection == Vector3.down) {
            if (direction == Vector3.right) {
                return 90f; 
            } else if (direction == Vector3.left) {
                return 180f;
            }
        }
        return 0f; 
    }

    bool ChangeDirection() {
         Vector3[] possibleDirections = {
            Vector3.right,
            Vector3.left,
            Vector3.up,
            Vector3.down
        };

        Vector3 newDirection = possibleDirections[Random.Range(0,4)];

        if (newDirection == direction || IsPathBlocked(currentPosition + newDirection * segmentLength, newDirection)) {
            return false;
        } else {
            lastDirection = direction;
            direction = newDirection;
            return true;
        }
    }

    bool IsPathBlocked(Vector3 start, Vector3 direction) {
        RaycastHit2D hit = Physics2D.Raycast(start, direction, Mathf.Infinity, corridorMask);
        if (hit.collider != null)
        {
            return true;
        }
        return false;
    }

    public void ClearDungeon() {
        foreach (GameObject corridor in generatedCorridors) {
            DestroyImmediate(corridor);
            Debug.Log("Something went wrong?");
        }
        
        failedAttempts = 0;

        generatedCorridors.Clear();
        //pathPositions.Clear();
    }
}
