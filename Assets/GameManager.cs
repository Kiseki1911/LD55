using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public int counter;
    public Volume volume;
    public AnimationCurve curve;
    public AnimationCurve secCurve;
    public List<GameObject> circiles = new List<GameObject>();
    public Light2D light2D;
    float curTime;
    public float randomHSV;
    bool summoned;
    public GameObject item;
    bool SpinAll;
    float mod;
    // Start is called before the first frame update
    void Start()
    {
        randomHSV = Random.Range(0, 360);
        LightUp(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (counter == 0)
        {
            randomHSV += Time.deltaTime * 30;
            if (randomHSV > 360) randomHSV -= 360;
            circiles[0].GetComponent<SpriteRenderer>().color = Color.HSVToRGB((randomHSV) / 360f, 1, 1);
        }
        if (counter < circiles.Count)
        {
            circiles[counter].transform.rotation = Quaternion.Euler(0, 0, mod * (10 + counter * 20) * Time.time);
            Camera.main.backgroundColor = Mathf.Lerp(Camera.main.backgroundColor.r, 1 - counter / 4f, 0.04f) * Color.white;
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                counter++;
                LightUp(counter);
                curTime = Time.time;
            }
        }
        else
        {
            Bloom item;
            volume.profile.TryGet(out item);
            item.intensity.value = 10 * curve.Evaluate(Time.time - curTime);
            float val = secCurve.Evaluate(Time.time - curTime);
            light2D.intensity = 1 - val / 2f;
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, new Vector3(5 * val, 0, -10), 0.05f);
            if (Time.time - curTime >= 1.8f && !summoned)
            {
                summoned = true;
                Summon();
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            counter = 0;
            SceneManager.LoadScene(0);
        }
        if (SpinAll)
        {
            foreach (GameObject item in circiles)
            {
                item.transform.localEulerAngles = new Vector3(0, 0, item.transform.localEulerAngles.z - Time.deltaTime * 20);
            }
        }
    }
    public void LightUp(int val)
    {
        if (val < circiles.Count)
        {
            circiles[val].SetActive(true);
            circiles[val].GetComponent<SpriteRenderer>().color = Color.HSVToRGB((randomHSV + val * 15) / 360f, 1, 1);
            mod = Random.value > 0.5f ? 1 : -1;
        }
    }
    IEnumerator Result()
    {
        yield return new WaitForSeconds(1f);
        item.GetComponent<ParticleSystem>().Play();
        item.GetComponent<SpriteRenderer>().sprite = ResultManager.Instance.items[(int)(randomHSV/36)].sprite;
        yield return new WaitForSeconds(1.5f);
        SpinAll = true;
        yield return new WaitForSeconds(.1f);
        ResultManager.Instance.ShowResult();
        
    }
    public void Summon()
    {
        item.SetActive(true);
        StartCoroutine(Result());
    }

    public void Reload()
    {
        counter = 0;
        SceneManager.LoadScene(0);
    }
}
