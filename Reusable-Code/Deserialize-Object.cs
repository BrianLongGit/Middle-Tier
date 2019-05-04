//Example of grabbing JSON data into format you can populate object

JsonConvert.DeserializeObject<Orders>(client.DownloadString("http://hotel.ordering.store/api/orders"));

string url = @"C:\Users\blong\Desktop\OrdersResponseTest.txt";
string orderText = File.ReadAllText(url);
var rootObject = JsonConvert.DeserializeObject<RootObject>(orderText);
