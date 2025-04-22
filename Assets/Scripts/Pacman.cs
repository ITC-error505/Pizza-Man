
using UnityEngine;
using UnityEngine.UI;

public class Pacman : MonoBehaviour
{
    public Movement movement;

    public Button up;
    public Button down;
    public Button left;
    public Button right;

    private void Awake()
    {
        this.movement = GetComponent<Movement>();
    }
    
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow  ))
        {
            this.movement.SetDirection(Vector2.up);
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            this.movement.SetDirection(Vector2.down);
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            this.movement.SetDirection(Vector2.left);
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            this.movement.SetDirection(Vector2.right);
        }

       up.onClick.AddListener(() => this.movement.SetDirection(Vector2.up));
       down.onClick.AddListener(() => this.movement.SetDirection(Vector2.down));
       left.onClick.AddListener(() => this.movement.SetDirection(Vector2.left));
       right.onClick.AddListener(() => this.movement.SetDirection(Vector2.right));

       float angle = Mathf.Atan2(this.movement.direction.y, this.movement.direction.x);
        this.transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);

    } 

  

    public void ResetState()
    {
        this.gameObject.SetActive(true);
        this.movement.ResetState();
    }
}
