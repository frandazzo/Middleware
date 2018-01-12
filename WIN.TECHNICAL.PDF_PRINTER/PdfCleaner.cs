using System;
using System.Collections.Generic;

using System.Text;
using com.lowagie.text.pdf;
using System.IO;
using java.io;
using System.Collections; 

namespace WIN.TECHNICAL.PDF_PRINTER
{
    public class PdfCleaner

    {
        private PdfReader _reader;
        private PdfDescriber _m;
        private AcroFields _form;
        private PdfStamper _stamper;
        
        private string _fileNameSorgente;
        private string _fileTemp="c:\\tempFile.pdf";


         public PdfCleaner(string fileNameSorgente)
        {
       
             if (System.IO.File.Exists(fileNameSorgente) == false)

                throw new System.IO.FileNotFoundException("file inesistente");

            else 
            {

                _fileNameSorgente = fileNameSorgente;
                
               
            }
       
        }

         public void CleanAllFields()
         {
             if (_reader != null)
             {
                 _m.Setup();
                 System.Collections.Hashtable _arr = _m.GetFieldsInfoToHashtable();
                 _m.Dispose();
                 foreach (DictionaryEntry elem in _arr)
                 {
                     _form.setField(elem.Key.ToString(), "");
                 }
             }
             else throw new FieldAccessException();
            
             
                    
         }

         public void ClearField(string nomeField)
         {
             if (_reader != null)
                 _form.setField(nomeField, "");
             else throw new FieldAccessException();
         }
       
        


         public void Dispose()
         {
             _reader.close();
             _stamper.close();
             System.IO.File.Replace(_fileTemp, _fileNameSorgente, "c:\\backupName.pdf");
             _reader = null;
         }

         public void Setup()
         {
             _reader = new PdfReader(_fileNameSorgente);
             _stamper = new PdfStamper(_reader, new FileOutputStream(_fileTemp));
             _form = _stamper.getAcroFields();
             _m = new PdfDescriber(_fileNameSorgente);

         }
    }
}
