using System;
using System.Collections;
using Infrastructure.GameMachine;
using Infrastructure.GameMachine.States;
using Infrastructure.SceneLoad;
using Infrastructure.Services.WindowProvider;
using UnityEngine;
using UnityEngine.Networking;

namespace Infrastructure.Services.ConnectionCheck
{
    public class ConnectionChecker : IConnectionChecker
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IWindowsProvider _windowsProvider;
        private readonly ICoroutineRunner _coroutineRunner;

        private const string EchoSite = "https://dns.google/";
        
        private const float ConnectionAttemptDelay = 2.5f;

        private ConnectionStatus _connectionStatus = ConnectionStatus.Connected;
        
        public event Action ConnectionLost, ConnectionRenewed;
        
        public ConnectionChecker(GameStateMachine gameStateMachine, IWindowsProvider windowsProvider,ICoroutineRunner coroutineRunner)
        {
            _gameStateMachine = gameStateMachine;
            _windowsProvider = windowsProvider;
            
            coroutineRunner.StartCoroutine(CheckConnectionPassively());
        }
        private IEnumerator CheckConnectionPassively()
        {
            while (true)
            {
                yield return new WaitForSeconds(ConnectionAttemptDelay);
                UnityWebRequest request = new UnityWebRequest(EchoSite);
                UnityWebRequestAsyncOperation result = request.SendWebRequest();

                while (!result.isDone)
                    yield return null;

                switch (request.result)
                {
                    case UnityWebRequest.Result.Success:
                        OnConnected();
                        break;
                    case UnityWebRequest.Result.ConnectionError:
                        OnConnectionLost();
                        break;
                }
            }
        }

        private void OnConnected()
        {
            if (_connectionStatus != ConnectionStatus.Connected)
            {
                _gameStateMachine.Enter<LoadGameState>();
                _windowsProvider.CloseWindow(WindowType.ConnectionLostWindow);
                ConnectionRenewed?.Invoke();
            }
            
            _connectionStatus = ConnectionStatus.Connected;
        }

        private void OnConnectionLost()
        {
            if (_connectionStatus == ConnectionStatus.Connected)
            {
                _gameStateMachine.Enter<GameStoppedState>();
                _windowsProvider.OpenWindow(WindowType.ConnectionLostWindow);
                ConnectionLost?.Invoke();
            }
            
            _connectionStatus = ConnectionStatus.Disconnected;
        }

        private ConnectionStatus CheckNetworkConnection()
        {
            ConnectionStatus toReturn = ConnectionStatus.Disconnected;
            
            UnityWebRequest request = new UnityWebRequest(EchoSite);
            UnityWebRequestAsyncOperation result = request.SendWebRequest();

            while (!result.isDone)
            {
            }

            switch (request.result)
            {
                case UnityWebRequest.Result.Success:
                    toReturn = ConnectionStatus.Connected;
                    break;
                case UnityWebRequest.Result.ConnectionError:
                    toReturn = ConnectionStatus.Disconnected;
                    break;
            }

            return toReturn;
        }

        public bool IsNetworkConnected => 
            CheckNetworkConnection() == ConnectionStatus.Connected;
        
        public ConnectionStatus NetworkStatus => CheckNetworkConnection();
    }
}