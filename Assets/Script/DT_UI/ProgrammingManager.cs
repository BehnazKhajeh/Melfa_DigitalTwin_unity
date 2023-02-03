using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System; 
using System.Linq;
using System.IO; 
using System.Text.RegularExpressions;
using Newtonsoft.Json;
public class ProgrammingManager : MonoBehaviour
{
    public TMP_InputField codeBox;
    public TMP_InputField nameBox;
    public const  int point_min_length=1;
    public const int point_max_length=6;
    PointByXYZ xyzz=new PointByXYZ();
    private DataManager data=new DataManager();
    float OVRD=0;
    bool isRunning=false;
    bool IsMoving=false;
    bool mv_f=false;
    bool ProgramExe=false;
    void Start()
    {
        
    }
           void OnEnable()
    {
           OVRD=PlayerPrefs.GetFloat( "OVRD_VAL",10)/10;
    }
    public void Send2Melfa()
    {
        string tmpcode=codeBox.text;
        string tmpname=nameBox.text;
        if (!tmpcode.Equals("")&&!tmpname.Equals(""))
        {

          StartCoroutine(Send2Menfa_DelayWithDelete(0.5f,tmpname,tmpcode));
        }
        else{
            FindObjectOfType<MessagePanelManager>().ShowMessage("Fill Code and Name Box",Color.red,3f,14);
        }
        
        
    }
          IEnumerator Send2Menfa_DelayWithDelete(float time,string tmpname,string tmpcode)
    {   
        FindObjectOfType<MQTT_Manager>().Publishing("melfa/control/Run");
        yield return new WaitForSeconds(time);
        // FindObjectOfType<MQTT_Manager>().Publishing_Payload("melfa/control/prgrm/"+tmpname.ToUpper(),tmpcode.ToUpper());
    }
      /// <summary>
    /// Handle IK
    /// </summary>
    /// <remarks>
      /// target is target to move
      /// Moving system by arrow key and rshift , rCNTRL (XYZ)
    /// </remarks>
  private  List<string> DecodeCode(string CodeLine){
        List<string> code_segment=new List<string>();
        code_segment=switchCase(CodeLine);
        //  Debug.Log(code_segment.Count);
        for(int i=0;i<code_segment.Count;i++)
        {
            // Debug.Log(code_segment[i]);
        }
        // if(code_segment.Count>=5)
        // {   
        //         return "CODE Error";
        // }
        //   Debug.Log(switchCase(CodeLine)[0]);  
            return code_segment;
    }
  private string GetRAWPAR(string par)
    {
        int sep_counter=0;
        string newPar="";
        for(int i=0;i<par.Length;i++)
        {
            if(par[i].Equals(" "))
            {
                if(sep_counter>0)
                {
                    return "SYNTAX ERROR CODE 1 : '"+par+"' At Line :";
                }
                sep_counter++;
            }
            else{
                  newPar=newPar+par[i];
            }
        }
        if(newPar.Length>=point_min_length && newPar.Length<=point_max_length)
        {
             return newPar;
        }
        else{
       
            return "SYNTAX ERROR CODE 2 : '"+par+"' At Line :";
        }
       
    
    }
   private List<string> ParameterExtracter(string parameters)
  {
    List<string> par =new List<string>();
    int num_specialChar=0;
    int C_specialChar=0;
    for(int i=0;i<parameters.Length;i++)
    {
        if(IsSpecial(parameters[i]))
        {
            if(parameters[i].Equals(',')&& num_specialChar<=0)
            {
                num_specialChar++;
                C_specialChar=i;
                    // it is seperator
                    par.Add(GetRAWPAR(parameters.Substring(0,i)));
            }
           else if(!parameters[i].Equals(' ') && !parameters[i].Equals('-') && !parameters[i].Equals('.'))
            {
              par.Add("SYNTAX ERROR CODE 3: '"+parameters[i]+"'  ->" +C_specialChar);

                return par;
            }
        }
        if(i==parameters.Length-1)
        {   
                if(num_specialChar==1)
                {
                    if(C_specialChar!=i){ par.Add(GetRAWPAR(parameters.Substring(C_specialChar+1)));}else{
                        par.Add("SYNTAX ERROR 4: '"+parameters[i]+"'");
                         return par;
                    }
                    
                }
               else if(num_specialChar==0){
                        par.Add(GetRAWPAR(parameters.Substring(0)));
                }
                else{
                        par.Add("SYNTAX ERROR 5: '"+parameters[i]+"'");
                         return par;
                }
        }

    }

      return par;
  }
  private bool IsSpecial(char c)
  {
          Regex rgx = new Regex("[^A-Za-z0-9]");
            bool containsSpecialCharacter = rgx.IsMatch(c.ToString());

            if (containsSpecialCharacter)
            {
                return true;
            }
            else
            {
                 return false;
            }
  
  }
    private bool IsChar(char c)
  {
          Regex rgx = new Regex("[^0-9]");
            bool containsSpecialCharacter = rgx.IsMatch(c.ToString());

            if (containsSpecialCharacter)
            {
                return true;
            }
            else
            {
                 return false;
            }
  
  }

