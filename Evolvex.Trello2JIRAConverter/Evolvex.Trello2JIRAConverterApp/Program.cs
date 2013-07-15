using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Evolvex.Trello2JIRAConverterLib;
using System.IO;

namespace Evolvex.Trello2JIRAConverterApp
{
    class Program
    {
        private delegate void CmdHandler(string[] args);

        private static readonly Dictionary<string, CmdHandler> _cmdHandlers;

        static Program()
        {
            _cmdHandlers = new Dictionary<string, CmdHandler>();

            #region populate
            _cmdHandlers.Add("justtry", JustTry);
            _cmdHandlers.Add("converttoxml", ConvertToXml);
            #endregion
        }

        static void Main(string[] args)
        {
            Console.Read();
            string cmdHandlerKey = string.Empty;
            if (args.Length > 0)
                cmdHandlerKey = args[0].ToLower();
            try
            {
                if (string.IsNullOrEmpty(cmdHandlerKey) || !_cmdHandlers.ContainsKey(cmdHandlerKey))
                    return;
                else
                    _cmdHandlers[cmdHandlerKey](args);
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.ToString());
            }
        }

        private static void JustTry(String[] args)
        {
            string jsonPath = args[1];
            Converter c = new Converter();
            c.Convert(File.ReadAllText(jsonPath));

        }

        private static void ConvertToXml(String[] args)
        {
            string jsonPath = args[1];
            string xmlPath = args[2];
            string rootNode = args.Length > 3 ? args[3] : "board";
            Converter c = new Converter();
            c.ConvertToXml(jsonPath, xmlPath, rootNode);

        }
        
    }
}
