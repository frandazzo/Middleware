Public Class FactoryConfrontoPeriodi

   Shared Function GetTypeOfConfronto(ByVal TipoConfronto As IConfrontoPeriodi.UnitaDiCompetenza) As IConfrontoPeriodi
      Select Case TipoConfronto
         Case IConfrontoPeriodi.UnitaDiCompetenza.Mensile
            Return New ConfrontoMensile
         Case IConfrontoPeriodi.UnitaDiCompetenza.Giornaliera
            Return New ConfrontoGiornaliero
         Case Else
            Throw New Exception("Impossibile creare un tipo di confronto per le unità di compentenza a causa di un tipo sconosciuto")
      End Select
   End Function
End Class
