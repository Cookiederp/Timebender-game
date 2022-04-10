
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlyphPuzzleController : MonoBehaviour
{

    private GameManager gameManager;
    // Start is called before the first frame update
    public GameObject SkeletonPrefab;

    public GameObject PositiveXGlyph;
    public GameObject NegativeXGlyph;
    public GameObject PositiveZGlyph;
    public GameObject NegativeZGlyph;

    public GameObject PositiveXPlateGlyph;
    public GameObject NegativeXPlateGlyph;
    public GameObject PositiveZPlateGlyph;
    public GameObject NegativeZPlateGlyph;

    public GameObject PositiveXPlate;
    public GameObject NegativeZPlate;
    public GameObject NegativeXPlate;
    public GameObject PositiveZPlate;

    public GameObject SkeletonWave2;
    public GameObject SkeletonWave3;
    public GameObject SkeletonWave4;

    public GameObject LadderCover;
    public GameObject NextLevelTrigger;

    private SpriteRenderer[] StaticGlyphs = new SpriteRenderer[4];
    private SpriteRenderer[] PlateGlyphs = new SpriteRenderer[4];
    public SelectablePlateController[] Plates = new SelectablePlateController[4];
    public List<int> spriteIndices;
    private int selectedPlate;
    private List<int> IncompletePlates;

    private GameObject[] SkeletonWaves = new GameObject[3];
    public Sprite[] Glyphs;
    private int waveIndex;

    void Start()
    {

        gameManager = GameManager.instance;
        //Clockwise from top down
        StaticGlyphs[0] = (SpriteRenderer) PositiveXGlyph.GetComponent(typeof(SpriteRenderer));
        StaticGlyphs[1] = (SpriteRenderer) NegativeZGlyph.GetComponent(typeof(SpriteRenderer));
        StaticGlyphs[2] = (SpriteRenderer) NegativeXGlyph.GetComponent(typeof(SpriteRenderer));
        StaticGlyphs[3] = (SpriteRenderer) PositiveZGlyph.GetComponent(typeof(SpriteRenderer));

        PlateGlyphs[0] = (SpriteRenderer) PositiveXPlateGlyph.GetComponent(typeof(SpriteRenderer));
        PlateGlyphs[1] = (SpriteRenderer) NegativeZPlateGlyph.GetComponent(typeof(SpriteRenderer));
        PlateGlyphs[2] = (SpriteRenderer) NegativeXPlateGlyph.GetComponent(typeof(SpriteRenderer));
        PlateGlyphs[3] = (SpriteRenderer) PositiveZPlateGlyph.GetComponent(typeof(SpriteRenderer));

        Plates[0] = (SelectablePlateController)PositiveXPlate.GetComponent(typeof(SelectablePlateController));
        Plates[1] = (SelectablePlateController)NegativeZPlate.GetComponent(typeof(SelectablePlateController));
        Plates[2] = (SelectablePlateController)NegativeXPlate.GetComponent(typeof(SelectablePlateController));
        Plates[3] = (SelectablePlateController)PositiveZPlate.GetComponent(typeof(SelectablePlateController));

        spriteIndices = new List<int>();
        IncompletePlates = new List<int>();
        Random.InitState((int) System.DateTime.Now.Second * 1000 + System.DateTime.Now.Millisecond);
        spriteIndices.Add((int) Mathf.Floor(Random.value * (Glyphs.Length - Mathf.Epsilon)));
        int temp;
        while (spriteIndices.Count < 4)
        {
            temp = (int)Mathf.Floor(Random.value * (Glyphs.Length - Mathf.Epsilon));
            if (!spriteIndices.Contains(temp))
            {
                spriteIndices.Add(temp);
            }
        }

        for (int i = 0; i < 4; i++) {
            IncompletePlates.Add(i);
            Plates[i].index = i;
        }

        //Clockwise from top down
        StaticGlyphs[0].sprite = Glyphs[spriteIndices[0]];
        StaticGlyphs[1].sprite = Glyphs[spriteIndices[1]];
        StaticGlyphs[2].sprite = Glyphs[spriteIndices[2]];
        StaticGlyphs[3].sprite = Glyphs[spriteIndices[3]];

        SkeletonWaves[0] = SkeletonWave2;
        SkeletonWaves[1] = SkeletonWave3;
        SkeletonWaves[2] = SkeletonWave4;

        waveIndex = 0;
        SelectNewPlate();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void KillPlayer()
    {
        gameManager.playerHealthManager.RemoveHP(100);
    }

    public void CompletedPlate(int index)
    {
        IncompletePlates.Remove(index);
        if (IncompletePlates.Count > 0)
        {
            SelectNewPlate();
            SpawnSkeletons();
        }
        else
        {
            LadderCover.SetActive(false);
            NextLevelTrigger.transform.position = new Vector3(0, 0, 0);
        }
    }

    public void SpawnSkeletons()
    {
        SkeletonWaves[waveIndex].transform.position = new Vector3(0,0,0);
        waveIndex++;
    }

    public void SelectNewPlate()
    {

        selectedPlate = IncompletePlates[(int)Mathf.Floor(Random.value * (IncompletePlates.Count - Mathf.Epsilon))];
        PlateGlyphs[selectedPlate].sprite = StaticGlyphs[selectedPlate].sprite;
        Plates[selectedPlate].isCorrectPlate = true;
        int Glyph;
        for (int i = 0; i < 4; i++)
        {
            if (i != selectedPlate)
            {
                //I think this makes sense
                Glyph = i + 1;
                if (Glyph >= 4)
                {
                    Glyph = 0;
                }
                if (Glyph == selectedPlate)
                {
                    Glyph++;
                }
                if (Glyph >= 4)
                {
                    Glyph = 0;
                }
                PlateGlyphs[i].sprite = Glyphs[spriteIndices[Glyph]];
            }
        }
        
    }
}
