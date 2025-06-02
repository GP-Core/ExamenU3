using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExamenU3
{
    public class ClienteWebSocketManual
    {
        private ClientWebSocket _cliente;
        private Uri _uri;
        private CancellationTokenSource _cts;
        public event Action<string> MensajeRecibido;

        public ClienteWebSocketManual(string url)
        {
            _cliente = new ClientWebSocket();
            _uri = new Uri(url);
            _cts = new CancellationTokenSource();
        }

        public async Task ConectarAsync()
        {
            try
            {
                await _cliente.ConnectAsync(_uri, CancellationToken.None);
                _ = Escuchar(); // No esperes, sigue ejecutando
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al conectar con WebSocket: " + ex.Message);
            }
        }

        private async Task Escuchar()
        {
            var buffer = new byte[1024];
            while (_cliente.State == WebSocketState.Open)
            {
                try
                {
                    var result = await _cliente.ReceiveAsync(new ArraySegment<byte>(buffer), _cts.Token);
                    if (result.MessageType == WebSocketMessageType.Close)
                    {
                        await _cliente.CloseAsync(WebSocketCloseStatus.NormalClosure, "Cerrado por el cliente", CancellationToken.None);
                    }
                    else
                    {
                        string mensaje = Encoding.UTF8.GetString(buffer, 0, result.Count);
                        MensajeRecibido?.Invoke(mensaje);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al recibir mensaje: " + ex.Message);
                }
            }
        }

        public async Task EnviarMensaje(string mensaje)
        {
            if (_cliente.State == WebSocketState.Open)
            {
                var buffer = Encoding.UTF8.GetBytes(mensaje);
                await _cliente.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, _cts.Token);
            }
        }

        public async Task CerrarConexion()
        {
            if (_cliente.State == WebSocketState.Open)
            {
                await _cliente.CloseAsync(WebSocketCloseStatus.NormalClosure, "Cierre solicitado", _cts.Token);
            }
        }
    }
}
