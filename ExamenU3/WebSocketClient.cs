using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;

namespace ExamenU3
{
    internal class WebSocketClient
    {
        public static WebSocket ws;
        public static string NombreUsuarioActual = ""; // Identificador del usuario actual

        public static void Inicializar(string url, string nombreUsuario)
        {
            NombreUsuarioActual = nombreUsuario;

            if (ws == null)
            {
                ws = new WebSocket(url);
                ws.Connect();
            }
        }

        public static void EnviarMensaje(string mensaje)
        {
            if (ws != null && ws.IsAlive)
            {
                // Se añade ":NombreUsuarioActual" al final para identificar quién hizo el cambio
                ws.Send(mensaje + ":" + NombreUsuarioActual);
            }
        }

        public static void Cerrar()
        {
            if (ws != null)
            {
                ws.Close();
            }
        }
    }
}
