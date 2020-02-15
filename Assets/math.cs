using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using UnityEngine;


public class math : MonoBehaviour
{     
        
    public MeshFilter Torso;
    public MeshFilter Hips;
    public MeshFilter Head;
    
    public MeshFilter LegL;
    public MeshFilter LLegL;
    public MeshFilter FootL;
    
    public MeshFilter LegR;
    public MeshFilter LLegR;
    public MeshFilter FootR;
    
    public MeshFilter FArmR;
    public MeshFilter ArmR;
    public MeshFilter HandR;
    
    public MeshFilter FArmL;
    public MeshFilter ArmL;
    public MeshFilter HandL;
    public GameObject mark;
    
    public List<MeshFilter> MF =new List<MeshFilter>();
    
    void Start()
    {
       //Instantiate(mark, ArmL.GetComponent<MeshRenderer>().bounds.center, Quaternion.identity);
       //Instantiate(mark, ArmL.GetComponent<MeshRenderer>().bounds.center + GetRoughDirection(ArmL.mesh.vertices) , Quaternion.identity);
       //Debug.DrawLine(ArmL.GetComponent<MeshRenderer>().bounds.center,ArmL.GetComponent<MeshRenderer>().bounds.center + cen,Color.red,2000,false);
       foreach (var m in MF)
       {
           Centroid(m);
       }
       //Debug.DrawLine(new Vector3(0,0,0),new Vector3(10,10,10),Color.red,2000,false );
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3 Centroid(MeshFilter _meshFilter)
    {
        Vector3 centroid  = Vector3.zero;
        Mesh mesh = _meshFilter.mesh;
        Vector3[] allvertex = mesh.vertices;

        float xmax = allvertex.Max(t => t.x);
        float xmin = allvertex.Min(t => t.x);
        
        float ymax = allvertex.Max(t => t.y);
        float ymin = allvertex.Min(t => t.y);
        
        float zmax = allvertex.Max(t => t.z);
        float zmin = allvertex.Min(t => t.z);

        float Xdist = Mathf.Abs(Mathf.Abs(xmax) - Mathf.Abs(xmin));
        float Ydist = Mathf.Abs(Mathf.Abs(ymax) - Mathf.Abs(ymin));
        float Zdist = Mathf.Abs(Mathf.Abs(zmax) - Mathf.Abs(zmin));

        if (Xdist > Ydist && Xdist > Zdist)
        {
            var xList = allvertex.OrderBy(m => m.x);
            var firstHalf = xList.ToList().GetRange(0, xList.Count() / 2);
            var lastHalf = xList.ToList().GetRange((xList.Count() / 2)+1,(xList.Count()/2)-1);

            Vector3 center1 = Vector3.zero;
            foreach (var vec in firstHalf)
            {
                center1 += vec;
            }
            center1 /= firstHalf.Count();
            
            Vector3 center2 = Vector3.zero;
            foreach (var vec in lastHalf)
            {
                center2 += vec;
            }
            center2 /= lastHalf.Count();
            
            Debug.Log(center1+" "+center2);
                        
            Vector3 finalVec = center2 - center1;
            finalVec = finalVec.normalized;
            finalVec *= (Xdist*2);
            Vector3 center = _meshFilter.GetComponent<MeshRenderer>().bounds.center;
            Debug.DrawLine(center-finalVec,center + finalVec,Color.red,2000,false);
        }
        else if (Ydist > Xdist && Ydist > Zdist)
        {
            var yList = allvertex.OrderBy(m => m.y);
            var firstHalf = yList.ToList().GetRange(0, yList.Count() / 2);
            var lastHalf = yList.ToList().GetRange((yList.Count() / 2)+1,(yList.Count()/2)-1);

            Vector3 center1 = Vector3.zero;
            foreach (var vec in firstHalf)
            {
                center1 += vec;
            }
            center1 /= firstHalf.Count();
            
            Vector3 center2 = Vector3.zero;
            foreach (var vec in lastHalf)
            {
                center2 += vec;
            }
            center2 /= lastHalf.Count();
            
            Vector3 finalVec = center2 - center1;
            finalVec = finalVec.normalized;
            finalVec *= (Ydist*2);;
            Vector3 center = _meshFilter.GetComponent<MeshRenderer>().bounds.center;
            Debug.DrawLine(center-finalVec,center + finalVec,Color.red,2000,false);
        }
        else if(Zdist > Xdist && Zdist > Ydist)
        {
            var zList = allvertex.OrderBy(m => m.z);
            var firstHalf = zList.ToList().GetRange(0, zList.Count() / 2);
            var lastHalf = zList.ToList().GetRange((zList.Count() / 2)+1,(zList.Count()/2)-1);

            Vector3 center1 = Vector3.zero;
            foreach (var vec in firstHalf)
            {
                center1 += vec;
            }
            center1 /= firstHalf.Count();
            
            Vector3 center2 = Vector3.zero;
            foreach (var vec in lastHalf)
            {
                center2 += vec;
            }
            center2 /= lastHalf.Count();
            
            Debug.Log(center1+" "+center2);
                        
            Vector3 finalVec = center2 - center1;
            finalVec = finalVec.normalized;
            finalVec *= (Zdist*2);;
            Vector3 center = _meshFilter.GetComponent<MeshRenderer>().bounds.center;
            Debug.DrawLine(center-finalVec,center + finalVec,Color.red,2000,false);
            
        }
        
        return centroid;
    }    
    
}
