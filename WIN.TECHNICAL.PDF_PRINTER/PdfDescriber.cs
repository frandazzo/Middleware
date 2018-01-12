using System;
using System.Collections.Generic;

using System.Text;
using com.lowagie.text.pdf;
using java.io;
using java.util;
using System.IO;
using System.Collections;

namespace WIN.TECHNICAL.PDF_PRINTER
{
   public class PdfDescriber
    {
       private string _fileNameSorgente;
       private string _fileNameDestinazione;
       private PdfReader _reader;
    
       private AcroFields _form;
       private HashMap _s;

       
       public PdfDescriber(string fileNameSorgente)
       {
           if (System.IO.File.Exists(fileNameSorgente) == false)

               throw new System.IO.FileNotFoundException("file inesistente");

           else
           {

               _fileNameSorgente = fileNameSorgente;
               
           }

       }   
       
       
       public void GetFieldsInfoToFile(string FileNameDestinazione)
       {
           if (_reader != null)
           {
               _fileNameDestinazione = FileNameDestinazione;
               HashMap _s = _form.getFields();
               System.Collections.Hashtable _arr = EstraiFields(_s);
               CreateFileWithFields(_arr, _fileNameDestinazione);
           }
           else throw new FieldAccessException();

       }

       public System.Collections.Hashtable GetFieldsInfoToHashtable()

       {
           if (_reader != null)
           {

               HashMap _s = _form.getFields();
               System.Collections.Hashtable arr = EstraiFields(_s);
               return arr;
           }
           else throw new FieldAccessException();
       }


          
       
       private void CreateFileWithFields(System.Collections.Hashtable arr,string destinazione)
       {
           StringBuilder sb = new StringBuilder();
           foreach (DictionaryEntry elem in arr)
           {
               sb.AppendLine(elem.Key .ToString() + "             "  + elem .Value.ToString () );

           }
           using (StreamWriter st = new StreamWriter(destinazione ))
           {
               st.WriteLine(sb.ToString());
           }
       }

       private System.Collections.Hashtable EstraiFields(HashMap s)
       {
           
           Set t = s.keySet();
           String key;
           System.Collections.Hashtable arr = new System.Collections.Hashtable();
           for (Iterator i = t.iterator(); i.hasNext(); )
           {

               key = (String)i.next();
               if ((_form.getFieldType(key).ToString()).Equals("4") == true)
                   arr.Add(key,_form.getField(key));


           }
           return arr;
       }

       public void Dispose()
       {
           _reader.close();
           _reader = null;
       }

       public void Setup()
       {
           _reader = new PdfReader(_fileNameSorgente);
           
           _form = _reader.getAcroFields();
           _s = new HashMap();
          
       }

       public string GetField(string nomeField)
       {
           if (_reader != null)
               return _form.getField(nomeField);
           else throw new FieldAccessException();
       }

    }
}
