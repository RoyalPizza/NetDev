using QFSW.QC;
using UnityEngine;
using Unity.Netcode;
using System;
using Unity.Netcode.Transports.UNET;

namespace BlueMountain.Diag
{
    /// <summary>
    /// 
    /// </summary>
    public static class NetworkDiag
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectAddresss"></param>
        [Command]
        public static void ConfigNet(string connectAddresss)
        {
            NetworkManager.Singleton.gameObject.GetComponent<UNetTransport>().ConnectAddress = connectAddresss;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientId"></param>
        [Command]
        public static void DisconnectClient(ulong clientId)
        {
            if (NetworkManager.Singleton.IsServer && NetworkManager.Singleton.ConnectedClients.ContainsKey(clientId))
                NetworkManager.Singleton.DisconnectClient(clientId);
            else if (!NetworkManager.Singleton.IsServer)
                Debug.LogWarning("Clients can only be force disconnected from the server.");
            else if (!NetworkManager.Singleton.ConnectedClients.ContainsKey(clientId))
                Debug.LogWarning($"Client ID {clientId} does not exist.");
        }

        /// <summary>
        /// 
        /// </summary>
        [Command]
        public static void DisplayClient()
        {
            if (NetworkManager.Singleton.IsClient)
                Debug.Log($"The local client id is {NetworkManager.Singleton.LocalClientId}");
            else
                Debug.LogWarning("The session is not a client.");
        }

        /// <summary>
        /// 
        /// </summary>
        [Command]
        public static void DisplayClients()
        {
            if (NetworkManager.Singleton.IsServer)
            {
                foreach (var client in NetworkManager.Singleton.ConnectedClientsList)
                    Debug.Log(client.ClientId);
            }
            else
                Debug.LogWarning("Only the server can display clients.");
        }

        /// <summary>
        /// 
        /// </summary>
        [Command]
        public static void DisplayConnectedHostname()
        {
            Debug.Log($"The connected hostname is {NetworkManager.Singleton.ConnectedHostname}");
        }

        /// <summary>
        /// 
        /// </summary>
        [Command]
        public static void StartClient()
        {
            if (!NetworkManager.Singleton.IsServer && !NetworkManager.Singleton.IsHost && !NetworkManager.Singleton.IsClient)
                NetworkManager.Singleton.StartClient();
            else
                Debug.LogWarning("Cannot start client. The session is already started.");
        }

        /// <summary>
        /// 
        /// </summary>
        [Command]
        public static void StartHost()
        {
            if (!NetworkManager.Singleton.IsServer && !NetworkManager.Singleton.IsHost && !NetworkManager.Singleton.IsClient)
                NetworkManager.Singleton.StartHost();
            else
                Debug.LogWarning("Cannot start client. The session is already started.");
        }

        /// <summary>
        /// 
        /// </summary>
        [Command]
        public static void StartServer()
        {
            if (!NetworkManager.Singleton.IsServer && !NetworkManager.Singleton.IsHost && !NetworkManager.Singleton.IsClient)
                NetworkManager.Singleton.StartServer();
            else
                Debug.LogWarning("Cannot start client. The session is already started.");
        }

        /// <summary>
        /// 
        /// </summary>
        [Command]
        public static void ShutdownServer()
        {
            if (NetworkManager.Singleton.IsServer)
                NetworkManager.Singleton.Shutdown();
            else
                Debug.LogWarning("Only the server can command a shutdown.");
        }
    }
}

