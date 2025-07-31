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
    public GridManager gridManager;
    // Amount of times gravity should pull down before the snake is considered over the void (0 to turn off gravity)
    public int maximumGravityChecks = 25;
    public void Start()
    {
        formerPositions.Add(Vector2Int.zero);
        formerPositions.Add(Vector2Int.zero);
        formerPositions.Add(Vector2Int.zero);
        formerPositions.Add(Vector2Int.zero);
        formerPositions.Add(Vector2Int.zero);
        formerPositions.Add(Vector2Int.zero);
        formerPositions.Add(Vector2Int.zero);
        AddSegment();
        AddSegment();
        AddSegment();
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
            var p = (Vector3Int)position;
            if (!gridManager.WallAt(p) &&
                !SelfAt(p))
            {
                formerPositions.Add(position);
                MoveSegments();
                Gravity(maximumGravityChecks);
            }
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
            var pos = (Vector3)(Vector3Int)formerPositions[formerPositions.Count - 1 - i];
            pos.x += 0.5f;
            pos.y += 0.5f;
            segments[i].transform.position = pos;
        }
    }
    public bool Gravity(int count)
    {
        if (count == 0)
        {
            return false;
        }
        foreach (var segment in segments)
        {
            if (gridManager.WallAt(Down((Vector2Int)Vector3Int.FloorToInt(segment.transform.position))))
            {
                return true;
            }
        }
        List<Vector2Int> newPoses = new List<Vector2Int>();
        foreach (var segment in segments)
        {
            newPoses.Add((Vector2Int)Down(Vector2Int.FloorToInt((Vector2)segment.transform.position)));
        }
        newPoses.Reverse();
        formerPositions.AddRange(newPoses);
        MoveSegments();
        return Gravity(count - 1);
    }
    private Vector3Int Up(Vector2Int pos)
    {
        var r = pos;
        r.y += 1;
        return (Vector3Int)r;
    }
    private Vector3Int Down(Vector2Int pos)
    {
        var r = pos;
        r.y -= 1;
        return (Vector3Int)r;
    }
    private Vector3Int Left(Vector2Int pos)
    {
        var r = pos;
        r.x -= 1;
        return (Vector3Int)r;
    }
    private Vector3Int Right(Vector2Int pos)
    {
        var r = pos;
        r.x += 1;
        return (Vector3Int)r;
    }
    private bool SelfAt(Vector3Int pos)
    {
        foreach (var segment in segments)
        {
            if (Vector3Int.FloorToInt(segment.transform.position) == pos)
            {
                return true;
            }
        }
        return false;
    }
}
