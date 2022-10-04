using System;


namespace ConvertImage
{
    class Program
    {
        static Settings settings;

        static void Main(string[] args)
        {
            settings = new Settings(args);

            try {
                new ConvertImageEngine().ConvertImage(settings);
            }
            catch(Exception ex)
            {
                new TraceHelper().Error(ex.ToString());
            }

            new TraceHelper().pressKeyToExit();
        }

        
    }
}
