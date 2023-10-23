using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Xamarin.Forms;

namespace PM2E1C76.controladores
{
    public class Validar
    {
        public bool validarcampo(ImageSource foto, string longitud, string latitud, string descripcion)
        {
            if(foto == null) return false;

            if(string.IsNullOrWhiteSpace(longitud)) return false;

            if(string.IsNullOrWhiteSpace(latitud)) return false;

            if(string.IsNullOrWhiteSpace(descripcion)) return false;

            return true;
        }
    }
}
