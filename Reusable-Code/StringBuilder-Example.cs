//Loop to Create Order Sheet OHR
//Declare empty strings for each section of Order Sheet file
string OHRStringVar = string.Empty;

//Declares new stringbuilder for each section of Order Sheet file
StringBuilder OHRStringBuilder = new StringBuilder();

//Gets properties of orderHeaderRecord to loop over properties
var orderHeaderRecordProps = orderHeaderRecord.GetType().GetProperties();

//Loop for OrderHeaderRecord properties
foreach (var prop in orderHeaderRecordProps)
{
  OHRStringVar += prop.GetValue(orderHeaderRecord) + "\t";
}

//To remove tab at the end of the string?
//OHRStringVar = OHRStringVar.Substring(0, OHRStringVar.Length - 1);

//Append String Variable to StringBuilder
OHRStringBuilder.Append(OHRStringVar);

string OHRLine = OHRStringBuilder.ToString();
//writes sections to Ordersheet using StreamWriter
OrderSheet.WriteLine(OHRLine);
