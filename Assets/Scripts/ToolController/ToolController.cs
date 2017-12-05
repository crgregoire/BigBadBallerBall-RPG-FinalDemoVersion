using UnityEngine;

//Anything that has a use. Includes the reload time, kick and if available spawning of said use.
//Pretty much the gun controller.
public class ToolController : MonoBehaviour
{
    public GameObject holder;
    public GameObject projectile;
    public GameObject buffedProjectile = null;
    public int kick = 0;
    public int rotKick = 0;
    public int kickVert = 0;
    public int kickHor = 0;
    public bool hasKick = true;
    public bool randKick = false;
    //public string[] targets;
    // public IBuff[] buffs;
    //IBuff is an interface that will accept a GameObject and return void. Basically sets stats if object has them.
    public float reloadTime = 10;
    public float lastUse;
    public Rigidbody kickWhat;
    public Rigidbody kickedRot;
    public Transform kickBy;
    public Trigger trs;
    public Trigger trs2;
    public bool autoUse = false;
    private Rigidbody playerRotation;
    public GameObject bulletList;
    public bool noAmmo = false;
    public Modifiers ms;


    private void Start()
    {
        ms = GetComponentInParent<Modifiers>();
        bulletList = GameObject.Find("BulletList");
        
        kickBy = transform;
        if (!noAmmo)
        {
            projectile = transform.GetChild(0).gameObject;
        }
        lastUse = Time.time-reloadTime*ms.reload;
        //SetBuffed(projectile, buffs);

        trs = gameObject.AddComponent<Trigger>();
        trs.Set(activate: Use);
        trs.condition = () => Time.time > lastUse + reloadTime *ms.reload;
        trs.enabled = autoUse;
        if(holder.name == "PlayerBody")
        {
            PlayerController ps = holder.GetComponent<PlayerController>();
            ToggleAuto(false);
            trs2 = gameObject.AddComponent<Trigger>();
            trs2.Set(activate: Use, active: Use);
			Create.Sound ("Sword Swish 1", gameObject.transform.position);
            trs2.condition = () => Input.GetMouseButtonDown(0) && ps.enabled;
            kickedRot = holder.GetComponent<Rigidbody>();
        }

    }
    public void ToggleAuto(bool? set = null)
    {
        if (set == null)
        {
            autoUse = !autoUse;
            trs.enabled = autoUse;
        }
        else
        {
            autoUse = (bool)set;
            trs.enabled = (bool)set;
        }

    }
    private void SetProjectile (){
        projectile = transform.GetChild(0).gameObject;
    }
    void OnDeath()
    {
        Destroy(projectile);
    }
    public void Use()
    {
        if (Time.time > lastUse + (reloadTime*ms.reload))
        {
            
                lastUse = Time.time;
            if (!noAmmo)
            {

                //should be done once, but im feeling lazy
                //FIX
                SetProjectile();
                Projectile ps = projectile.GetComponent<Projectile>();
                ps.firer = gameObject;
                //buffedprojectile. if buffed you will need to instantiate on buff change to save on performance then just delete the old buff each change.
                //if projectile. Could have a sword using this class for reload time and kick. it would have collision as trigger and function as impact damage. NO THIS IS NOT QUITE RIGHT
                GameObject proj = Instantiate(projectile, gameObject.transform.position + (transform.forward * ps.distance), gameObject.transform.rotation, bulletList.transform/*ps.firer.transform.parent.transform.parent.transform*/);
                if (!proj.activeSelf)
                {
                    proj.SetActive(true);
                }
                proj.GetComponent<Projectile>().fs = Faction.GetFactionScript(gameObject);
                CopyComponent.Copy(ms,proj);
                //Movement ms = proj.GetComponent<Movement>();
            }
                /*
                ms.defaultMovement = projectile.GetComponent<Movement>().defaultMovement;
                ms.speed += kickWhat.velocity.magnitude *2;
                */
                // Targets will be handled in each projectile. Heals vs bullets, makes more sense. SetTargets(bullet, targets)

                if (hasKick)
                {
                    ApplyKick(kickWhat, kickedRot, kickBy, randKick, kick, kickVert, rotKick, kickHor);
                }
            
        }

    }
    public void SetHolder(GameObject holder)
    {
        this.holder = holder;
        kickWhat = holder.GetComponent<Rigidbody>();

            kickedRot = kickWhat;

    }
    //Have Buff be an interface for something that modifies stats of an object. Either by adding scripts, setting variables or whatever.
    //Accepts a game object, returns a gameObject. The first buff to make is basic stats. Buffs damage and speed

    /* public void SetBuffed(GameObject projectile, IBuff[] buffs)
     { 
        buffedProjectile = projectile;
 if (buffs)
         {
             foreach(IBuff buff in buffs){
             buff(buffedProjectile);
             }
         }

       }

     }*/
    /*
    public void SetTargets(GameObject projectile, string[] targets)
    {
        foreach (string target in targets)
        {
            projectile.GetComponents<Sendable>()[0].targets.Add(target);
        }
    }
    */
    public static int RandomSign(){
            return (int)((Random.Range(0, 2) - 0.5) * 2);
        }
public static void ApplyKick(Rigidbody kickWhat, Rigidbody kickRot,Transform kickBy, bool frandKick = false, int fkick = 0, int fkickVert = 0, int frotKick = 0, int fkickHor = 0)
    {
  
        if (frandKick)
        {
            fkick = Mathf.FloorToInt(Random.value * fkick);
            fkickVert = Mathf.FloorToInt(Random.value * fkickVert);
            frotKick = Mathf.FloorToInt(Random.value * frotKick) * RandomSign();
            fkickHor = Mathf.FloorToInt(Random.value * fkickHor) * RandomSign();
        }
        if (fkick != 0)
        {
            kickWhat.AddForce(kickBy.forward * -fkick);
        }
        if (fkickVert != 0)
        {
            kickWhat.AddForce(kickBy.up * fkickVert);
        }
        if (frotKick != 0)
        {
            kickRot.AddTorque(new Vector3(0f, frotKick, 0f));
        }
        if (fkickHor != 0)
        {
            kickWhat.AddForce(kickBy.right * fkickHor);
        }
    }

}