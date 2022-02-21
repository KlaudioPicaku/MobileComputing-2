using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spriteswap : MonoBehaviour
{
    [SerializeField] Sprite[] sprites;
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] float startTimer = 0f;
    private void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        startTimer += Time.deltaTime;

        if (Mathf.RoundToInt(startTimer) % 2 == 0)
        {
            sprite.sprite = sprites[1];
        }
        else
        {
            sprite.sprite = sprites[0];
        }
    }
}
