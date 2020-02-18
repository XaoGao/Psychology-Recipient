namespace Psychology_Recipient.Rabbit
{
    public static class Settings
    {
        public readonly static string Uri = $"amqp://qdgnxkqb:0RL1IJLmd6mMDf7rUvbc_l8emBSPLB3K@hawk.rmq.cloudamqp.com/qdgnxkqb";
        public readonly static string KeyRequest = "Document-Request";
        public readonly static string KeyResponse = "Document-Response";
    }
}