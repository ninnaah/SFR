using SFR;

// WillhabenClient willClient = new WillhabenClient();
// var data = await willClient.GetAdverts();

var producer = new ProducerService();
await producer.Produce();

var consumer = new ConsumerService();
consumer.Consume();