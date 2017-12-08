using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
//A series of functions used to create things. Basically coded prefabs.
public class Create : MonoBehaviour
{
    //This is my way of getting around prefabs and vanishing values in editor. Also it allows for nested prefabs. I am not sure if I want all objects to be generated in this manner.
    //This is to be called by a random generator, or perhaps a create Gunman function
    //make the random functions (minus the spawner) all delegates, because they are all fairly the same object in new out, then you could randomize which of the functions are called. Etc etc manipulate however
    private static string[] shapes = { "sphere", "cube", "cylinder", "capsule" };
    #region basic



    public static void SetScale(GameObject obj, float x = 2, float y = 2, float z = 2, float mult = 1)
    {
        Transform tObj = obj.transform;

            tObj.localScale = new Vector3(x*mult,y*mult,z*mult);

    }
    public static void ModScale(GameObject obj, float x = 1, float y = 1, float z = 1, float mult = 1)
    {
        Transform tObj = obj.transform;
            tObj.localScale = new Vector3(tObj.localScale.x*x*mult, tObj.localScale.y*y*mult, tObj.localScale.z*z*mult);
        
    }

    public static GameObject GetPrefab(string prefab, bool instantiate = false)
    {
        string location = "Prefabs/" + prefab;
        GameObject nonexistent = Resources.Load(location, typeof(GameObject)) as GameObject;
        if (instantiate == false)
        {
            return nonexistent;
        }
        else
        {
            return Instantiate(nonexistent);
        }

    }
    
    public static void SetColor(GameObject obj, Color color, string mode = "_Color")
    {

        Renderer rend = obj.GetComponent<Renderer>();
        color.a = rend.material.color.a;
        rend.material.SetColor(mode, color);
    }
    public static void SetMaterial(GameObject obj, string material)
    {
        string location = "Materials/" + material;
        obj.GetComponent<Renderer>().material = Resources.Load(location, typeof(Material)) as Material;

    }
    public static void SetFade(GameObject obj, int alpha = 1, string mode = "_Color")
    {
        Color color = obj.GetComponent<Material>().color;
        obj.GetComponent<Renderer>().material.SetColor(mode, new Color(color.r, color.g, color.b, alpha));
    }
    #endregion
    #region target involved
    public static void AddReward(GameObject obj, int amount = 1, GameObject Drop = null, Vector3 offset = new Vector3(), float velocity = 0.001f)
    {
        if (Drop == null)
        {
            Drop = GetPrefab("Coin");
        }
        DropScatter script = obj.AddComponent<DropScatter>();
        script.amount = amount;
        script.drop = Drop;
        script.offset = offset;
        script.velocity = velocity;
    }



    #endregion
    #region assembling
    public static void AddHealth(GameObject toWhat,int  health = 50)
    {
        GameObject healthBox = GetPrefab("HealthBox");
        GameObject healthBar = Instantiate(healthBox);
        Follow followScript = healthBar.GetComponent<Follow>();
        GameObject body = toWhat.GetComponent<Unit>().body;
        followScript.target = body;
        followScript.offset = new Vector3(0f, body.transform.localScale.y, 0f);
        
        Health healthScript = toWhat.AddComponent<Health>();
        healthScript.maxHealth = health;
        healthScript.healthBox = healthBar.transform;
        SetScale(healthBar, 1,0.1f,0.1f);
        ModScale(healthBar, x: ((float)health) / 100);
        healthBar.transform.parent = toWhat.transform;
    }

    //amount should probably be moved to vars so it could be updated easily and accessed easily. Make higher worth have worth bar etc etc bounty yata yata. Plus it would be settable at make unit.
    public static void AddLoadout(string name, GameObject to, bool equip = false)
    {

        Equipment es = to.GetComponent<Equipment>();

        if (!es)
        {
            es = to.AddComponent<Equipment>();
        }
        if (!es.available.ContainsKey(name))
        {
            GameObject start = GetPrefab(name);
            GameObject loadout = Instantiate(GetPrefab(name), to.transform.position, to.transform.rotation, to.transform.parent);
 
            es.NewAvailable(name, loadout);
            
            GetRefs(loadout);
            for (int i = 0; i < loadout.transform.childCount; i++)
            {
                Quaternion startRot = start.transform.GetChild(i).gameObject.transform.rotation;
                Vector3 startPos = start.transform.GetChild(i).gameObject.transform.position;
                GameObject go = loadout.transform.GetChild(i).gameObject;

                go.tag = to.tag;
                DamageTransfer ds = go.GetComponent<DamageTransfer>();
                if (!ds)
                {
                    ds = go.AddComponent<DamageTransfer>();
                }
                go.AddComponent<IsEquipment>();
                CenterMass cs = go.AddComponent<CenterMass>();
                cs.relativePos = Quaternion.Inverse(startRot) * (startPos*-1);
                
            }
            loadout.SetActive(false);
        }
        else
        {

            print("already has loadout");
        }
        if (equip)
        {
            es.currentNum = es.availableCount;
            EquipLoadout(name, to);
        }
    }

