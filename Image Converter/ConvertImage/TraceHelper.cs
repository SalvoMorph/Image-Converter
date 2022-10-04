using System;

public class TraceHelper
{

    public void getHelpMenu()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("######################################### Image Converter Helper #########################################");
        Console.WriteLine("# ConvertImage [options]");
        Console.WriteLine("# [options] --> -path= -file= -to= -quality= -destination= \n#");
        Console.WriteLine("# -path          Used to indicate the source path. If not declared, will be take the executable path");
        Console.WriteLine("#                Please, path in quotes. e.g \"c:\\Program Files\"  \n#");
        Console.WriteLine("# -file          The name of the file to convert. You can also use, for example, this syntax *.jpg\n#");
        Console.WriteLine("# -to            The new file extension. You can conver into png, jpeg, tiff, bmp icon\n#");
        Console.WriteLine("# -quality       The compression level of quality. From 0(low) to 100(high)\n#");
        Console.WriteLine("# -destination   If any, the new files will be saved at this path.");
        Console.WriteLine("#                Please, path in quotes. e.g \"c:\\Program Files\"  \n#");
        Console.WriteLine("#########################################################################################################\n");
        Console.ResetColor();
        pressKeyToExit();

    }

    public void MandatoryField(string field)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("This option " + field + " is mandatory. Please see --helper");
        Console.ResetColor();
        pressKeyToExit();
        Environment.Exit(1);
    }

    public void Error(string error)
    {
        Console.WriteLine("An error occurred" + error);
        pressKeyToExit();
        Environment.Exit(1);
    }

    public void Info(string msg)
    {
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine(msg);
    }

    public void Warning(string msg)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(msg);
        Console.ResetColor();
    }

    public void Exit(string msg)
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine(msg);
        Console.ResetColor();
        pressKeyToExit();
        Environment.Exit(0);
    }

    public void pressKeyToExit()
    {
        Console.WriteLine("Press any key to exit..");
        Console.ReadKey();
    }


}
