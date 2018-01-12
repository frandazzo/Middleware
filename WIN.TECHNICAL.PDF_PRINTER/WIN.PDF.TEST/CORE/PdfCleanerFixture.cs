using System;
using System.Collections.Generic;

using System.Text;
using NUnit.Framework;
using java.util;
using System.Collections;
using System.IO;

namespace WIN.TECHNICAL.PDF_PRINTER.WIN.PDF.TEST.CORE
{
    [TestFixture ]

    public class PdfCleanerFixture
    
    {
   
        [Test]
   
        public void TestPdfCleaner()
       {
           PdfCompiler m = new PdfCompiler("c:\\ds21vuoto.pdf", "c:\\ds21prova3.pdf");
           m.Setup();
           m.SetCampo("datanascita", "2/10/1984");
           m.SetCampo("nome", "paolo");
           m.Compile();
           m.Dispose();

           PdfCleaner n = new PdfCleaner("c:\\ds21prova3.pdf");
           n.Setup();
           n.CleanAllFields();

           n.Dispose();

           PdfDescriber c = new PdfDescriber("c:\\ds21prova3.pdf");
           c.Setup();
           System.Collections .Hashtable arr= c.GetFieldsInfoToHashtable();
           foreach (DictionaryEntry elem in arr)
           {
              
               Assert.AreEqual(c.GetField(elem.Key.ToString ()), "");
           }
            
            c.Dispose();
            File.Delete("c:\\ds21prova3.pdf");
                 
                





       }
    }
}
