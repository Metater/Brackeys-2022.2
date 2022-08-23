using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float orbitalSelectionRadiusRange = 0.5f;
    [SerializeField] private float rockDroppableMaxScale = 1.5f;

    [SerializeField] private List<Rock> rockPrefabs;

    [SerializeField] private List<RockOrbital> orbitals;

    [SerializeField] private int rocksCount;

    private int cachedRocksCount;
    private List<Rock> groundedRocks;

    private Rock selectedRock = null;
    private Vector3 selectedRockScale;

    private void Awake()
    {
        cachedRocksCount = rocksCount;
        groundedRocks = new();
    }

    private void Update()
    {
        if (cachedRocksCount != rocksCount)
        {
            cachedRocksCount = rocksCount;
            orbitals[0].RemoveAll();
            AddRocks();
        }

        if (selectedRock == null && Input.GetMouseButtonDown(0) && GetSelectedRock(out var rock))
        {
            if (!rock.IsGrounded)
            {
                rock.Orbital.Remove(rock);
            }

            selectedRock = rock;
            selectedRockScale = rock.transform.localScale;
            rock.SetGrounded();
        }
        else if (selectedRock != null && Input.GetMouseButton(0))
        {
            var cursorWorldPos = GetCursorWorldPosition();
            selectedRock.transform.position = cursorWorldPos;

            if (GetSelectedOrbital(out var orbital) && orbital.Capacity >= orbital.rocks.Count + 1)
            {
                selectedRock.transform.localScale = Vector3.Lerp(selectedRockScale, selectedRockScale * rockDroppableMaxScale, Mathf.Abs(Mathf.Sin(Time.time * 4)));
            }
            else
            {
                selectedRock.transform.localScale = selectedRockScale;
            }
        }
        else if (selectedRock != null && Input.GetMouseButtonUp(0))
        {
            if (GetSelectedOrbital(out var orbital) && orbital.Capacity >= orbital.rocks.Count + 1)
            {
                selectedRock.SetOrbital(orbital);
            }
            else
            {
                groundedRocks.Add(selectedRock);
            }

            selectedRock.transform.localScale = selectedRockScale;
            selectedRock = null;
        }
    }

    private bool GetSelectedOrbital(out RockOrbital orbital)
    {
        orbital = null;

        var cursorWorldPos = GetCursorWorldPosition();

        foreach (var o in orbitals)
        {
            var distance = Vector2.Distance(player.position, cursorWorldPos);
            var radius = o.GetRadius();
            if (distance >= Mathf.Max(0, radius - orbitalSelectionRadiusRange) && distance <= radius + orbitalSelectionRadiusRange)
            {
                orbital = o;
                return true;
            }
        }

        return false;
    }

    private bool GetSelectedRock(out Rock rock)
    {
        rock = null;

        var cursorWorldPos = GetCursorWorldPosition();

        foreach (var o in orbitals)
        {
            foreach (var r in o.rocks)
            {
                if (Vector2.Distance(r.transform.position, cursorWorldPos) <= r.dragRadius)
                {
                    rock = r;
                    return true;
                }
            }
        }

        foreach (var r in groundedRocks)
        {
            if (Vector2.Distance(r.transform.position, cursorWorldPos) <= r.dragRadius)
            {
                rock = r;
                return true;
            }
        }

        return false;
    }

    private void AddRocks()
    {
        for (int i = 0; i < rocksCount; i++)
        {
            var rock = Instantiate(rockPrefabs[0], Vector3.zero, Quaternion.identity, transform);
            rock.Init(player);
            rock.SetOrbital(orbitals[0]);
        }
    }

    private Vector2 GetCursorWorldPosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