    private bool IsNum(string c)
  {
        for (int i=0;i<c.Length;i++)
        {
            if(!c[i].Equals(' ') && !c[i].Equals('-') && !c[i].Equals('.')) {
               if(IsChar(c[i]))
            {
                Debug.Log("CHar"+c[i]);
                return false;
            } 
            if(IsSpecial(c[i]))
            {

                    Debug.Log(c[i]);
                      return false;   
            }
            }   
        }
return true;
  
  }
  private List<string> switchCase(string CodeLine)
    {
        List<string> codeSeg=new List<string>();
            string p_MOV="^MOV\\s(\\w*?)";
            Match match_MOV =Regex.Match(CodeLine,p_MOV);
        if(match_MOV.Length>0)
        {
            codeSeg.Add("MOV");
            // codeSeg.Add(Regex.Replace(CodeLine,p_MOV,""));
            codeSeg.AddRange(ParameterExtracter(Regex.Replace(CodeLine,p_MOV,"")));
             return(codeSeg);
        }

                  string p_MVS="^MVS\\s(\\w*?)";
            Match match_MVS =Regex.Match(CodeLine,p_MVS);
        if(match_MVS.Length>0)
        {
                codeSeg.Add("MVS");
              codeSeg.AddRange(ParameterExtracter(Regex.Replace(CodeLine,p_MVS,"")));
             return(codeSeg);
        }
        
        string p_OVRD="^OVRD\\s(\\w*?)";
        Match match_OVRD =Regex.Match(CodeLine,p_OVRD);
        if(match_OVRD.Length>0)
        {
                codeSeg.Add("OVRD");
              codeSeg.AddRange(ParameterExtracter(Regex.Replace(CodeLine,p_OVRD,"")));
             return(codeSeg);
        }
        string p_DLY="^DLY\\s(\\w*?)";
        Match match_DLY =Regex.Match(CodeLine,p_DLY);
        if(match_DLY.Length>0)
        {
                codeSeg.Add("DLY");
              codeSeg.AddRange(ParameterExtracter(Regex.Replace(CodeLine,p_DLY,"")));
             return(codeSeg);
        }
        string p_HOPEN="^HOPEN\\s(\\w*?)";
        Match match_HOPEN =Regex.Match(CodeLine,p_HOPEN);
        if(match_HOPEN.Length>0)
        {
            codeSeg.Add("HOPEN");
            codeSeg.AddRange(ParameterExtracter(Regex.Replace(CodeLine,p_HOPEN,"")));
            return(codeSeg);
        }
        if(CodeLine.Equals("END"))
        {
             codeSeg.Add("END");
               return(codeSeg);
        }
        codeSeg.Add("ERROR");
        return codeSeg;
    }
    public void ProgramSimulation()
    {
        Debug.Log("PROGRAMME RUN !");
        string code=codeBox.text;
        string tmpname=nameBox.text;
        bool  ERROR=false;
        int line_counter=0;
        List<string> Code_Segment=new List<string>();
        List<List<string>> Code_Segments=new List<List<string>>();

         StringReader stringReader =new StringReader(code);
         
         while(true){
             string line=stringReader.ReadLine();
            if(line!=null && !line.Equals(""))
            {
               
                line_counter++;
                
                Code_Segment=DecodeCode(line);
                for(int i=0 ; i <Code_Segment.Count;i++)
                {
                    Debug.Log(i+" :"+Code_Segment[i]);
                 if(Code_Segment[i].Contains("ERROR"))
                {
                    // IT IS ERROR 
                    ERROR=true;
                    // Debug.Log("ERRROR HERE :"+line_counter);
                    FindObjectOfType<MessagePanelManager>().ShowMessage("SYNTAX '"+Code_Segment[0]+"' at Line :"+line_counter);
        
                }
                   
              
                }
               
                 if (ERROR)
                    {
                        break;
                    }
                       Code_Segments.Add(Code_Segment);
            }
            else{
                break;
            }
         }
         if (!ERROR)
         {
        
            FindObjectOfType<MessagePanelManager>().ShowMessage("CODE IS RUNNING .... ",Color.green,3,12);
            ProgramExe=true;
            for (int i=0;i<Code_Segments.Count;i++){
                // Code_Segments[i].
                Debug.Log(i);
                Runnig_Operation(Code_Segments[i]);
            }
         }
    }
    public bool code_dispatcher(List<string> Code_Segment )
    {
            switch(Code_Segment[0])
            {
                case "MOV":
                if(Code_Segment.Count==2)
                {
                   return FUNC_MOV(Code_Segment[1],"0");
                }
                if(Code_Segment.Count==3)
                {
                   return FUNC_MOV(Code_Segment[1],Code_Segment[2]);
                }
                else{
                        return false;
                }
      
                     break;
                case "MVS":
                             if(Code_Segment.Count==2)
                {
                   return FUNC_MVS(Code_Segment[1],"0");
                }
                if(Code_Segment.Count==3)
                {
                   return FUNC_MVS(Code_Segment[1],Code_Segment[2]);
                }
                 else{
                  return false;
                }
      
      
                     break;
                case "OVRD":
                 return FUNC_OVRD(Code_Segment[1]); 
                     break;
                case "DLY":
                 return FUNC_DLY(Code_Segment[1]); 
                     break;
                case "HOPEN":
                return  FUNC_HOPEN(Code_Segment[1]); 
                     break;
                 case "END":
                 Debug.Log("HERE");
                return  false; 
                     break;
                
                     
            }
        
          return  false; 
    }
 
public float stringToFloat(string para){
           //   Debug.Log("RUNNING MOV");
          float parameter=0;
          if(!IsNum(para))
          {
             FindObjectOfType<MessagePanelManager>().ShowMessage("parameter2 :'"+para+"'Must Be number");
             isRunning=false;
                return 10000000000;
          }

          float.TryParse( para,out parameter);
          return parameter;
        //   Debug.Log(parameter2);
        //Get Point para1
}
        public bool FUNC_MOV(string para1 ,string para2)
    {
        Debug.Log("FUNC_MOV");

          Points_XYZ point_=new Points_XYZ();
        //   Debug.Log("RUNNING MOV");
        stringToFloat(para2);
        //   Debug.Log(parameter2);
        //Get Point para1
       point_= GetPoint(para1);
       if(point_.name.Equals("ERROR"))
       {
          FindObjectOfType<MessagePanelManager>().ShowMessage("Point :'"+para1+"'Not Exist in Point List");
           isRunning=false;
           Debug.Log("CODE FALSE MOVE :"+para1);
           
          return false;
       }
             
        isRunning=true;
         //Move To POint 
         FindObjectOfType<MovingTarget>().SetToGO(new Vector3(point_.point.point[0],point_.point.point[1],point_.point.point[2]),OVRD);
           mv_f=true;
        //finish
         return true;
    }
        public bool FUNC_MVS(string para1 ,string para2)
    {
           Debug.Log("FUNC_MVS");
        return true;
    }
   public bool FUNC_DLY(string para)
    {
          Debug.Log("FUNC_DLY");
        isRunning=true;
        float time =stringToFloat(para);
        StartCoroutine(DELY(time));
        return true;
    }

