using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// XYZ

[Serializable]
public class XYZ_
{
public List<float> point;
}


[Serializable]
public class PointByXYZ
{
   public List<Points_XYZ> points;
}

[Serializable]
public class Points_XYZ
{
   public XYZ_ point;
   public string name;
}


///Joint
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
