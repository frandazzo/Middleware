using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime ;
using System.Runtime.InteropServices ; 


namespace WIN.TECHNICAL.MIDDLEWARE.Internet
{
    public class InternetConnectionChecker
    {
        //Creating the extern function…
        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int  Description, int ReservedValue ) ;

        //Creating a function that uses the API function…
        public static bool IsConnectedToInternet()
        {
            try
            {
                int Desc;
                return InternetGetConnectedState(out Desc, 0);
            }
            catch (Exception)
            {

                return false;
            }
            
        }


        public static bool IsConnectedToInternet(Uri urlToCheck)
        {

            try
            {
                if (urlToCheck == null)
                    urlToCheck =new System.Uri("http://www.microsoft.com");
               

                System.Net.WebRequest WebReq;
                System.Net.WebResponse Resp;
                WebReq = System.Net.WebRequest.Create(urlToCheck);

                try
                {
                    Resp = WebReq.GetResponse();
                    Resp.Close();
                    WebReq = null;
                    return true;
                }

                catch
                {
                    WebReq = null;
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
            
        }

    }
}

