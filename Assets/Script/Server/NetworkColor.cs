using Unity.Netcode;
using UnityEngine;

public class NetworkColor : NetworkBehaviour
{
    private readonly NetworkVariable<Color> color_ = new NetworkVariable<Color>(Color.cyan);

    SpriteRenderer spriteRenderer;

    void Awake()
    {
       // PrintMultiplayerDebug("NetworkColor:Awake");
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public override void OnNetworkSpawn()
    {
        //PrintMultiplayerDebug("NetworkColor:OnNetworkSpawn");
        color_.OnValueChanged += colorChanged;
    }

    public override void OnNetworkDespawn()
    {
        color_.OnValueChanged -= colorChanged;
    }

    public void colorChanged(Color oldColor, Color newColor)
    {
     //   PrintMultiplayerDebug("NetworkColor:colorChanged");
        spriteRenderer.color = newColor;
    }

    [ServerRpc]
    public void SetColorRequestServerRpc(Color color)
    {
       // PrintMultiplayerDebug("NetworkColor:SetColorRequestServerRpc");
        color_.Value = color;
    }

    [ServerRpc(RequireOwnership = false)]
    public void SetDirtyRequestServerRpc()
    {
       // PrintMultiplayerDebug("NetworkColor:RequestDirtyServerRpc");
        color_.SetDirty(true);
    }

    private void PrintMultiplayerDebug(string message)
    {
        Debug.Log($"IsOwner={IsOwner}, IsServer={IsServer}, {message}"); // TODO meraz make a bit more generic
    }
}