     public bool FUNC_OVRD(string para)
    {
        Debug.Log("FUNC_OVRD");
      isRunning=true;
        OVRD=stringToFloat(para)/10;
           isRunning=false;
return true;
    }
        public bool FUNC_HOPEN(string para)
    {
        Debug.Log("FUNC_HOPEN");
return true;
    }

public Points_XYZ GetPoint(string point)
{
                 //XYZ
        XYZ_ xyz=new XYZ_();
        Points_XYZ pxyz=new Points_XYZ();
        pxyz.point=new XYZ_();
        xyzz.points=new List<Points_XYZ>();
        Points_XYZ point_=new Points_XYZ();
        point_.name="ERROR";
        point_.point=new XYZ_();
        string tmpdataXYZ=data.loadjson("XYZPoint");
                   ///XYZ 
        xyzz=JsonConvert.DeserializeObject<PointByXYZ>(tmpdataXYZ);
       
        for (int i=0 ;i<xyzz.points.Count;i++)
        {
            //   Debug.Log( joi.points[i].name);
            if(xyzz.points[i].name.Equals(point))
            {
             point_=xyzz.points[i];
            }

        }

    return point_;

}
IEnumerator Delay()
{
    yield return new  WaitForSeconds(4);
}
  public void REGEXTMPPPPPPP()  {
               // string pattern = @"\b[MOV ]\w+";
        // pattern=@"^MOV ?";
        //  string patternText = @"(\w+)\.(jpg|png|jpeg|gif)$";
        //   string patternText11 = @"^www.[a-zA-Z0-9]{3,20}.(com|in|org|co\.in|net|dev)$";
  
        //   string patternText1 = @"^MOV [a-zA-Z0-9]{1,20}$";
        //   pattern=@"[MOV] [^\w]";
        //   pattern="MOV\\s(\\w*?)(,)$";
        //    pattern="<TAG\b[^>]*>(.*?)</TAG>";
        //   point  =Regex.Replace(code,pattern,"");
        //    Debug.Log(point);
        //   pattern="MOV\\s(\\w*?),\\s(\\d*?)";
  
        //    string patternText2 = @"^[a-zA-Z0-9\._-]{5,25}.@.[a-z]{2,12}.(com|org|co\.in|net)";
        // string pattern2 = @"\b[MOV ]\w+ ,\d";
        // int num;
        // //  int.TryParse(,out num);
        //  point= Regex.Replace(code, "^[0-9]","");
        //  point  =Regex.Replace(code,pattern,"");
        // Match match =Regex.Match(code,patternText1);
        // if(match.Length>0)
        // {
        //     // point  =Regex.Replace(code,patternText1,);
        // }
       
    }
    // Update is called once per frame
    void Update()
    {
   
        
        if(ProgramExe)
        {
                   IsMoving=FindObjectOfType<MovingTarget>().IsMoving();
            if(mv_f)
            {
        
          if(!IsMoving)
          {
            Debug.Log("Code in Update1");
            isRunning=false;
            mv_f=false;
          }
            }
        }
    }






