using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spriteswapLightning : MonoBehaviour
{
    [SerializeField] List<Sprite> sprites;
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] float startTimer = 0f;
    [SerializeField] int currentIndex;
    private void Start()
    {
        currentIndex = 0;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        startTimer += Time.deltaTime;

        if (Mathf.RoundToInt(startTimer) % 2 == 0)
        {
            sprite.sprite = sprites[currentIndex];
            currentIndex = (currentIndex + 1) % sprites.Count;
        }
    }
}
