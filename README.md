# AWESOME SFR PROJECT
MSE-2A — Group 2

## The Domain
An application that displays various second-hand clothing adverts.

The sources are for example:
- Willhaben
- Vinted
- Sellpy

The adverts can be searched and filtered by various attributes, such as location, category, and seller.

## Technologies
- .NET 8 Backend
- EF Core
- Apache Kafka
- Angular Frontend

## Kafka Setup
Start Apache Kafka with docker-compose.yml

## DB Setup
- "dotnet tool install --global dotnet-ef" for installing dotnet-ef tools
- "dotnet ef database update" for updating the database and adding migrations
- Connection String in appsettings.json

### Cluster Overview
- Brokers:
    - 3 Brokers: broker-1, broker-2, broker-3
- Controllers:
    - 3 Controllers: controller-1, controller-2, controller-3

### Partitions
- Units of parallelism for each topic
- Default partitions per topic:
    - KAFKA_NUM_PARTITIONS: 3
    - You can create topics with more partitions as needed.

### Replication Factor
- Ensures redundancy and availability of data
- Default settings:
    - KAFKA_OFFSETS_TOPIC_REPLICATION_FACTOR = 3
    - KAFKA_TRANSACTION_STATE_LOG_REPLICATION_FACTOR = 3
    - KAFKA_TRANSACTION_STATE_LOG_MIN_ISR = 2

## Key Strategy for Kafka Producers

Each producer (Willhaben, Vinted, Sellpy) sends messages with a Key, which determines message partitioning and supports efficient filtering and aggregation.

### Current Key Design
- The Kafka message key follows this pattern:  
  Location-Category  
  Example Keys:
    - Wien-Schuhe
    - Graz-Kleider

### Why?
- Ensures messages are grouped by region and category
- Supports use cases like:
    - "Show me all shoes in Vienna"
    - "Analyze which categories are most popular in Linz"
- Enables partitioning based on key for ordered processing within groups.

### Future Extensions (Optional)
- Add SellerId to the key for seller-specific aggregations  
  Example:  
  Wien-Schuhe-Seller123

## Usage Scenarios
- Efficient filtering by location and category

// ML TODO: Entsprechend dann anpassen
- Aggregations per region, category, and optionally seller
- Parallel consumer processing based on partitioned data

## SFR Backend
Produces and consumes Kafka messages per producer:

| Producer            | Kafka Topic       | Key Example   |
|---------------------|-------------------|---------------|
| WillhabenProducer   | willhaben-items   | Wien-Schuhe   |
| VintedProducer      | vinted-items      | Wien-Schuhe   |
| SellpyProducer      | sellpy-items      | Wien-Schuhe   |

## How to Run
1. Start Kafka using docker-compose up -d
2. Run the .NET 8 backend project
3. Use the producers to publish messages into the Kafka topics
4. Verify consumption and filtering through consumers or the Angular frontend (TBD)