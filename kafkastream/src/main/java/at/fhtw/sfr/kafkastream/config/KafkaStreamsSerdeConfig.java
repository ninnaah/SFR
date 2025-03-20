package at.fhtw.sfr.kafkastream.config;

import at.fhtw.sfr.kafkastream.serdes.SerdesUtils;
import jakarta.annotation.PostConstruct;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.context.annotation.Configuration;

import java.util.Map;

@Configuration
public class KafkaStreamsSerdeConfig {

    @Value("${spring.kafka.streams.properties.schema.registry.url}")
    private String schemaRegistryUrl;

    @PostConstruct
    public void configureSerdes() {
        SerdesUtils.setSerdesConfig(Map.of("schema.registry.url", schemaRegistryUrl));
    }
}
