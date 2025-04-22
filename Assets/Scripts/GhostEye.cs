using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostEye : MonoBehaviour
{
    public Sprite up;
    public Sprite down;
    public Sprite left;
    public Sprite right;

    public SpriteRenderer spriteRenderer { get; private set; }

    public Movement movement { get; private set; }
    public void Awake()
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        this.movement = GetComponentInParent<Movement>();
    }

    public void Update()
    {
        if (this.movement.direction == Vector2.up)
        {
            this.spriteRenderer.sprite = this.up;
        }
        else if (this.movement.direction == Vector2.down)
        {
            this.spriteRenderer.sprite = this.down;
        }
        else if (this.movement.direction == Vector2.left)
        {
            this.spriteRenderer.sprite = this.left;
        }
        else if (this.movement.direction == Vector2.right)
        {
            this.spriteRenderer.sprite = this.right;
        }


    }
}
