using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParalacsView : MonoBehaviour
{
    [SerializeField] private List<GameObject> Tiles;
    [SerializeField] private float TagPosition;

    public List<GameObject> tiles => Tiles;
    public float tagPosition => TagPosition;
}
