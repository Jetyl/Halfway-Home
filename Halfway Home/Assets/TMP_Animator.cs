using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TMP_Animator : MonoBehaviour
{

    public string JitterKey = "Jitter";

    public string PulsateKey = "Pulse";

    public string FlowKey = "Flow";

    private TextMeshProUGUI textmesh;

    private TMP_Text m_TextComponent;
    private bool hasTextChanged;

    public float JitterIntensity = 1.0f;
    public float PulseIntensity = 1.0f;
    public float FlowIntensity = 1.0f;
    public float FlowSpeed = 1.0f;

    float t = 0.0f;

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
    void Start ()
    {
        textmesh = GetComponent<TextMeshProUGUI>();
        AnimateText();
    }

    /// <summary>
    /// Updates the animator with the new text, and finds the points of it begining/ending.
    /// </summary>
    public void DynamicAnimator(ref string text)
    {

        //for jitter text
        int start = FindStart(JitterKey, ref text);
        if(start != 0)
        {
            int end = FindEnd(start, JitterKey, ref text);

            if (end == start)
                Debug.LogError("ERROR: Ending Tag (" + JitterKey + ") missing for line: " + text);

            //Jitter(); //currently doesn't call correctly
        }

        //for pulse text
        start = FindStart(PulsateKey, ref text);
        if (start != 0)
        {
            int end = FindEnd(start, PulsateKey, ref text);

            if (end == start)
                Debug.LogError("ERROR: Ending Tag (" + PulsateKey + ") missing for line: " + text);

            //Pulse(); //currently doesn't call correctly
        }

        //for flowy text
        start = FindStart(FlowKey, ref text);
        if (start != 0)
        {
            int end = FindEnd(start, FlowKey, ref text);

            if (end == start)
                Debug.LogError("ERROR: Ending Tag (" + FlowKey + ") missing for line: " + text);

            //Flow(); //currently doesn't call it correctly
        }

    }

    /// <summary>
    /// finds the start of tag KEY, and removes the tag from the string, while returning that point.
    /// </summary>
    int FindStart(string key, ref string text)
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

                        text = text.Remove(i, i + (key.Length + 2));
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
    int FindEnd(int start, string key, ref string text)
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
    }
    

    IEnumerator AnimateText()
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

        //infinite loop 
        while (true)
        {
            // we gotta get that NEW mesh if the text is diffrntetnt
            if (hasTextChanged)
            {
                // Update the copy of the vertex data for the text object.
                cachedMeshInfo = textInfo.CopyMeshInfoVertexData();

                numMaterials = textInfo.materialCount;

                sourceVertices = new Vector3[numMaterials][];

                //Y'see, like
                //We can have more than 1 material for a text thing, so we gotta have multiple arrays cuz they can't be shared
                //among different materials.
                //I don't know why I'm commenting the stuff I didn't write.
                for (int i = 0; i < numMaterials; i++)
                {
                    int numVerts = cachedMeshInfo[i].vertices.Length;

                    sourceVertices[i] = new Vector3[numVerts];
                    destVertices[i] = new Vector3[numVerts];

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
                yield return new WaitForSeconds(0.25f);
                continue;
            }

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

                //do the thing
                switch (t)
                {
                    case AnimType.Jitter:
                        Jitter(destVertices[materialIndex], sourceVertices[materialIndex], vertexIndex);
                        break;
                    case AnimType.Pulse:
                        Pulse(destVertices[materialIndex], sourceVertices[materialIndex], vertexIndex);
                        break;
                    case AnimType.Flow:
                        Flow(destVertices[materialIndex], sourceVertices[materialIndex], vertexIndex);
                        break;
                    default:
                        Nothing(destVertices[materialIndex], sourceVertices[materialIndex], vertexIndex);
                        break;
                }

                System.Array.Copy(destVertices[materialIndex], textInfo.meshInfo[materialIndex].vertices, destVertices[materialIndex].Length);

               // vertexAnim[i] = vertAnim; //error, vertexanim[i] does not exist
            }
        }
    }


    void Jitter(Vector3[] dst, Vector3[] src, int startIndex) //scared jittering text
    {
        //Sorta like flow, but we do crazy and the sine wave just sorta looks random
        float tempT = t * FlowSpeed * 16.123847f + 11.126435f * startIndex;
        float scalar = Mathf.Sin(t * FlowSpeed * 16.123847f) * JitterIntensity;
        Vector3 offset = new Vector3(Mathf.Cos(tempT), Mathf.Sin(tempT), 0.0f) * scalar;

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

    void Pulse(Vector3[] dst, Vector3[] src, int startIndex) //like a heartbeat
    {
        //I can't think of what things yes off the top of my head and I've got food to go eat so I'm do this later

        dst[startIndex] = src[startIndex];
        dst[startIndex + 1] = src[startIndex + 1];
        dst[startIndex + 2] = src[startIndex + 2];
        dst[startIndex + 3] = src[startIndex + 3];
    }

    void Flow(Vector3[] dst, Vector3[] src, int startIndex) //wavy sin wave stuff
    {
        float tempT = t * FlowSpeed + startIndex * 0.3f;
        Vector3 offset = new Vector3(Mathf.Cos(tempT), Mathf.Sin(tempT), 0.0f) * FlowIntensity;

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
