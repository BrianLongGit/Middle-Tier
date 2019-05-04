//Useful chain of methods to loop over properties when you want to manipulate values of props

//Gets properties of orderHeaderRecord to loop over properties
var orderHeaderRecordProps = orderHeaderRecord.GetType().GetProperties();

//Loop for OrderHeaderRecord properties
foreach (var prop in orderHeaderRecordProps)
{
    OHRStringVar += prop.GetValue(orderHeaderRecord) + "\t";
}
