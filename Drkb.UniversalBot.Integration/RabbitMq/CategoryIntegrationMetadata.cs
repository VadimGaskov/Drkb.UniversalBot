namespace Drkb.UniversalBot.Integration.RabbitMq;

public static class CategoryIntegrationMetadata
{
    private const string ExchangeName = "category-events";
    
    public static class Created
    {
        public const string EventName = "category.created";
        public const string RoutingKey = "category.created";
        public const string Exchange = ExchangeName;
    }
    
    public static class Updated
    {
        public const string EventName = "category.updated";
        public const string RoutingKey = "category.updated";
        public const string Exchange = ExchangeName;
    }
    
    public static class Deleted
    {
        public const string EventName = "category.deleted";
        public const string RoutingKey = "category.deleted";
        public const string Exchange = ExchangeName;
    }
    
    // Для Wildcard паттернов в Topic
    public const string AllRoutingKeys = "category.*";
}

