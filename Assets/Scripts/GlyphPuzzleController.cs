using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlyphPuzzleController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject SkeletonPrefab;
    public GameObject PositiveXGlyph;
    public GameObject NegativeXGlyph;
    public GameObject PositiveZGlyph;
    public GameObject NegativeZGlyph;

    private SpriteRenderer posXGlyphSprite;
    private SpriteRenderer negXGlyphSprite;
    private SpriteRenderer posZGlyphSprite;
    private SpriteRenderer negZGlyphSprite;

    public Sprite[] Glyphs;
    private 

    void Start()
    {
        posXGlyphSprite = (SpriteRenderer) PositiveXGlyph.GetComponent(typeof(SpriteRenderer));
        negXGlyphSprite = (SpriteRenderer) NegativeXGlyph.GetComponent(typeof(SpriteRenderer));
        posZGlyphSprite = (SpriteRenderer) PositiveZGlyph.GetComponent(typeof(SpriteRenderer));
        negZGlyphSprite = (SpriteRenderer) NegativeZGlyph.GetComponent(typeof(SpriteRenderer));

        List<int> spriteIndices = new List<int>();
        spriteIndices.Add((int) Mathf.Floor(Random.value * (8 - Mathf.Epsilon)));
        int temp;
        while (spriteIndices.Count < 4)
        {
            temp = (int)Mathf.Floor(Random.value * (8 - Mathf.Epsilon));
            if (!spriteIndices.Contains(temp))
            {
                spriteIndices.Add(temp);
            }
        }
        //Clockwise from top down
        posXGlyphSprite.sprite = Glyphs[spriteIndices[0]];
        negZGlyphSprite.sprite = Glyphs[spriteIndices[1]];
        negXGlyphSprite.sprite = Glyphs[spriteIndices[2]];
        posZGlyphSprite.sprite = Glyphs[spriteIndices[3]];

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewGlyph()
    {

    }
}
