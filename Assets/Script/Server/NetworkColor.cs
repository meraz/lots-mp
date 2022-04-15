using Unity.Netcode;
using UnityEngine;
using UnityEngine.Assertions;

public class NetworkColor : NetworkBehaviour
{
    private readonly NetworkVariable<Color> color_ = new NetworkVariable<Color>(Color.cyan);

    SpriteRenderer spriteRenderer;

    public bool updateOnConnect = true;

    void Awake()
    {
        PrintMultiplayerDebug("NetworkColor:Awake");
        spriteRenderer = GetComponent<SpriteRenderer>();
        Assert.IsNotNull(spriteRenderer, "Sprite Renderer cannot be null.");
    }

    public override void OnNetworkSpawn()
    {
        PrintMultiplayerDebug("NetworkColor:OnNetworkSpawn");
        color_.OnValueChanged += onColorChanged;
        if (updateOnConnect && !IsOwner)
        {
            SetDirtyRequestServerRpc();
        }
    }

    public override void OnNetworkDespawn()
    {
        color_.OnValueChanged -= onColorChanged;
    }

    public void onColorChanged(Color oldColor, Color newColor)
    {
        // PrintMultiplayerDebug("NetworkColor:colorChanged");
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
