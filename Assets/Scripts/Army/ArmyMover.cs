﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmyMover : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float rayDistance;

    private СombatUnit _troop;
    private void FixedUpdate()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(transform.position, ray.direction*rayDistance);
        RaycastHit rayHit;
        if (Input.GetMouseButton(0))
        {
            if (_troop)
            {
                if (Physics.Raycast(ray, out rayHit))
                {
                    if (rayHit.collider.TryGetComponent<Plane>(out Plane plane))
                    {
                        if (plane.canMove)
                        {
                            _troop.Move(plane);
                            _troop.PlaneDeselecte();
                        }
                    }

                    if (rayHit.collider.TryGetComponent(out СombatUnit combatEnemy))
                    {
                        if (!combatEnemy.IsPlayerUnits)
                        {
                            _troop.Battle(combatEnemy);
                        }
                    }

                    if (rayHit.collider.TryGetComponent(out EnemyCastle enemyCastle))
                    {
                        enemyCastle.Defense(_troop);
                    }
                }
            }
            
            
            if (Physics.Raycast(ray, out rayHit))
            {
                if (rayHit.collider.TryGetComponent<СombatUnit>(out СombatUnit troop))
                {
                    
                    _troop = troop;
                    if (_troop.IsSelected)
                    {
                        _troop.IsSelected = false;
                        _troop.PlaneDeselecte();
                        return;
                    }

                    if (_troop.CanMove)
                    {
                        _troop.IsSelected = true;
                        _troop.Selected();

                    }
                }
            }
        }
        
    }
    
}