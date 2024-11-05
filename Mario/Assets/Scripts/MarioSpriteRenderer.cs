using UnityEngine;

public class MarioSpriteRenderer : MonoBehaviour
{
    public SpriteRenderer spriteRenderer {get; private set; }
    private PlayerMovements movement;

    public Sprite idle;
    public Sprite jump;
    public Sprite slide;
    public AnimatedSprite run;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        movement = GetComponentInParent<PlayerMovements>();

    }

    private void OnEnable()
    {
        spriteRenderer.enabled = true;
    }

    private void OnDisable()
    {
        spriteRenderer.enabled = false;
        run.enabled = false;
    }

    private void LateUpdate()
    {
        run.enabled = movement.isRunning;

        if(movement.isJumping)
        {
            spriteRenderer.sprite = jump;
        }
        else if(movement.isSliding)
        {
            spriteRenderer.sprite = slide;
        }
        else if (!movement.isRunning){
            spriteRenderer.sprite = idle;
        }
    }

}
