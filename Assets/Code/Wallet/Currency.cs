public class Currency
{
    public string Id { get; private set; }
    /* I used uint here because I suggest that 0 is the least amount of any currency 
        and there is not any real currency used in the game directly.  */
    public uint Value { get; set; }

    public Currency(string id) => Id = id;

    public Currency(string id, uint value)
    {
        Id = id;
        Value = value;
    }
}