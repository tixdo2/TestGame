using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneGrid : MonoBehaviour
{
    //static public PlaneGrid Grid;
    public  List<Plane> Planes;

    [SerializeField] 
    private int space;
    
    [SerializeField] 
    private GameObject planePrefab;
    
    private int _size;

    


    public void InitMap(int size)
    {
        _size = size;
        Planes = new List<Plane>(_size*_size);
        if (_size == 51)
        {
            Debug.Log("size51");
        }
        
        
        for (int i = 0; i < _size * _size; i++)
        {
            int positionX = space * (i % _size);
            int positionZ = -space * (i / _size);
            
            var go = Instantiate(planePrefab, new Vector3(positionX, 0, positionZ), Quaternion.identity);
            go.transform.SetParent(transform);
            
            var plane = go.GetComponent<Plane>();
            
            Planes.Add(plane);
            plane.positionX = positionX;
            plane.positionZ = positionZ;
        }
    }
}


