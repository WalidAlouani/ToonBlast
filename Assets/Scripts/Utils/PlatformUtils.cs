using UnityEngine;

public static class PlatformUtils
{
    public static bool IsPCPlatform()
    {
        switch (Application.platform)
        {
            case RuntimePlatform.WindowsPlayer:
            case RuntimePlatform.WindowsEditor:
            case RuntimePlatform.OSXPlayer:
            case RuntimePlatform.OSXEditor:
            case RuntimePlatform.LinuxPlayer:
            case RuntimePlatform.LinuxEditor:
                return true;
            default:
                return false;
        }
    }

    public static bool IsMobilePlatform()
    {
        switch (Application.platform)
        {
            case RuntimePlatform.Android:
            case RuntimePlatform.IPhonePlayer:
                //case RuntimePlatform.WP8Player:
                //case RuntimePlatform.BlackBerryPlayer:
                return true;
            default:
                return false;
        }
    }

    public static SupportedPlatform GetCurrentPlatform()
    {
        if (IsMobilePlatform())
            return SupportedPlatform.Mobile;
        if (IsPCPlatform())
            return SupportedPlatform.PC;

        return SupportedPlatform.Unsupported;
    }
}
