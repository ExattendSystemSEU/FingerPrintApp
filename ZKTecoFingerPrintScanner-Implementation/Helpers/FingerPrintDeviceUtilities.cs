namespace ZKTecoFingerPrintScanner_Implementation.Helpers
{
    public class FingerPrintDeviceUtilities
    {
        public static string DisplayDeviceErrorByCode(int errCode)
        {
            string message = string.Empty;
            switch (errCode)
            {
                case -25: message = "The device is already connected"; break;

                case -24: message = "The device is not initialized"; break;

                case -23: message = "The device is not started"; break;



                case -6: message = "Failed to start the device"; break;


                case -3: message = "No device connected"; break;


                case 0: message = "Operation succeeded"; break;

                case 1: message = "Initalized"; break;

               // default: message = "Unknown operation"; break;
            }

            return message;
        }
    }
}
