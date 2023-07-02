using UnityEngine;
[RequireComponent(typeof(SpriteRenderer))]
public class AnimatedSprites : MonoBehaviour
{
    public SpriteRenderer spriteRenderer { get; private set; }
    public Sprite[] sprites;
    public float AnimationTime = 0.25f;
    public int AnimationFrame { get; private set; }
    public bool loop = true;
    private void Awake()
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        InvokeRepeating(nameof(Advance),this.AnimationTime,this.AnimationTime);
    }
    private void Advance()
    {
        if(!this.spriteRenderer.enabled) { return; }
        this.AnimationFrame++;
        if (this.AnimationFrame >= this.sprites.Length && this.loop) 
        {
            this.AnimationFrame = 0;
        }
        if (this.AnimationFrame >= 0 && this.AnimationFrame < this.sprites.Length) 
        {
            this.spriteRenderer.sprite = this.sprites[this.AnimationFrame];
        }
    }
    public void Restart()
    {
        this.AnimationFrame = -1;
        Advance();
    }
}
