using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Plane : MonoBehaviour
{
    public int positionX, positionZ;
    public Material EnterMaterial, ClickMaterial;

    public bool canMove;
    public int Cost;

    [SerializeField] private MeshRenderer meshRenderer;
    private Material _defaultMaterial;


    public void CanMove()
    {
        meshRenderer.material = EnterMaterial;
        canMove = true;
    }

    public void Deselecte()
    {
        meshRenderer.material = _defaultMaterial;
        canMove = false;
    }
    
    
    private void Awake()
    {
        _defaultMaterial = meshRenderer.material;
    }
    
    private void OnMouseEnter()
    {
        
        meshRenderer.material = EnterMaterial;
        if(canMove)
            meshRenderer.material = ClickMaterial;
            
    }

    private void OnMouseExit()
    {
        if(!canMove)
            meshRenderer.material = _defaultMaterial;
        else
        {
            meshRenderer.material = EnterMaterial;
        }
    }

    private void OnMouseDown()
    {
        meshRenderer.material = ClickMaterial;
    }
    
    private void OnMouseUp()
    {
        meshRenderer.material = EnterMaterial;
    }
}
