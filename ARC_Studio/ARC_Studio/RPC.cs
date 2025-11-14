using DiscordRPC;
using DiscordRPC.Logging;
using System;
using System.IO;
using System.Threading;
using System.Net.Http;

namespace ARC_Studio
{
    public static class RPC
    {
        private static readonly HttpClient httpClient = new HttpClient();

        private static DiscordRpcClient client;
        private static bool isInitialized = false;
        public static bool IsConnected { get; private set; } = true;

        private const string DefaultLargeIcon = "nova_icon";
        private const string DefaultLargeText = "ARC_Studio";
        private const string DefaultSmallIcon = "nova_small_icon";
        private const string DefaultSmallText = "nova";

        public static class Icons
        {
            public const string Main = "nova_icon";
            public const string LocEditor = "loc_editor_icon";
        }           

        public static void Initialize(string applicationId = null)
        {
            if (isInitialized && client != null && client.IsInitialized)
                return;

            try
            {
                string clientId = !string.IsNullOrEmpty(applicationId)
                    ? applicationId
                    : "1438894976766312458";

                client = new DiscordRpcClient(clientId)
                {
                    Logger = new ConsoleLogger { Level = LogLevel.Warning }
                };

                client.OnReady += (sender, e) =>
                    Console.WriteLine($"RPC Ready for user {e.User.Username}");

                client.OnError += (sender, e) =>
                    Console.WriteLine($"RPC Error: {e.Message}");

                client.Initialize();
                isInitialized = true;
                IsConnected = true;

                Console.WriteLine("Discord RPC initialized successfully");
                RPC.Initialize();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"RPC Initialization Error: {ex.Message}");
                ShutDown();
                IsConnected = false;
                isInitialized = false;
            }
        }

        public static void UpdatePresence(
            string details,
            string state,
            string largeImageKey = null,
            string largeImageText = null,
            string smallImageKey = null,
            string smallImageText = null)
        {
            if (client == null || !client.IsInitialized)
            {
                Initialize();
                Thread.Sleep(100); // give it a moment
                IsConnected = client?.IsInitialized ?? false;
            }

            try
            {
                client?.SetPresence(new RichPresence
                {
                    Details = details,
                    State = state,
                    Timestamps = Timestamps.Now,
                    Assets = new Assets
                    {
                        LargeImageKey = largeImageKey ?? DefaultLargeIcon,
                        LargeImageText = largeImageText ?? DefaultLargeText,
                        SmallImageKey = smallImageKey ?? DefaultSmallIcon,
                        SmallImageText = smallImageText ?? DefaultSmallText
                    }
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to update presence: {ex.Message}");
            }
        }

        public static void SetLoadingFormPresence(string fileName = null)
        {
            string state = string.IsNullOrEmpty(fileName)
                ? "Opening..."
                : $"File {Path.GetFileName(fileName)}";

            UpdatePresence("", state, Icons.Main, "Loading Form");
        }

        public static void SetArcEditorPresence(string fileName = null)
        {
            string state = string.IsNullOrEmpty(fileName)
                ? "Made by DescZ"
                : $"File {Path.GetFileName(fileName)}";

            UpdatePresence("", state, Icons.Main, ".ARC Editor");
        }

        public static void SetLocEditorPresence(string fileName = null)
        {
            string state = string.IsNullOrEmpty(fileName)
                ? "Made by DescZ"
                : $"File {Path.GetFileName(fileName)}";

            UpdatePresence("", state, Icons.LocEditor, ".LOC Editor");
        }

        public static void ShutDown()
        {
            try
            {
                client?.Dispose();
                client = null;
                isInitialized = false;
                Console.WriteLine("Discord RPC shut down");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error shutting down RPC: {ex.Message}");
            }
        }

        public static void ClearPresence()
        {
            if (client?.IsInitialized == true)
                client.ClearPresence();
        }

        public static void Disconnect()
        {
            IsConnected = false;
        }
    }
}
