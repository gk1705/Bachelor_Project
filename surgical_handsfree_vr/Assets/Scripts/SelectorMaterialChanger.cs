using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorMaterialChanger : MonoBehaviour
{
    [SerializeField] private Renderer _renderer;
    [SerializeField] private Material _selecedMaterial, _unselectedMaterial;

    public void Select()
    {
        _renderer.material = _selecedMaterial;
    }

    public void Unselect()
    {
        _renderer.material = _unselectedMaterial;
    }
}
