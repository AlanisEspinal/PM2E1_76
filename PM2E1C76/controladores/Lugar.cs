using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace PM2E1C76.controladores
{
    public class Lugar
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public double Longitud { get; set; }   
        public double Latitude { get; set; }

        public string Descripcion { get; set; }

        public byte[] foto { get; set; }
    }
}
