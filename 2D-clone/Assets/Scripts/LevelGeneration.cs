using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    public Transform startingPoint;
    public GameObject[] rooms;
    public float moveAmount;

    private float timeBtwRoom;
    public float startTimeBtwRoom = 0.25f;
    private float last = -1;
    // Update is called once per frame
    void Start()
    {
        transform.position = startingPoint.position;
        int rand = Random.Range(1, rooms.Length); 
        Instantiate(rooms[rand], transform.position, Quaternion.identity);
    }
    void Update()
    {
        if (timeBtwRoom <= 0)
        {
            Move();
            timeBtwRoom = startTimeBtwRoom;
        }
        else
        {
            timeBtwRoom -= Time.deltaTime;
        }
    }
    private void Move()
    {
        int rand = Random.Range(1, rooms.Length);
        Vector2 newPos = new Vector2(transform.position.x + moveAmount, transform.position.y);
        if (rand != last)
        {
            transform.position = newPos;
            Instantiate(rooms[rand], transform.position, Quaternion.identity);
            last = rand;
        }
            
    }
}
