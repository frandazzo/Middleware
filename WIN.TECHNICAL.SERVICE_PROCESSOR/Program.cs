﻿using System;
using System.Collections.Generic;

using System.ServiceProcess;
using System.Text;


namespace WIN.TECHNICAL.SERVICE_PROCESSOR
{
    static class Program
    {
        /// <summary>
        /// Punto di ingresso principale dell'applicazione.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
			{ 
				new Service_Processor() 
			};
            ServiceBase.Run(ServicesToRun);
        }
    }
}
