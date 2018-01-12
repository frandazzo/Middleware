using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;

using System.ServiceProcess;
using System.Text;
using System.Configuration;
using System.Reflection;
using System.IO;
using System.Threading;
using WIN.TECHNICAL.SERVICE_PROCESSOR.Properties;


namespace WIN.TECHNICAL.SERVICE_PROCESSOR
{
    public partial class Service_Processor : ServiceBase
    {
        IServiceProcessor[] _processor;

        System.Timers.Timer timer1 = new System.Timers.Timer();

        public Service_Processor()
        {
            InitializeComponent();
            SetEventLogProperties();
            //SetProcessor();
            SetTimerProperties();
           
        }

        private void SetProcessor()
        {
            try
            {
                eventLog1.WriteEntry("Processore da instanziare: " + Settings.Default.PROCESSOR_ASSEMBLY, EventLogEntryType.Information);
                eventLog1.WriteEntry("Classe da instanziare: " + Settings.Default.PROCESSOR_CLASS, EventLogEntryType.Information);

                //definisco il processore come lista di processori
                _processor = new IServiceProcessor[] { };

                //Recupera gli assembly e i nomi delle classi
                string[] assemblyNames = GetAssemblyNames();
                string[] classNames = Settings.Default.PROCESSOR_CLASS.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);



                for (int i = 0; i < assemblyNames.Length; i++)
                {
                    Assembly asm = Assembly.LoadFrom(assemblyNames[i]);

                    //verifica che non sia nullo
                    if (asm == null)
                        eventLog1.WriteEntry(string.Format("Assembly del processore {0} non istanziato o non trovato!", assemblyNames[i]));
                    else
                    {

                        //instanzia la classe processore
                        IServiceProcessor p = (IServiceProcessor)asm.CreateInstance(classNames[i], true, System.Reflection.BindingFlags.CreateInstance, null, null, new System.Globalization.CultureInfo("it-IT"), null);
                        if (p != null)
                        {
                            Array.Resize<IServiceProcessor>(ref _processor, _processor.Length + 1);
                            _processor[_processor.Length - 1] = p;

                        }
    
                    }      
                }


                //verifica della presenza della classe
                if (_processor.Length == 0)
                    eventLog1.WriteEntry("Classe/i del processore non istanziata/e o non trovata!");
                else
                    eventLog1.WriteEntry("Processore e classe del processore correttamente instanziate", EventLogEntryType.Information);
                
            }
            catch (Exception ex)
            {
                string error = "Impossibile instanziare il processore del servizio!";
                error += Environment.NewLine + ex.Message;
                if (ex.InnerException != null)
                    error = error + "; inner: " + ex.InnerException.Message;

                eventLog1.WriteEntry(error, EventLogEntryType.Error);

               
            }
        }

        private string[] GetAssemblyNames()
        {
            string executableName = Assembly.GetExecutingAssembly().CodeBase.Replace("file:///", "");
            FileInfo executableFileInfo = new FileInfo(executableName);
            string[] assemblyNames = Properties.Settings.Default.PROCESSOR_ASSEMBLY.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);


            string[] assemlyPaths = new string[]{};

            foreach (string assName in assemblyNames)
	        {
                Array.Resize<string>(ref assemlyPaths, assemlyPaths.Length + 1);
                assemlyPaths[assemlyPaths.Length - 1] = executableFileInfo.DirectoryName + "\\" + assName;
	        }

           return assemlyPaths;
            
        }

        private void SetEventLogProperties()
        {
            try
            {
                if (!System.Diagnostics.EventLog.SourceExists(Settings.Default.EVENT_LOG_SOURCE))
                {
                    System.Diagnostics.EventLog.CreateEventSource(
                        Settings.Default.EVENT_LOG_SOURCE, Settings.Default.EVENT_LOG);
                }
                eventLog1.Source = Settings.Default.EVENT_LOG_SOURCE;
                eventLog1.Log = Settings.Default.EVENT_LOG;
                eventLog1.ModifyOverflowPolicy(OverflowAction.OverwriteAsNeeded,5);
            }
            catch (Exception ex)
            {
                string filename = "SeriviceProcessor.txt";


                filename = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "//" + filename;


                File.AppendAllText(filename, ex.Message);
            }
           
           
        }

        private void SetTimerProperties()
        {
            timer1.Elapsed += new System.Timers.ElapsedEventHandler(timer1_Elapsed);
            timer1.Interval = Settings.Default.TIMER_INTERVAL;
            timer1.AutoReset  = false;
            timer1.Enabled = true;
            timer1.Start();

            eventLog1.WriteEntry("Timer avviato correttamente su un intervallo di " + Settings.Default.TIMER_INTERVAL.ToString () + " millisecondi.", EventLogEntryType.Information);
        }



        void timer1_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {

                //ricerco il processore
                SetProcessor();
           
                eventLog1.WriteEntry("Inizio processo richiesta", EventLogEntryType.Information );


                int i = 0;
                    //avvia il processo
                    foreach (IServiceProcessor item in _processor)
                    {

                        i++;
                        if (item == null)
                        {
                            eventLog1.WriteEntry("Processore nullo. Impossiblie processare la richiesta." + Settings.Default.PROCESSOR_ASSEMBLY);
                        }
                        else
                        {
                                try
                                {
                                    eventLog1.WriteEntry(string.Format("MAIN SERVICE -- Avvio attività del processore  {0}", i), EventLogEntryType.Information);
                                    item.Process();
                                    eventLog1.WriteEntry(string.Format("MAIN SERVICE -- Termine attività del processore  {0}", i), EventLogEntryType.Information);
                                }
                                catch (Exception ex)
                                {
                                    string error = ex.Message;
                                    if (ex.InnerException != null)
                                        error += Environment.NewLine + ex.InnerException.Message + Environment.NewLine +
                                                                "Stack Trace: " + ex.StackTrace;
                                   
                                    eventLog1.WriteEntry(error, EventLogEntryType.Error);
                                    try
                                    {
                                        if (_processor != null)
                                            item.NotifyError(error + Environment.NewLine + "Processo andato in errore!");
                                    }
                                    catch (Exception)
                                    {
                                      
                                    }
                                   
                                    eventLog1.WriteEntry("Termine processo" + ex.Message, EventLogEntryType.Error);
                                    //this.Stop();
                                }
                           
                        }
                
                   
                }
                _processor = null;
                eventLog1.WriteEntry("Termine processo richiesta", EventLogEntryType.Information);


                //riavvio il processo
                timer1.Start();
          
        }

        protected override void OnStart(string[] args)
        {

            eventLog1.WriteEntry("Processo avviato",  EventLogEntryType.Information );
        }

        protected override void OnStop()
        {
            eventLog1.WriteEntry("Processo stoppato",  EventLogEntryType.Information );
            //if (_processor != null)
            //    _processor.NotifyError("Processo elaboratore importazioni FenealgestWeb stoppato");
        }
    }
}