    //cannot find prefabs that are in folders currently. Due to GetPrefabs limitations.
    //Could be done, simply add a directory to is ref. Then pass that to get prefab.
    public static void GetRefs(GameObject parent)
    {
        
        if (parent)
        {
            int length = parent.transform.childCount;
            GameObject[] children = new GameObject[length];
            for (int i = 0; i < length; i++)
            {
                children[i] = parent.transform.GetChild(i).gameObject;
            }
                for (int i = 0; i < length; i++)
            {
                GameObject childRef = children[i];
                GameObject child;
                if (childRef.GetComponent<IsRef>())
                {
                    GameObject prefab = GetPrefab(childRef.name);
                    if (prefab)
                    {
                        child = Instantiate(prefab, childRef.transform.position, childRef.transform.rotation, childRef.transform.parent);
                        childRef.transform.parent = GameObject.Find("GlobalScripts").transform;
                        Destroy(childRef);
                    }
                    else
                    {
                        child = null;
                        print("could not find prefab reference");
                    }
                }
                else
                {
                    child = childRef;
                }
                //Recursive check if children have children, then apply to them as well.
                GetRefs(child);
            }
        }
    }

    public static void AddFaction(GameObject go, string faction = "Enemy")
    {
        Faction fs = go.AddComponent<Faction>();
        fs.faction = faction;
    }
    public static void EquipLoadout(string name, GameObject to)
    {
        DequipLoadout(to);
        Equipment es = to.GetComponent<Equipment>();
        GameObject loadout;
        if (es.available.ContainsKey(name))
        {
            loadout = es.available[name];
            loadout.SetActive(true);
            GameObject start = GetPrefab(name);
            for (int i = 0; i < start.transform.childCount; i++)
            {
                GameObject Go = loadout.transform.GetChild(i).gameObject;
                /*
                if (loadout.transform.childCount == 2) {
                    print(loadout.transform.childCount + " " + loadout.name + loadout.transform.GetChild(0).name + loadout.transform.GetChild(1).name);
                }*/
                GameObject startGo = start.transform.GetChild(i).gameObject;
                AttachFixed(Go, to, startGo.transform.localPosition,startGo.transform.rotation);
            }
            es.current = loadout;
        }
        else
        {
            print("trying to equip loadout not posessed");
        }
        
    }
    public static void DequipLoadout(GameObject from)
    {
        GameObject current = from.GetComponent<Equipment>().current;
        if (current)
        {
            current.SetActive(false);
            //Destroy(current);
        }
    }
    public static void AttachFixed(GameObject obj, GameObject to, Vector3 displacement, Quaternion rotation = new Quaternion())
    {
        obj.transform.position = to.transform.position + (to.transform.rotation * displacement);
        
        obj.transform.rotation = to.transform.rotation;
        obj.transform.rotation *=rotation;
        FixedJoint already = obj.GetComponent<FixedJoint>();
        if (already)
        {
            Destroy(already);
        }
        ToolController ts = obj.GetComponent<ToolController>();
        if (ts)
        {
            ts.SetHolder(to);
        }
        FixedJoint joint = obj.AddComponent<FixedJoint>();
        joint.connectedBody = to.GetComponent<Rigidbody>();

    }
    #endregion
    #region creators

    public static GameObject Unit(Vector3 location, string bodyName, string faction, string loadout = "none", int reward = 1, float level = 1)
    {
        GameObject bodyFab = GetPrefab(bodyName);
        GameObject body = Instantiate(bodyFab,location,bodyFab.transform.rotation);
        body.name = bodyName;
        body.tag = faction;
        SetMaterial(body, faction);
        int health = Mathf.FloorToInt(10 * level);

        GameObject unit = new GameObject(faction);
        Unit us = unit.AddComponent<Unit>();
        us.body = body;
        body.transform.parent = unit.transform;
        AddHealth(unit, health);
        body.AddComponent<DamageTransfer>();

        AddFaction(unit,faction);
        if (loadout != "none")
        {
            AddLoadout(loadout, body, true);
        }
        unit.AddComponent<Modifiers>();
        return unit;
    }


