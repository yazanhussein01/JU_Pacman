using UnityEngine;

public class GhostChase : GhostBehaviour
{
    private void OnDisable()
    {
        this.ghost.scatter.Enable();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Node node = collision.GetComponent<Node>();
        if (node != null && this.enabled && !this.ghost.frightend.enabled)
        {
            Vector2 direction = Vector2.zero;
            float minDistance = float.MaxValue;
            foreach(Vector2 availableDirections in node.AvailableDirections)
            {
                Vector3 newPos = this.transform.position + new Vector3(availableDirections.x, availableDirections.y, 0.0f);
                float distance = (this.ghost.target.position - newPos).sqrMagnitude;
                if (distance < minDistance)
                {
                    direction = availableDirections;
                    minDistance = distance;
                }
            }
            this.ghost.movement.SetDirection(direction);
        }
    }
}
