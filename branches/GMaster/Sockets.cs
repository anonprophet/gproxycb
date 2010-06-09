using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace CustomSockets
{
    public class CTCPClient : TcpClient
    {
        bool receiving = true;
        public delegate void OnSocketConnectHandler(CTCPClient sender);
        public delegate void OnSocketDataArrivalHandler(CTCPClient sender, byte[] data);
        public delegate void OnSocketDisconnectHandler(CTCPClient sender);

        public event OnSocketConnectHandler OnSocketConnect;
        public event OnSocketDataArrivalHandler OnSocketDataArrival;
        public event OnSocketDisconnectHandler OnSocketDisconnect;

        public CTCPClient(Socket i)
        {
            this.Client = i;
        }
        public CTCPClient() : base() { }

        /// <summary>
        /// Connects asychronously to the remote host and raises the event OnSocketConnect when done
        /// </summary>
        /// <param name="hostname">the remote hostname to connect to</param>
        /// <param name="port">the port to connect in the remote host</param>
        public void ConnectAsync(string hostname, short port)
        {
            this.BeginConnect(hostname, port, new AsyncCallback(SocketConnected), null);
        }
        /// <summary>
        /// Disconnectes asynchronously from the remote host and raises the event OnSocketDisconnect when done
        /// </summary>
        public void DisconnectAsync()
        {
            this.Client.BeginDisconnect(false, new AsyncCallback(SocketDisconnected), null);
        }
        /// <summary>
        /// Writes the data to the underlying stream asynchronously and raises the event OnSocketSent when done
        /// </summary>
        /// <param name="data">The data to be sent to the remote host</param>
        public void WriteAsync(byte[] data)
        {
            this.GetStream().BeginWrite(data, 0, data.Length, new AsyncCallback(SocketWrite), null);
        }
        public void BeginRead()
        {
            byte[] buffer = new byte[1024];
            this.GetStream().BeginRead(buffer, 0, 1024, new AsyncCallback(SocketRead), buffer);
        }
        public void EndRead()
        {
            receiving = false;
        }
        private void SocketConnected(IAsyncResult ar)
        {
            this.EndConnect(ar);
            if (OnSocketConnect != null)
            {
                OnSocketConnect(this);
            }
        }
        private void SocketDisconnected(IAsyncResult ar)
        {
            this.Client.EndDisconnect(ar);
            if (OnSocketDisconnect != null)
            {
                OnSocketDisconnect(this);
            }
        }
        private void SocketRead(IAsyncResult ar)
        {

            try
            {
                if (this.Connected)
                {
                    byte[] buffer = (byte[])ar.AsyncState;
                    int read = this.GetStream().EndRead(ar);
                    if (read > 0)
                    {
                        byte[] data = new byte[read];
                        Buffer.BlockCopy(buffer, 0, data, 0, read);
                        buffer = new byte[this.Available + 16384];
                        if (receiving)
                            this.GetStream().BeginRead(buffer, 0, this.Available + 16384, new AsyncCallback(SocketRead), buffer);
                        if (OnSocketDataArrival != null)
                        {
                            OnSocketDataArrival(this, data);
                        }
                    }
                    if (read == 0)
                    {
                        // disconnected
                        DisconnectAsync();
                    }
                }
            }
            catch (SocketException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (System.IO.IOException ex)
            {
                if (OnSocketDisconnect != null)
                {
                    OnSocketDisconnect(this);
                }
            }

        }
        private void SocketWrite(IAsyncResult ar)
        {
            this.GetStream().EndWrite(ar);
        }
    }
    public class CTCPServer : TcpListener
    {
        public CTCPServer(int port) : base(IPAddress.Any, port) { }
        public delegate void OnSocketConnectionRequestHandler(Socket con);
        public event OnSocketConnectionRequestHandler OnSocketConnectionRequest;
        public void BeginAcceptingConnections()
        {
            this.BeginAcceptSocket(new AsyncCallback(Socket_Req), null);
        }
        private void Socket_Req(IAsyncResult ar)
        {
            if (this.Active)
            {
                if (OnSocketConnectionRequest != null)
                {
                    OnSocketConnectionRequest(this.EndAcceptSocket(ar));
                }

                this.BeginAcceptSocket(new AsyncCallback(Socket_Req), null);
            }
        }
    }
}
