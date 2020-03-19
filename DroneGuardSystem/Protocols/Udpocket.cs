using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace DroneGuardSystem.Protocols
{
    public class UdpSocket
    {
        private readonly Socket _socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        private const int BufSize = 8 * 1024;
        private readonly State _state = new State();
        private EndPoint _epFrom = new IPEndPoint(IPAddress.Any, 0);
        private AsyncCallback _recv = null;

        private class State
        {
            public readonly byte[] Buffer = new byte[BufSize];
        }

        public void Server(string address, int port)
        {
            _socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.ReuseAddress, true);
            _socket.Bind(new IPEndPoint(IPAddress.Parse(address), port));
            Receive();            
        }

        private void Receive()
        {            
            _socket.BeginReceiveFrom(_state.Buffer, 0, BufSize, SocketFlags.None, ref _epFrom, _recv = (ar) =>
            {
                var so = (State)ar.AsyncState;
                var bytes = _socket.EndReceiveFrom(ar, ref _epFrom);
                _socket.BeginReceiveFrom(so.Buffer, 0, BufSize, SocketFlags.None, ref _epFrom, _recv, so);
                Console.WriteLine("RECV: {0}: {1}, {2}", _epFrom, bytes, Encoding.ASCII.GetString(so.Buffer, 0, bytes));
            }, _state);
        }
    }
}