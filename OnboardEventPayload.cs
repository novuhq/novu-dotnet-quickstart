// OnboardEventPayload.cs
using Newtonsoft.Json;

public class OnboardEventPayload
{
  [JsonProperty("username")]
  public string Username { get; set; }

  [JsonProperty("welcomeMessage")]
  public string WelcomeMessage { get; set; }
}