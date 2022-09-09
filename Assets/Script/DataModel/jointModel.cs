using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Joints_
{
public List<float> joint;
}

[Serializable]
public class PointByJoint
{
   public List<Points_joint> points;
}
[Serializable]
public class Points_joint
{
   public Joints_ joints;
   public string name;
}
