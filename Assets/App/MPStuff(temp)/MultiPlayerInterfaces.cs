public interface MPRoomListener
{
    void SetRoomStatusMessage(string message);
    void HideRoom();
}

public interface MPUpdateListener
{
    void UpdateReceived(string participantId, float posX, float posY, float velX, float velY);
}