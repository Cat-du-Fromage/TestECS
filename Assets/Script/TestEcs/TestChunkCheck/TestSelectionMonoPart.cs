using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;

public class TestSelectionMonoPart : MonoBehaviour
{
    public static TestSelectionMonoPart instance;

    public RectTransform selectionBox;

    private TestSelectionEntity _testSelectionEntity;

    private float3 _startPosition;

    private void Awake()
    {
        instance = this; // Allow to take object/rectTransform from this class
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void UpdateSelectionBox(float3 mousePosition)
    {

    }
}
