
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
//This whole class handles getting a target.
//If you need something changed here, have me do it. It is a really huge, complicated and interelated class.
//It checks every object in a certain range to see if it is a desired target. Then chooses which of them is closest or highest priority. In the future I may implement choosing target based an health etc etc.
//Also includes how long it takes to find a new target. How often it checks for a new target etc etc.
public class Targeting : MonoBehaviour{
    public List<string> targetByTags;
    public GameObject target;
    public bool targetsTeam = false;
    public string targeting = "nearest";
    public float targetingSpeed = 3;
    public float retargetingSpeed = 1;
    public bool retargetOnInterval = true;
    public float targetingRange = 100;
    public float lastTargetSet;
    private Condition newTargetWhen;
    public string newTargetOn;
  public string whileWaitingType = "nothing";
    public bool waiting = true;
    public Trigger ts;
    public Movement ms;
    public AI ais;
    public Trigger wts;
    private void Start()
    {

        Faction fs = Faction.GetFactionScript(gameObject);
        if (targetsTeam)
        {
            targetByTags = fs.allies;
        }
        else
        {
            targetByTags = fs.enemies;
        }
        lastTargetSet = Time.time;
        ts = gameObject.AddComponent<Trigger>();
        ais = GetComponent<AI>();
        ms = GetComponent<Movement>();
        SetRetargeting(newTargetOn);
        ts.condition = newTargetWhen;
        ts.name = "targeting trigger";
        wts = gameObject.AddComponent<Trigger>();
        wts.condition = () => waiting;
        wts.name = "waiting trigger";
        SetWaiting(whileWaitingType);
        Invoke("SetTarget", 0.1f);
    }
    //It does not have to be this way. I could make these conditions be set at a very high level. However for now this will work. THIS METHOD IS BEST FOR BULLET DUPLICATION THOUGH
    public void SetWaiting(string doWhileWaiting)
    {
        this.whileWaitingType = doWhileWaiting;
        switch (doWhileWaiting)
        {
            case "forward":
                Actor forward = () => Movement.Velocity(gameObject.transform.forward, ms.rb, ms.speed);
                wts.Set(forward, active:forward);
                break;
            case "death":
                wts.Set(() => Destroy(gameObject));
                break;
            case "hold":
                wts.Set(() => ais.SetHold(true),() =>ais.SetHold(false));
                break;
            case "nothing":
                wts.Set();
                break;
            default:
                wts.Set();
                print("wait type not recognized");
                break;

        }
    }
    public void SetRetargeting(string newTargetOn)
    {
        this.newTargetOn = newTargetOn;
        switch (newTargetOn)
        {
            case "":
                newTargetWhen = () => false;
                break;
            case "interval":
                newTargetWhen = () => Time.time > lastTargetSet + retargetingSpeed;
                ts.Set(SetTarget);
                break;
            case "kill":
                newTargetWhen = () => target == null;
                ts.Set(SetTarget, active:() => { SetTarget(); });
                break;
            default:
                newTargetWhen = () => false;
                print("retargeting type not recognized");
                break;

        }
    }
public void TargetAtSpeed()
    {
        if (targetingSpeed < 0)
        {
            SetTarget();
        }
        else
        {
            Invoke("SetTarget", targetingSpeed);
        }
    }
    //this could be static if I so desired
    public static GameObject GetTarget(List<string> targetTags, GameObject obj, string targeting, float range)
    {
        List<GameObject> gos = new List<GameObject>();
        foreach (string tag in targetTags)
        {
            gos.AddRange(GameObject.FindGameObjectsWithTag(tag));
        }
        List<GameObject> theGos = gos.Where((go => (go.transform.position - obj.transform.position).sqrMagnitude < range*range && !go.GetComponent<IsEquipment>())).ToList();


        switch (targeting)
        {
            case "nearest":
                return Nearest(obj, theGos);

            case "priority":
                return Priority(obj, theGos);

            default:
                print("defaulting to nearest");
                return Nearest(obj, theGos);

        }
    }
public void SetTarget()
    {
        lastTargetSet = Time.time;
        target = GetTarget(targetByTags,gameObject,targeting,targetingRange);
        waiting = (target == null);
        

    }
       
    
    public static GameObject Nearest(GameObject obj,List<GameObject> objs)
    {
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = obj.transform.position;
        foreach (GameObject go in objs)
        {

            Vector3 diff = go.transform.position - position;

            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance && go != obj.gameObject)
            {

                closest = go;
                distance = curDistance;
            }
        }
            return closest;
    }
    public static GameObject Priority(GameObject obj,List<GameObject> objs)
    {
        GameObject highestPriority = null;
        float priority = Mathf.Infinity;
        foreach (GameObject go in objs)
        {
            float curPriority = 0;
            curPriority = go.GetComponent<Priority>().priority;

            if (curPriority < priority)
            {
                highestPriority = go;
                priority = curPriority;
            }
        }
            if (priority < 1000)
            {
                return highestPriority;
        }
        else
        {
            return null;
        }
    }
    
}