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

        public static void Inicializar(string url)
        {
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
                ws.Send(mensaje);
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
