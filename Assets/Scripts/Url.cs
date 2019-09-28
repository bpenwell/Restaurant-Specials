/*
 * Ryan Long
 * Opens URL for headcounts on button press
 */

using UnityEngine;

public class Url : MonoBehaviour {
    public void OpenURL()
    {
        string url = "https://unr.teamdynamix.com/TDClient/Requests/ServiceDet?ID=27661";
        Application.OpenURL(url);
    }
}
