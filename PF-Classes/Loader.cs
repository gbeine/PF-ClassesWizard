using System;
using PF_Classes.Classes.Charlatan;
using PF_Core;

namespace PF_Classes
{
    public class Loader
    {
        private static readonly Logger _logger = Logger.INSTANCE;
        private static bool loaded = false;

        public static void init()
        {
            if (loaded)
            {
                _logger.Log("Already loaded...");
            }
            else
            {
                _logger.Log("Loading classes...");
                try
                {
                    Charlatan c = new Charlatan();

                }
                catch (Exception e)
                {
                    _logger.Error(e.Message);
                    _logger.Error(e.StackTrace);
                    throw;
                }
                _logger.Log("DONE: Loading classes...");

                loaded = true;
            }
        }
    }
}
