using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
//json
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Net;
using TMPro;

public class Jsonconvert : MonoBehaviour
{
    public GameObject Level;
    public GameObject Base;

    
    public Mesh SignMesh;
    public Mesh StartEndMesh;
    
    public Material WoodMat;
    public Material CrumbleMat;
    public Material DefaultMat;
    public Material DefaultColorMat;
    public Material IceMat;
    public Material GrabMat;
    public Material GrappableMat;

    public Material StartMat;
    public Material EndMat;

    public TMP_InputField Output;


//look at my beatiful mess :> have fun theres no comments and most of it is done by directly printing to a file good luck



    const string quote = "\"";
    private float complexity = 0;


    void Start()
    {
        //import();
        //export();
    }

    public void import()
    {
        //read import.json
        string json = File.ReadAllText(Output.text);

        //parse json
        JObject obj = JObject.Parse(json);

        //print nodes
        var nodes = obj["nodes"];
        var start = obj["start"];
        var finish = obj["finish"];
        //for each node in nodes array add 1 to node count
        //delete all children of Level
        foreach (Transform child in Level.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        GameObject Start = Instantiate(Base, Level.transform);

        GameObject Finish = Instantiate(Base, Level.transform);

        var startrawpos = start["position"];
        var finishrawpos = finish["position"];

        var startrawrot = start["rotation"];
        var finishrawrot = finish["rotation"];

        var rawstartrad = start["radius"].ToString();
        var rawfinishrad = finish["radius"].ToString();

        string startrawposx = startrawpos[0].ToString();
        string startrawposy = startrawpos[1].ToString();
        string startrawposz = startrawpos[2].ToString();

        string finishrawposx = finishrawpos[0].ToString();
        string finishrawposy = finishrawpos[1].ToString();
        string finishrawposz = finishrawpos[2].ToString();

        string startrawrotx = startrawrot[0].ToString();
        string startrawroty = startrawrot[1].ToString();
        string startrawrotz = startrawrot[2].ToString();
        string startrawrotw = startrawrot[3].ToString();

        string finishrawrotx = finishrawrot[0].ToString();
        string finishrawroty = finishrawrot[1].ToString();
        string finishrawrotz = finishrawrot[2].ToString();
        string finishrawrotw = finishrawrot[3].ToString();

        float startposx = float.Parse(startrawposx, CultureInfo.InvariantCulture.NumberFormat);
        float startposy = float.Parse(startrawposy, CultureInfo.InvariantCulture.NumberFormat);
        float startposz = float.Parse(startrawposz, CultureInfo.InvariantCulture.NumberFormat);

        float finishposx = float.Parse(finishrawposx, CultureInfo.InvariantCulture.NumberFormat);
        float finishposy = float.Parse(finishrawposy, CultureInfo.InvariantCulture.NumberFormat);
        float finishposz = float.Parse(finishrawposz, CultureInfo.InvariantCulture.NumberFormat);

        float startrotx = float.Parse(startrawrotx, CultureInfo.InvariantCulture.NumberFormat);
        float startroty = float.Parse(startrawroty, CultureInfo.InvariantCulture.NumberFormat);
        float startrotz = float.Parse(startrawrotz, CultureInfo.InvariantCulture.NumberFormat);
        float startrotw = float.Parse(startrawrotw, CultureInfo.InvariantCulture.NumberFormat);

        float finishrotx = float.Parse(finishrawrotx, CultureInfo.InvariantCulture.NumberFormat);
        float finishroty = float.Parse(finishrawroty, CultureInfo.InvariantCulture.NumberFormat);
        float finishrotz = float.Parse(finishrawrotz, CultureInfo.InvariantCulture.NumberFormat);
        float finishrotw = float.Parse(finishrawrotw, CultureInfo.InvariantCulture.NumberFormat);

        float startrad = float.Parse(rawstartrad, CultureInfo.InvariantCulture.NumberFormat);
        float finishrad = float.Parse(rawfinishrad, CultureInfo.InvariantCulture.NumberFormat);

        startposx = startposx * -1;
        startroty = startroty * -1;
        startrotz = startrotz * -1;

        finishposx = finishposx * -1;
        finishroty = finishroty * -1;
        finishrotz = finishrotz * -1;

        Start.transform.position = new Vector3(startposx, startposy, startposz);
        Finish.transform.position = new Vector3(finishposx, finishposy, finishposz);

        Start.transform.rotation = Quaternion.Euler(startrotx, startroty, startrotz);
        Finish.transform.rotation = Quaternion.Euler(finishrotx, finishroty, finishrotz);

        Start.transform.localScale = new Vector3(startrad, startrad/2, startrad);
        //get descripter component
        Start.GetComponent<descripter>().type = "start";
        Start.GetComponent<descripter>().shape = "start";
        Start.GetComponent<descripter>().radius = startrad;

        Finish.GetComponent<descripter>().type = "finish";
        Finish.GetComponent<descripter>().shape = "finish";
        Finish.GetComponent<descripter>().radius = finishrad;

        Finish.transform.localScale = new Vector3(finishrad, finishrad / 2, finishrad);

        //set start mesh to start mesh
        Start.GetComponent<MeshFilter>().mesh = StartEndMesh;
        Start.GetComponent<MeshRenderer>().material = StartMat;

        //set finish mesh to finish mesh
        Finish.GetComponent<MeshFilter>().mesh = StartEndMesh;
        Finish.GetComponent<MeshRenderer>().material = EndMat;

        Start.name = "Start";
        Finish.name = "Finish";




        foreach (var node in nodes)
        {
            //instance a instance of base inside Level
            GameObject newBase = Instantiate(Base, Level.transform);
            descripter descripter = newBase.GetComponent<descripter>();

            string type = node["type"].ToString();

            string shape = node["shape"].ToString();

            descripter.type = type;
            descripter.shape = shape;



            var rawpos = node["position"];

            var rawrot = node["rotation"];
         

            string rawposx = rawpos[0].ToString();
            string rawposy = rawpos[1].ToString();
            string rawposz = rawpos[2].ToString();

            string rawrotx = rawrot[0].ToString();
            string rawroty = rawrot[1].ToString();
            string rawrotz = rawrot[2].ToString();
            string rawrotw = rawrot[3].ToString();

            float posx = float.Parse(rawposx, CultureInfo.InvariantCulture.NumberFormat);
            float posy = float.Parse(rawposy, CultureInfo.InvariantCulture.NumberFormat);
            float posz = float.Parse(rawposz, CultureInfo.InvariantCulture.NumberFormat);

            float rotx = float.Parse(rawrotx, CultureInfo.InvariantCulture.NumberFormat);
            float roty = float.Parse(rawroty, CultureInfo.InvariantCulture.NumberFormat);
            float rotz = float.Parse(rawrotz, CultureInfo.InvariantCulture.NumberFormat);
            float rotw = float.Parse(rawrotw, CultureInfo.InvariantCulture.NumberFormat);

            posx = posx * -1;
            roty = roty * -1;
            rotz = rotz * -1;



            //create vector3 from position
            Vector3 pos = new Vector3(posx, posy, posz);

            //create quaternion from rotation
            Quaternion rot = new Quaternion(rotx, roty, rotz, rotw);


            if (type == "Sign")
            {
                rot = Quaternion.AngleAxis(0, Vector3.up);
            }


            newBase.transform.position = pos;
            newBase.transform.rotation = rot;

            //if type is Sign then don't do scale
            if (type != "Sign")
            {
                var rawsca = node["scale"];
                var rawcolor = node["color"];

                string rawscax = rawsca[0].ToString();
                string rawscay = rawsca[1].ToString();
                string rawscaz = rawsca[2].ToString();

                string rawcolorr = rawcolor[0].ToString();
                string rawcolorg = rawcolor[1].ToString();
                string rawcolorb = rawcolor[2].ToString();
                string rawcolora = rawcolor[3].ToString();

                float scax = float.Parse(rawscax, CultureInfo.InvariantCulture.NumberFormat);
                float scay = float.Parse(rawscay, CultureInfo.InvariantCulture.NumberFormat);
                float scaz = float.Parse(rawscaz, CultureInfo.InvariantCulture.NumberFormat);

                float colorr = float.Parse(rawcolorr, CultureInfo.InvariantCulture.NumberFormat);
                float colorg = float.Parse(rawcolorg, CultureInfo.InvariantCulture.NumberFormat);
                float colorb = float.Parse(rawcolorb, CultureInfo.InvariantCulture.NumberFormat);
                float colora = float.Parse(rawcolora, CultureInfo.InvariantCulture.NumberFormat);

                //create vector3 from scale
                Vector3 sca = new Vector3(scax, scay, scaz);

                newBase.transform.localScale = sca;
                //set material to defaultcolor
                newBase.GetComponent<Renderer>().material = DefaultColorMat;
                newBase.GetComponent<Renderer>().material.color = new Color(colorr, colorg, colorb, colora);
            }
            else
            {
                string text = node["text"].ToString();
                //get meshrender of base and set the model to sign mesh
                MeshFilter meshFilter = newBase.GetComponent<MeshFilter>();
                meshFilter.mesh = SignMesh;

                //set descripter script of base to text
                descripter.text = text;

                //change meshrender material
                MeshRenderer meshRenderer = newBase.GetComponent<MeshRenderer>();
                //set material to wood
                meshRenderer.material = WoodMat;
                newBase.GetComponent<Renderer>().material.color = Color.white;
            }

            if (type == "GRABBABLE_CRUMBLING")
            {
                string RawStable_time = node["stable_type"].ToString();
                string RawRespawn_time = node["stable_shape"].ToString();

                float stable_time = float.Parse(RawStable_time.ToString(), CultureInfo.InvariantCulture.NumberFormat);
                float respawn_time = float.Parse(RawRespawn_time.ToString(), CultureInfo.InvariantCulture.NumberFormat);

                descripter.stable_time = stable_time;
                descripter.respawn_time = respawn_time;

                newBase.GetComponent<Renderer>().material = CrumbleMat;
                newBase.GetComponent<Renderer>().material.color = Color.white;
            }

            if (type == "GRABBABLE")
            {
                newBase.GetComponent<Renderer>().material = GrabMat;
                newBase.GetComponent<Renderer>().material.color = Color.white;
            }

            if (type == "ICE")
            {
                newBase.GetComponent<Renderer>().material = IceMat;
                newBase.GetComponent<Renderer>().material.color = Color.white;
            }

            if (type == "DEFAULT")
            {
                newBase.GetComponent<Renderer>().material = DefaultMat;
                //reset material color
                newBase.GetComponent<Renderer>().material.color = Color.white;
            }
            
            if (type == "GRAPPLABLE")
            {
                newBase.GetComponent<Renderer>().material = GrappableMat;
                newBase.GetComponent<Renderer>().material.color = Color.white;
            }




        }


    }


    public void export()
        {
            //find start and finish in the level
            GameObject start = GameObject.Find("Start");
            GameObject finish = GameObject.Find("Finish");

            //flip the start and finish rotation y and z
            var startRotation = start.transform.rotation;
            var startPosition = start.transform.position;
            var finishRotation = finish.transform.rotation;
            var finishPosition = finish.transform.position;


            startPosition.x = startPosition.x * -1;
            startRotation.y = startRotation.y * -1;
            startRotation.z = startRotation.z * -1;

            finishPosition.x = finishPosition.x * -1;
            finishRotation.y = finishRotation.y * -1;
            finishRotation.z = finishRotation.z * -1;


            //clear Output.text.txt
            File.WriteAllText(@Output.text, string.Empty);


            Debug.Log(@"{");
            Debug.Log(@"	""title"": " + quote + this.GetComponent<LevelDetails>().title + quote + ", ");
            Debug.Log(@"	""description"": " + quote + this.GetComponent<LevelDetails>().description + quote + ", ");
            Debug.Log(@"	""creators"": " + quote + this.GetComponent<LevelDetails>().creators + quote + ", ");
            Debug.Log(@"	""checkpoints"": " + this.GetComponent<LevelDetails>().checkpoints + ", ");
            Debug.Log(@"");
            Debug.Log(@"        ""start"": {");
            Debug.Log(@"		""position"": " + "[" + startPosition.x + ", " + " " + startPosition.y + ", " + " " + startPosition.z + "], ");
            Debug.Log(@"		""rotation"": " + "[" + startRotation.x + ", " + " " + startRotation.y + ", " + " " + startRotation.z + ", " + " " + startRotation.w + "], ");
            Debug.Log(@"        ""radius"": " + start.GetComponent<descripter>().radius);
            Debug.Log(@"        },");
            Debug.Log(@"        ""finish"": {");
            Debug.Log(@"		""position"": " + "[" + finishPosition.x + ", " + " " + finishPosition.y + ", " + " " + finishPosition.z + "], ");
            Debug.Log(@"		""rotation"": " + "[" + finishRotation.x + ", " + " " + finishRotation.y + ", " + " " + finishRotation.z + ", " + " " + finishRotation.w + "], ");
            Debug.Log(@"        ""radius"": " + finish.GetComponent<descripter>().radius);
            Debug.Log(@"        },");
            Debug.Log(@"");
            Debug.Log(@"	""nodes"": [");

            File.AppendAllText(@Output.text, @"{" + Environment.NewLine);
            File.AppendAllText(@Output.text, @"	""title"": " + quote + this.GetComponent<LevelDetails>().title + quote + ", " + Environment.NewLine);
            File.AppendAllText(@Output.text, @"	""description"": " + quote + this.GetComponent<LevelDetails>().description + quote + ", " + Environment.NewLine);
            File.AppendAllText(@Output.text, @"	""creators"": " + quote + this.GetComponent<LevelDetails>().creators + quote + ", " + Environment.NewLine);
            File.AppendAllText(@Output.text, @"	""checkpoints"": " + this.GetComponent<LevelDetails>().checkpoints + ", " + Environment.NewLine);
            File.AppendAllText(@Output.text, @"" + Environment.NewLine);
            File.AppendAllText(@Output.text, @"        ""start"": {" + Environment.NewLine);
            File.AppendAllText(@Output.text, @"		""position"": " + "[" + startPosition.x + ", " + " " + startPosition.y + ", " + " " + startPosition.z + "], " + Environment.NewLine);
            File.AppendAllText(@Output.text, @"		""rotation"": " + "[" + startRotation.x + ", " + " " + startRotation.y + ", " + " " + startRotation.z + ", " + " " + startRotation.w + "], " + Environment.NewLine);
            File.AppendAllText(@Output.text, @"        ""radius"": " + start.GetComponent<descripter>().radius + Environment.NewLine);
            File.AppendAllText(@Output.text, @"        }," + Environment.NewLine);
            File.AppendAllText(@Output.text, @"        ""finish"": {" + Environment.NewLine);
            File.AppendAllText(@Output.text, @"		""position"": " + "[" + finishPosition.x + ", " + " " + finishPosition.y + ", " + " " + finishPosition.z + "], " + Environment.NewLine);
            File.AppendAllText(@Output.text, @"		""rotation"": " + "[" + finishRotation.x + ", " + " " + finishRotation.y + ", " + " " + finishRotation.z + ", " + " " + finishRotation.w + "], " + Environment.NewLine);
            File.AppendAllText(@Output.text, @"        ""radius"": " + finish.GetComponent<descripter>().radius + Environment.NewLine);
            File.AppendAllText(@Output.text, @"        }," + Environment.NewLine);
            File.AppendAllText(@Output.text, @"" + Environment.NewLine);
            File.AppendAllText(@Output.text, @"	""nodes"": [" + Environment.NewLine);



            foreach (Transform child in Level.transform)
            {

                //go to child script descriper
                var type = child.GetComponent<descripter>().type;
                var shape = child.GetComponent<descripter>().shape;
                var posx = child.GetComponent<Transform>().position.x;
                var posy = child.GetComponent<Transform>().position.y;
                var posz = child.GetComponent<Transform>().position.z;
                var rot = child.GetComponent<Transform>().rotation;
                var scax = child.GetComponent<Transform>().localScale.x;
                var scay = child.GetComponent<Transform>().localScale.y;
                var scaz = child.GetComponent<Transform>().localScale.z;
                var ColorR = child.GetComponent<Renderer>().material.color.r;
                var ColorG = child.GetComponent<Renderer>().material.color.g;
                var ColorB = child.GetComponent<Renderer>().material.color.b;
                var ColorA = child.GetComponent<Renderer>().material.color.a;
                var text = child.GetComponent<descripter>().text;
                var stable_time = child.GetComponent<descripter>().stable_time;
                var respawn_time = child.GetComponent<descripter>().respawn_time;
                var rotx = rot.x;
                var roty = rot.y;
                var rotz = rot.z;
                var rotw = rot.w;


                //if type is sign rotate the y axis by 180 degrees
                if (type == "Sign")
                {
                    rot = Quaternion.AngleAxis(180, Vector3.up);
                }

                rotx = rot.x;
                roty = rot.y;
                rotz = rot.z;
                rotw = rot.w;


                posx = posx * -1;
                roty = roty * -1;
                rotz = rotz * -1;

                if (type == "start" | type == "finish")
                {
                }
                else
                {
                    Debug.Log("		{");
                    File.AppendAllText(@Output.text, Environment.NewLine + @"		{" + Environment.NewLine);
                    Debug.Log(@"			""type"": " + quote + type + quote + ",");
                    Debug.Log(@"			""shape"": " + quote + shape + quote + ",");
                    File.AppendAllText(@Output.text, @"			""type"": " + quote + type + quote + "," + Environment.NewLine);
                    File.AppendAllText(@Output.text, @"			""shape"": " + quote + shape + quote + "," + Environment.NewLine);
                    Debug.Log(@"			""position"": [" + posx + "," + " " + posy + "," + " " + posz + "],");
                    Debug.Log(@"			""rotation"": [" + rotx + "," + " " + roty + "," + " " + rotz + "," + " " + rotw + "],");
                    File.AppendAllText(@Output.text, @"			""position"": [" + posx + "," + " " + posy + "," + " " + posz + "]," + Environment.NewLine);
                    File.AppendAllText(@Output.text, @"			""rotation"": [" + rotx + "," + " " + roty + "," + " " + rotz + "," + " " + rotw + "]," + Environment.NewLine);
                    if (type != "Sign")
                    {
                        Debug.Log(@"			""scale"": [" + scax + "," + " " + scay + "," + " " + scaz + "],");
                        File.AppendAllText(@Output.text, @"			""scale"": [" + scax + "," + " " + scay + "," + " " + scaz + "]," + Environment.NewLine);
                        if (type == "GRABBABLE_CRUMBLING")
                        {
                            File.AppendAllText(@Output.text, @"			""color"": [" + ColorR + "," + " " + ColorG + "," + " " + ColorB + "," + " " + ColorA + "]," + Environment.NewLine);
                            Debug.Log(@"			""color"": [" + ColorR + "," + " " + ColorG + "," + " " + ColorB + "," + " " + ColorA + "],");
                        }
                        else
                        {
                            Debug.Log(@"			""color"": [" + ColorR + "," + " " + ColorG + "," + " " + ColorB + "," + " " + ColorA + "]");
                            File.AppendAllText(@Output.text, @"			""color"": [" + ColorR + "," + " " + ColorG + "," + " " + ColorB + "," + " " + ColorA + "]" + Environment.NewLine);
                        }
                    }
                    if (type == "Sign")
                    {
                        Debug.Log(@"			""text"": " + quote + text + quote);
                        File.AppendAllText(@Output.text, @"			""text"": " + quote + text + quote + Environment.NewLine);
                    }
                    if (type == "GRABBABLE_CRUMBLING")
                    {
                        Debug.Log(@"			""stable_time"": " + stable_time + ",");
                        Debug.Log(@"			""respawn_time"": " + respawn_time);
                        File.AppendAllText(@Output.text, @"			""stable_time"": " + stable_time + "," + Environment.NewLine);
                        File.AppendAllText(@Output.text, @"			""respawn_time"": " + respawn_time + Environment.NewLine);
                    }
                    Debug.Log("		},");
                    File.AppendAllText(@Output.text, "		},");


                    //if type is sign add 5 to complexity
                    if (type == "Sign")
                    {
                        complexity = complexity + 5;
                    }
                    //if type is crumblable add 3 to complexity
                    else if (type == "GRABBABLE_CRUMBLING")
                    {
                        complexity = complexity + 3;
                    }
                    //otherwise add 2 to complexity
                    else
                    {
                        complexity = complexity + 2;
                    }
                }
            }

            //remove the last line of the file

            File.WriteAllText(@Output.text, File.ReadAllText(@Output.text).TrimEnd(','));


            File.AppendAllText(@Output.text, Environment.NewLine + "	]," + Environment.NewLine);
            //complexity
            File.AppendAllText(@Output.text, @"	""complexity"": " + complexity + Environment.NewLine);
            File.AppendAllText(@Output.text, "}");

        }
}
