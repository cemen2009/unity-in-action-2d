using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryCard : MonoBehaviour
{
    private GameObject cardBack;
    private SpriteRenderer spriteRenderer;

    [SerializeField] private SceneController sceneController;

    private int _id;
    public int Id
    {
        get { return _id; }
    }

    private void Awake()
    {
        cardBack = transform.GetChild(0).gameObject;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnMouseDown()
    {
        if (cardBack.activeSelf && sceneController.canReveal)
        {
            cardBack.SetActive(false);
            sceneController.CardRevealed(this);
        }
    }

    public void Unreveal()
    {
        cardBack.SetActive(true);
    }

    public void SetCard(int id, Sprite image)
    {
        _id = id;
        spriteRenderer.sprite = image;
    }
}
