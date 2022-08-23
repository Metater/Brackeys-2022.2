using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;

    [SerializeField] private List<Rock> rockPrefabs;

    [SerializeField] private List<RockOrbital> orbitals;

    [SerializeField] private int rocksCount;

    private int cachedRocksCount;
    private List<Rock> groundedRocks;
    private Rock selectedRock = null;

    private void Awake()
    {
        cachedRocksCount = rocksCount;
        groundedRocks = new();
        AddRocks();
    }

    private void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        rb.velocity = speed * new Vector3(x, y);

        if (cachedRocksCount != rocksCount)
        {
            cachedRocksCount = rocksCount;
            orbitals[0].RemoveAll();
            AddRocks();
        }

        if (selectedRock == null && Input.GetMouseButtonDown(0) && GetSelectedRockAndOrbital(out var rock, out var orbital))
        {
            if (orbital is not null)
            {
                orbital.Remove(rock);
            }

            selectedRock = rock;
            rock.SetGrounded();
            rock.transform.SetParent(transform, true);
        }
        else if (selectedRock != null && Input.GetMouseButton(0))
        {
            var cursorWorldPos = GetCursorWorldPosition();
            selectedRock.transform.position = cursorWorldPos;
        }
        else if (selectedRock != null && Input.GetMouseButtonUp(0))
        {
            groundedRocks.Add(selectedRock);
            selectedRock = null;
        }
    }

    private bool GetSelectedRockAndOrbital(out Rock rock, out RockOrbital orbital)
    {
        rock = null;
        orbital = null;

        foreach (var o in orbitals)
        {
            foreach (var r in o.rocks)
            {
                var cursorWorldPos = GetCursorWorldPosition();
                if (Vector2.Distance(r.transform.position, cursorWorldPos) <= r.dragRadius)
                {
                    rock = r;
                    orbital = o;
                    return true;
                }
            }
        }
        foreach (var r in groundedRocks)
        {
            var cursorWorldPos = GetCursorWorldPosition();
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
            rock.Init(transform);
            rock.SetOrbital(orbitals[0]);
            orbitals[0].Add(rock);
        }
    }

    private Vector2 GetCursorWorldPosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