    //different method for spot lights as rotation is involved.
    //This should use a prefab.
    public static GameObject ALight(Vector3 position, Color? color = null, float range = 10, float intensity = 1f, float indirect = 0, Vector3 angle = new Vector3(), LightType? type = null)
    {


        GameObject lightGameObject = new GameObject("The Light");
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        SetMaterial(sphere, "Emissive");
        SetColor(sphere, color ?? Color.white, "_EmissionColor");


        sphere.transform.parent = lightGameObject.transform;
        Light lightComp = lightGameObject.AddComponent<Light>();
        lightComp.color = color ?? Color.white;
        lightGameObject.transform.position = position;
        lightComp.range = range;
        lightComp.intensity = intensity;
        lightComp.bounceIntensity = indirect;
        lightGameObject.transform.eulerAngles = angle;

        lightComp.type = type ?? LightType.Point;
        return lightGameObject;
    }
    #endregion
    #region spawner


    //Make a master projectile creator for randomization. Takes list of behaviours to add, and randomizes their inputs. SetProjectileAspects
    public static void AddProjectile(GameObject obj, GameObject firer = null, GameObject spawner = null, int collisionsAllowed = -1, float distance = 1)
    {
        Projectile ps = obj.AddComponent<Projectile>();
        ps.firer = firer;
        ps.spawner = spawner;
        ps.collisionsAllowed = collisionsAllowed;
        ps.distance = distance;

    }
    //may want isPlayers to be isSeperate. That way its easy to add shields and such to things. that need seperate collisions. Add rotate about and have velocity of (var) have rot of (var)
    public static void AttachSpawner(GameObject spawner, GameObject body, GameObject kicked = null, GameObject kickedRot = null, Vector3 displacement = new Vector3()/*, bool isPlayers = false*/)
    {
        kicked = kicked ?? body;
        kickedRot = kickedRot ?? body;
        ToolController toolScript = spawner.GetComponent<ToolController>();
        toolScript.kickWhat = kicked.GetComponent<Rigidbody>();
        toolScript.kickedRot = kickedRot.GetComponent<Rigidbody>();
        AttachFixed(spawner, body, displacement);
        
        /*
        if (isPlayers)
        {
            RotateAbout
        }*/
    }

    
    public static void AddSpawner( GameObject spawner, GameObject projectile, GameObject kickBy = null, int kick = 0, int kickVert = 0, int kickRot = 0, int kickHor = 0, float reloadTime = 10, bool randKick = false, bool hasKick = true, bool auto = false /*,Buff[]? buffs = null*/)
    {
            kickBy = kickBy ?? spawner;
        
        ToolController toolScript = spawner.AddComponent<ToolController>();
        toolScript.autoUse = auto;
        toolScript.projectile = projectile;
        toolScript.kick = kick;
        toolScript.kickBy = kickBy.transform;
        toolScript.kickVert = kickVert;
        toolScript.kickHor = kickHor;
        toolScript.rotKick = kickRot;
        toolScript.randKick = randKick;
        toolScript.hasKick = hasKick;
        toolScript.reloadTime = reloadTime;
    }
    #endregion
    #region audio
	public static void Sound(string name, Vector3? location = null, float duration = -1, int times = 1, GameObject altLocation = null, float volume = 1f)
	{
		AudioClip clip = Resources.Load<AudioClip>("Sounds/"+name);
		if (duration < 0)
		{
			duration = clip.length;
		}
		Vector3 pos;
		if (altLocation != null)
		{
			pos = altLocation.transform.position;
		}
		else
		{
			pos = location ?? GameObject.Find("Main Camera").transform.position;
		}
		//      	SoundRepeat(clip, pos, duration, times, volume);
		AudioSource.PlayClipAtPoint(clip, pos, volume);
	}



    public IEnumerator SoundRepeat(AudioClip clip, Vector3 location, float duration, int number, float volume)
    {
        int i = 0;
        while (i < number)
        {
            AudioSource.PlayClipAtPoint(clip, location, volume);
            yield return new WaitForSeconds(duration); //wait 1 second per interval
            i++;
        }
    }
    #endregion






}
