using UnityEngine;

public class GhostScatter : GhostBehaviour
{
    private void OnDisable()
    {
        this.ghost.chase.Enable();   
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Node node = collision.GetComponent<Node>();
        if (node != null && this.enabled && !this.ghost.frightend.enabled)
        {
            int index = Random.Range(0, node.AvailableDirections.Count);
            if (node.AvailableDirections[index] == -this.ghost.movement.direction && node.AvailableDirections.Count > 1)
            {
                index++;
                if(index >= node.AvailableDirections.Count)
                {
                    index = 0;
                }
            }
            this.ghost.movement.SetDirection(node.AvailableDirections[index]);
        }
    }
}
