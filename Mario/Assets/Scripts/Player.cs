using System.Collections;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    public MarioSpriteRenderer smallRenderer;
    public MarioSpriteRenderer bigRenderer;
    private MarioSpriteRenderer activeRenderer;

    private DeathAnimation deathAnimation;
    private CapsuleCollider2D capsuleCollider;

    public bool big => bigRenderer.enabled;
    public bool small => smallRenderer.enabled;
    public bool dead => deathAnimation.enabled;
    public bool star {get; private set;}
    
    private void Awake()
    {
        deathAnimation = GetComponent<DeathAnimation>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        activeRenderer = smallRenderer;
    }
    
    public void Hit()
    {
        if(!star && !dead)
        {
            if(big)
            {
                Shrink();

            }
            else
            {
                AudioManager.Instance.playDeath();
                Death();
            }
        }

    }

    private void Shrink()
    {
        smallRenderer.enabled = true;
        bigRenderer.enabled = false;
        activeRenderer = smallRenderer;

        capsuleCollider.size = new Vector2(1f,1f);
        capsuleCollider.offset = new Vector2(0f, 0f);
        StartCoroutine(ScaleAnimation());
    }

    public void Grow()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.grow);
        smallRenderer.enabled = false;
        bigRenderer.enabled = true;
        activeRenderer = bigRenderer;

        capsuleCollider.size = new Vector2(1f,2f);
        capsuleCollider.offset = new Vector2(0f, 0.5f);
        StartCoroutine(ScaleAnimation());
    }

    private IEnumerator ScaleAnimation()
    {
        float elapsed = 0f;
        float duration = 0.3f;

        while(elapsed<duration)
        {
            elapsed += Time.deltaTime;
            if(Time.frameCount % 4 == 0)
            {
                smallRenderer.enabled = !smallRenderer.enabled;
                bigRenderer.enabled = !bigRenderer.enabled;
            }

            yield return null;
        }
        smallRenderer.enabled = false;
        bigRenderer.enabled = false;
        activeRenderer.enabled = true;
    }
    

    private void Death()
    {
        smallRenderer.enabled = false;
        bigRenderer.enabled = false;
        deathAnimation.enabled = true;

        GameManagement.Instance.ResetLevel(3f);
    }

    public void StarPower(float duration = 10f)
    {
        StartCoroutine(StarAnimation(duration));
    }

    private IEnumerator StarAnimation(float duration)
    {
        AudioManager.Instance.playStar();
        star = true;

        float elapsed = 0f;
        Color color1 = Random.ColorHSV(0f,1f,1f,1f,1f,1f);
        Color color2 = Random.ColorHSV(0f,1f,1f,1f,1f,1f);

        while(elapsed < duration)
        {
            elapsed += Time.deltaTime;
            activeRenderer.spriteRenderer.color = Color.Lerp(color1, color2, Time.deltaTime*60);
            if(activeRenderer.spriteRenderer.color == color2)
            {
                color1 = color2;
                color2 = Random.ColorHSV(0f,1f,1f,1f,1f,1f);
            }
            yield return null;
        }

        bigRenderer.spriteRenderer.color = Color.white;
        smallRenderer.spriteRenderer.color = Color.white;

        star = false;
        AudioManager.Instance.playReg();
    }
}
