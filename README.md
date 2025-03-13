# AWESOME SFR PROJECT
MSE-2A — Group 2

## The Domain
An application that displays various second-hand clothing adverts.

The sources are for example
- Willhaben
- Vinted
- Sellpy

The adverts can be searched and filtered.

## Technologies
- .NET 8 Backend
- Apache Kafka
- Angular Frontend

## Usage
### Kafka
start Apache Kafka with **docker-compose.yml**

**Broker**
- Number of kafka servers
- 3 Brokers: broker-1, broker-2, broker-3

**Partitions**
- Units of parallelism for each topic
- KAFKA_NUM_PARTITIONS: 3 (sets the default number of partitions for newly created topics)

**Replication Factor**
- Number of "copies" for each partition (across brokers)
- default KAFKA_OFFSETS_TOPIC_REPLICATION_FACTOR = 3 (needs at least 3 brokers)
- default KAFKA_TRANSACTION_STATE_LOG_REPLICATION_FACTOR = 3 (number of replicas for the transaction state log topic)
- default KAFKA_TRANSACTION_STATE_LOG_MIN_ISR = 2 (minimum number of in-sync replicas required for the transaction state log topic to accept writes)

### SFR
Produces and consumes 10 dummy kafka messages in topic "clothing-ad"