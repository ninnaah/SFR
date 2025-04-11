namespace Shared.Kakfaconfig
{
    public static class KafkaSettings
    {
        public static string BootstrapServers =>
            Environment.GetEnvironmentVariable("KAFKA_BOOTSTRAP") ?? "localhost:29092";

        public static string SchemaRegistryUrl =>
            Environment.GetEnvironmentVariable("SCHEMA_REGISTRY_URL") ?? "http://localhost:8081";

        public const string ClothingAdAvroTopic = "clothing-ad-avro";
        public const string CategoryCountTopic = "category-counts";
        public const int DefaultMessageCount = 10;
    }
}