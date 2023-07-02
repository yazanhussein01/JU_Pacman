using UnityEngine;
using UnityEngine.Tilemaps;

public class Ghost : MonoBehaviour
{
  
    public Rigidbody2D rb2d;
    public Movement movement { get; private set; }
    public GhostHome home { get; private set; }
    public GhostScatter scatter { get; private set; }
    public GhostChase chase { get; private set; }
    public GhostFrightend frightend { get; private set; }
    public GhostBehaviour InitialBehavior;
    public Transform target;
    public int points = 200;

    private void Awake()
    {
        this.movement = GetComponent<Movement>();
        this.home=GetComponent<GhostHome>();
        this.scatter = GetComponent<GhostScatter>();
        this.chase = GetComponent<GhostChase>();
        this.frightend = GetComponent<GhostFrightend>();
        this.rb2d = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        ResetState();

    }
    public void ResetState()
    {
        this.gameObject.SetActive(true);
        this.movement.ResetState();
        this.frightend.Disable();
        this.chase.Disable();
        this.scatter.Enable();
        if(this.home!=this.InitialBehavior)
        {
            this.home.Disable();
        }
        if(this.InitialBehavior!=null)
        {
            this.InitialBehavior.Enable();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            if (this.frightend.enabled)
            {
                FindObjectOfType<GameManager>().GhostEaten(this);
            }
            else
            {
                FindObjectOfType<GameManager>().PacmanEaten();
            }
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Ghost") && !this.home.enabled) 
        {
            WhenGhostCollide();
            if(collision.contactCount > 2) {
                Debug.Log(collision.contactCount);
                Invoke(nameof(AfterGhostCollide), 0.2f);
            }
            else Invoke(nameof(AfterGhostCollide), 0.1f);

        }
    }
    private void WhenGhostCollide()
    {
        this.rb2d.isKinematic = true;
    }
    private void AfterGhostCollide()
    {
        this.rb2d.isKinematic = false;
    }
}
