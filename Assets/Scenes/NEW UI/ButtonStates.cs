using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class ButtonStates : MonoBehaviour
{
    [Header("Sound Source")]
    public AudioSource hoverSoundSource;
    public AudioSource clickSoundSource;
    [Header("Button")]
    public Sprite normalSprite;
    public Sprite clickSprite;
    public List<Sprite> hoverSprites;
    public float hoverSpeed = 0.1f;
    public bool hoverLooping = true;
    [Header("Size")]
    public Vector2 normalSize = new Vector2(353, 99);
    public Vector2 hoverSize = new Vector2(353, 99);
    public Vector2 clickSize = new Vector2(353, 99);

    private Image buttonImage;
    private AudioSource audioSource;
    private RectTransform rectTransform;
    private Coroutine hoverCoroutine;
    private float hoverTimer = 0f;
    private int hoverIndex = 0;
    private bool isHovering = false;

    void Start()
    {
        buttonImage = GetComponent<Image>();
        buttonImage.sprite = normalSprite;
        rectTransform = GetComponent<RectTransform>();
        rectTransform.sizeDelta = normalSize;
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update()
    {
        if (isHovering && hoverCoroutine != null)
        {
            hoverTimer += Time.unscaledDeltaTime;

            if (hoverTimer >= hoverSpeed)
            {
                hoverTimer = 0f;
                hoverIndex = (hoverIndex + 1) % hoverSprites.Count;
                buttonImage.sprite = hoverSprites[hoverIndex];

                if (!hoverLooping && hoverIndex == 0)
                {
                    StopCoroutine(hoverCoroutine);
                    hoverCoroutine = null;
                }
            }
        }
    }

    public void Enter()
    {
        isHovering = true;
        hoverIndex = 0; 
        if (hoverCoroutine == null && hoverSprites.Count > 0)
        {
            hoverCoroutine = StartCoroutine(PlayHoverAnimation());
        }
        rectTransform.sizeDelta = hoverSize;
        hoverSoundSource.Play();
    }

    public void Exit()
    {
        isHovering = false;
        if (hoverCoroutine != null)
        {
            StopCoroutine(hoverCoroutine);
            hoverCoroutine = null;
        }
        buttonImage.sprite = normalSprite;
        rectTransform.sizeDelta = normalSize;
    }

    public void Down()
    {
        if (hoverCoroutine != null)
        {
            StopCoroutine(hoverCoroutine);
            hoverCoroutine = null;
        }
        buttonImage.sprite = clickSprite;
        rectTransform.sizeDelta = clickSize;
        clickSoundSource.Play();
    }

    public void Up()
    {
        if (hoverSprites.Count > 0)
        {
            hoverCoroutine = StartCoroutine(PlayHoverAnimation());
        }
        rectTransform.sizeDelta = hoverSize;
    }

    private IEnumerator PlayHoverAnimation()
    {
        while (true)
        {
            yield return null; 
        }
    }
}