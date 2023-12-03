using Riptide;
using Riptide.Utils;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    private Server server = new();

    public static bool IsConnected;

    // Codes for initial setup and sending data are from:
    // https://riptide.tomweiland.net/manual/overview/getting-started.html

    void Start()
    {
        RiptideLogger.Initialize(Debug.Log, Debug.Log, Debug.LogWarning, Debug.LogError, false);
        server.Start(7777, 10);
    }

    private void FixedUpdate()
    {
        server.Update();
        Message message1 = Message.Create(MessageSendMode.Unreliable, 1);
        for (int i = 0; i < FakeDataSetup.reservations.Count; i++)
        {
            message1.AddInt(FakeDataSetup.reservations[i].timeScale);
            message1.AddInt(FakeDataSetup.reservations[i].courtNumber);
            message1.AddBool(FakeDataSetup.reservations[i].available);
        }
        server.SendToAll(message1);

        Message message2 = Message.Create(MessageSendMode.Unreliable, 2);
        for (int i = 0; i < FakeDataSetup.events.Count; i++)
        {
            message2.AddString(FakeDataSetup.events[i].eventName);
            message2.AddInt(FakeDataSetup.events[i].eventDate);
            message2.AddString(FakeDataSetup.events[i].eventPlace);
            message2.AddInt(FakeDataSetup.events[i].GetCapacity());
        }
    }

    private void OnDestroy()
    {
        server.Stop();
    }

    [MessageHandler(0)]
    private static void HandleMessage1FromServer(ushort i, Message message)
    {
        //FakeDataSetup.events[i].AddParticipant();
    }

    [MessageHandler(1)]
    private static void HandleMessage2FromServer(ushort i, Message message)
    {
        //FakeDataSetup.reservations[i].Reserve();
    }

}
