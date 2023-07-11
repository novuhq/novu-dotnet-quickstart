/*  ‼️ Please note that there may be multiple instances 
of duplicated errors in the example code provided. 
This code is intended to serve as a reference for 
you to copy and paste individual functions into 
your own code. By doing so, you can adapt and utilize 
the functions as needed within your project. */

// <-----------------Importing and setting Up Client ----------------->

using Novu.DTO;
using Novu.Models;
using Novu;
using Novu.DTO.Topics;

 var novuConfiguration = new NovuClientConfiguration
{
    ApiKey = "96d89daa32ba2e14211a5832c438eb32",
};

var novu = new NovuClient(novuConfiguration);


// <-----------------Create a subscriber----------------->
var newSubscriberDto = new CreateSubscriberDto
{
  SubscriberId = "77809", //replace with system_internal_user_id
  FirstName = "John",
  LastName = "Doe",
  Email = "benlin1994@gmail.com",
  Data = new List<AdditionalDataDto>{
    new AdditionalDataDto
    {
        Key = "External ID",
        Value = "1122334455"
    },
    new AdditionalDataDto
    {
        Key = "Job Title",
        Value = "Software Engineer"
    }
  }
};



var subscriber = await novu.Subscriber.CreateSubscriber(newSubscriberDto);

// <-----------------Updating a subscriber----------------->
var subscriber7789 = await novu.Subscriber.GetSubscriber("7789");

subscriber7789.Email = "validemail@gmail.com"; // replace with valid email
subscriber7789.FirstName = "<insert-fir2stname>"; // optional
subscriber7789.LastName = "<insert-lastname>"; // optional

var updatedSubscriber = await novu.Subscriber.UpdateSubscriber("7789",subscriber7789);

// <-----------------Trigger a Notification----------------->


var onboardingMessage = new OnboardEventPayload
{
  Username = "jdoe",
  WelcomeMessage = "Welcome to novu-dotnet"
};

var payload = new EventTriggerDataDto()
{
  EventName = "onboarding",
  To = { SubscriberId = "7789" },
  Payload = onboardingMessage
};

var trigger = await novu.Event.Trigger(payload);

if (trigger.TriggerResponsePayloadDto.Acknowledged)
{
  Console.WriteLine("Trigger has been created.");
}


// <-----------------Creating a Topic----------------->

var topicRequest = new TopicCreateDto
{
    Key = "frontend-users",
    Name = "All frontende users",
    
};

var topic = await novu.Topic.CreateTopicAsync(topicRequest);


// <-----------------Adding Subscribers to a Topic----------------->


var topicKey = "frontender-user";
var subscriberList = new TopicSubscriberUpdateDto(
    new List<string>
    {
        "7789",
        "7790",
    }
);
    
var result = await novu.Topic.AddSubscriberAsync(topicKey, subscriberList);

// <-----------------Triggering Notitication to a Topic----------------->


var payloadTopic = new EventTopicTriggerDto
{
  EventName = "onboarding",
  Payload = onboardingMessage,
  Topic = { Type = "7789", TopicKey = "frontend-users" },

};

var triggerTopic = await novu.Event.TriggerTopicAsync(payloadTopic);

if (triggerTopic.TriggerResponsePayloadDto.Acknowledged);
{
  Console.WriteLine("Trigger has been created.");
}



