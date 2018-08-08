using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using MyUtility;

[RequireComponent(typeof (AudioSource))]
public class AudioManager : Singleton<AudioManager>
{
//------------------------------------------------------------------------CONSTANTS:

//---------------------------------------------------------------------------FIELDS:
	
	public AudioSource ASource;
	public AudioClip _AudioClip;
	public bool UsingMic;
	public AudioMixerGroup MixerGroupMic, MixerGroupMaster;
	public string SelectedDevice;
	public float BufferIncreaseVal = 1.2f, BufferDecreaseVal = 0.005f, AudioProfileVal;

	[System.NonSerialized]
	public float[] 			Samples = new float[512],
							AudioBand = new float[8],
							AudioBandBuffer = new float[8];
	public static float Amplitude, AmplitudeBuffer;						

	private float[]	FreqBand = new float[8],
					BandBufferVal = new float[8],
					bufferDecrease = new float[8],
					freqBandHighest = new float[8];
	private float amplitudeHighest;
	
//---------------------------------------------------------------------MONO METHODS:

	void Start() 
	{
		ASource = GetComponent<AudioSource>();
		AudioProfile(AudioProfileVal);
		if(UsingMic)
		{
			if(Microphone.devices.Length > 0)
			{
				SelectedDevice = Microphone.devices[0].ToString();
				ASource.outputAudioMixerGroup = MixerGroupMic;
				ASource.clip = Microphone.Start(SelectedDevice,true,10, AudioSettings.outputSampleRate);
				ASource.loop = true;
			}
			else
			{
				UsingMic = false;
			}
		}
		else
		{
			ASource.outputAudioMixerGroup = MixerGroupMaster;
			ASource.clip = _AudioClip;
		}

		while(!(Microphone.GetPosition(null) > 0)) {}

		ASource.Play();
	}
		
	void Update()
    {
		if(!GameManager.Instance.End)
		{
			GetSpectrumAudioSource();
			MakeFrequencyBands();
			BandBuffer();
			CreateAudioBands();
			GetAmplitude();
		}
    }

//--------------------------------------------------------------------------METHODS:

//--------------------------------------------------------------------------HELPERS:
	private void AudioProfile(float audioProfile)
	{
		for(int i = 0; i<8; i++)
		{
			freqBandHighest[i] = audioProfile;
		}

	}
	private void BandBuffer()
	{
		for(int i = 0; i < 8; ++i)
		{
			if(FreqBand[i] > BandBufferVal[i])
			{
				BandBufferVal[i] = FreqBand[i];
				bufferDecrease[i] = BufferDecreaseVal; //Decrease speed
			}
			if(FreqBand[i] < BandBufferVal[i])
			{
				BandBufferVal[i] -= bufferDecrease[i];
				bufferDecrease[i]*= BufferIncreaseVal;
			}
		}
	}
	
	private void CreateAudioBands()
	{
		for(int i = 0; i<8; i++)
		{
			if(FreqBand[i] > freqBandHighest[i])
			{
				freqBandHighest[i] = FreqBand[i];
			}
			AudioBand[i] = (FreqBand[i] / freqBandHighest[i]);
			AudioBandBuffer[i] = (BandBufferVal[i] / freqBandHighest[i]);
		}
	}
	
	private void GetAmplitude()
	{
		float currentAmplitude = 0;
		float currentAmplitudeBuffer =0;

		for(int i = 0; i < 8; i++)
		{
			currentAmplitude += AudioBand[i];
			currentAmplitudeBuffer += AudioBandBuffer[i];
		}
		if(currentAmplitude > amplitudeHighest)
			amplitudeHighest = currentAmplitude;
		
		Amplitude = currentAmplitude / amplitudeHighest;
		AmplitudeBuffer = currentAmplitudeBuffer / amplitudeHighest;

	}
	private void GetSpectrumAudioSource()
	{
		//512 samples on channel 0(Left) that reduces the spectrum of data via Blackman calculation
		ASource.GetSpectrumData(Samples, 0, FFTWindow.Blackman);
	}
	private void MakeFrequencyBands()
	{
		/*
		*	44100 Hertz / 512 = 86 hertz per sample
		*	
			Pow		2^pow		Hertz		Range
			0		2			172
			1		4			344		-	173-518
			2		8			688		-	519-1207
			3		16			1376	-	1208-2584
			4		32			2752	-	2585-5338	
			5		64			5504	-	5339-10843
			6		128			11008	-	10844-21852
			7		256			22016	-	21853-43869
			231

		*/

		int count = 0;

		for(int i = 1; i <= 8; i++)
		{
			float average = 0;

			int sampleCount = (int)Mathf.Pow(2,i);
			if(i-1 == 7)
			{
				sampleCount += 2;
			}
			for(int j = 0; j < sampleCount; j++)
			{
				average += Samples[count]*(count +1);
				count++;
			}

			average /= count;

			FreqBand[i-1] = average * 10;
		}
	}

}