using DevEscuelita.Business.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Text;

namespace ProyectoEscuelita.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArchivoController : ControllerBase
    {
        [HttpGet("generar-txt")]
        public IActionResult GenerarArchivoTxt([FromQuery] DateTime inicio, [FromQuery] DateTime fin)
        {
            // Crear datos de ejemplo
            var poliza = new Poliza
            {
                NumeroPoliza = "123456789",
                FechaInicio = inicio,
                FechaFin = fin,
                PrimaBruta = 12.2,
                DNIAsegurado = "11223344",
                NombreAsegurado = "Juan Perez",

                DatosBeneficiario = new Beneficiario
                {
                    DNIBeneficiario = "987654321",
                    NombreBeneficiario = "Maria Perez",
                    Relacion = "Esposa"
                }
            };

            //Completar con 0 a la izquierda
            //var numeroPolizaConCeros = poliza.NumeroPoliza.PadLeft(10, '0');

            // Generar contenido del archivo TXT
            //var contenido = "Pruebita";
            var fechaActual = DateTime.Now.ToString("yyyyMMdd");
            var contenido = new StringBuilder();
            contenido.AppendLine($"EDGP" + //TIP_TRAMA
                $"   N" + //MCA_EMIS_MAPFRE
                $"   {fechaActual}" + //FEC_LOTE
                $"   1" + //NUM_LOTE
                $"   V" + //COD_TRATAMIENTO
                $"   {poliza.NumeroPoliza}" + //NUM_POLIZA_MANUAL
                $"   {poliza.DNIAsegurado}" + //COD_DOCUM_CONT
                $"   {poliza.PrimaBruta}" + //PRIMA
                $"   {poliza.DatosBeneficiario.DNIBeneficiario}" + //Beneficiario_DNI
                $"   {poliza.DatosBeneficiario.NombreBeneficiario}" + //Beneficiario_Nombre
                $"   {poliza.DatosBeneficiario.Relacion}" + //Beneficiario_Relacion
                $""
                );
            var fileName = $"EDGP_{fechaActual}_00000001_V.txt";

            // Convertir el contenido a bytes
            //var bytes = Encoding.UTF8.GetBytes(concontenidotent);
            var bytes = Encoding.UTF8.GetBytes(contenido.ToString());

            // Devolver el archivo para su descarga
            return File(bytes, "text/plain", fileName);
        }
    }
}
