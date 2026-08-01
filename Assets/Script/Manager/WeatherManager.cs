using UnityEngine;
using System.Collections;
public enum WeatherType
{
    Sunny, // 맑음
    Cloudy, // 흐림
    Rain, // 비
    Thunder // 천둥 
}

public class WeatherManager : MonoBehaviour
{
    public GameObject rainParticle; // 비 파티클 오브젝트

    public WeatherType currentWeather = WeatherType.Sunny; // 초기값 맑음

    [Header("천둥")]
    public GameObject thunderWarningPrefab; // 경고 장판 프리팹
    public GameObject thunderEffectPrefab;  // 번개 프리팹 (Thunder 스크립트 부착된 것)
    public Transform[] spawnPoints; // 번개 소환 위치

    private void Start()
    {
        ApplyWeather();
    }

    public void ApplyWeather()
    {
        switch (currentWeather)
        {
            case WeatherType.Sunny:
                RenderSettings.fog = false;
                rainParticle.SetActive(false);
                Debug.Log("맑음 효과 적용");
                break;

            case WeatherType.Cloudy:
                RenderSettings.fog = true;
                RenderSettings.fogColor = new Color(0.7f, 0.7f, 0.7f);
                rainParticle.SetActive(false);
                Debug.Log("흐림 효과 적용");
                break;

            case WeatherType.Rain:
                RenderSettings.fog = false;
                rainParticle.SetActive(true); // 비 오브젝트 활성화
                Debug.Log("비 효과 적용");
                break;

            case WeatherType.Thunder:
                RenderSettings.fog = false;
                rainParticle.SetActive(false);
                StartCoroutine(SpawnThunder());
                Debug.Log("천둥 효과 적용");
                break;
        }
    }

    public void ChangeRandomWeather()
    {
        int random = Random.Range(0, 4);

        switch (random)
        {
            case 0:
                currentWeather = WeatherType.Sunny;
                break;
            case 1:
                currentWeather = WeatherType.Cloudy;
                break;
            case 2:
                currentWeather = WeatherType.Rain;
                break;
            case 3:
                currentWeather = WeatherType.Thunder;
                break;
        }
        ApplyWeather();
        Debug.Log("현재 날씨: " + currentWeather);
    }

    public void ChangeNextWeather() // 치트키
    {
        currentWeather ++;

        if (currentWeather > WeatherType.Thunder)
        {
            currentWeather = WeatherType.Sunny;
        }
        ApplyWeather();
        Debug.Log("날씨 변경: " + currentWeather);
    }

    public IEnumerator SpawnThunder()
    {
        if (currentWeather == WeatherType.Thunder)
        {
            int randomIndex = Random.Range(0, spawnPoints.Length);

            Vector3 spawnPos = spawnPoints[randomIndex].position;

            GameObject warning = Instantiate(thunderWarningPrefab, spawnPos, Quaternion.identity); // 장판 소환

            yield return new WaitForSeconds(3f); // 3초 대기

            // 장판 지우기
            Destroy(warning);

            // 번개 소환
            Instantiate(thunderEffectPrefab, spawnPos, Quaternion.identity);

            yield return new WaitForSeconds(3f); // 10초 후 자기자신 호출

            StartCoroutine(SpawnThunder());
        }
    }
}