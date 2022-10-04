using System;
using System.Collections.Generic;

public class Settings
{
    private Dictionary<string, string> DicSettings;
    private string path, file, to, quality, destination;

    #region get set region
    public string Path
    {
        get
        {
            return path;
        }
        protected set
        {
            path = value;
        }
    }

    public string File
    {
        get
        {
            return file;
        }
        protected set
        {
            file = value;
        }
    }
    public string To
    {
        get
        {
            return to;
        }
        protected set
        {
            to = value;
        }
    }

    public string Quality
    {
        get
        {
            return quality;
        }
        protected set
        {
            quality = value;
        }
    }

    public string Destination
    {
        get
        {
            return destination;
        }
        protected set
        {
            destination = value;
        }
    }
    #endregion

    public Settings(string [] options)
	{
        if (options.Length == 0)
            new TraceHelper().Exit("Please enter options. --help or -help or help");

        DicSettings = new Dictionary<string, string>();

        foreach (var option in options)
        {
            var split = option.Split('=');

            if (split[0].ToUpper().Contains("HELP"))
            {
                new TraceHelper().getHelpMenu();
                Environment.Exit(0);
            }

            DicSettings.Add(split[0], split[1]);
            new TraceHelper().Info(String.Format("Setting {0}:{1} ", split[0], split[1]));
        }

        initSettings();
    }

    private void initSettings()
    {

        this.path = DicSettings.ContainsKey("-path") ? DicSettings["-path"] : System.Reflection.Assembly.GetEntryAssembly().Location.Replace("ConvertImage.exe", "");

        if (!DicSettings.ContainsKey("-file"))
            new TraceHelper().MandatoryField("-file");
        else
            this.file = DicSettings["-file"];

        this.to = DicSettings.ContainsKey("-to")? DicSettings["-to"] : ".jpeg";
        this.quality = DicSettings.ContainsKey("-quality") ? DicSettings["-quality"] : "100";
        this.destination = DicSettings.ContainsKey("-destination") ? DicSettings["-destination"] : System.Reflection.Assembly.GetEntryAssembly().Location.Replace("ConvertImage.exe", "");

    }

}
