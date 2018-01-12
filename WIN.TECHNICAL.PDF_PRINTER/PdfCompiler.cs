using System;
using System.Collections;

using System.Text;
using System.Windows.Forms;
using com.lowagie.text.pdf;
using System.IO;
using java.io;


namespace WIN.TECHNICAL.PDF_PRINTER
{
    public class PdfCompiler
    {
        private string _fileNameSorgente;
        private string _fileNameDestinazione;
        private System.Collections.Hashtable _hTable;
        private PdfReader _reader;
        private PdfStamper _stamper;
        private AcroFields _form;
   

       

        public PdfCompiler(string fileNameSorgente,string fileNameDestinazione)
        {
        if (System.IO.File.Exists(fileNameSorgente) == false)

                throw new System.IO.FileNotFoundException("file inesistente");

            else 
            {

                _fileNameSorgente = fileNameSorgente;
                _fileNameDestinazione =fileNameDestinazione ;
                
            }
       
        }



        public string GetSorgente { get { return _fileNameSorgente; } }

        public string GetDestinazione { get { return _fileNameDestinazione; } }

        public string GetField(string nomeCampo)
        {
           if (_reader!=null)
                return _form.getField(nomeCampo);
           else throw new FieldAccessException();
        }

        public void SetCampo(string nomeCampo, string value)
        {

            if (_reader != null)
                _hTable.Add(nomeCampo, value);
            
            else throw new FieldAccessException();
       
        }

        public void SetAllFieldToEmpty()
        {
            if (_reader != null)
            {
                PdfDescriber m = new PdfDescriber(_fileNameSorgente);
                m.Setup();
                System.Collections.Hashtable _arr = m.GetFieldsInfoToHashtable();
                foreach (DictionaryEntry elem in _arr)
                {
                    _form.setField(elem.Key.ToString (), "");
                }
                m.Dispose();
            }
            else throw new FieldAccessException();
        
        }

        public void Compile()
        {
            if (_reader != null)
            {

                FillFields();
                //_stamper.setFormFlattening(false);
            }
            else throw new FieldAccessException();
        }

        private void FillFields()
        {
            foreach (DictionaryEntry element in _hTable)
            {
                _form.setField((string)element.Key, (string)element.Value);

            }
           //_hTable.Clear();
        }

        public void Setup()
        {
            //System.IO.File.Copy(_fileNameSorgente, _fileNameDestinazione); 
            _reader = new PdfReader(_fileNameSorgente);
           
            _stamper = new PdfStamper(_reader,new FileOutputStream(_fileNameDestinazione,true ));
            if (_reader.isEncrypted ())
                System.Diagnostics.Debug.Print(PdfEncryptor.getPermissionsVerbose (_reader.getPermissions()));
            _stamper.setEncryption(new byte[] { }, new byte[] { },
PdfWriter.AllowPrinting | PdfWriter.AllowCopy | PdfWriter.AllowModifyContents | PdfWriter.AllowAssembly | PdfWriter.AllowDegradedPrinting | PdfWriter.AllowModifyAnnotations | PdfWriter.AllowFillIn | PdfWriter.AllowScreenReaders ,
PdfWriter.STRENGTH128BITS);
            
            
            _form = _stamper.getAcroFields();
            _hTable = new Hashtable();
           
        }


        public void Dispose()
        {
            _stamper.close();
            _reader.close();
            _hTable.Clear();
            _reader = null;
        }

        public void Clear()

        {
           if (_reader !=null)
             {
                foreach (DictionaryEntry element in _hTable)
                {
                    _form.setField((string)element.Key, "");

                }
                //_hTable.Clear();
             }
           else throw new FieldAccessException(); 
        }

        public void ClearField(string nomeField)
        {
            if (_reader!=null)
            _form.setField(nomeField, "");
            else throw new FieldAccessException();
        }
       
        
    }




}
