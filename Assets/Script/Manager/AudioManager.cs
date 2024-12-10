using UnityEngine;
using System;
using UnityEngine.Audio;

[Serializable]
public class Sound
{
    public string name;           // 사운드 이름 (고유 키)
    public AudioClip clip;        // 재생할 오디오 클립
    [Range(0f, 1f)]
    public float volume = 1f;     // 기본 볼륨
    [Range(.1f, 3f)]
    public float pitch = 1f;      // 기본 피치
    public bool loop;             // 루프 여부
    public bool playOnAwake;      // 오브젝트 활성화 시 자동 재생 여부

    [HideInInspector]
    public AudioSource source;    // 내부적으로 참조할 AudioSource
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioMixerGroup mixerGroup;
    public Sound[] sounds; // Inspector에서 관리할 사운드 목록

    private string targetObjectName = "GameManager";

    private void Awake()
    {
        // 싱글톤 초기화
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Sound 배열에 대해 AudioSource 초기화
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.playOnAwake = s.playOnAwake;

            if (mixerGroup != null)
                s.source.outputAudioMixerGroup = mixerGroup;

            if (s.playOnAwake && s.clip != null)
            {
                s.source.Play();
            }
        }
    }

    private void OnEnable()
    {
        // 씬 로드 완료 이벤트에 등록
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // 씬 로드 완료 이벤트에서 제거
        UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
    {
        // 목표 오브젝트 찾기
        GameObject targetParent = GameObject.Find(targetObjectName);
        if (targetParent != null)
        {
            // AudioManager를 목표 오브젝트의 자식으로 설정
            transform.SetParent(targetParent.transform);
        }
    }

    /// <summary>
    /// 특정 이름의 사운드를 재생
    /// </summary>
    public void Play(string name)
    {
        Sound s = FindSound(name);
        if (s == null) return;
        s.source.Play();
    }

    /// <summary>
    /// 특정 이름의 사운드를 정지
    /// </summary>
    public void Stop(string name)
    {
        Sound s = FindSound(name);
        if (s == null) return;
        s.source.Stop();
    }

    /// <summary>
    /// 특정 이름의 사운드를 일시정지
    /// </summary>
    public void Pause(string name)
    {
        Sound s = FindSound(name);
        if (s == null) return;
        s.source.Pause();
    }

    /// <summary>
    /// 특정 이름의 사운드를 일시정지 해제(재개)
    /// </summary>
    public void UnPause(string name)
    {
        Sound s = FindSound(name);
        if (s == null) return;
        s.source.UnPause();
    }

    /// <summary>
    /// 모든 사운드 일시정지
    /// </summary>
    public void PauseAll()
    {
        foreach (Sound s in sounds)
        {
            if (s.source.isPlaying)
                s.source.Pause();
        }
    }

    /// <summary>
    /// 모든 사운드 일시정지 해제(재개)
    /// </summary>
    public void UnPauseAll()
    {
        foreach (Sound s in sounds)
        {
            // 일시정지되었던 사운드만 재개
            // isPlaying이 false이고 timeSamples > 0 이면 재개 대상
            if (!s.source.isPlaying && s.source.timeSamples > 0)
                s.source.UnPause();
        }
    }

    /// <summary>
    /// 특정 사운드 볼륨 변경
    /// </summary>
    public void SetVolume(string name, float volume)
    {
        Sound s = FindSound(name);
        if (s == null) return;
        s.source.volume = volume;
    }

    /// <summary>
    /// 특정 사운드 피치 변경
    /// </summary>
    public void SetPitch(string name, float pitch)
    {
        Sound s = FindSound(name);
        if (s == null) return;
        s.source.pitch = pitch;
    }

    /// <summary>
    /// 특정 사운드가 재생 중인지 확인
    /// </summary>
    public bool IsPlaying(string name)
    {
        Sound s = FindSound(name);
        if (s == null) return false;
        return s.source.isPlaying;
    }

    /// <summary>
    /// 모든 사운드 정지
    /// </summary>
    public void StopAllSounds()
    {
        foreach (Sound s in sounds)
        {
            if (s.source.isPlaying)
                s.source.Stop();
        }
    }

    /// <summary>
    /// 사운드를 이름으로 찾는 헬퍼 함수
    /// </summary>
    private Sound FindSound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
            Debug.LogWarning($"[AudioManager] '{name}' 사운드를 찾을 수 없습니다.");
        return s;
    }
}
