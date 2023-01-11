using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogoDemo : MonoBehaviour
{
    [Header("Dogos Meshes")]
    [SerializeField] private SkinnedMeshRenderer m_bodyMesh;
    [SerializeField] private SkinnedMeshRenderer[] m_EarsMesh;
    [SerializeField] private MeshRenderer[] m_EyesMesh;
    [SerializeField] private MeshRenderer m_MouthMesh;

    [Header("Textures")]
    [SerializeField] private Texture2D[] eyesTextures;
    [SerializeField] private Texture2D[] mouthTextures;
    [SerializeField] private Texture2D[] furAlphas;

    private Material m_Mouth;
    private Material m_Body;
    private Material m_Eyes;


    private void Start()
    {
        RandomizeDogo();
    }

    public void RandomizeEyes()
    {
        //Get random index
        int randomEyeIndex = Random.Range( 0, eyesTextures.Length);

        //Apply eye texture
        foreach (var eye in m_EyesMesh)
        {
            eye.material.SetTexture("_MainTex", eyesTextures[randomEyeIndex]);
        }
    }

    public void RandomizeMouth()
    {
        //Get random indext
        int randomMouthIndex = Random.Range(0, mouthTextures.Length);
        //Apply mouth texture
        m_MouthMesh.material.SetTexture("_MainTex", mouthTextures[ randomMouthIndex ]);
    }

    public void RandomizeEars()
    {
        foreach (var item in m_EarsMesh)
        {
            item.gameObject.SetActive(false);
        }

        m_EarsMesh[Random.Range(0, m_EarsMesh.Length)].gameObject.SetActive(true);
    }

    public void RandomizeFur()
    {
        //Get random indext
        int randomFurIndex = Random.Range(0, furAlphas.Length);
        //Apply fur alpha
        m_bodyMesh.material.SetTexture("_FurAlpha", furAlphas[randomFurIndex]);

        foreach (var ear in m_EarsMesh)
        {
            ear.material.SetTexture("_FurAlpha", furAlphas[randomFurIndex]);
        }
    }

    public void RandomizeColors()
    {
        Color color1 = Random.ColorHSV();
        Color color2 = Random.ColorHSV();

        m_bodyMesh.material.SetColor("_FurColor1", color1);
        m_bodyMesh.material.SetColor("_FurColor2", color2);

        foreach (var ear in m_EarsMesh)
        {
            ear.material.SetColor("_FurColor1", color1);
            ear.material.SetColor("_FurColor2", color2);
        }
    }

    public void RandomizeDogo()
    {
        RandomizeEyes();
        RandomizeFur();
        RandomizeMouth();
        RandomizeColors();
        RandomizeEars();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            RandomizeEyes();
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            RandomizeMouth();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            RandomizeFur();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            RandomizeColors();
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            RandomizeEars();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            RandomizeDogo();
        }
    }
}
