using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TMP_Animator : MonoBehaviour
{
    [System.Serializable]
    public class TagSegment
    {
        public int stert, ernd;
        public AnimType type;
        public string yes;

        public TagSegment(int start, int end, AnimType whatkindasegmentthisisupinhere)
        {
            stert = start;
            ernd = end;
            type = whatkindasegmentthisisupinhere;
        }
    }

    public string JitterKey = "Jitter";

    public string PulsateKey = "Pulse";

    public string FlowKey = "Flow";

    private TextMeshProUGUI textmesh;

    private TMP_Text m_TextComponent;
    private bool hasTextChanged;

    [HideInInspector]
    public List<TagSegment> segments;

    public float JitterIntensity = 1.0f;

    public float PulseIntensity = 1.0f;
    public float PulseSpeed = 1.0f;
    public Color PulseColor = new Color(1.0f, 0.0f, 0.0f, 1.0f);
    public AnimationCurve PulseCurve;

    public float FlowIntensity = 1.0f;
    public float FlowSpeed = 1.0f;

    public float t = 0.0f;

    public enum AnimType
    {
        Nope,
        Jitter,
        Pulse,
        Flow,
        COUNT,
    }



    void Awake()
    {
        m_TextComponent = GetComponent<TMP_Text>();
    }

    void OnEnable()
    {
        // Subscribe to event fired when text object has been regenerated.
        TMPro_EventManager.TEXT_CHANGED_EVENT.Add(ON_TEXT_CHANGED);
    }

    void OnDisable()
    {
        TMPro_EventManager.TEXT_CHANGED_EVENT.Remove(ON_TEXT_CHANGED);
    }

    // Use this for initialization
    void Start()
    {
        textmesh = GetComponent<TextMeshProUGUI>();
        JitterKey = JitterKey.ToLower();
        PulsateKey = PulsateKey.ToLower();
        FlowKey = FlowKey.ToLower();
        //StartCoroutine(AnimateText());
    }

    void RemoveFirst(ref string baseText, string thingToHuntDownAndKill)
    {
        int index = baseText.IndexOf(thingToHuntDownAndKill);

        baseText = baseText.Substring(0, index) + baseText.Substring(index + thingToHuntDownAndKill.Length);
    }

    public void KillAllCustomTags(ref string rawText)
    {
        int i = rawText.IndexOf('<');
        int limiter = 0;

        while (i != -1 && limiter++ < 100)
        {
            int j = rawText.IndexOf('>', i + 1);

            string tag = rawText.Substring(i + 1, j - i - 1);
            string lTag = tag.ToLower();

            if (lTag == FlowKey || lTag == PulsateKey || lTag == JitterKey)
            {
                RemoveFirst(ref rawText, "<" + tag + ">");
                RemoveFirst(ref rawText, "</" + tag + ">");
            }

            i = rawText.IndexOf('<', i + 1);
        }
    }

    /// <summary>
    /// Updates the animator with the new text, and finds the points of it begining/ending.
    /// </summary>
    public void DynamicAnimator()
    {

        textmesh.ForceMeshUpdate();
        string rawText = textmesh.text;
        string text = textmesh.GetParsedText().ToLower();


        segments = new List<TagSegment>();

        int limit = 0;

        int i = text.IndexOf('<');
        while (i != -1 && limit < 100)
        {
            int j = text.IndexOf('>', i + 1);

            print(j);

            string thisBit = text.Substring(i + 1, j - i - 1);

            print(thisBit);

            if (thisBit == JitterKey)
            {
                int segmentEnd = text.IndexOf('<', i + 1);
                int tagLength = JitterKey.Length + 2;

                segments.Add(new TagSegment(i, segmentEnd - tagLength, AnimType.Jitter));

                RemoveFirst(ref text, "<" + JitterKey + ">");
                RemoveFirst(ref text, "</" + JitterKey + ">");

                int index = segments.Count - 1;
                segments[index].yes = text.Substring(segments[index].stert, segments[index].ernd - segments[index].stert);
            }
            else if (thisBit == PulsateKey)
            {
                int segmentEnd = text.IndexOf('<', i + 1);
                int tagLength = PulsateKey.Length + 2;

                segments.Add(new TagSegment(i, segmentEnd - tagLength, AnimType.Pulse));

                RemoveFirst(ref text, "<" + PulsateKey + ">");
                RemoveFirst(ref text, "</" + PulsateKey + ">");

                int index = segments.Count - 1;
                segments[index].yes = text.Substring(segments[index].stert, segments[index].ernd - segments[index].stert);
            }
            else if (thisBit == FlowKey)
            {
                int segmentEnd = text.IndexOf('<', i + 1);
                int tagLength = FlowKey.Length + 2;

                segments.Add(new TagSegment(i, segmentEnd - tagLength, AnimType.Flow));

                RemoveFirst(ref text, "<" + FlowKey + ">");
                RemoveFirst(ref text, "</" + FlowKey + ">");

                int index = segments.Count - 1;
                segments[index].yes = text.Substring(segments[index].stert, segments[index].ernd - segments[index].stert);
            }

            i = text.IndexOf('<');
            limit++;
        }

        KillAllCustomTags(ref rawText);
        //print("new text be: " + rawText);

        textmesh.text = rawText;
    }

    /// <summary>
    /// finds the start of tag KEY, and removes the tag from the string, while returning that point.
    /// </summary>
    int FindStart(string key, string text)
    {
        int start = 0;

        for (int i = 0; i < text.Length; ++i)
        {
            if (text[i] == '<')
            {
                if (text.Length > i + key.Length + 1)
                {
                    var check = text.Substring(i, key.Length + 1);

                    if (check.ToLower() == "<" + key.ToLower() + ">")
                    {
                        start = i;


                        break;
                    }

                }

            }
        }

        return start;

    }

    /// <summary>
    /// finds the End of tag KEY, and removes the tag from the string, while returning that point.
    /// </summary>
    int FindEnd(int start, string key, string text)
    {
        int end = start;
        for (int j = start + key.Length + 1; j < text.Length; ++j)
        {
            if (text[j] == '<')
            {
                if (text.Length > j + key.Length + 2)
                {
                    var check = text.Substring(j, key.Length + 2);

                    if (check.ToLower() == "</" + key.ToLower() + ">")
                    {
                        end = j;
                        text = text.Remove(j, j + (key.Length + 3));

                        break;
                    }

                }

            }

        }

        return end;
    }


    void ON_TEXT_CHANGED(Object obj)
    {
        if (obj == m_TextComponent)
            hasTextChanged = true;
    }

    void Update()
    {
        //or whatever. I'm doing this in Notepad++, so the names might be a bit off
        t += Time.deltaTime;
        //updateDT = 1.0f / UpdateRate;
    }


    public void AnimateText()
    {
        //Y'know.
        m_TextComponent.ForceMeshUpdate();

        //we gotta get the mesh info. we gotta.
        TMP_TextInfo textInfo = m_TextComponent.textInfo;
        TMP_MeshInfo[] cachedMeshInfo = textInfo.CopyMeshInfoVertexData();

        hasTextChanged = true;

        //materials.
        int numMaterials;
        Vector3[][] sourceVertices = new Vector3[1][];
        Vector3[][] destVertices = new Vector3[1][];
        Color[][] destColors = new Color[1][];


        //infinite loop 
        //while (true)
        {

            // we gotta get that NEW mesh if the text is diffrntetnt
            if (hasTextChanged)
            {
                // Update the copy of the vertex data for the text object.
                cachedMeshInfo = textInfo.CopyMeshInfoVertexData();

                numMaterials = textInfo.materialCount;

                sourceVertices = new Vector3[numMaterials][];
                destVertices = new Vector3[numMaterials][];
                destColors = new Color[numMaterials][];

                //Y'see, like
                //We can have more than 1 material for a text thing, so we gotta have multiple arrays cuz they can't be shared
                //among different materials.
                //I don't know why I'm commenting the stuff I didn't write.
                for (int i = 0; i < numMaterials; i++)
                {
                    int numVerts = cachedMeshInfo[i].vertices.Length;

                    sourceVertices[i] = new Vector3[numVerts];
                    destVertices[i] = new Vector3[numVerts];
                    destColors[i] = textInfo.meshInfo[i].mesh.colors;

                    System.Array.Copy(cachedMeshInfo[i].vertices, sourceVertices[i], numVerts);
                }

                hasTextChanged = false;
            }

            int characterCount = textInfo.characterCount;


            if (characterCount == 0)
            {
                //oh no, we don't have any text ;(
                //so sad.
                //better luck next time.
                //yield return new WaitForSeconds(0.25f);
                //continue;
            }

            int curSegment = 0;

            //We have passed the test above and have characters. Praise Talos.
            for (int i = 0; i < characterCount; i++)
            {
                TMP_CharacterInfo charInfo = textInfo.characterInfo[i];

                //why was there a comment on this line.
                //like
                //it's pretty friggin obvious what's going on here.
                if (!charInfo.isVisible)
                    continue;

                // Retrieve the pre-computed animation data for the given character.
                //VertexAnim vertAnim = vertexAnim[i]; //JESSE: this does not exist, does not know what vertexAnim[i] is

                // Get the index of the material used by the current character.
                int materialIndex = textInfo.characterInfo[i].materialReferenceIndex;

                // Get the index of the first vertex used by this text element.
                int vertexIndex = textInfo.characterInfo[i].vertexIndex;

                //I don't fuggin know how
                AnimType t = AnimType.Nope;

                //keep track of which animated segment we're in
                //...If there are any, anyway

                //print("hello?");
                if (curSegment < segments.Count)
                {
                    if (i >= segments[curSegment].stert && i < segments[curSegment].ernd)
                    {
                        t = segments[curSegment].type;
                        //print("we displayin a segment up in this bitch");
                    }
                    else if (i > segments[curSegment].ernd)
                    {
                        curSegment++;
                    }
                }

                //we did it

                //we didn't do it
                //destColors[materialIndex][vertexIndex + 0] = textmesh.color;
                //destColors[materialIndex][vertexIndex + 1] = textmesh.color;
                //destColors[materialIndex][vertexIndex + 2] = textmesh.color;
                //destColors[materialIndex][vertexIndex + 3] = textmesh.color;


                //do the thing
                switch (t)
                {
                    case AnimType.Jitter:
                        Jitter(destVertices[materialIndex], sourceVertices[materialIndex], vertexIndex);
                        break;
                    case AnimType.Pulse:
                        {
                            float intensity = Pulse(destVertices[materialIndex], sourceVertices[materialIndex], vertexIndex);

                            Color textColor = Color.Lerp(textmesh.color, PulseColor, intensity);
                            //textColor = Color.Lerp();

                            destColors[materialIndex][vertexIndex] = textColor;
                            destColors[materialIndex][vertexIndex + 1] = textColor;
                            destColors[materialIndex][vertexIndex + 2] = textColor;
                            destColors[materialIndex][vertexIndex + 3] = textColor;
                        }
                        break;
                    case AnimType.Flow:
                        Flow(ref destVertices[materialIndex], ref sourceVertices[materialIndex], vertexIndex);
                        break;
                    default:
                        Nothing(destVertices[materialIndex], sourceVertices[materialIndex], vertexIndex);
                        break;
                }

                System.Array.Copy(destVertices[materialIndex], textInfo.meshInfo[materialIndex].vertices, destVertices[materialIndex].Length);
                //System.Array.Copy(destColors[materialIndex], textInfo.meshInfo[materialIndex].vertices, destVertices[materialIndex].Length);
                // vertexAnim[i] = vertAnim; //error, vertexanim[i] does not exist
            }

            for (int i = 0; i < textInfo.meshInfo.Length; i++)
            {
                textInfo.meshInfo[i].mesh.vertices = textInfo.meshInfo[i].vertices;
                if (i < destColors.Length)
                    textInfo.meshInfo[i].mesh.colors = destColors[i];
                m_TextComponent.UpdateGeometry(textInfo.meshInfo[i].mesh, i);
            }

            //yield return new WaitForSeconds(updateDT);
        }
    }


    void Jitter(Vector3[] dst, Vector3[] src, int startIndex) //scared jittering text
    {
        Vector3 offset = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0.0f) * JitterIntensity;

        dst[startIndex] = src[startIndex] + offset;
        dst[startIndex + 1] = src[startIndex + 1] + offset;
        dst[startIndex + 2] = src[startIndex + 2] + offset;
        dst[startIndex + 3] = src[startIndex + 3] + offset;
    }

    void Nothing(Vector3[] dst, Vector3[] src, int startIndex)
    {
        dst[startIndex] = src[startIndex];
        dst[startIndex + 1] = src[startIndex + 1];
        dst[startIndex + 2] = src[startIndex + 2];
        dst[startIndex + 3] = src[startIndex + 3];
    }

    float Pulse(Vector3[] dst, Vector3[] src, int startIndex) //like a heartbeat
    {
        //I can't think of what things yes off the top of my head and I've got food to go eat so I'm do this later

        Matrix4x4 wew = Matrix4x4.Rotate(Quaternion.Euler(0.0f, 0.0f, t * 300.0f + startIndex * 50.0f));

        Vector3 center = (src[startIndex] + src[startIndex + 1] + src[startIndex + 2] + src[startIndex + 3]) * 0.25f;
        //Vector4 center2 = new Vector4(center.x, center.y, center.z, 1.0f);

        float roundedT = (t * PulseSpeed) % 1.0f;

        float intensity = PulseCurve.Evaluate(roundedT);

        dst[startIndex + 0] = (src[startIndex + 0] - center) * (1.0f + intensity * PulseIntensity) + center;
        dst[startIndex + 1] = (src[startIndex + 1] - center) * (1.0f + intensity * PulseIntensity) + center;
        dst[startIndex + 2] = (src[startIndex + 2] - center) * (1.0f + intensity * PulseIntensity) + center;
        dst[startIndex + 3] = (src[startIndex + 3] - center) * (1.0f + intensity * PulseIntensity) + center;

        //dst[startIndex] = wew * (src[startIndex] - center) * startIndex / 30.0f + center2;
        //dst[startIndex + 1] = wew * (src[startIndex + 1] - center) * startIndex / 30.0f + center2;
        //dst[startIndex + 2] = wew * (src[startIndex + 2] - center) * startIndex / 30.0f + center2;
        //dst[startIndex + 3] = wew * (src[startIndex + 3] - center) * startIndex / 30.0f + center2;

        return intensity;

    }

    void Flow(ref Vector3[] dst, ref Vector3[] src, int startIndex) //wavy sin wave stuff
    {
        float tempT = t * FlowSpeed + startIndex * 0.3f;
        Vector3 offset = new Vector3(Mathf.Cos(tempT), Mathf.Sin(tempT), 0.0f) * FlowIntensity;

        print(src[startIndex] + offset);

        dst[startIndex] = src[startIndex] + offset;
        dst[startIndex + 1] = src[startIndex + 1] + offset;
        dst[startIndex + 2] = src[startIndex + 2] + offset;
        dst[startIndex + 3] = src[startIndex + 3] + offset;
    }

    /// <summary>
    /// Structure to hold pre-computed animation data.
    /// </summary>
    private struct VertexAnim
    {
        public float angleRange;
        public float angle;
        public float speed;
    }

}
