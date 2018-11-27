using UnityEngine;
using System.Collections;


[RequireComponent (typeof (AudioSource))]
public class AudioAnalyzer : MonoBehaviour {


	public AudioSource _audio;
	public static float[] _samples = new float[512];
	public static float[] freqBands = new float[8];
	public static int CurrentFreq;

	void ResetWPM () {
		_samples = new float[512];
		freqBands = new float[8];
	}

	// Use this for initialization
	void Start () {
		_audio = GetComponent<AudioSource> ();


		_audio.clip = Microphone.Start("Built-in Microphone", true, 1, 4400);
		_audio.loop = true;
		while (!(Microphone.GetPosition("Built-in Microphone") > 0)){}
		_audio.Play();

	}
	
	// Update is called once per frame
	void Update () {
		GetSpectrumAudioSource ();
		MakeFreqBands ();
	}

	void GetSpectrumAudioSource () {
		_audio.GetSpectrumData (_samples, 0, FFTWindow.Blackman);
	}

	void MakeFreqBands () {
		int count = 0;
		for (int i = 0; i < 8; i++) {
			float average = 0;
			int sampleCount = (int)Mathf.Pow (2, i) * 2;

			if (i == 7) {
				sampleCount += 2;
			}
			for (int j = 0; j < sampleCount; j++) {
				average += _samples [count] * (count + 1);
					count++;
			}

			average /= count;

			freqBands [i] = average * 10;
		}
	}
}
