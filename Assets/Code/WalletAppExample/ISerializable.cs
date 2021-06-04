
interface ISerializable
{
    public string SerializeToJson();

    public void Deserialize(string json);

    public byte[] SerializeToBinary();

    public void Deserialize(byte[] binary);
}

