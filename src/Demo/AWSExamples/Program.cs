using Amazon;
using Amazon.SQS;
using Amazon.SQS.Model;

RegionEndpoint ServiceRegion = RegionEndpoint.USEast1;

IAmazonSQS client = new AmazonSQSClient(ServiceRegion);
var url = "https://sqs.us-east-1.amazonaws.com/847888492411/aspnetb11";
var body = "Hello from Bangladesh.";

await SendMessage(client, url, body, null);

static async Task<SendMessageResponse> SendMessage(
            IAmazonSQS client,
            string queueUrl,
            string messageBody,
            Dictionary<string, MessageAttributeValue> messageAttributes)
{
    var sendMessageRequest = new SendMessageRequest
    {
        DelaySeconds = 10,
        MessageAttributes = messageAttributes,
        MessageBody = messageBody,
        QueueUrl = queueUrl,
    };

    var response = await client.SendMessageAsync(sendMessageRequest);
    Console.WriteLine($"Sent a message with id : {response.MessageId}");

    return response;
}