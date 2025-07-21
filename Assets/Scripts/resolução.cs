using UnityEngine;
using UnityEngine.UI;

public class ResolutionDropdown : MonoBehaviour
{
    public Dropdown resolutionDropdown;

    private Resolution[] resolutions;

    void Start()
    {
        // Obtém todas as resoluções disponíveis
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        int currentResolutionIndex = 0;
        var options = new System.Collections.Generic.List<string>();

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            if (!options.Contains(option)) // evita resoluções duplicadas
                options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        resolutionDropdown.onValueChanged.AddListener(ChangeResolution);
    }

    void ChangeResolution(int index)
    {
        Resolution res = resolutions[index];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
    }
}
