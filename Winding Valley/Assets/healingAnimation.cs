using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healingAnimation : MonoBehaviour
{
    SpriteRenderer sprite;
    [SerializeField] Sprite[] sprites;
    [SerializeField] float duration;
    [SerializeField] float changeTime = 0f;
    [SerializeField] float currentTime = 0f;
    [SerializeField] float speed = 0f;
    [SerializeField] int i = 0;
    // Start is called before the first frame update
    void Awake()
    {
        sprite = this.gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTime <= duration)
        {
            currentTime += Time.deltaTime;
            if (currentTime > changeTime && i <= 7)
            {
                sprite.sprite = sprites[i];
                changeTime=(changeTime+currentTime+ Time.deltaTime)*speed;
                i++;
            }
        }
        else
        {
            Destroy(this.gameObject,0.3f);
        }
    }
}
