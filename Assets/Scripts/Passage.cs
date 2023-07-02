
using UnityEngine;

public class Passage : MonoBehaviour
{
    public Transform Connection;
    public Pacman pacman;
    public Ghost[] ghosts;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Vector3 position = collision.transform.position;
        position.x = this.Connection.position.x;
        position.y = this.Connection.position.y;
        collision.transform.position = position;
        if (collision.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            this.pacman.movement.direction.y = 1;
            this.pacman.movement.direction.x = 0;
        }
        else
        {
            if (collision.gameObject.tag == "Blinky")
            {
                this.ghosts[0].movement.direction.y = 1;
                this.ghosts[0].movement.direction.x = 0;
            }else if (collision.gameObject.tag == "Inky")
            {
                this.ghosts[1].movement.direction.y = 1;
                this.ghosts[1].movement.direction.x = 0;
            }
            else if (collision.gameObject.tag == "Pinky")
            {
                this.ghosts[2].movement.direction.y = 1;
                this.ghosts[2].movement.direction.x = 0;
            }
            else if (collision.gameObject.tag == "Clyde")
            {
                this.ghosts[3].movement.direction.y = 1;
                this.ghosts[3].movement.direction.x = 0;
            }
        }
    }
}
