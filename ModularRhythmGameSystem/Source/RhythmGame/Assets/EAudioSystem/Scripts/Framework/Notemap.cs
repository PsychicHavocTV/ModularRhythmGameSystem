using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Notemap
{
    public string noteTiming { get; set; }
    public float songBPM = 0;
    public string songName { get; set; }

    public AudioClip song = null;
    public string songArtist { get; set; }
    public string levelName = "";
    public decimal[] timingDecimals { get; set; }

    public string[] noteTimings { get; set; }
    public string[] songTiming = new string[] { };
    public float[] noteTimingsNUMS = new float[] { };
    public IList<float> timings = new List<float>();
}