    //////////

    public void Runnig_Operation(List<string> Code_Segment)
    {
     
            // dialogBox.SetActive(true);
           
          if(!IsMoving &&!isRunning){
        Debug.Log("COD 1:"+Code_Segment[0]);
             if( !code_dispatcher(Code_Segment))
    {
             Debug.Log("COD 2:"+Code_Segment[0]);
 FindObjectOfType<MessagePanelManager>().ShowMessage("CODE IS END  ",Color.green,3,12);
    }
            // StartCoroutine(Running(name,text,DurationPerChar,isEnd));
          }else{
             Debug.Log("COD 3:"+Code_Segment[0]);
              StartCoroutine(waitUnitlOperationEnd(Code_Segment));
          }
    }
    IEnumerator waitUnitlOperationEnd(List<string> Code_Segment)
    {
        yield return new WaitUntil(()=>!IsMoving && !isRunning);
        //  StartCoroutine(Running(name,text,dur,isEnd));
         Debug.Log("COD 4:"+Code_Segment[0]);
       if( !code_dispatcher(Code_Segment))
    {
         Debug.Log("COD 5:"+Code_Segment[0]);
 FindObjectOfType<MessagePanelManager>().ShowMessage("CODE IS END ",Color.green,3,12);
    }
    }
      IEnumerator DELY(float time)
    {   
        
        yield return new WaitForSeconds(time);
         isRunning=false;
    }

    
    // IEnumerator Running(string name,string text,float dur,bool isEnd)
    // { 
    //     isRunning=true;
    // //  namePlace.text=name;
    //      int lenght=text.Length;
    //      int i=0;
    //      while(lenght>=i)
    //      {      
    //                 // textPlace.text=text.Substring(0,i);           
    //             //  audio.PlayOneShot(KeyBoardSound[Random.Range(0,KeyBoardSound.Count)]);
    //                 //start writhing sound
    //                 i++;
    //             yield return new WaitForSeconds(dur);            
    //      }
    //      if (isEnd)
    //      {           
    //             yield return new WaitForSeconds(2f);
    //             // dialogBox.SetActive(false);
    //              isRunning=false;
    //      }
    //      else{
    //           yield return new WaitForSeconds(2f);
    //           isRunning=false;
    //      }
    // }







}
