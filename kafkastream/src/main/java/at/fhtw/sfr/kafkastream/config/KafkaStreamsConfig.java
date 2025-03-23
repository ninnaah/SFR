package at.fhtw.sfr.kafkastream.config;

import SFR.AvroModels.V1.ClothingAdAvro;
import at.fhtw.sfr.kafkastream.topologys.KafkaStreamsTopology;
import org.apache.kafka.streams.StreamsBuilder;
import org.apache.kafka.streams.kstream.KStream;
import org.slf4j.Logger;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.kafka.annotation.EnableKafkaStreams;


@Configuration
@EnableKafkaStreams
public class KafkaStreamsConfig {

    @Bean
    public KStream<String, ClothingAdAvro> kStream(StreamsBuilder streamsBuilder) {
        return KafkaStreamsTopology.topology(streamsBuilder);
    }
}