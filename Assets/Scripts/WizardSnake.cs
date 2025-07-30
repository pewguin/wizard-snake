using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngineInternal;

public class WizardSnake : MonoBehaviour
{
    public List<Vector2Int> formerPositions = new List<Vector2Int>();
    public List<GameObject> segments = new List<GameObject>();
    public GameObject segmentPrefab;
    public void Start()
    {
        formerPositions.Add(Vector2Int.zero);
        formerPositions.Add(Vector2Int.zero);
        formerPositions.Add(Vector2Int.zero);
        formerPositions.Add(Vector2Int.zero);
        AddSegment();
        AddSegment();
        AddSegment();
        AddSegment();
    }
    public void Update()
    {
        bool pressed = false;
        Vector2Int position = formerPositions[formerPositions.Count - 1];
        if (Input.GetKeyDown(KeyCode.W))
        {
            position.y++;
            pressed = true;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            position.y--;
            pressed = true;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            position.x++;
            pressed = true;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            position.x--;
            pressed = true;
        }
        if (pressed)
        {
            formerPositions.Add(position);
            MoveSegments();
        }
    }
    public void AddSegment()
    {
        var g = Instantiate(segmentPrefab, transform);
        segments.Add(g);
    }
    public void MoveSegments()
    {
        for (int i = 0; i < segments.Count; i++)
        {
            segments[i].transform.position = (Vector3Int)formerPositions[formerPositions.Count - 1 - i];
        }
    }
}
