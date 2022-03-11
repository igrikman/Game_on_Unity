using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 5.0F;
    private Vector3 derection;
    public Vector3 Derection { set { derection = value; } }
    public SpriteRenderer sprite;

    private void Awake()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
    }
    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + derection, speed * Time.deltaTime);
    }
    private void Start()
    {
        Destroy(gameObject, 1.5F);
    }
}
