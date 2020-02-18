using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Security;
using UnityEngine;
using UnityEngine.Animations;
using Debug = UnityEngine.Debug;


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
    
    public MeshFilter FArmL;
    public MeshFilter ArmL;
    public GameObject mark;
    
    public List<MeshFilter> MF =new List<MeshFilter>();

    public SkinnedMeshRenderer rend;
    
    void Start()
    {
       
       SkeletonBone SK; 
       List<Transform> bones = new List<Transform>();
       foreach (var m in MF)
       {
           SK.name = m.gameObject.name;
           SK.rotation = Quaternion.identity;
           SK.position = m.GetComponent<MeshRenderer>().bounds.center;
           SK.scale = Centroid(m);
           GameObject g = Instantiate(mark, SK.position, SK.rotation);
           g.transform.localScale = SK.scale;
           BoneWeight[] weights = new BoneWeight[1];

           weights[0].boneIndex0 = 0;
           weights[0].weight0 = 1;
            
           Matrix4x4[] bindPoses = new Matrix4x4[1];

           bindPoses[0] = g.transform.worldToLocalMatrix * transform.localToWorldMatrix;
           m.mesh.bindposes = bindPoses;
           g.name = m.name;
           
           bones.Add(g.transform);
           
       }
       
       
       rend.bones = bones.ToArray();
      
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
            finalVec *= (Xdist+ Ydist+Zdist);
            Vector3 center = _meshFilter.GetComponent<MeshRenderer>().bounds.center;
            Debug.DrawLine(center-finalVec,center + finalVec,Color.red,2000,false);
            return (center + finalVec) - (center - finalVec);
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
            finalVec *= (Xdist+Ydist+Zdist);
            Vector3 center = _meshFilter.GetComponent<MeshRenderer>().bounds.center;
            Debug.DrawLine(center-finalVec,center + finalVec,Color.red,2000,false);
            return (center + finalVec) - (center - finalVec);
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
            finalVec *= (Xdist+Ydist+Zdist);
            Vector3 center = _meshFilter.GetComponent<MeshRenderer>().bounds.center;
            Debug.DrawLine(center-finalVec,center + finalVec,Color.red,2000,false);
            return (center + finalVec) - (center - finalVec);
        }
        
        return centroid;
    }    
    
}
