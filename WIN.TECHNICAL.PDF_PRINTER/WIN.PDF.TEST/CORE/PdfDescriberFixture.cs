using System;
using System.Collections.Generic;

using System.Text;
using NUnit.Framework;
using System.IO;
using System.Collections;

namespace WIN.TECHNICAL.PDF_PRINTER.WIN.PDF.TEST.CORE
{
    
    [TestFixture ]
    public class PdfDescriberFixture
    {
        [Test]
        public void PdfDescriberTest()
        {
            //PdfCompiler c = new PdfCompiler ("c:\\ds21vuoto.pdf","c:\\provadescriber.pdf");
            //c.Setup();
            //c.SetCampo("nome", "paolo");
            //c.Compile();
            //c.Dispose();
            PdfDescriber p = new PdfDescriber("C:\\bolzano.pdf");
            //PdfDescriber p = new PdfDescriber("C:\\Mod Cure Termali.pdf");
            p.Setup();
            p.GetFieldsInfoToFile("c:\\prova.txt");
            p.Dispose ();
            File.Delete("c:\\provadescriber.pdf");
            string[] s=File.ReadAllLines("c:\\prova.txt");
            //Assert.IsTrue(s.Contains("nome             paolo"));
            //File.Delete("c:\\prova.txt");
                       
          
          
 
        }


    }



}
